using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quiz.Migrations
{
    /// <inheritdoc />
    public partial class CourseandSeminarCards : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_ClassCards_CourseCardId",
                table: "Subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_ClassCards_SeminarCardId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_FinalExamCards_SubjectId",
                table: "FinalExamCards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassCards",
                table: "ClassCards");

            migrationBuilder.RenameTable(
                name: "ClassCards",
                newName: "ClassCard");

            migrationBuilder.AddColumn<int>(
                name: "CourseCardId",
                table: "LessonCards",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SeminarCardId",
                table: "LessonCards",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassCard",
                table: "ClassCard",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_LessonCards_CourseCardId",
                table: "LessonCards",
                column: "CourseCardId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonCards_SeminarCardId",
                table: "LessonCards",
                column: "SeminarCardId");

            migrationBuilder.CreateIndex(
                name: "IX_FinalExamCards_SubjectId",
                table: "FinalExamCards",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonCards_ClassCard_CourseCardId",
                table: "LessonCards",
                column: "CourseCardId",
                principalTable: "ClassCard",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonCards_ClassCard_SeminarCardId",
                table: "LessonCards",
                column: "SeminarCardId",
                principalTable: "ClassCard",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_ClassCard_CourseCardId",
                table: "Subjects",
                column: "CourseCardId",
                principalTable: "ClassCard",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_ClassCard_SeminarCardId",
                table: "Subjects",
                column: "SeminarCardId",
                principalTable: "ClassCard",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonCards_ClassCard_CourseCardId",
                table: "LessonCards");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonCards_ClassCard_SeminarCardId",
                table: "LessonCards");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_ClassCard_CourseCardId",
                table: "Subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_ClassCard_SeminarCardId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_LessonCards_CourseCardId",
                table: "LessonCards");

            migrationBuilder.DropIndex(
                name: "IX_LessonCards_SeminarCardId",
                table: "LessonCards");

            migrationBuilder.DropIndex(
                name: "IX_FinalExamCards_SubjectId",
                table: "FinalExamCards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassCard",
                table: "ClassCard");

            migrationBuilder.DropColumn(
                name: "CourseCardId",
                table: "LessonCards");

            migrationBuilder.DropColumn(
                name: "SeminarCardId",
                table: "LessonCards");

            migrationBuilder.RenameTable(
                name: "ClassCard",
                newName: "ClassCards");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassCards",
                table: "ClassCards",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FinalExamCards_SubjectId",
                table: "FinalExamCards",
                column: "SubjectId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_ClassCards_CourseCardId",
                table: "Subjects",
                column: "CourseCardId",
                principalTable: "ClassCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_ClassCards_SeminarCardId",
                table: "Subjects",
                column: "SeminarCardId",
                principalTable: "ClassCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
