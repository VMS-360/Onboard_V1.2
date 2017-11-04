using Microsoft.EntityFrameworkCore;
using Onboard.Entities;
using System;
using System.Linq;

namespace Onboard.Web.UI.DataContexts
{
    public class OnboardDb : DbContext
    {
        public OnboardDb(DbContextOptions options) :base(options)
        {
            
        }

        public DbSet<ProductOwner> Product_Owner { get; set; }
        public DbSet<EmploymentType> Ref_Employment_Type { get; set; }
        public DbSet<TaxStatus> Ref_Tax_Status { get; set; }
        public DbSet<VisaType> Ref_Visa_Type { get; set; }
        public DbSet<EndClient> End_Client { get; set; }
        public DbSet<Vendor> Vendor { get; set; }
        public DbSet<VendorContact> Vendor_Contact { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<ClientContact> Client_Contact { get; set; }
        public DbSet<Candidate> Candidate { get; set; }
        public DbSet<CandidateVisaStatus> Candidate_Visa_Status { get; set; }
        public DbSet<Enrollment> Enrollment { get; set; }
        public DbSet<EnrollmentActivity> Enrollment_Activity { get; set; }
        public DbSet<EnrollmentComment> Enrollment_Comment { get; set; }
        public DbSet<VendorChecklist> Vendor_Checklist { get; set; }
        public DbSet<ClientChecklist> Client_Checklist { get; set; }
        public DbSet<Checklist> Checklist { get; set; }
        public DbSet<LookupChecklist> Ref_Checklist { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<VendorContact>()
            //.HasOne(d => d.Vendor)
            //.WithMany();
        }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedDate = DateTime.UtcNow;
                    ((BaseEntity)entity.Entity).CreatedUser = ((BaseEntity)entity.Entity).CurrentUser;
                }
                else
                {
                    ((BaseEntity)entity.Entity).ModifiedDate = DateTime.UtcNow;
                    ((BaseEntity)entity.Entity).ModifiedUser = ((BaseEntity)entity.Entity).CurrentUser;
                }
            }
        }
    }
}
