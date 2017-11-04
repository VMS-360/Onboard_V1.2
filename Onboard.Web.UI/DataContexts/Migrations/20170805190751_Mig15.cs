using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Onboard.Web.UI.DataContexts.Migrations
{
    public partial class Mig15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ActiveIndicator",
                table: "Enrollment",
                nullable: true,
                oldClrType: typeof(char));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<char>(
                name: "ActiveIndicator",
                table: "Enrollment",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
