using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Onboard.Web.UI.DataContexts.Migrations
{
    public partial class Mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductOwners",
                table: "ProductOwners");

            migrationBuilder.DropColumn(
                name: "CurrentUser",
                table: "ProductOwners");

            migrationBuilder.RenameTable(
                name: "ProductOwners",
                newName: "ProductOwner");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductOwner",
                table: "ProductOwner",
                column: "ProductOwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductOwner",
                table: "ProductOwner");

            migrationBuilder.RenameTable(
                name: "ProductOwner",
                newName: "ProductOwners");

            migrationBuilder.AddColumn<string>(
                name: "CurrentUser",
                table: "ProductOwners",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductOwners",
                table: "ProductOwners",
                column: "ProductOwnerId");
        }
    }
}
