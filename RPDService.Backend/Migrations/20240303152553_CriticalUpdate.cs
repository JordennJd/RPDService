using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPDSerice.Migrations
{
    /// <inheritdoc />
    public partial class CriticalUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiffTest",
                table: "CriticalInfo");

            migrationBuilder.DropColumn(
                name: "Exam",
                table: "CriticalInfo");

            migrationBuilder.DropColumn(
                name: "KandExam",
                table: "CriticalInfo");

            migrationBuilder.DropColumn(
                name: "Test",
                table: "CriticalInfo");

            migrationBuilder.AddColumn<string>(
                name: "TypeOfControl",
                table: "CriticalInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeOfControl",
                table: "CriticalInfo");

            migrationBuilder.AddColumn<bool>(
                name: "DiffTest",
                table: "CriticalInfo",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Exam",
                table: "CriticalInfo",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "KandExam",
                table: "CriticalInfo",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Test",
                table: "CriticalInfo",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
