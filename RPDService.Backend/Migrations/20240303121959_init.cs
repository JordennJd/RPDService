using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPDSerice.Migrations
{
	/// <inheritdoc />
	public partial class init : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "CriticalInfo",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Faculty = table.Column<string>(type: "nvarchar(max)", nullable: false),
					SpecialtyNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
					SPZ = table.Column<string>(type: "nvarchar(max)", nullable: false),
					FO = table.Column<string>(type: "nvarchar(max)", nullable: false),
					GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
					NumberOfDepartament = table.Column<string>(type: "nvarchar(max)", nullable: false),
					TypeOfCourseProject = table.Column<string>(type: "nvarchar(max)", nullable: false),
					CountOfHourLecture = table.Column<string>(type: "nvarchar(max)", nullable: false),
					CountOfHourPractice = table.Column<string>(type: "nvarchar(max)", nullable: false),
					CountOfHourLab = table.Column<string>(type: "nvarchar(max)", nullable: false),
					CountOfHourCourseProject = table.Column<string>(type: "nvarchar(max)", nullable: false),
					CountOfHourCourseWork = table.Column<string>(type: "nvarchar(max)", nullable: false),
					ExamHours = table.Column<string>(type: "nvarchar(max)", nullable: false),
					SRS = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Test = table.Column<bool>(type: "bit", nullable: false),
					DiffTest = table.Column<bool>(type: "bit", nullable: false),
					KandExam = table.Column<bool>(type: "bit", nullable: false),
					Exam = table.Column<bool>(type: "bit", nullable: false),

				},
				constraints: table =>
				{
					table.PrimaryKey("PK_CriticalInfo", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "RpdInfo",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1")
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_RpdInfo", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "RPDs",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					CriticalInfoId = table.Column<int>(type: "int", nullable: false),
					RpdlInfoId = table.Column<int>(type: "int", nullable: false),
					RpdInfoId = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_RPDs", x => x.Id);
					table.ForeignKey(
						name: "FK_RPDs_CriticalInfo_CriticalInfoId",
						column: x => x.CriticalInfoId,
						principalTable: "CriticalInfo",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_RPDs_RpdInfo_RpdInfoId",
						column: x => x.RpdInfoId,
						principalTable: "RpdInfo",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_RPDs_CriticalInfoId",
				table: "RPDs",
				column: "CriticalInfoId");

			migrationBuilder.CreateIndex(
				name: "IX_RPDs_RpdInfoId",
				table: "RPDs",
				column: "RpdInfoId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "RPDs");

			migrationBuilder.DropTable(
				name: "CriticalInfo");

			migrationBuilder.DropTable(
				name: "RpdInfo");
		}
	}
}
