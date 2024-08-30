using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quiz.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassCards_Subjects_SubjectId",
                table: "ClassCards");

            migrationBuilder.DropIndex(
                name: "IX_ClassCards_SubjectId",
                table: "ClassCards");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "ClassCards");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "ClassCards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ClassCards_SubjectId",
                table: "ClassCards",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassCards_Subjects_SubjectId",
                table: "ClassCards",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
