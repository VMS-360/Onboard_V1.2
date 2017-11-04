using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Onboard.Web.UI.DataContexts.Migrations
{
    public partial class Mig11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidate_Visa_Status_Ref_Visa_Status_VisaTypeCode",
                table: "Candidate_Visa_Status");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ref_Visa_Status",
                table: "Ref_Visa_Status");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "End_Client");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Client");

            migrationBuilder.RenameTable(
                name: "Ref_Visa_Status",
                newName: "Ref_Visa_Type");

            migrationBuilder.AddColumn<string>(
                name: "AddressLine1",
                table: "Vendor",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressLine2",
                table: "Vendor",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Vendor",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductOwnerId",
                table: "Vendor",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Vendor",
                maxLength: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Zip",
                table: "Vendor",
                maxLength: 11,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressLine1",
                table: "End_Client",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressLine2",
                table: "End_Client",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "End_Client",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductOwnerId",
                table: "End_Client",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "End_Client",
                maxLength: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Zip",
                table: "End_Client",
                maxLength: 11,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressLine1",
                table: "Client",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressLine2",
                table: "Client",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Client",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductOwnerId",
                table: "Client",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Client",
                maxLength: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Zip",
                table: "Client",
                maxLength: 11,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductOwnerId",
                table: "Candidate",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ref_Visa_Type",
                table: "Ref_Visa_Type",
                column: "VisaTypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_ProductOwnerId",
                table: "Vendor",
                column: "ProductOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_End_Client_ProductOwnerId",
                table: "End_Client",
                column: "ProductOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Client_ProductOwnerId",
                table: "Client",
                column: "ProductOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_ProductOwnerId",
                table: "Candidate",
                column: "ProductOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidate_Product_Owner_ProductOwnerId",
                table: "Candidate",
                column: "ProductOwnerId",
                principalTable: "Product_Owner",
                principalColumn: "ProductOwnerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidate_Visa_Status_Ref_Visa_Type_VisaTypeCode",
                table: "Candidate_Visa_Status",
                column: "VisaTypeCode",
                principalTable: "Ref_Visa_Type",
                principalColumn: "VisaTypeCode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Client_Product_Owner_ProductOwnerId",
                table: "Client",
                column: "ProductOwnerId",
                principalTable: "Product_Owner",
                principalColumn: "ProductOwnerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_End_Client_Product_Owner_ProductOwnerId",
                table: "End_Client",
                column: "ProductOwnerId",
                principalTable: "Product_Owner",
                principalColumn: "ProductOwnerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendor_Product_Owner_ProductOwnerId",
                table: "Vendor",
                column: "ProductOwnerId",
                principalTable: "Product_Owner",
                principalColumn: "ProductOwnerId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidate_Product_Owner_ProductOwnerId",
                table: "Candidate");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidate_Visa_Status_Ref_Visa_Type_VisaTypeCode",
                table: "Candidate_Visa_Status");

            migrationBuilder.DropForeignKey(
                name: "FK_Client_Product_Owner_ProductOwnerId",
                table: "Client");

            migrationBuilder.DropForeignKey(
                name: "FK_End_Client_Product_Owner_ProductOwnerId",
                table: "End_Client");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendor_Product_Owner_ProductOwnerId",
                table: "Vendor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ref_Visa_Type",
                table: "Ref_Visa_Type");

            migrationBuilder.DropIndex(
                name: "IX_Vendor_ProductOwnerId",
                table: "Vendor");

            migrationBuilder.DropIndex(
                name: "IX_End_Client_ProductOwnerId",
                table: "End_Client");

            migrationBuilder.DropIndex(
                name: "IX_Client_ProductOwnerId",
                table: "Client");

            migrationBuilder.DropIndex(
                name: "IX_Candidate_ProductOwnerId",
                table: "Candidate");

            migrationBuilder.DropColumn(
                name: "AddressLine1",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "AddressLine2",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "ProductOwnerId",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "Zip",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "AddressLine1",
                table: "End_Client");

            migrationBuilder.DropColumn(
                name: "AddressLine2",
                table: "End_Client");

            migrationBuilder.DropColumn(
                name: "City",
                table: "End_Client");

            migrationBuilder.DropColumn(
                name: "ProductOwnerId",
                table: "End_Client");

            migrationBuilder.DropColumn(
                name: "State",
                table: "End_Client");

            migrationBuilder.DropColumn(
                name: "Zip",
                table: "End_Client");

            migrationBuilder.DropColumn(
                name: "AddressLine1",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "AddressLine2",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "ProductOwnerId",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "Zip",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "ProductOwnerId",
                table: "Candidate");

            migrationBuilder.RenameTable(
                name: "Ref_Visa_Type",
                newName: "Ref_Visa_Status");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Vendor",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "End_Client",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Client",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ref_Visa_Status",
                table: "Ref_Visa_Status",
                column: "VisaTypeCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidate_Visa_Status_Ref_Visa_Status_VisaTypeCode",
                table: "Candidate_Visa_Status",
                column: "VisaTypeCode",
                principalTable: "Ref_Visa_Status",
                principalColumn: "VisaTypeCode",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
