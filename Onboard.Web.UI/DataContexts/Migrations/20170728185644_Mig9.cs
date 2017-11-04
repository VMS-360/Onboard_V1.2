using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Onboard.Web.UI.DataContexts.Migrations
{
    public partial class Mig9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VendorContactId",
                table: "Enrollment",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VendorId",
                table: "Enrollment",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_VendorContactId",
                table: "Enrollment",
                column: "VendorContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_VendorId",
                table: "Enrollment",
                column: "VendorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_Vendor_Contact_VendorContactId",
                table: "Enrollment",
                column: "VendorContactId",
                principalTable: "Vendor_Contact",
                principalColumn: "VendorContactId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_Vendor_VendorId",
                table: "Enrollment",
                column: "VendorId",
                principalTable: "Vendor",
                principalColumn: "VendorId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_Vendor_Contact_VendorContactId",
                table: "Enrollment");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_Vendor_VendorId",
                table: "Enrollment");

            migrationBuilder.DropIndex(
                name: "IX_Enrollment_VendorContactId",
                table: "Enrollment");

            migrationBuilder.DropIndex(
                name: "IX_Enrollment_VendorId",
                table: "Enrollment");

            migrationBuilder.DropColumn(
                name: "VendorContactId",
                table: "Enrollment");

            migrationBuilder.DropColumn(
                name: "VendorId",
                table: "Enrollment");
        }
    }
}
