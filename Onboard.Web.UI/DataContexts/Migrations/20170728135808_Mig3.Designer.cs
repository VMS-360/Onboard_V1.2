using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Onboard.Web.UI.DataContexts;

namespace Onboard.Web.UI.DataContexts.Migrations
{
    [DbContext(typeof(OnboardDb))]
    [Migration("20170728135808_Mig3")]
    partial class Mig3
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

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("ModifiedUser");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("ProductOwnerId");

                    b.ToTable("ProductOwner");
                });
        }
    }
}
