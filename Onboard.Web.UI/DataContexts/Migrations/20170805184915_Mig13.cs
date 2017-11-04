using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Onboard.Web.UI.DataContexts.Migrations
{
    public partial class Mig13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<char>(
                name: "ActiveIndicator",
                table: "Enrollment",
                maxLength: 1,
                nullable: false,
                oldClrType: typeof(decimal));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ActiveIndicator",
                table: "Enrollment",
                nullable: false,
                oldClrType: typeof(char),
                oldMaxLength: 1);
        }
    }
}
