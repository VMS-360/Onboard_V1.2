using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Onboard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onboard.Web.UI.DataContexts
{
    public static class OnboardData
    {
        public static async Task InitializeDatabaseAsync(IServiceProvider serviceProvider, bool createUsers = true)
        {
            using (var serviceScope = serviceProvider.CreateScope())
            {
                var scopeServiceProvider = serviceScope.ServiceProvider;
                var db = scopeServiceProvider.GetService<OnboardDb>();

                await InsertData(scopeServiceProvider);

                if (await db.Database.EnsureCreatedAsync())
                {
                    if (createUsers)
                    {
                        //await CreateAdminUser(scopeServiceProvider);
                    }
                }
            }
        }

        private static async Task InsertData(IServiceProvider serviceProvider)
        {
            //var owners = new ProductOwner[]
            //{
            //    new ProductOwner { Name = "Themesoft Inc", CreatedUser = "istcrrm", CreatedDate = DateTime.UtcNow },
            //    new ProductOwner { Name = "Amor Sai Inc", CreatedUser = "istcrrm", CreatedDate = DateTime.UtcNow },
            //    new ProductOwner { Name = "My Productowner Inc", CreatedUser = "istcrrm", CreatedDate = DateTime.UtcNow },
            //};

            //await AddOrUpdateAsync(serviceProvider, a => a.ProductOwnerId, owners);

            var taxStatuses = new TaxStatus[]
            {
                new TaxStatus {TaxStatusCode ="1099", Description="1099", CreatedUser = "istcrrm", CreatedDate = DateTime.UtcNow  },
                new TaxStatus {TaxStatusCode ="W2", Description="W2", CreatedUser = "istcrrm", CreatedDate = DateTime.UtcNow  },
                new TaxStatus {TaxStatusCode ="C2C", Description="C2C", CreatedUser = "istcrrm", CreatedDate = DateTime.UtcNow  }
            };

            await AddOrUpdateAsync(serviceProvider, a => a.TaxStatusCode, taxStatuses);

            var employmentTypes = new EmploymentType[]
            {
                new EmploymentType {EmploymentTypeCode ="1", Description="Placement", CreatedUser = "istcrrm", CreatedDate = DateTime.UtcNow  },
                new EmploymentType {EmploymentTypeCode ="2", Description="Passthrough", CreatedUser = "istcrrm", CreatedDate = DateTime.UtcNow  }
            };

            await AddOrUpdateAsync(serviceProvider, a => a.EmploymentTypeCode, employmentTypes);

            var VisaStatuses = new VisaType[]
            {
                new VisaType {VisaTypeCode ="1", Description="US Citizen", CreatedUser = "istcrrm", CreatedDate = DateTime.UtcNow  },
                new VisaType {VisaTypeCode ="2", Description="H1-B", CreatedUser = "istcrrm", CreatedDate = DateTime.UtcNow  },
                new VisaType {VisaTypeCode ="3", Description="GC", CreatedUser = "istcrrm", CreatedDate = DateTime.UtcNow  },
                new VisaType {VisaTypeCode ="4", Description="OPT EAD", CreatedUser = "istcrrm", CreatedDate = DateTime.UtcNow  },
                new VisaType {VisaTypeCode ="5", Description="H4 EAD", CreatedUser = "istcrrm", CreatedDate = DateTime.UtcNow  },
                new VisaType {VisaTypeCode ="6", Description="GC EAD", CreatedUser = "istcrrm", CreatedDate = DateTime.UtcNow  },
                new VisaType {VisaTypeCode ="7", Description="TN", CreatedUser = "istcrrm", CreatedDate = DateTime.UtcNow  },
            };

            await AddOrUpdateAsync(serviceProvider, a => a.VisaTypeCode, VisaStatuses);

            //var endClient = new EndClient[]
            //{
            //    new EndClient { CompanyName="Walmart", TaxId="17-2466710", AddressLine1 ="Walmart Line 1", AddressLine2="Walmart Line 2", City= "Wal City", State ="NY", Zip ="12310", ProductOwnerId=4,  CreatedUser = "istcrrm", CreatedDate = DateTime.UtcNow  },
            //    new EndClient { CompanyName="Berkshire Hathaway", TaxId="17-2466711", AddressLine1 ="Berkshire Hathaway Line 1", AddressLine2="Berkshire Hathaway Line 2", City= "Berk City", State ="NY", Zip ="12315", ProductOwnerId=4,  CreatedUser = "istcrrm", CreatedDate = DateTime.UtcNow  },
            //    new EndClient { CompanyName="Apple", TaxId="17-2466712", AddressLine1 ="Apple Line 1", AddressLine2="Apple Line 2", City= "Wal City", State ="NY", Zip ="12316", ProductOwnerId=4,  CreatedUser = "istcrrm", CreatedDate = DateTime.UtcNow  },
            //};

            //await AddOrUpdateAsync(serviceProvider, a => a.EndClientId, endClient);

            //var client = new Client[]
            //{
            //    new Client { CompanyName="Exxon Mobil", TaxId="17-2466713", AddressLine1 ="Exxon Mobil Line 1", AddressLine2="Exxon Mobil Line 2", City= "Wal City", State ="NY", Zip ="12317", ProductOwnerId=4,  CreatedUser = "istcrrm", CreatedDate = DateTime.UtcNow  },
            //    new Client { CompanyName="UnitedHealth Group", TaxId="17-2466714", AddressLine1 ="UnitedHealth Group Line 1", AddressLine2="UnitedHealth Group Line 2", City= "Wal City", State ="NY", Zip ="12318", ProductOwnerId=4,  CreatedUser = "istcrrm", CreatedDate = DateTime.UtcNow  },
            //    new Client { CompanyName="CVS Health", TaxId="17-2466715", AddressLine1 ="CVS Health Line 1", AddressLine2="CVS Health Line 2", City= "Wal City", State ="NY", Zip ="12319", ProductOwnerId=4,  CreatedUser = "istcrrm", CreatedDate = DateTime.UtcNow  },
            //    new Client { CompanyName="General Motors", TaxId="17-2466716", AddressLine1 ="General Motors Line 1", AddressLine2="General Motors Line 2", City= "Wal City", State ="NY", Zip ="12312", ProductOwnerId=4,  CreatedUser = "istcrrm", CreatedDate = DateTime.UtcNow  },
            //};

            //await AddOrUpdateAsync(serviceProvider, a => a.ClientId, client);

            //var vendor = new Vendor[]
            //{
            //    new Vendor { CompanyName="Deloitte Consulting", TaxId="17-2466710", AddressLine1 ="Deloitte Consulting Line 1", AddressLine2="Deloitte Consulting Line 2", City= "D City", State ="NY", Zip ="12310", ProductOwnerId=4,  CreatedUser = "istcrrm", CreatedDate = DateTime.UtcNow  },
            //    new Vendor { CompanyName="Accenture", TaxId="17-2466711", AddressLine1 ="Accenture Line 1", AddressLine2="Accenture Line 2", City= "A City", State ="NY", Zip ="12315", ProductOwnerId=4,  CreatedUser = "istcrrm", CreatedDate = DateTime.UtcNow  },
            //    new Vendor { CompanyName="Mahindra", TaxId="17-2466712", AddressLine1 ="Mahindra Line 1", AddressLine2="Mahindra Line 2", City= "M City", State ="NY", Zip ="12316", ProductOwnerId=4,  CreatedUser = "istcrrm", CreatedDate = DateTime.UtcNow  },
            //    new Vendor { CompanyName="IBM Global", TaxId="17-2466714", AddressLine1 ="IBM Global Line 1", AddressLine2="IBM Global Line 2", City= "I City", State ="NY", Zip ="12318", ProductOwnerId=4,  CreatedUser = "istcrrm", CreatedDate = DateTime.UtcNow  },
            //};

            //await AddOrUpdateAsync(serviceProvider, a => a.VendorId, vendor);

            //var candidate = new Candidate[]
            //{
            //    new Candidate {FirstName= "John", LastName= "Lenon", MI= "", Gender= "M", SSN= "123231224", Email= "john.lenon@gmail.com", Phone= "1234567111", Phone2= "", AddressLine1 ="John Line 1", AddressLine2="John Line 2", City= "J City", State ="NY", Zip ="12310", ProductOwnerId=4,  CreatedUser = "istcrrm", CreatedDate = DateTime.UtcNow},
            //    new Candidate {FirstName= "Jerry", LastName= "Lewls", MI= "", Gender= "M", SSN= "123231212", Email= "jerry.lewls@yahoo.com", Phone= "1244467890", Phone2= "", AddressLine1 ="Jerry Line 1", AddressLine2="Jerry Line 2", City= "je City", State ="NY", Zip ="12310", ProductOwnerId=4,  CreatedUser = "istcrrm", CreatedDate = DateTime.UtcNow},
            //    new Candidate {FirstName= "Shawn", LastName= "Grandy", MI= "", Gender= "M", SSN= "123231120", Email= "sgrandy@gmail.com", Phone= "1234561290", Phone2= "", AddressLine1 ="Grandy Line 1", AddressLine2="Grandy Line 2", City= "g City", State ="NY", Zip ="12310", ProductOwnerId=4,  CreatedUser = "istcrrm", CreatedDate = DateTime.UtcNow},
            //    new Candidate {FirstName= "Chelsey", LastName= "Charbeneau", MI= "", Gender= "F", SSN= "121131226", Email= "chelsey@abc.com", Phone= "1114445544", Phone2= "", AddressLine1 ="chelsey Line 1", AddressLine2="chelsey Line 2", City= "c City", State ="NY", Zip ="12310", ProductOwnerId=4,  CreatedUser = "istcrrm", CreatedDate = DateTime.UtcNow},
            //};

            //await AddOrUpdateAsync(serviceProvider, a => a.CandidateId, candidate);

            //var enroll = new Enrollment[]
            //{
            //    new Enrollment {Internal= "I", HRUserId= 1, JobTitle= "Job Title 1", DurationYears= 1, DurationMonths= 0, BillRate= 75, PayRate= 71, StartDate= new DateTime(2017, 1, 1), EndDate= new DateTime(2018, 1, 1), VendorPO= "125jh6", CandidateId= 1005, EmploymentTypeCode= "1", TaxStatusCode= "C2C", ClientId= 4, EndClientId= 1, VendorId= 1,  CreatedUser = "istcrrm", CreatedDate = DateTime.UtcNow},
            //    new Enrollment {Internal= "I", HRUserId= 1, JobTitle= "Job Title 2", DurationYears= 1, DurationMonths= 6, BillRate= 80, PayRate= 75, StartDate= new DateTime(2017, 3, 1), EndDate= new DateTime(2019, 8, 1), VendorPO= "163454", CandidateId= 1006, EmploymentTypeCode= "1", TaxStatusCode= "C2C", ClientId= 1, EndClientId= 2, VendorId= 2,  CreatedUser = "istcrrm", CreatedDate = DateTime.UtcNow},
            //    new Enrollment {Internal= "I", HRUserId= 1, JobTitle= "Job Title 1", DurationYears= 2, DurationMonths= 0, BillRate= 60, PayRate= 45, StartDate= new DateTime(2016, 1, 1), EndDate= new DateTime(2018, 1, 1), VendorPO= "k23452", CandidateId= 1007, EmploymentTypeCode= "1", TaxStatusCode= "1099", ClientId= 2, EndClientId= 3, VendorId= 3,  CreatedUser = "istcrrm", CreatedDate = DateTime.UtcNow},
            //    new Enrollment {Internal= "I", HRUserId= 1, JobTitle= "Job Title 3", DurationYears= 0, DurationMonths= 6, BillRate= 55, PayRate= 50, StartDate= new DateTime(2017, 3, 1), EndDate= new DateTime(2017, 9, 1), VendorPO= "er3457", CandidateId= 1008, EmploymentTypeCode= "1", TaxStatusCode= "C2C", ClientId= 3, EndClientId= 2, VendorId= 3,  CreatedUser = "istcrrm", CreatedDate = DateTime.UtcNow},
            //};
            //await AddOrUpdateAsync(serviceProvider, a => a.EnrollmentId, enroll);
        }

        private static async Task AddOrUpdateAsync<TEntity>(
            IServiceProvider serviceProvider,
            Func<TEntity, object> propertyToMatch, IEnumerable<TEntity> entities)
            where TEntity : class
        {
            // Query in a separate context so that we can attach existing entities as modified
            List<TEntity> existingData;
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<OnboardDb>();
                existingData = db.Set<TEntity>().ToList();
            }

            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<OnboardDb>();
                foreach (var item in entities)
                {
                    db.Entry(item).State = existingData.Any(g => propertyToMatch(g).Equals(propertyToMatch(item)))
                        ? EntityState.Modified
                        : EntityState.Added;
                }

                await db.SaveChangesAsync();
            }
        }
    }
}
