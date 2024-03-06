using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPDSerice.Migrations
{
    /// <inheritdoc />
    public partial class rpdInfoUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TestProp",
                table: "RpdInfo",
                newName: "RequaredOrNotRequiared");

            migrationBuilder.AddColumn<string>(
                name: "CharacteristicsOfTheSubjectArea",
                table: "RpdInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LearningGoals",
                table: "RpdInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Zach",
                table: "CriticalInfo",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CharacteristicsOfTheSubjectArea",
                table: "RpdInfo");

            migrationBuilder.DropColumn(
                name: "LearningGoals",
                table: "RpdInfo");

            migrationBuilder.DropColumn(
                name: "Zach",
                table: "CriticalInfo");

            migrationBuilder.RenameColumn(
                name: "RequaredOrNotRequiared",
                table: "RpdInfo",
                newName: "TestProp");
        }
    }
}
