using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Onboard.Web.UI.DataContexts.Migrations
{
    public partial class Mig6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TaxStatus",
                table: "TaxStatus");

            migrationBuilder.RenameTable(
                name: "TaxStatus",
                newName: "Ref_Tax_Status");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Vendor_Contact",
                newName: "Phone");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Vendor_Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedUser",
                table: "Vendor_Contact",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Vendor_Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedUser",
                table: "Vendor_Contact",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ref_Tax_Status",
                table: "Ref_Tax_Status",
                column: "Code");

            migrationBuilder.CreateTable(
                name: "Candidate",
                columns: table => new
                {
                    CandidateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddressLine1 = table.Column<string>(maxLength: 100, nullable: true),
                    AddressLine2 = table.Column<string>(maxLength: 100, nullable: true),
                    City = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedUser = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    FirstName = table.Column<string>(maxLength: 100, nullable: true),
                    LastName = table.Column<string>(maxLength: 100, nullable: true),
                    MI = table.Column<string>(maxLength: 20, nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedUser = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(maxLength: 12, nullable: true),
                    Phone2 = table.Column<string>(maxLength: 12, nullable: true),
                    SSN = table.Column<string>(maxLength: 9, nullable: true),
                    State = table.Column<string>(maxLength: 20, nullable: true),
                    Zip = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidate", x => x.CandidateId);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    ClientId = table.Column<int>(nullable: false)
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
                    table.PrimaryKey("PK_Client", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "End_Client",
                columns: table => new
                {
                    EndClientId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompanyName = table.Column<string>(maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedUser = table.Column<string>(nullable: true),
                    Location = table.Column<string>(maxLength: 300, nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedUser = table.Column<string>(nullable: true),
                    TaxId = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_End_Client", x => x.EndClientId);
                });

            migrationBuilder.CreateTable(
                name: "Ref_Visa_Status",
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
                    table.PrimaryKey("PK_Ref_Visa_Status", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Client_Contact",
                columns: table => new
                {
                    ClientContactId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClientId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedUser = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedUser = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Phone = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client_Contact", x => x.ClientContactId);
                    table.ForeignKey(
                        name: "FK_Client_Contact_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Candidate_Visa_Status",
                columns: table => new
                {
                    CandidateVisaStatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CandidateId = table.Column<int>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedUser = table.Column<string>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedUser = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidate_Visa_Status", x => x.CandidateVisaStatusId);
                    table.ForeignKey(
                        name: "FK_Candidate_Visa_Status_Candidate_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidate",
                        principalColumn: "CandidateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Candidate_Visa_Status_Ref_Visa_Status_Code",
                        column: x => x.Code,
                        principalTable: "Ref_Visa_Status",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_Visa_Status_CandidateId",
                table: "Candidate_Visa_Status",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_Visa_Status_Code",
                table: "Candidate_Visa_Status",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_Client_Contact_ClientId",
                table: "Client_Contact",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Candidate_Visa_Status");

            migrationBuilder.DropTable(
                name: "Client_Contact");

            migrationBuilder.DropTable(
                name: "End_Client");

            migrationBuilder.DropTable(
                name: "Candidate");

            migrationBuilder.DropTable(
                name: "Ref_Visa_Status");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ref_Tax_Status",
                table: "Ref_Tax_Status");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Vendor_Contact");

            migrationBuilder.DropColumn(
                name: "CreatedUser",
                table: "Vendor_Contact");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Vendor_Contact");

            migrationBuilder.DropColumn(
                name: "ModifiedUser",
                table: "Vendor_Contact");

            migrationBuilder.RenameTable(
                name: "Ref_Tax_Status",
                newName: "TaxStatus");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Vendor_Contact",
                newName: "PhoneNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaxStatus",
                table: "TaxStatus",
                column: "Code");
        }
    }
}
