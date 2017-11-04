using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Onboard.Web.UI.DataContexts.Migrations
{
    public partial class Mig20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VendorId",
                table: "Ref_Checklist");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Vendor_Checklist",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedUser",
                table: "Vendor_Checklist",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Vendor_Checklist",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedUser",
                table: "Vendor_Checklist",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Ref_Checklist",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedUser",
                table: "Ref_Checklist",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Ref_Checklist",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedUser",
                table: "Ref_Checklist",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Client_Checklist",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedUser",
                table: "Client_Checklist",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Client_Checklist",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedUser",
                table: "Client_Checklist",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Checklist",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedUser",
                table: "Checklist",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IndVendor",
                table: "Checklist",
                maxLength: 2,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Checklist",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedUser",
                table: "Checklist",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Vendor_Checklist");

            migrationBuilder.DropColumn(
                name: "CreatedUser",
                table: "Vendor_Checklist");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Vendor_Checklist");

            migrationBuilder.DropColumn(
                name: "ModifiedUser",
                table: "Vendor_Checklist");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Ref_Checklist");

            migrationBuilder.DropColumn(
                name: "CreatedUser",
                table: "Ref_Checklist");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Ref_Checklist");

            migrationBuilder.DropColumn(
                name: "ModifiedUser",
                table: "Ref_Checklist");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Client_Checklist");

            migrationBuilder.DropColumn(
                name: "CreatedUser",
                table: "Client_Checklist");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Client_Checklist");

            migrationBuilder.DropColumn(
                name: "ModifiedUser",
                table: "Client_Checklist");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Checklist");

            migrationBuilder.DropColumn(
                name: "CreatedUser",
                table: "Checklist");

            migrationBuilder.DropColumn(
                name: "IndVendor",
                table: "Checklist");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Checklist");

            migrationBuilder.DropColumn(
                name: "ModifiedUser",
                table: "Checklist");

            migrationBuilder.AddColumn<int>(
                name: "VendorId",
                table: "Ref_Checklist",
                nullable: true);
        }
    }
}
