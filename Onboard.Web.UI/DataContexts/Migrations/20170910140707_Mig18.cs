using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Onboard.Web.UI.DataContexts.Migrations
{
    public partial class Mig18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Client_Checklist_Client_ClientId",
                table: "Client_Checklist");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Client_Checklist",
                newName: "EnrollmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Client_Checklist_ClientId",
                table: "Client_Checklist",
                newName: "IX_Client_Checklist_EnrollmentId");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Checklist",
                newName: "CommentType");

            migrationBuilder.AddColumn<string>(
                name: "CommentType",
                table: "Vendor_Checklist",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CommentValue",
                table: "Vendor_Checklist",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CommentType",
                table: "Client_Checklist",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CommentValue",
                table: "Client_Checklist",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CommentValue",
                table: "Checklist",
                maxLength: 30,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Ref_Checklist",
                columns: table => new
                {
                    LookupChecklistId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClientId = table.Column<int>(nullable: false),
                    CommentType = table.Column<string>(maxLength: 10, nullable: true),
                    EmploymentType = table.Column<string>(maxLength: 10, nullable: true),
                    IsActive = table.Column<string>(maxLength: 2, nullable: true),
                    Text = table.Column<string>(maxLength: 200, nullable: true),
                    VendorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ref_Checklist", x => x.LookupChecklistId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Client_Checklist_Enrollment_EnrollmentId",
                table: "Client_Checklist",
                column: "EnrollmentId",
                principalTable: "Enrollment",
                principalColumn: "EnrollmentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Client_Checklist_Enrollment_EnrollmentId",
                table: "Client_Checklist");

            migrationBuilder.DropTable(
                name: "Ref_Checklist");

            migrationBuilder.DropColumn(
                name: "CommentType",
                table: "Vendor_Checklist");

            migrationBuilder.DropColumn(
                name: "CommentValue",
                table: "Vendor_Checklist");

            migrationBuilder.DropColumn(
                name: "CommentType",
                table: "Client_Checklist");

            migrationBuilder.DropColumn(
                name: "CommentValue",
                table: "Client_Checklist");

            migrationBuilder.DropColumn(
                name: "CommentValue",
                table: "Checklist");

            migrationBuilder.RenameColumn(
                name: "EnrollmentId",
                table: "Client_Checklist",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Client_Checklist_EnrollmentId",
                table: "Client_Checklist",
                newName: "IX_Client_Checklist_ClientId");

            migrationBuilder.RenameColumn(
                name: "CommentType",
                table: "Checklist",
                newName: "Type");

            migrationBuilder.AddForeignKey(
                name: "FK_Client_Checklist_Client_ClientId",
                table: "Client_Checklist",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
