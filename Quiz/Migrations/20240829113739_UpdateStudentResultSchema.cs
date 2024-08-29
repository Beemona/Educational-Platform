using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quiz.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStudentResultSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionResults_StudentResults_StudentResultId",
                table: "QuestionResults");

            migrationBuilder.AlterColumn<int>(
                name: "StudentResultId",
                table: "QuestionResults",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionResults_StudentResults_StudentResultId",
                table: "QuestionResults",
                column: "StudentResultId",
                principalTable: "StudentResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionResults_StudentResults_StudentResultId",
                table: "QuestionResults");

            migrationBuilder.AlterColumn<int>(
                name: "StudentResultId",
                table: "QuestionResults",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionResults_StudentResults_StudentResultId",
                table: "QuestionResults",
                column: "StudentResultId",
                principalTable: "StudentResults",
                principalColumn: "Id");
        }
    }
}
