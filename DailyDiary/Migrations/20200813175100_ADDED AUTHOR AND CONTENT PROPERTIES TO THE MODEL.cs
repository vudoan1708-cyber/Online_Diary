using Microsoft.EntityFrameworkCore.Migrations;

namespace DailyDiary.Migrations
{
    public partial class ADDEDAUTHORANDCONTENTPROPERTIESTOTHEMODEL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Diary",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Diary",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Diary");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "Diary");
        }
    }
}
