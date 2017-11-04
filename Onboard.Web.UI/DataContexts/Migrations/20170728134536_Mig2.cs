using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Onboard.Web.UI.DataContexts.Migrations
{
    public partial class Mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ProductOwners",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedUser",
                table: "ProductOwners",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrentUser",
                table: "ProductOwners",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "ProductOwners",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedUser",
                table: "ProductOwners",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ProductOwners");

            migrationBuilder.DropColumn(
                name: "CreatedUser",
                table: "ProductOwners");

            migrationBuilder.DropColumn(
                name: "CurrentUser",
                table: "ProductOwners");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "ProductOwners");

            migrationBuilder.DropColumn(
                name: "ModifiedUser",
                table: "ProductOwners");
        }
    }
}
