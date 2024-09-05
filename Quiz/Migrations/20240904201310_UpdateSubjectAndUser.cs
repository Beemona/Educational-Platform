using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quiz.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSubjectAndUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Users_SubjectId",
                table: "Subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Users_TaughtSubjectId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_SubjectId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_TaughtSubjectId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TaughtSubjectId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "TaughtSubjectId",
                table: "Subjects");

            migrationBuilder.CreateTable(
                name: "StudentSubjects",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSubjects", x => new { x.SubjectId, x.UserId });
                    table.ForeignKey(
                        name: "FK_StudentSubjects_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentSubjects_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherSubjects",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherSubjects", x => new { x.SubjectId, x.UserId });
                    table.ForeignKey(
                        name: "FK_TeacherSubjects_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherSubjects_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjects_UserId",
                table: "StudentSubjects",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSubjects_UserId",
                table: "TeacherSubjects",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentSubjects");

            migrationBuilder.DropTable(
                name: "TeacherSubjects");

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TaughtSubjectId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "Subjects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TaughtSubjectId",
                table: "Subjects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_SubjectId",
                table: "Subjects",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_TaughtSubjectId",
                table: "Subjects",
                column: "TaughtSubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Users_SubjectId",
                table: "Subjects",
                column: "SubjectId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Users_TaughtSubjectId",
                table: "Subjects",
                column: "TaughtSubjectId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
