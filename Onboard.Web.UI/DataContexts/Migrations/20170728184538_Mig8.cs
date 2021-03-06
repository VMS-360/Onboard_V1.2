﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Onboard.Web.UI.DataContexts.Migrations
{
    public partial class Mig8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidate_Visa_Status_Ref_Visa_Status_Code",
                table: "Candidate_Visa_Status");

            migrationBuilder.DropIndex(
                name: "IX_Candidate_Visa_Status_Code",
                table: "Candidate_Visa_Status");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Candidate_Visa_Status");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Ref_Visa_Status",
                newName: "VisaTypeCode");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Ref_Tax_Status",
                newName: "TaxStatusCode");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Ref_Employment_Type",
                newName: "EmploymentTypeCode");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "Candidate_Visa_Status",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VisaTypeCode",
                table: "Candidate_Visa_Status",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<char>(
                name: "Gender",
                table: "Candidate",
                nullable: false,
                defaultValue: ' ');

            migrationBuilder.CreateTable(
                name: "Enrollment",
                columns: table => new
                {
                    EnrollmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BGVCompany = table.Column<string>(nullable: true),
                    BGVRequired = table.Column<char>(nullable: true),
                    BillRate = table.Column<decimal>(nullable: true),
                    CandidateId = table.Column<int>(nullable: false),
                    ClientContactId = table.Column<int>(nullable: true),
                    ClientId = table.Column<int>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedUser = table.Column<string>(nullable: true),
                    DrugTestCompany = table.Column<string>(nullable: true),
                    DrugTestRequired = table.Column<char>(nullable: true),
                    DurationMonths = table.Column<int>(nullable: true),
                    DurationYears = table.Column<int>(nullable: true),
                    EmploymentTypeCode = table.Column<string>(nullable: true),
                    EndClientId = table.Column<int>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    HRUserId = table.Column<int>(nullable: true),
                    Internal = table.Column<char>(nullable: false),
                    JobTitle = table.Column<string>(maxLength: 80, nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedUser = table.Column<string>(nullable: true),
                    PayRate = table.Column<decimal>(nullable: true),
                    ProtfolioManagerId = table.Column<int>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    TaxStatusCode = table.Column<string>(nullable: true),
                    VendorPO = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollment", x => x.EnrollmentId);
                    table.ForeignKey(
                        name: "FK_Enrollment_Candidate_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidate",
                        principalColumn: "CandidateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollment_Client_Contact_ClientContactId",
                        column: x => x.ClientContactId,
                        principalTable: "Client_Contact",
                        principalColumn: "ClientContactId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Enrollment_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Enrollment_Ref_Employment_Type_EmploymentTypeCode",
                        column: x => x.EmploymentTypeCode,
                        principalTable: "Ref_Employment_Type",
                        principalColumn: "EmploymentTypeCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Enrollment_End_Client_EndClientId",
                        column: x => x.EndClientId,
                        principalTable: "End_Client",
                        principalColumn: "EndClientId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Enrollment_Ref_Tax_Status_TaxStatusCode",
                        column: x => x.TaxStatusCode,
                        principalTable: "Ref_Tax_Status",
                        principalColumn: "TaxStatusCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Enrollment_Activity",
                columns: table => new
                {
                    EnrollmentActivityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Action = table.Column<string>(maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedUser = table.Column<string>(nullable: true),
                    EnrollmentId = table.Column<int>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollment_Activity", x => x.EnrollmentActivityId);
                    table.ForeignKey(
                        name: "FK_Enrollment_Activity_Enrollment_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollment",
                        principalColumn: "EnrollmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enrollment_Comment",
                columns: table => new
                {
                    EnrollmentCommentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comment = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedUser = table.Column<string>(nullable: true),
                    EnrollmentId = table.Column<int>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollment_Comment", x => x.EnrollmentCommentId);
                    table.ForeignKey(
                        name: "FK_Enrollment_Comment_Enrollment_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollment",
                        principalColumn: "EnrollmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_Visa_Status_VisaTypeCode",
                table: "Candidate_Visa_Status",
                column: "VisaTypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_CandidateId",
                table: "Enrollment",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_ClientContactId",
                table: "Enrollment",
                column: "ClientContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_ClientId",
                table: "Enrollment",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_EmploymentTypeCode",
                table: "Enrollment",
                column: "EmploymentTypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_EndClientId",
                table: "Enrollment",
                column: "EndClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_TaxStatusCode",
                table: "Enrollment",
                column: "TaxStatusCode");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_Activity_EnrollmentId",
                table: "Enrollment_Activity",
                column: "EnrollmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_Comment_EnrollmentId",
                table: "Enrollment_Comment",
                column: "EnrollmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidate_Visa_Status_Ref_Visa_Status_VisaTypeCode",
                table: "Candidate_Visa_Status",
                column: "VisaTypeCode",
                principalTable: "Ref_Visa_Status",
                principalColumn: "VisaTypeCode",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidate_Visa_Status_Ref_Visa_Status_VisaTypeCode",
                table: "Candidate_Visa_Status");

            migrationBuilder.DropTable(
                name: "Enrollment_Activity");

            migrationBuilder.DropTable(
                name: "Enrollment_Comment");

            migrationBuilder.DropTable(
                name: "Enrollment");

            migrationBuilder.DropIndex(
                name: "IX_Candidate_Visa_Status_VisaTypeCode",
                table: "Candidate_Visa_Status");

            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "Candidate_Visa_Status");

            migrationBuilder.DropColumn(
                name: "VisaTypeCode",
                table: "Candidate_Visa_Status");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Candidate");

            migrationBuilder.RenameColumn(
                name: "VisaTypeCode",
                table: "Ref_Visa_Status",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "TaxStatusCode",
                table: "Ref_Tax_Status",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "EmploymentTypeCode",
                table: "Ref_Employment_Type",
                newName: "Code");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Candidate_Visa_Status",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_Visa_Status_Code",
                table: "Candidate_Visa_Status",
                column: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidate_Visa_Status_Ref_Visa_Status_Code",
                table: "Candidate_Visa_Status",
                column: "Code",
                principalTable: "Ref_Visa_Status",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
