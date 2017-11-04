using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Onboard.Web.UI.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Onboard_User_Token",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Onboard_User_Token", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "Onboard_Role",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    IsActive = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Onboard_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Onboard_User",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    IsInternal = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    NotificationColor = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    PrimaryColor = table.Column<string>(nullable: true),
                    ProductOwnerId = table.Column<int>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    WarningColor = table.Column<string>(nullable: true),
                    WarningColor2 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Onboard_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Onboard_Role_Claim",
                columns: table => new
                {
                    RoleClaimId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Onboard_Role_Claim", x => x.RoleClaimId);
                    table.ForeignKey(
                        name: "FK_Onboard_Role_Claim_Onboard_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Onboard_Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Onboard_User_Claim",
                columns: table => new
                {
                    UserClaimId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Onboard_User_Claim", x => x.UserClaimId);
                    table.ForeignKey(
                        name: "FK_Onboard_User_Claim_Onboard_User_UserId",
                        column: x => x.UserId,
                        principalTable: "Onboard_User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Onboard_User_Login",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Onboard_User_Login", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_Onboard_User_Login_Onboard_User_UserId",
                        column: x => x.UserId,
                        principalTable: "Onboard_User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Onboard_User_Role",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Onboard_User_Role", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_Onboard_User_Role_Onboard_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Onboard_Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Onboard_User_Role_Onboard_User_UserId",
                        column: x => x.UserId,
                        principalTable: "Onboard_User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Onboard_Role_Claim_RoleId",
                table: "Onboard_Role_Claim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Onboard_User_Claim_UserId",
                table: "Onboard_User_Claim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Onboard_User_Login_UserId",
                table: "Onboard_User_Login",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Onboard_User_Role_RoleId",
                table: "Onboard_User_Role",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Onboard_Role",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Onboard_User",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Onboard_User",
                column: "NormalizedUserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Onboard_Role_Claim");

            migrationBuilder.DropTable(
                name: "Onboard_User_Claim");

            migrationBuilder.DropTable(
                name: "Onboard_User_Login");

            migrationBuilder.DropTable(
                name: "Onboard_User_Role");

            migrationBuilder.DropTable(
                name: "Onboard_User_Token");

            migrationBuilder.DropTable(
                name: "Onboard_Role");

            migrationBuilder.DropTable(
                name: "Onboard_User");
        }
    }
}
