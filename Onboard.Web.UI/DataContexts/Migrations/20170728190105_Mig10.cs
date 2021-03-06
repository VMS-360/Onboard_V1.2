﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Onboard.Web.UI.DataContexts.Migrations
{
    public partial class Mig10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<char>(
                name: "ActiveIndicator",
                table: "Enrollment",
                nullable: false,
                defaultValue: ' ');

            migrationBuilder.AddColumn<DateTime>(
                name: "InactiveDate",
                table: "Enrollment",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OnboardedDate",
                table: "Enrollment",
                nullable: true);

            migrationBuilder.AddColumn<char>(
                name: "OnboardedIndicator",
                table: "Enrollment",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveIndicator",
                table: "Enrollment");

            migrationBuilder.DropColumn(
                name: "InactiveDate",
                table: "Enrollment");

            migrationBuilder.DropColumn(
                name: "OnboardedDate",
                table: "Enrollment");

            migrationBuilder.DropColumn(
                name: "OnboardedIndicator",
                table: "Enrollment");
        }
    }
}
