using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DailyDiary.Migrations
{
    public partial class CHANGEDTHEDATESPELLING : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dates",
                table: "Diary");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Diary",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Diary");

            migrationBuilder.AddColumn<DateTime>(
                name: "Dates",
                table: "Diary",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
