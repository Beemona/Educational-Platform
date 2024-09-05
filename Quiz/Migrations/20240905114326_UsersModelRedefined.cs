using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quiz.Migrations
{
    /// <inheritdoc />
    public partial class UsersModelRedefined : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_Users_UserId",
                table: "StudentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSubjects_Users_UserId",
                table: "TeacherSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Faculties_FacultyId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentSubjects",
                table: "StudentSubjects");

            migrationBuilder.DropIndex(
                name: "IX_StudentSubjects_UserId",
                table: "StudentSubjects");

            migrationBuilder.DropColumn(
                name: "EducationType",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EducationType",
                table: "Specializations");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "TeacherSubjects",
                newName: "TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherSubjects_UserId",
                table: "TeacherSubjects",
                newName: "IX_TeacherSubjects_TeacherId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "StudentSubjects",
                newName: "StudentId");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Users",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EducationTypeId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Specializations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EducationTypeId",
                table: "Specializations",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentSubjects",
                table: "StudentSubjects",
                columns: new[] { "StudentId", "SubjectId" });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeacherFaculty",
                columns: table => new
                {
                    FacultyId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherFaculty", x => new { x.FacultyId, x.TeacherId });
                    table.ForeignKey(
                        name: "FK_TeacherFaculty_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeacherFaculty_Users_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeacherSpecializations",
                columns: table => new
                {
                    SpecializationId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherSpecializations", x => new { x.SpecializationId, x.TeacherId });
                    table.ForeignKey(
                        name: "FK_TeacherSpecializations_Specializations_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "Specializations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherSpecializations_Users_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EducationTypeTeacher",
                columns: table => new
                {
                    EducationTypesId = table.Column<int>(type: "int", nullable: false),
                    TeachersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationTypeTeacher", x => new { x.EducationTypesId, x.TeachersId });
                    table.ForeignKey(
                        name: "FK_EducationTypeTeacher_EducationTypes_EducationTypesId",
                        column: x => x.EducationTypesId,
                        principalTable: "EducationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EducationTypeTeacher_Users_TeachersId",
                        column: x => x.TeachersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartmentId",
                table: "Users",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_EducationTypeId",
                table: "Users",
                column: "EducationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjects_SubjectId",
                table: "StudentSubjects",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Specializations_DepartmentId",
                table: "Specializations",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Specializations_EducationTypeId",
                table: "Specializations",
                column: "EducationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationTypeTeacher_TeachersId",
                table: "EducationTypeTeacher",
                column: "TeachersId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherFaculty_TeacherId",
                table: "TeacherFaculty",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSpecializations_TeacherId",
                table: "TeacherSpecializations",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Specializations_Department_DepartmentId",
                table: "Specializations",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Specializations_EducationTypes_EducationTypeId",
                table: "Specializations",
                column: "EducationTypeId",
                principalTable: "EducationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_Users_StudentId",
                table: "StudentSubjects",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSubjects_Users_TeacherId",
                table: "TeacherSubjects",
                column: "TeacherId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Department_DepartmentId",
                table: "Users",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_EducationTypes_EducationTypeId",
                table: "Users",
                column: "EducationTypeId",
                principalTable: "EducationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Faculties_FacultyId",
                table: "Users",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Specializations_Department_DepartmentId",
                table: "Specializations");

            migrationBuilder.DropForeignKey(
                name: "FK_Specializations_EducationTypes_EducationTypeId",
                table: "Specializations");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_Users_StudentId",
                table: "StudentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSubjects_Users_TeacherId",
                table: "TeacherSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Department_DepartmentId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_EducationTypes_EducationTypeId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Faculties_FacultyId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "EducationTypeTeacher");

            migrationBuilder.DropTable(
                name: "TeacherFaculty");

            migrationBuilder.DropTable(
                name: "TeacherSpecializations");

            migrationBuilder.DropTable(
                name: "EducationTypes");

            migrationBuilder.DropIndex(
                name: "IX_Users_DepartmentId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_EducationTypeId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentSubjects",
                table: "StudentSubjects");

            migrationBuilder.DropIndex(
                name: "IX_StudentSubjects_SubjectId",
                table: "StudentSubjects");

            migrationBuilder.DropIndex(
                name: "IX_Specializations_DepartmentId",
                table: "Specializations");

            migrationBuilder.DropIndex(
                name: "IX_Specializations_EducationTypeId",
                table: "Specializations");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EducationTypeId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Specializations");

            migrationBuilder.DropColumn(
                name: "EducationTypeId",
                table: "Specializations");

            migrationBuilder.RenameColumn(
                name: "TeacherId",
                table: "TeacherSubjects",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherSubjects_TeacherId",
                table: "TeacherSubjects",
                newName: "IX_TeacherSubjects_UserId");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "StudentSubjects",
                newName: "UserId");

            migrationBuilder.AddColumn<string>(
                name: "EducationType",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EducationType",
                table: "Specializations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentSubjects",
                table: "StudentSubjects",
                columns: new[] { "SubjectId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjects_UserId",
                table: "StudentSubjects",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_Users_UserId",
                table: "StudentSubjects",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSubjects_Users_UserId",
                table: "TeacherSubjects",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Faculties_FacultyId",
                table: "Users",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id");
        }
    }
}
