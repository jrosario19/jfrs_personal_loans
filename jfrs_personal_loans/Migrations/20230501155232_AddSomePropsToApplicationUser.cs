using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace jfrs_personal_loans.Migrations
{
    public partial class AddSomePropsToApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOnDate",
                table: "CompanyConfigurations",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ResetPasswordCode",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResetPasswordCodeTimeStamp",
                table: "AspNetUsers",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ResetPasswordCode",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ResetPasswordCodeTimeStamp",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOnDate",
                table: "CompanyConfigurations",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");
        }
    }
}
