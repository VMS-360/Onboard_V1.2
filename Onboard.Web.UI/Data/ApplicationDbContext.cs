using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Onboard.Web.UI.Models;

namespace Onboard.Web.UI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "Onboard_User");
                entity.Property(e => e.Id).HasColumnName("UserId");
            });

            builder.Entity<ApplicationRole>(entity =>
            {
                entity.ToTable(name: "Onboard_Role");
                entity.Property(e => e.Id).HasColumnName("RoleId");
            });

            builder.Entity<IdentityUserClaim<int>>(entity =>
            {
                entity.ToTable("Onboard_User_Claim");
                entity.Property(e => e.UserId).HasColumnName("UserId");
                entity.Property(e => e.Id).HasColumnName("UserClaimId");
            });

            builder.Entity<IdentityUserLogin<int>>(entity =>
            {
                entity.ToTable("Onboard_User_Login");
                entity.Property(e => e.UserId).HasColumnName("UserId");
            });

            builder.Entity<IdentityRoleClaim<int>>(entity =>
            {
                entity.ToTable("Onboard_Role_Claim");
                entity.Property(e => e.Id).HasColumnName("RoleClaimId");
                entity.Property(e => e.RoleId).HasColumnName("RoleId");
            });

            builder.Entity<IdentityUserRole<int>>(entity =>
            {
                entity.ToTable("Onboard_User_Role");
                entity.Property(e => e.UserId).HasColumnName("UserId");
                entity.Property(e => e.RoleId).HasColumnName("RoleId");
            });

            builder.Entity<IdentityUserToken<int>>(entity =>
            {
                entity.ToTable("Onboard_User_Token");
                entity.Property(e => e.UserId).HasColumnName("UserId");
            });
        }
    }
}
