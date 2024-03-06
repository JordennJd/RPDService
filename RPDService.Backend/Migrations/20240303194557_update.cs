using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPDSerice.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddForeignKey(
                name: "FK_RPDs_RpdInfo_RpdInfoId",
                table: "RPDs",
                column: "RpdInfoId",
                principalTable: "RpdInfo",
                principalColumn: "Id");
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
    }
}
