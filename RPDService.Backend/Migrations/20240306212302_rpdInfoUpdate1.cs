using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPDSerice.Migrations
{
    /// <inheritdoc />
    public partial class rpdInfoUpdate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameOfTheFieldOfStudy",
                table: "RpdInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameOfTheFieldOfStudy",
                table: "RpdInfo");
        }
    }
}
