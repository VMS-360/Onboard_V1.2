using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Onboard.Web.UI.DataContexts.Migrations
{
    public partial class Mig14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OnboardedIndicator",
                table: "Enrollment",
                nullable: true,
                oldClrType: typeof(char),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Internal",
                table: "Enrollment",
                nullable: true,
                oldClrType: typeof(char));

            migrationBuilder.AlterColumn<string>(
                name: "DrugTestRequired",
                table: "Enrollment",
                nullable: true,
                oldClrType: typeof(char),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BGVRequired",
                table: "Enrollment",
                nullable: true,
                oldClrType: typeof(char),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<char>(
                name: "OnboardedIndicator",
                table: "Enrollment",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<char>(
                name: "Internal",
                table: "Enrollment",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<char>(
                name: "DrugTestRequired",
                table: "Enrollment",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<char>(
                name: "BGVRequired",
                table: "Enrollment",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
