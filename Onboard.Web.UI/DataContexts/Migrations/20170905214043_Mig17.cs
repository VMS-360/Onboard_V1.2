using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Onboard.Web.UI.DataContexts.Migrations
{
    public partial class Mig17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Checklist",
                columns: table => new
                {
                    ChecklistId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EnrollmentId = table.Column<int>(nullable: false),
                    IndCompleted = table.Column<string>(maxLength: 2, nullable: true),
                    IsActive = table.Column<string>(maxLength: 2, nullable: true),
                    Text = table.Column<string>(maxLength: 200, nullable: true),
                    Type = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checklist", x => x.ChecklistId);
                    table.ForeignKey(
                        name: "FK_Checklist_Enrollment_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollment",
                        principalColumn: "EnrollmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Client_Checklist",
                columns: table => new
                {
                    ClientChecklistId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClientId = table.Column<int>(nullable: false),
                    IndCompleted = table.Column<string>(maxLength: 2, nullable: true),
                    IsActive = table.Column<string>(maxLength: 2, nullable: true),
                    Text = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client_Checklist", x => x.ClientChecklistId);
                    table.ForeignKey(
                        name: "FK_Client_Checklist_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vendor_Checklist",
                columns: table => new
                {
                    VendorChecklistId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IndCompleted = table.Column<string>(maxLength: 2, nullable: true),
                    IsActive = table.Column<string>(maxLength: 2, nullable: true),
                    Text = table.Column<string>(maxLength: 200, nullable: true),
                    VendorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendor_Checklist", x => x.VendorChecklistId);
                    table.ForeignKey(
                        name: "FK_Vendor_Checklist_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Checklist_EnrollmentId",
                table: "Checklist",
                column: "EnrollmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Client_Checklist_ClientId",
                table: "Client_Checklist",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_Checklist_VendorId",
                table: "Vendor_Checklist",
                column: "VendorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Checklist");

            migrationBuilder.DropTable(
                name: "Client_Checklist");

            migrationBuilder.DropTable(
                name: "Vendor_Checklist");
        }
    }
}
