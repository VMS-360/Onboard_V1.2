using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Onboard.Web.UI.DataContexts.Migrations
{
    public partial class Mig4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductOwner",
                table: "ProductOwner");

            migrationBuilder.RenameTable(
                name: "ProductOwner",
                newName: "Product_Owner");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product_Owner",
                table: "Product_Owner",
                column: "ProductOwnerId");

            migrationBuilder.CreateTable(
                name: "Ref_Employment_Type",
                columns: table => new
                {
                    Code = table.Column<string>(maxLength: 10, nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedUser = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 100, nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ref_Employment_Type", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Ref_Tax_Status",
                columns: table => new
                {
                    Code = table.Column<string>(maxLength: 10, nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedUser = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 100, nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ref_Tax_Status", x => x.Code);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ref_Employment_Type");

            migrationBuilder.DropTable(
                name: "Ref_Tax_Status");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product_Owner",
                table: "Product_Owner");

            migrationBuilder.RenameTable(
                name: "Product_Owner",
                newName: "ProductOwner");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductOwner",
                table: "ProductOwner",
                column: "ProductOwnerId");
        }
    }
}
