using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPDSerice.Migrations
{
    /// <inheritdoc />
    public partial class update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RPDs_RpdInfo_RpdInfoId",
                table: "RPDs");

            migrationBuilder.DropColumn(
                name: "RpdlInfoId",
                table: "RPDs");

            migrationBuilder.AlterColumn<int>(
                name: "RpdInfoId",
                table: "RPDs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RPDs_RpdInfo_RpdInfoId",
                table: "RPDs",
                column: "RpdInfoId",
                principalTable: "RpdInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RPDs_RpdInfo_RpdInfoId",
                table: "RPDs");

            migrationBuilder.AlterColumn<int>(
                name: "RpdInfoId",
                table: "RPDs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "RpdlInfoId",
                table: "RPDs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_RPDs_RpdInfo_RpdInfoId",
                table: "RPDs",
                column: "RpdInfoId",
                principalTable: "RpdInfo",
                principalColumn: "Id");
        }
    }
}
