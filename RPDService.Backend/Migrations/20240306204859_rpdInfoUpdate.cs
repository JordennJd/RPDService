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
            migrationBuilder.CreateTable(
                name: "CriticalInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Faculty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zach = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecialtyNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SPZ = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfDepartament = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeOfCourseProject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountOfHourLecture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountOfHourPractice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountOfHourLab = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountOfHourCourseProject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountOfHourCourseWork = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExamHours = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SRS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeOfControl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CriticalInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flags",
                columns: table => new
                {
                    isExcelDataInstalled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "RpdInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharacteristicsOfTheSubjectArea = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LearningGoals = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequaredOrNotRequiared = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DirPosAcadDegree = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Initials = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatorInitials = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatorDegree = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeadDegree = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeadInititials = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RespDegree = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RespInitials = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ViceDegree = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ViceInitials = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Program = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZachHours = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TheNameOfTheOrientation = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    CriticalInfoId = table.Column<int>(type: "int", nullable: false),
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
                name: "Flags");

            migrationBuilder.DropTable(
                name: "RPDs");

            migrationBuilder.DropTable(
                name: "CriticalInfo");

            migrationBuilder.DropTable(
                name: "RpdInfo");
        }
    }
}
