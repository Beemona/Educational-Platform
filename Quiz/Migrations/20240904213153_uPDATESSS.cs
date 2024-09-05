using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quiz.Migrations
{
    /// <inheritdoc />
    public partial class uPDATESSS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Specializations_Faculties_FacultyId",
                table: "Specializations");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "FacultyId",
                table: "Subjects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SpecializationSubjects",
                columns: table => new
                {
                    SpecializationId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecializationSubjects", x => new { x.SpecializationId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_SpecializationSubjects_Specializations_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "Specializations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpecializationSubjects_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_FacultyId",
                table: "Subjects",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecializationSubjects_SubjectId",
                table: "SpecializationSubjects",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Specializations_Faculties_FacultyId",
                table: "Specializations",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Faculties_FacultyId",
                table: "Subjects",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Specializations_Faculties_FacultyId",
                table: "Specializations");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Faculties_FacultyId",
                table: "Subjects");

            migrationBuilder.DropTable(
                name: "SpecializationSubjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_FacultyId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FacultyId",
                table: "Subjects");

            migrationBuilder.AddForeignKey(
                name: "FK_Specializations_Faculties_FacultyId",
                table: "Specializations",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
