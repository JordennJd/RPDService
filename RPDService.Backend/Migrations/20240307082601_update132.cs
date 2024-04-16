using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPDSerice.Migrations
{
    /// <inheritdoc />
    public partial class update132 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatorDegree",
                table: "RpdInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatorInitials",
                table: "RpdInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DirPosAcadDegree",
                table: "RpdInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HeadDegree",
                table: "RpdInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HeadInitials",
                table: "RpdInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Initials",
                table: "RpdInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameOfTheFieldOfStudy",
                table: "RpdInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Program",
                table: "RpdInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RespDegree",
                table: "RpdInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RespInitials",
                table: "RpdInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TheNameOfTheOrientation",
                table: "RpdInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ViceDegree",
                table: "RpdInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ViceInitials",
                table: "RpdInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ZachHours",
                table: "RpdInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorDegree",
                table: "RpdInfo");

            migrationBuilder.DropColumn(
                name: "CreatorInitials",
                table: "RpdInfo");

            migrationBuilder.DropColumn(
                name: "DirPosAcadDegree",
                table: "RpdInfo");

            migrationBuilder.DropColumn(
                name: "HeadDegree",
                table: "RpdInfo");

            migrationBuilder.DropColumn(
                name: "HeadInitials",
                table: "RpdInfo");

            migrationBuilder.DropColumn(
                name: "Initials",
                table: "RpdInfo");

            migrationBuilder.DropColumn(
                name: "NameOfTheFieldOfStudy",
                table: "RpdInfo");

            migrationBuilder.DropColumn(
                name: "Program",
                table: "RpdInfo");

            migrationBuilder.DropColumn(
                name: "RespDegree",
                table: "RpdInfo");

            migrationBuilder.DropColumn(
                name: "RespInitials",
                table: "RpdInfo");

            migrationBuilder.DropColumn(
                name: "TheNameOfTheOrientation",
                table: "RpdInfo");

            migrationBuilder.DropColumn(
                name: "ViceDegree",
                table: "RpdInfo");

            migrationBuilder.DropColumn(
                name: "ViceInitials",
                table: "RpdInfo");

            migrationBuilder.DropColumn(
                name: "ZachHours",
                table: "RpdInfo");
        }
    }
}
