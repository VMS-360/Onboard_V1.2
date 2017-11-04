using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Onboard.Web.UI.DataContexts;

namespace Onboard.Web.UI.DataContexts.Migrations
{
    [DbContext(typeof(OnboardDb))]
    [Migration("20170728141556_Mig4")]
    partial class Mig4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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
        }
    }
}
