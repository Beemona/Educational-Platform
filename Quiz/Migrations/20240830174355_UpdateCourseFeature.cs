using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quiz.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCourseFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClassCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Professor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfLessonsPosted = table.Column<int>(type: "int", nullable: false),
                    WeeksPassed = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FinalExamCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfQuestions = table.Column<int>(type: "int", nullable: false),
                    TotalPoints = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinalExamCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseProfessor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeminarProfessor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IconUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BackgroundColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseCardId = table.Column<int>(type: "int", nullable: true),
                    SeminarCardId = table.Column<int>(type: "int", nullable: true),
                    FinalExamCardId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subjects_ClassCards_CourseCardId",
                        column: x => x.CourseCardId,
                        principalTable: "ClassCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Subjects_ClassCards_SeminarCardId",
                        column: x => x.SeminarCardId,
                        principalTable: "ClassCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Subjects_FinalExamCards_FinalExamCardId",
                        column: x => x.FinalExamCardId,
                        principalTable: "FinalExamCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LessonCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LessonName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LessonTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeekNumber = table.Column<int>(type: "int", nullable: false),
                    DateRange = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonCards_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LessonPreviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LessonTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfessorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LessonText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonPreviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonPreviews_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LessonProgresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    IsFinished = table.Column<bool>(type: "bit", nullable: false),
                    LessonPreviewId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonProgresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonProgresses_LessonPreviews_LessonPreviewId",
                        column: x => x.LessonPreviewId,
                        principalTable: "LessonPreviews",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassCards_SubjectId",
                table: "ClassCards",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_FinalExamCards_SubjectId",
                table: "FinalExamCards",
                column: "SubjectId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LessonCards_SubjectId",
                table: "LessonCards",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonPreviews_SubjectId",
                table: "LessonPreviews",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonProgresses_LessonPreviewId",
                table: "LessonProgresses",
                column: "LessonPreviewId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_CourseCardId",
                table: "Subjects",
                column: "CourseCardId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_FinalExamCardId",
                table: "Subjects",
                column: "FinalExamCardId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_SeminarCardId",
                table: "Subjects",
                column: "SeminarCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassCards_Subjects_SubjectId",
                table: "ClassCards",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FinalExamCards_Subjects_SubjectId",
                table: "FinalExamCards",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassCards_Subjects_SubjectId",
                table: "ClassCards");

            migrationBuilder.DropForeignKey(
                name: "FK_FinalExamCards_Subjects_SubjectId",
                table: "FinalExamCards");

            migrationBuilder.DropTable(
                name: "LessonCards");

            migrationBuilder.DropTable(
                name: "LessonProgresses");

            migrationBuilder.DropTable(
                name: "LessonPreviews");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "ClassCards");

            migrationBuilder.DropTable(
                name: "FinalExamCards");
        }
    }
}
