using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Onboard.Web.UI.Data;
using Onboard.Web.UI.Models;
using Onboard.Web.UI.Services;
using Microsoft.AspNetCore.Identity;
using Onboard.Web.UI.DataContexts;
using Newtonsoft.Json.Serialization;

namespace Onboard.Web.UI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<OnboardDb>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext, int>()
                .AddDefaultTokenProviders();

            // Maintain property names during serialization. See:
            // https://github.com/aspnet/Announcements/issues/194
            services.AddMvc()
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            // Add Kendo UI services to the services container
            services.AddKendo();

            // Add application services.
            services.AddTransient<IDatabaseService, DatabaseService>();
            services.AddTransient<IChecklistService, ChecklistService>();
            services.AddTransient<ICandidateService, CandidateService>();
            services.AddTransient<IUserLookupService, UserLookupService>();
            services.AddTransient<ILookupService, LookupService>();
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddTransient<IEnrollmentService, EnrollmentService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            // Configure Kendo UI
            app.UseKendo(env);

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            // Ensure roles are in DB - OK not to await this for now
            EnsureRoles(app, loggerFactory);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

            // Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            OnboardData.InitializeDatabaseAsync(app.ApplicationServices).Wait();
        }

        private async Task EnsureRoles(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            ILogger logger = loggerFactory.CreateLogger<Startup>();
            RoleManager<ApplicationRole> roleManager = app.ApplicationServices.GetService<RoleManager<ApplicationRole>>();

            string[] roleNames = { "Recruiters", "Account Manager", "Clients Manager", "Portfolio", "Accounting", "HR", "Executive", "Admin" };

            foreach (string roleName in roleNames)
            {
                bool roleExists = await roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    logger.LogInformation(String.Format("!roleExists for roleName {0}", roleName));
                    IdentityRole identityRole = new IdentityRole(roleName);
                    IdentityResult identityResult = await roleManager.CreateAsync(new ApplicationRole { Name = roleName, IsActive = "Y" });
                    if (!identityResult.Succeeded)
                    {
                        logger.LogCritical(
                            String.Format("!identityResult.Succeeded after  roleManager.CreateAsync(identityRole) for identityRole with roleName { 0} ",
                                roleName));
                        foreach (var error in identityResult.Errors)
                        {
                            logger.LogCritical(
                                String.Format(
                                    "identityResult.Error.Description: {0}",
                                    error.Description));
                            logger.LogCritical(
                                String.Format(
                                    "identityResult.Error.Code: {0}",
                                 error.Code));
                        }
                    }
                }
            }

            List<ApplicationUser> users = new List<ApplicationUser> {
                new ApplicationUser { UserName = "istcrrm", Email = "istcrrm@themesoft.com", FirstName = "Ravi", LastName = "Maddela", IsInternal ="N" },
                new ApplicationUser {  UserName = "thyagu", Email = "thyagu@themesoft.com", FirstName = "Thyagu", LastName = "Manickam", IsInternal = "Y" },
                new ApplicationUser {  UserName = "jpowell", Email = "jeffrey@themesoft.com", FirstName = "Jeffrey", LastName = "Powell", IsInternal = "Y" },
                new ApplicationUser {   UserName = "balaji", Email = "balaji.nagubadi@themesoft.com", FirstName = "Balaji", LastName = "Nagubadi", IsInternal = "N" },
                new ApplicationUser { UserName = "AConda", Email = "AConda@themesoft.com", FirstName = "Anna", LastName = "Conda", IsInternal ="N" },
new ApplicationUser { UserName = "Araham", Email = "Araham@themesoft.com", FirstName = "Anna", LastName = "Graham", IsInternal ="N" },
new ApplicationUser { UserName = "Aasin", Email = "Aasin@themesoft.com", FirstName = "Anna", LastName = "Sasin", IsInternal ="N" },
new ApplicationUser { UserName = "Aeak", Email = "Aeak@themesoft.com", FirstName = "Anne", LastName = "Teak", IsInternal ="N" },
new ApplicationUser { UserName = "ACurtain", Email = "ACurtain@themesoft.com", FirstName = "Annette", LastName = "Curtain", IsInternal ="N" },
new ApplicationUser { UserName = "AHolly", Email = "AHolly@themesoft.com", FirstName = "Aretha", LastName = "Holly", IsInternal ="N" },
new ApplicationUser { UserName = "AHammer", Email = "AHammer@themesoft.com", FirstName = "Armand", LastName = "Hammer", IsInternal ="N" },
new ApplicationUser { UserName = "Bwyer", Email = "Bwyer@themesoft.com", FirstName = "Barb", LastName = "Dwyer", IsInternal ="N" },
new ApplicationUser { UserName = "BSeville", Email = "BSeville@themesoft.com", FirstName = "Barbara", LastName = "Seville", IsInternal ="N" },
new ApplicationUser { UserName = "BCade", Email = "BCade@themesoft.com", FirstName = "Barry", LastName = "Cade", IsInternal ="N" },
new ApplicationUser { UserName = "Bellington", Email = "Bellington@themesoft.com", FirstName = "Biff", LastName = "Wellington", IsInternal ="N" },
new ApplicationUser { UserName = "Board", Email = "Board@themesoft.com", FirstName = "Bill", LastName = "Board", IsInternal ="N" },
new ApplicationUser { UserName = "Bing", Email = "Bing@themesoft.com", FirstName = "Bill", LastName = "Ding", IsInternal ="N" },
new ApplicationUser { UserName = "Boldes", Email = "Boldes@themesoft.com", FirstName = "Bill", LastName = "Foldes", IsInternal ="N" },
new ApplicationUser { UserName = "Boney", Email = "Boney@themesoft.com", FirstName = "Bill", LastName = "Loney", IsInternal ="N" },
new ApplicationUser { UserName = "BRubin", Email = "BRubin@themesoft.com", FirstName = "Billy", LastName = "Rubin", IsInternal ="N" },
new ApplicationUser { UserName = "pple", Email = "pple@themesoft.com", FirstName = "Bob", LastName = "Apple", IsInternal ="N" },
new ApplicationUser { UserName = "BEarly", Email = "BEarly@themesoft.com", FirstName = "Brighton", LastName = "Early", IsInternal ="N" },
new ApplicationUser { UserName = "BLee", Email = "BLee@themesoft.com", FirstName = "Brock", LastName = "Lee", IsInternal ="N" },
new ApplicationUser { UserName = "BTrout", Email = "BTrout@themesoft.com", FirstName = "Brooke", LastName = "Trout", IsInternal ="N" },
new ApplicationUser { UserName = "ight", Email = "ight@themesoft.com", FirstName = "Bud", LastName = "Light", IsInternal ="N" },
new ApplicationUser { UserName = "COakey", Email = "COakey@themesoft.com", FirstName = "Carrie", LastName = "Oakey", IsInternal ="N" },
new ApplicationUser { UserName = "CMacy", Email = "CMacy@themesoft.com", FirstName = "Casey", LastName = "Macy", IsInternal ="N" },
new ApplicationUser { UserName = "CCase", Email = "CCase@themesoft.com", FirstName = "Charity", LastName = "Case", IsInternal ="N" },
new ApplicationUser { UserName = "Cunk", Email = "Cunk@themesoft.com", FirstName = "Chip", LastName = "Munk", IsInternal ="N" },
new ApplicationUser { UserName = "CCoe", Email = "CCoe@themesoft.com", FirstName = "Chris", LastName = "Coe", IsInternal ="N" },
new ApplicationUser { UserName = "CCross", Email = "CCross@themesoft.com", FirstName = "Chris", LastName = "Cross", IsInternal ="N" },
new ApplicationUser { UserName = "CWaggon", Email = "CWaggon@themesoft.com", FirstName = "Chuck", LastName = "Waggon", IsInternal ="N" },
new ApplicationUser { UserName = "CNoring", Email = "CNoring@themesoft.com", FirstName = "Constance", LastName = "Noring", IsInternal ="N" },
new ApplicationUser { UserName = "CAnder", Email = "CAnder@themesoft.com", FirstName = "Corey", LastName = "Ander", IsInternal ="N" },
new ApplicationUser { UserName = "CMoorehead", Email = "CMoorehead@themesoft.com", FirstName = "Craven", LastName = "Moorehead", IsInternal ="N" },
new ApplicationUser { UserName = "ruff", Email = "ruff@themesoft.com", FirstName = "Dan", LastName = "Druff", IsInternal ="N" },
new ApplicationUser { UserName = "DDeeds", Email = "DDeeds@themesoft.com", FirstName = "Darren", LastName = "Deeds", IsInternal ="N" },
new ApplicationUser { UserName = "DRhea", Email = "DRhea@themesoft.com", FirstName = "Daryl", LastName = "Rhea", IsInternal ="N" },
new ApplicationUser { UserName = "Durns", Email = "Durns@themesoft.com", FirstName = "Dick", LastName = "Burns", IsInternal ="N" },
new ApplicationUser { UserName = "Dace", Email = "Dace@themesoft.com", FirstName = "Dick", LastName = "Face", IsInternal ="N" },
new ApplicationUser { UserName = "Dong", Email = "Dong@themesoft.com", FirstName = "Dick", LastName = "Long", IsInternal ="N" },
new ApplicationUser { UserName = "Dussell", Email = "Dussell@themesoft.com", FirstName = "Dick", LastName = "Mussell", IsInternal ="N" },
new ApplicationUser { UserName = "Dound", Email = "Dound@themesoft.com", FirstName = "Dick", LastName = "Pound", IsInternal ="N" },
new ApplicationUser { UserName = "Dwett", Email = "Dwett@themesoft.com", FirstName = "Dick", LastName = "Swett", IsInternal ="N" },
new ApplicationUser { UserName = "Dator", Email = "Dator@themesoft.com", FirstName = "Dick", LastName = "Tator", IsInternal ="N" },
new ApplicationUser { UserName = "DYamada", Email = "DYamada@themesoft.com", FirstName = "Dickson", LastName = "Yamada", IsInternal ="N" },
new ApplicationUser { UserName = "DPickles", Email = "DPickles@themesoft.com", FirstName = "Dilbert", LastName = "Pickles", IsInternal ="N" },
new ApplicationUser { UserName = "DSoares", Email = "DSoares@themesoft.com", FirstName = "Dinah", LastName = "Soares", IsInternal ="N" },
new ApplicationUser { UserName = "ey", Email = "ey@themesoft.com", FirstName = "Don", LastName = "Key", IsInternal ="N" },
new ApplicationUser { UserName = "DDuck", Email = "DDuck@themesoft.com", FirstName = "Donald", LastName = "Duck", IsInternal ="N" },
new ApplicationUser { UserName = "DBrook", Email = "DBrook@themesoft.com", FirstName = "Donny", LastName = "Brook", IsInternal ="N" },
new ApplicationUser { UserName = "Draves", Email = "Draves@themesoft.com", FirstName = "Doug", LastName = "Graves", IsInternal ="N" },
new ApplicationUser { UserName = "Dole", Email = "Dole@themesoft.com", FirstName = "Doug", LastName = "Hole", IsInternal ="N" },
new ApplicationUser { UserName = "Ditherspoon", Email = "Ditherspoon@themesoft.com", FirstName = "Doug", LastName = "Witherspoon", IsInternal ="N" },
new ApplicationUser { UserName = "DFurr", Email = "DFurr@themesoft.com", FirstName = "Douglas", LastName = "Furr", IsInternal ="N" },
new ApplicationUser { UserName = "Deacock", Email = "Deacock@themesoft.com", FirstName = "Drew", LastName = "Peacock", IsInternal ="N" },
new ApplicationUser { UserName = "DPipe", Email = "DPipe@themesoft.com", FirstName = "Duane", LastName = "Pipe", IsInternal ="N" },
new ApplicationUser { UserName = "Eader", Email = "Eader@themesoft.com", FirstName = "Ella", LastName = "Vader", IsInternal ="N" },
new ApplicationUser { UserName = "Eoyds", Email = "Eoyds@themesoft.com", FirstName = "Emma", LastName = "Royds", IsInternal ="N" },
new ApplicationUser { UserName = "Ehinn", Email = "Ehinn@themesoft.com", FirstName = "Eric", LastName = "Shinn", IsInternal ="N" },
new ApplicationUser { UserName = "Eeel", Email = "Eeel@themesoft.com", FirstName = "Evan", LastName = "Keel", IsInternal ="N" },
new ApplicationUser { UserName = "FChristian", Email = "FChristian@themesoft.com", FirstName = "Faith", LastName = "Christian", IsInternal ="N" },
new ApplicationUser { UserName = "FWheeler", Email = "FWheeler@themesoft.com", FirstName = "Ferris", LastName = "Wheeler", IsInternal ="N" },
new ApplicationUser { UserName = "FSparks", Email = "FSparks@themesoft.com", FirstName = "Flint", LastName = "Sparks", IsInternal ="N" },
new ApplicationUser { UserName = "Farker", Email = "Farker@themesoft.com", FirstName = "Ford", LastName = "Parker", IsInternal ="N" },
new ApplicationUser { UserName = "FGreen", Email = "FGreen@themesoft.com", FirstName = "Forrest", LastName = "Green", IsInternal ="N" },
new ApplicationUser { UserName = "FChild", Email = "FChild@themesoft.com", FirstName = "Foster", LastName = "Child", IsInternal ="N" },
new ApplicationUser { UserName = "FEnstein", Email = "FEnstein@themesoft.com", FirstName = "Frank", LastName = "Enstein", IsInternal ="N" },
new ApplicationUser { UserName = "Garr", Email = "Garr@themesoft.com", FirstName = "Gaye", LastName = "Barr", IsInternal ="N" },
new ApplicationUser { UserName = "Gorce", Email = "Gorce@themesoft.com", FirstName = "Gail", LastName = "Force", IsInternal ="N" },
new ApplicationUser { UserName = "Goole", Email = "Goole@themesoft.com", FirstName = "Gene", LastName = "Poole", IsInternal ="N" },
new ApplicationUser { UserName = "ish", Email = "ish@themesoft.com", FirstName = "Gil", LastName = "Fish", IsInternal ="N" },
new ApplicationUser { UserName = "HThicke", Email = "HThicke@themesoft.com", FirstName = "Harden", LastName = "Thicke", IsInternal ="N" },
new ApplicationUser { UserName = "HNutt", Email = "HNutt@themesoft.com", FirstName = "Hazle", LastName = "Nutt", IsInternal ="N" },
new ApplicationUser { UserName = "HClare", Email = "HClare@themesoft.com", FirstName = "Heidi", LastName = "Clare", IsInternal ="N" },
new ApplicationUser { UserName = "HBack", Email = "HBack@themesoft.com", FirstName = "Helen", LastName = "Back", IsInternal ="N" },
new ApplicationUser { UserName = "HMcRell", Email = "HMcRell@themesoft.com", FirstName = "Holly", LastName = "McRell", IsInternal ="N" },
new ApplicationUser { UserName = "HBee", Email = "HBee@themesoft.com", FirstName = "Honey", LastName = "Bee", IsInternal ="N" },
new ApplicationUser { UserName = "HDoohan", Email = "HDoohan@themesoft.com", FirstName = "Howie", LastName = "Doohan", IsInternal ="N" },
new ApplicationUser { UserName = "Hass", Email = "Hass@themesoft.com", FirstName = "Hugh", LastName = "Jass", IsInternal ="N" },
new ApplicationUser { UserName = "Horgan", Email = "Horgan@themesoft.com", FirstName = "Hugh", LastName = "Jorgan", IsInternal ="N" },
new ApplicationUser { UserName = "eage", Email = "eage@themesoft.com", FirstName = "Ivy", LastName = "Leage", IsInternal ="N" },
new ApplicationUser { UserName = "Joff", Email = "Joff@themesoft.com", FirstName = "Jack", LastName = "Hoff", IsInternal ="N" },
new ApplicationUser { UserName = "Jaas", Email = "Jaas@themesoft.com", FirstName = "Jack", LastName = "Haas", IsInternal ="N" },
new ApplicationUser { UserName = "Jammer", Email = "Jammer@themesoft.com", FirstName = "Jack", LastName = "Hammer", IsInternal ="N" },
new ApplicationUser { UserName = "Jnoff", Email = "Jnoff@themesoft.com", FirstName = "Jack", LastName = "Knoff", IsInternal ="N" },
new ApplicationUser { UserName = "Jott", Email = "Jott@themesoft.com", FirstName = "Jack", LastName = "Pott", IsInternal ="N" },
new ApplicationUser { UserName = "JHyde", Email = "JHyde@themesoft.com", FirstName = "Jacklyn", LastName = "Hyde", IsInternal ="N" },
new ApplicationUser { UserName = "alker", Email = "alker@themesoft.com", FirstName = "Jay", LastName = "Walker", IsInternal ="N" },
new ApplicationUser { UserName = "Joole", Email = "Joole@themesoft.com", FirstName = "Jean", LastName = "Poole", IsInternal ="N" },
new ApplicationUser { UserName = "JTull", Email = "JTull@themesoft.com", FirstName = "Jenny", LastName = "Tull", IsInternal ="N" },
new ApplicationUser { UserName = "meena", Email = "meena@themesoft.com", FirstName = "Meena", LastName = "pv", IsInternal ="Y" },
new ApplicationUser { UserName = "valli", Email = "valli@themesoft.com", FirstName = "valli", LastName = "pv", IsInternal ="Y" },
new ApplicationUser { UserName = "JAtrick", Email = "JAtrick@themesoft.com", FirstName = "Jerry", LastName = "Atrick", IsInternal ="Y" }
            };

            UserManager<ApplicationUser> userManager = app.ApplicationServices.GetService<UserManager<ApplicationUser>>();
            foreach (ApplicationUser user in users)
            {
                if (userManager.Users.Where(r => r.UserName == user.UserName).FirstOrDefault() == null)
                {
                    var newUser = await userManager.CreateAsync(user, "theme");
                    if (newUser.Succeeded)
                    {
                        var dbUser = userManager.Users.Where(r => r.UserName == user.UserName).First();
                        await userManager.AddToRoleAsync(dbUser, roleNames[new Random().Next(roleNames.Count())]);
                    }
                }
            }
        }
    }
}
