using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Onboard.Web.UI.DataContexts;

namespace Onboard.Web.UI.DataContexts.Migrations
{
    [DbContext(typeof(OnboardDb))]
    [Migration("20170910140707_Mig18")]
    partial class Mig18
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Onboard.Entities.Candidate", b =>
                {
                    b.Property<int>("CandidateId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AddressLine1")
                        .HasMaxLength(100);

                    b.Property<string>("AddressLine2")
                        .HasMaxLength(100);

                    b.Property<string>("City")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("CreatedUser");

                    b.Property<string>("Email")
                        .HasMaxLength(100);

                    b.Property<string>("FirstName")
                        .HasMaxLength(100);

                    b.Property<string>("Gender");

                    b.Property<string>("LastName")
                        .HasMaxLength(100);

                    b.Property<string>("MI")
                        .HasMaxLength(20);

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("ModifiedUser");

                    b.Property<string>("Phone")
                        .HasMaxLength(12);

                    b.Property<string>("Phone2")
                        .HasMaxLength(12);

                    b.Property<int>("ProductOwnerId");

                    b.Property<string>("SSN")
                        .HasMaxLength(9);

                    b.Property<string>("State")
                        .HasMaxLength(20);

                    b.Property<string>("Zip")
                        .HasMaxLength(10);

                    b.HasKey("CandidateId");

                    b.HasIndex("ProductOwnerId");

                    b.ToTable("Candidate");
                });

            modelBuilder.Entity("Onboard.Entities.CandidateVisaStatus", b =>
                {
                    b.Property<int>("CandidateVisaStatusId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CandidateId");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("CreatedUser");

                    b.Property<DateTime?>("EndDate");

                    b.Property<DateTime?>("ExpirationDate");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("ModifiedUser");

                    b.Property<DateTime?>("StartDate");

                    b.Property<string>("VisaTypeCode")
                        .IsRequired();

                    b.HasKey("CandidateVisaStatusId");

                    b.HasIndex("CandidateId");

                    b.HasIndex("VisaTypeCode");

                    b.ToTable("Candidate_Visa_Status");
                });

            modelBuilder.Entity("Onboard.Entities.Checklist", b =>
                {
                    b.Property<int>("ChecklistId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CommentType")
                        .HasMaxLength(10);

                    b.Property<string>("CommentValue")
                        .HasMaxLength(30);

                    b.Property<int>("EnrollmentId");

                    b.Property<string>("IndCompleted")
                        .HasMaxLength(2);

                    b.Property<string>("IsActive")
                        .HasMaxLength(2);

                    b.Property<string>("Text")
                        .HasMaxLength(200);

                    b.HasKey("ChecklistId");

                    b.HasIndex("EnrollmentId");

                    b.ToTable("Checklist");
                });

            modelBuilder.Entity("Onboard.Entities.Client", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AddressLine1")
                        .HasMaxLength(100);

                    b.Property<string>("AddressLine2")
                        .HasMaxLength(100);

                    b.Property<string>("City")
                        .HasMaxLength(50);

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("CreatedUser");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("ModifiedUser");

                    b.Property<int>("ProductOwnerId");

                    b.Property<string>("State")
                        .HasMaxLength(2);

                    b.Property<string>("TaxId")
                        .HasMaxLength(10);

                    b.Property<string>("Zip")
                        .HasMaxLength(11);

                    b.HasKey("ClientId");

                    b.HasIndex("ProductOwnerId");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("Onboard.Entities.ClientChecklist", b =>
                {
                    b.Property<int>("ClientChecklistId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CommentType")
                        .HasMaxLength(10);

                    b.Property<string>("CommentValue")
                        .HasMaxLength(30);

                    b.Property<int>("EnrollmentId");

                    b.Property<string>("IndCompleted")
                        .HasMaxLength(2);

                    b.Property<string>("IsActive")
                        .HasMaxLength(2);

                    b.Property<string>("Text")
                        .HasMaxLength(200);

                    b.HasKey("ClientChecklistId");

                    b.HasIndex("EnrollmentId");

                    b.ToTable("Client_Checklist");
                });

            modelBuilder.Entity("Onboard.Entities.ClientContact", b =>
                {
                    b.Property<int>("ClientContactId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ClientId");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("CreatedUser");

                    b.Property<string>("Email")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("ModifiedUser");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Phone")
                        .HasMaxLength(20);

                    b.HasKey("ClientContactId");

                    b.HasIndex("ClientId");

                    b.ToTable("Client_Contact");
                });

            modelBuilder.Entity("Onboard.Entities.EmploymentType", b =>
                {
                    b.Property<string>("EmploymentTypeCode")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(10);

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("CreatedUser");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("ModifiedUser");

                    b.HasKey("EmploymentTypeCode");

                    b.ToTable("Ref_Employment_Type");
                });

            modelBuilder.Entity("Onboard.Entities.EndClient", b =>
                {
                    b.Property<int>("EndClientId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AddressLine1")
                        .HasMaxLength(100);

                    b.Property<string>("AddressLine2")
                        .HasMaxLength(100);

                    b.Property<string>("City")
                        .HasMaxLength(50);

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("CreatedUser");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("ModifiedUser");

                    b.Property<int>("ProductOwnerId");

                    b.Property<string>("State")
                        .HasMaxLength(2);

                    b.Property<string>("TaxId")
                        .HasMaxLength(10);

                    b.Property<string>("Zip")
                        .HasMaxLength(11);

                    b.HasKey("EndClientId");

                    b.HasIndex("ProductOwnerId");

                    b.ToTable("End_Client");
                });

            modelBuilder.Entity("Onboard.Entities.Enrollment", b =>
                {
                    b.Property<int>("EnrollmentId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ActiveIndicator");

                    b.Property<string>("BGVCompany");

                    b.Property<string>("BGVRequired");

                    b.Property<decimal?>("BillRate");

                    b.Property<int>("CandidateId");

                    b.Property<int?>("ClientContactId");

                    b.Property<int?>("ClientId");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("CreatedUser");

                    b.Property<string>("DrugTestCompany");

                    b.Property<string>("DrugTestRequired");

                    b.Property<int?>("DurationMonths");

                    b.Property<int?>("DurationYears");

                    b.Property<string>("EmploymentTypeCode");

                    b.Property<int?>("EndClientId");

                    b.Property<DateTime?>("EndDate");

                    b.Property<int?>("HRUserId");

                    b.Property<DateTime?>("InactiveDate");

                    b.Property<string>("Internal");

                    b.Property<string>("JobTitle")
                        .HasMaxLength(80);

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("ModifiedUser");

                    b.Property<DateTime?>("OnboardedDate");

                    b.Property<string>("OnboardedIndicator");

                    b.Property<decimal?>("PayRate");

                    b.Property<int?>("ProtfolioManagerId");

                    b.Property<DateTime?>("StartDate");

                    b.Property<string>("TaxStatusCode");

                    b.Property<int?>("VendorContactId");

                    b.Property<int?>("VendorId");

                    b.Property<string>("VendorPO");

                    b.HasKey("EnrollmentId");

                    b.HasIndex("CandidateId");

                    b.HasIndex("ClientContactId");

                    b.HasIndex("ClientId");

                    b.HasIndex("EmploymentTypeCode");

                    b.HasIndex("EndClientId");

                    b.HasIndex("TaxStatusCode");

                    b.HasIndex("VendorContactId");

                    b.HasIndex("VendorId");

                    b.ToTable("Enrollment");
                });

            modelBuilder.Entity("Onboard.Entities.EnrollmentActivity", b =>
                {
                    b.Property<int>("EnrollmentActivityId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Action")
                        .HasMaxLength(500);

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("CreatedUser");

                    b.Property<int>("EnrollmentId");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("ModifiedUser");

                    b.HasKey("EnrollmentActivityId");

                    b.HasIndex("EnrollmentId");

                    b.ToTable("Enrollment_Activity");
                });

            modelBuilder.Entity("Onboard.Entities.EnrollmentComment", b =>
                {
                    b.Property<int>("EnrollmentCommentId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment")
                        .HasMaxLength(1000);

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("CreatedUser");

                    b.Property<int>("EnrollmentId");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("ModifiedUser");

                    b.HasKey("EnrollmentCommentId");

                    b.HasIndex("EnrollmentId");

                    b.ToTable("Enrollment_Comment");
                });

            modelBuilder.Entity("Onboard.Entities.LookupChecklist", b =>
                {
                    b.Property<int>("LookupChecklistId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ClientId");

                    b.Property<string>("CommentType")
                        .HasMaxLength(10);

                    b.Property<string>("EmploymentType")
                        .HasMaxLength(10);

                    b.Property<string>("IsActive")
                        .HasMaxLength(2);

                    b.Property<string>("Text")
                        .HasMaxLength(200);

                    b.Property<int>("VendorId");

                    b.HasKey("LookupChecklistId");

                    b.ToTable("Ref_Checklist");
                });

            modelBuilder.Entity("Onboard.Entities.ProductOwner", b =>
                {
                    b.Property<int>("ProductOwnerId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("CreatedUser");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("ModifiedUser");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("ProductOwnerId");

                    b.ToTable("Product_Owner");
                });

            modelBuilder.Entity("Onboard.Entities.TaxStatus", b =>
                {
                    b.Property<string>("TaxStatusCode")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(10);

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("CreatedUser");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("ModifiedUser");

                    b.HasKey("TaxStatusCode");

                    b.ToTable("Ref_Tax_Status");
                });

            modelBuilder.Entity("Onboard.Entities.Vendor", b =>
                {
                    b.Property<int>("VendorId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AddressLine1")
                        .HasMaxLength(100);

                    b.Property<string>("AddressLine2")
                        .HasMaxLength(100);

                    b.Property<string>("City")
                        .HasMaxLength(50);

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("CreatedUser");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("ModifiedUser");

                    b.Property<int>("ProductOwnerId");

                    b.Property<string>("State")
                        .HasMaxLength(2);

                    b.Property<string>("TaxId")
                        .HasMaxLength(10);

                    b.Property<string>("Zip")
                        .HasMaxLength(11);

                    b.HasKey("VendorId");

                    b.HasIndex("ProductOwnerId");

                    b.ToTable("Vendor");
                });

            modelBuilder.Entity("Onboard.Entities.VendorChecklist", b =>
                {
                    b.Property<int>("VendorChecklistId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CommentType")
                        .HasMaxLength(10);

                    b.Property<string>("CommentValue")
                        .HasMaxLength(30);

                    b.Property<string>("IndCompleted")
                        .HasMaxLength(2);

                    b.Property<string>("IsActive")
                        .HasMaxLength(2);

                    b.Property<string>("Text")
                        .HasMaxLength(200);

                    b.Property<int>("VendorId");

                    b.HasKey("VendorChecklistId");

                    b.HasIndex("VendorId");

                    b.ToTable("Vendor_Checklist");
                });

            modelBuilder.Entity("Onboard.Entities.VendorContact", b =>
                {
                    b.Property<int>("VendorContactId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("CreatedUser");

                    b.Property<string>("Email")
                        .HasMaxLength(100);

                    b.Property<string>("IsSignatory")
                        .HasMaxLength(1);

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("ModifiedUser");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Phone")
                        .HasMaxLength(20);

                    b.Property<int>("VendorId");

                    b.HasKey("VendorContactId");

                    b.HasIndex("VendorId");

                    b.ToTable("Vendor_Contact");
                });

            modelBuilder.Entity("Onboard.Entities.VisaType", b =>
                {
                    b.Property<string>("VisaTypeCode")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(10);

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("CreatedUser");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("ModifiedUser");

                    b.HasKey("VisaTypeCode");

                    b.ToTable("Ref_Visa_Type");
                });

            modelBuilder.Entity("Onboard.Entities.Candidate", b =>
                {
                    b.HasOne("Onboard.Entities.ProductOwner", "ProductOwner")
                        .WithMany()
                        .HasForeignKey("ProductOwnerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Onboard.Entities.CandidateVisaStatus", b =>
                {
                    b.HasOne("Onboard.Entities.Candidate", "Candidate")
                        .WithMany("CandidateVisaStatuses")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Onboard.Entities.VisaType", "VisaType")
                        .WithMany("CandidateVisaStatuses")
                        .HasForeignKey("VisaTypeCode")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Onboard.Entities.Checklist", b =>
                {
                    b.HasOne("Onboard.Entities.Enrollment", "Enrollment")
                        .WithMany()
                        .HasForeignKey("EnrollmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Onboard.Entities.Client", b =>
                {
                    b.HasOne("Onboard.Entities.ProductOwner", "ProductOwner")
                        .WithMany()
                        .HasForeignKey("ProductOwnerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Onboard.Entities.ClientChecklist", b =>
                {
                    b.HasOne("Onboard.Entities.Enrollment", "Enrollment")
                        .WithMany()
                        .HasForeignKey("EnrollmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Onboard.Entities.ClientContact", b =>
                {
                    b.HasOne("Onboard.Entities.Client", "Client")
                        .WithMany("ClientContacts")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Onboard.Entities.EndClient", b =>
                {
                    b.HasOne("Onboard.Entities.ProductOwner", "ProductOwner")
                        .WithMany()
                        .HasForeignKey("ProductOwnerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Onboard.Entities.Enrollment", b =>
                {
                    b.HasOne("Onboard.Entities.Candidate", "Candidate")
                        .WithMany()
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Onboard.Entities.ClientContact", "ClientContact")
                        .WithMany()
                        .HasForeignKey("ClientContactId");

                    b.HasOne("Onboard.Entities.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId");

                    b.HasOne("Onboard.Entities.EmploymentType", "EmploymentType")
                        .WithMany()
                        .HasForeignKey("EmploymentTypeCode");

                    b.HasOne("Onboard.Entities.EndClient", "EndClient")
                        .WithMany()
                        .HasForeignKey("EndClientId");

                    b.HasOne("Onboard.Entities.TaxStatus", "TaxStatus")
                        .WithMany()
                        .HasForeignKey("TaxStatusCode");

                    b.HasOne("Onboard.Entities.VendorContact", "VendorContact")
                        .WithMany()
                        .HasForeignKey("VendorContactId");

                    b.HasOne("Onboard.Entities.Vendor", "Vendor")
                        .WithMany()
                        .HasForeignKey("VendorId");
                });

            modelBuilder.Entity("Onboard.Entities.EnrollmentActivity", b =>
                {
                    b.HasOne("Onboard.Entities.Enrollment", "Enrollment")
                        .WithMany()
                        .HasForeignKey("EnrollmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Onboard.Entities.EnrollmentComment", b =>
                {
                    b.HasOne("Onboard.Entities.Enrollment", "Enrollment")
                        .WithMany()
                        .HasForeignKey("EnrollmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Onboard.Entities.Vendor", b =>
                {
                    b.HasOne("Onboard.Entities.ProductOwner", "ProductOwner")
                        .WithMany()
                        .HasForeignKey("ProductOwnerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Onboard.Entities.VendorChecklist", b =>
                {
                    b.HasOne("Onboard.Entities.Vendor", "Vendor")
                        .WithMany()
                        .HasForeignKey("VendorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Onboard.Entities.VendorContact", b =>
                {
                    b.HasOne("Onboard.Entities.Vendor", "Vendor")
                        .WithMany("VendorContacts")
                        .HasForeignKey("VendorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
