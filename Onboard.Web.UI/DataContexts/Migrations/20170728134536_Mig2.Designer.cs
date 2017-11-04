using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Onboard.Web.UI.DataContexts;

namespace Onboard.Web.UI.DataContexts.Migrations
{
    [DbContext(typeof(OnboardDb))]
    [Migration("20170728134536_Mig2")]
    partial class Mig2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Onboard.Entities.ProductOwner", b =>
                {
                    b.Property<int>("ProductOwnerId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("CreatedUser");

                    b.Property<string>("CurrentUser");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("ModifiedUser");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("ProductOwnerId");

                    b.ToTable("ProductOwners");
                });
        }
    }
}
