using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Onboard.Web.UI.DataContexts;

namespace Onboard.Web.UI.DataContexts.Migrations
{
    [DbContext(typeof(OnboardDb))]
    [Migration("20170728173341_Mig7")]
    partial class Mig7
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

                    b.Property<string>("SSN")
                        .HasMaxLength(9);

                    b.Property<string>("State")
                        .HasMaxLength(20);

                    b.Property<string>("Zip")
                        .HasMaxLength(10);

                    b.HasKey("CandidateId");

                    b.ToTable("Candidate");
                });

            modelBuilder.Entity("Onboard.Entities.CandidateVisaStatus", b =>
                {
                    b.Property<int>("CandidateVisaStatusId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CandidateId");

                    b.Property<string>("Code");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("CreatedUser");

                    b.Property<DateTime?>("EndDate");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("ModifiedUser");

                    b.Property<DateTime?>("StartDate");

                    b.HasKey("CandidateVisaStatusId");

                    b.HasIndex("CandidateId");

                    b.HasIndex("Code");

                    b.ToTable("Candidate_Visa_Status");
                });

            modelBuilder.Entity("Onboard.Entities.Client", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .HasMaxLength(300);

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("CreatedUser");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("ModifiedUser");

                    b.Property<string>("TaxId")
                        .HasMaxLength(10);

                    b.HasKey("ClientId");

                    b.ToTable("Client");
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
                    b.Property<string>("Code")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(10);

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("CreatedUser");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("ModifiedUser");

                    b.HasKey("Code");

                    b.ToTable("Ref_Employment_Type");
                });

            modelBuilder.Entity("Onboard.Entities.EndClient", b =>
                {
                    b.Property<int>("EndClientId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("CreatedUser");

                    b.Property<string>("Location")
                        .HasMaxLength(300);

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("ModifiedUser");

                    b.Property<string>("TaxId")
                        .HasMaxLength(10);

                    b.HasKey("EndClientId");

                    b.ToTable("End_Client");
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
                    b.Property<string>("Code")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(10);

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("CreatedUser");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("ModifiedUser");

                    b.HasKey("Code");

                    b.ToTable("Ref_Tax_Status");
                });

            modelBuilder.Entity("Onboard.Entities.Vendor", b =>
                {
                    b.Property<int>("VendorId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .HasMaxLength(300);

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("CreatedUser");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("ModifiedUser");

                    b.Property<string>("TaxId")
                        .HasMaxLength(10);

                    b.HasKey("VendorId");

                    b.ToTable("Vendor");
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
                    b.Property<string>("Code")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(10);

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("CreatedUser");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("ModifiedUser");

                    b.HasKey("Code");

                    b.ToTable("Ref_Visa_Status");
                });

            modelBuilder.Entity("Onboard.Entities.CandidateVisaStatus", b =>
                {
                    b.HasOne("Onboard.Entities.Candidate", "Candidate")
                        .WithMany("CandidateVisaStatuses")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Onboard.Entities.VisaType", "VisaType")
                        .WithMany("CandidateVisaStatuses")
                        .HasForeignKey("Code");
                });

            modelBuilder.Entity("Onboard.Entities.ClientContact", b =>
                {
                    b.HasOne("Onboard.Entities.Client", "Client")
                        .WithMany("ClientContacts")
                        .HasForeignKey("ClientId")
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
