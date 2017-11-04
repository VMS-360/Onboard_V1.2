using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Onboard.Web.UI.DataContexts.Migrations
{
    public partial class Mig5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Ref_Tax_Status",
                table: "Ref_Tax_Status");

            migrationBuilder.RenameTable(
                name: "Ref_Tax_Status",
                newName: "TaxStatus");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaxStatus",
                table: "TaxStatus",
                column: "Code");

            migrationBuilder.CreateTable(
                name: "Vendor",
                columns: table => new
                {
                    VendorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(maxLength: 300, nullable: true),
                    CompanyName = table.Column<string>(maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedUser = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedUser = table.Column<string>(nullable: true),
                    TaxId = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendor", x => x.VendorId);
                });

            migrationBuilder.CreateTable(
                name: "Vendor_Contact",
                columns: table => new
                {
                    VendorContactId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    IsSignatory = table.Column<string>(maxLength: 1, nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 20, nullable: true),
                    VendorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendor_Contact", x => x.VendorContactId);
                    table.ForeignKey(
                        name: "FK_Vendor_Contact_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_Contact_VendorId",
                table: "Vendor_Contact",
                column: "VendorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vendor_Contact");

            migrationBuilder.DropTable(
                name: "Vendor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaxStatus",
                table: "TaxStatus");

            migrationBuilder.RenameTable(
                name: "TaxStatus",
                newName: "Ref_Tax_Status");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ref_Tax_Status",
                table: "Ref_Tax_Status",
                column: "Code");
        }
    }
}
