using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quiz.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserModel2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Users_UserId",
                table: "Subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Users_UserId1",
                table: "Subjects");

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "Subjects",
                newName: "TaughtSubjectId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Subjects",
                newName: "SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Subjects_UserId1",
                table: "Subjects",
                newName: "IX_Subjects_TaughtSubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Subjects_UserId",
                table: "Subjects",
                newName: "IX_Subjects_SubjectId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Users_SubjectId",
                table: "Subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Users_TaughtSubjectId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TaughtSubjectId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "TaughtSubjectId",
                table: "Subjects",
                newName: "UserId1");

            migrationBuilder.RenameColumn(
                name: "SubjectId",
                table: "Subjects",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Subjects_TaughtSubjectId",
                table: "Subjects",
                newName: "IX_Subjects_UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Subjects_SubjectId",
                table: "Subjects",
                newName: "IX_Subjects_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Users_UserId",
                table: "Subjects",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Users_UserId1",
                table: "Subjects",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
