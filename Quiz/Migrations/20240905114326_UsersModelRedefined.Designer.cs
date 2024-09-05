﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuizDbContext.Data;

#nullable disable

namespace Quiz.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240905114326_UsersModelRedefined")]
    partial class UsersModelRedefined
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Authentication.Models.Department", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("Authentication.Models.EducationType", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EducationTypes");
                });

            modelBuilder.Entity("Authentication.Models.Faculty", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("Authentication.Models.Specialization", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<int?>("EducationTypeId")
                        .HasColumnType("int");

                    b.Property<int?>("FacultyId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("EducationTypeId");

                    b.HasIndex("FacultyId");

                    b.ToTable("Specializations");
                });

            modelBuilder.Entity("Authentication.Models.User", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator().HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("EducationTypeTeacher", b =>
                {
                    b.Property<int>("EducationTypesId")
                        .HasColumnType("int");

                    b.Property<int>("TeachersId")
                        .HasColumnType("int");

                    b.HasKey("EducationTypesId", "TeachersId");

                    b.HasIndex("TeachersId");

                    b.ToTable("EducationTypeTeacher");
                });

            modelBuilder.Entity("Lesson.Models.ClassCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("NumberOfLessonsPosted")
                        .HasColumnType("int");

                    b.Property<string>("Professor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WeeksPassed")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ClassCard");
                });

            modelBuilder.Entity("Lesson.Models.FinalExamCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ExamName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfQuestions")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.Property<int>("TotalPoints")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SubjectId");

                    b.ToTable("FinalExamCards");
                });

            modelBuilder.Entity("Lesson.Models.LessonCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CourseCardId")
                        .HasColumnType("int");

                    b.Property<string>("DateRange")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LessonName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LessonTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SeminarCardId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WeekNumber")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseCardId");

                    b.HasIndex("SeminarCardId");

                    b.HasIndex("SubjectId");

                    b.ToTable("LessonCards");
                });

            modelBuilder.Entity("Lesson.Models.LessonPreview", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("LessonText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LessonTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfessorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SubjectId");

                    b.ToTable("LessonPreviews");
                });

            modelBuilder.Entity("Lesson.Models.LessonProgress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsFinished")
                        .HasColumnType("bit");

                    b.Property<int>("LessonId")
                        .HasColumnType("int");

                    b.Property<int?>("LessonPreviewId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LessonPreviewId");

                    b.ToTable("LessonProgresses");
                });

            modelBuilder.Entity("Lesson.Models.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BackgroundColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CourseCardId")
                        .HasColumnType("int");

                    b.Property<string>("CourseProfessor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FacultyId")
                        .HasColumnType("int");

                    b.Property<int?>("FinalExamCardId")
                        .HasColumnType("int");

                    b.Property<string>("IconUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SeminarCardId")
                        .HasColumnType("int");

                    b.Property<string>("SeminarProfessor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CourseCardId");

                    b.HasIndex("FacultyId");

                    b.HasIndex("FinalExamCardId");

                    b.HasIndex("SeminarCardId");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("OptionModel.Models.Option", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("bit");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Options");
                });

            modelBuilder.Entity("QuestionModel.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<decimal>("Points")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("QuestionModel.Models.QuestionResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CorrectAnswer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Points")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<string>("QuestionText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SelectedAnswer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StudentResultId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentResultId");

                    b.ToTable("QuestionResults");
                });

            modelBuilder.Entity("SpecializationSubjects", b =>
                {
                    b.Property<int>("SpecializationId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.HasKey("SpecializationId", "SubjectId");

                    b.HasIndex("SubjectId");

                    b.ToTable("SpecializationSubjects");
                });

            modelBuilder.Entity("StudentModel.Models.StudentResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Score")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<string>("StudentName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TotalQuestions")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("StudentResults");
                });

            modelBuilder.Entity("StudentSubjects", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.HasKey("StudentId", "SubjectId");

                    b.HasIndex("SubjectId");

                    b.ToTable("StudentSubjects");
                });

            modelBuilder.Entity("TeacherFaculty", b =>
                {
                    b.Property<int>("FacultyId")
                        .HasColumnType("int");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("FacultyId", "TeacherId");

                    b.HasIndex("TeacherId");

                    b.ToTable("TeacherFaculty");
                });

            modelBuilder.Entity("TeacherSpecializations", b =>
                {
                    b.Property<int>("SpecializationId")
                        .HasColumnType("int");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("SpecializationId", "TeacherId");

                    b.HasIndex("TeacherId");

                    b.ToTable("TeacherSpecializations");
                });

            modelBuilder.Entity("TeacherSubjects", b =>
                {
                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("SubjectId", "TeacherId");

                    b.HasIndex("TeacherId");

                    b.ToTable("TeacherSubjects");
                });

            modelBuilder.Entity("Authentication.Models.Student", b =>
                {
                    b.HasBaseType("Authentication.Models.User");

                    b.Property<int?>("EducationTypeId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("FacultyId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("SpecializationId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasIndex("EducationTypeId");

                    b.HasIndex("FacultyId");

                    b.HasIndex("SpecializationId");

                    b.HasDiscriminator().HasValue("Student");
                });

            modelBuilder.Entity("Authentication.Models.Teacher", b =>
                {
                    b.HasBaseType("Authentication.Models.User");

                    b.Property<int?>("DepartmentId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasIndex("DepartmentId");

                    b.HasDiscriminator().HasValue("Teacher");
                });

            modelBuilder.Entity("Authentication.Models.Specialization", b =>
                {
                    b.HasOne("Authentication.Models.Department", "Department")
                        .WithMany("Specializations")
                        .HasForeignKey("DepartmentId");

                    b.HasOne("Authentication.Models.EducationType", "EducationType")
                        .WithMany("Specializations")
                        .HasForeignKey("EducationTypeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Authentication.Models.Faculty", "Faculty")
                        .WithMany("Specializations")
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Department");

                    b.Navigation("EducationType");

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("EducationTypeTeacher", b =>
                {
                    b.HasOne("Authentication.Models.EducationType", null)
                        .WithMany()
                        .HasForeignKey("EducationTypesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Authentication.Models.Teacher", null)
                        .WithMany()
                        .HasForeignKey("TeachersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Lesson.Models.FinalExamCard", b =>
                {
                    b.HasOne("Lesson.Models.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("Lesson.Models.LessonCard", b =>
                {
                    b.HasOne("Lesson.Models.ClassCard", null)
                        .WithMany()
                        .HasForeignKey("CourseCardId");

                    b.HasOne("Lesson.Models.ClassCard", null)
                        .WithMany()
                        .HasForeignKey("SeminarCardId");

                    b.HasOne("Lesson.Models.Subject", "Subject")
                        .WithMany("Lessons")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("Lesson.Models.LessonPreview", b =>
                {
                    b.HasOne("Lesson.Models.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("Lesson.Models.LessonProgress", b =>
                {
                    b.HasOne("Lesson.Models.LessonPreview", "LessonPreview")
                        .WithMany()
                        .HasForeignKey("LessonPreviewId");

                    b.Navigation("LessonPreview");
                });

            modelBuilder.Entity("Lesson.Models.Subject", b =>
                {
                    b.HasOne("Lesson.Models.ClassCard", "CourseCard")
                        .WithMany()
                        .HasForeignKey("CourseCardId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Authentication.Models.Faculty", "Faculty")
                        .WithMany()
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Lesson.Models.FinalExamCard", "FinalExam")
                        .WithMany()
                        .HasForeignKey("FinalExamCardId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Lesson.Models.ClassCard", "SeminarCard")
                        .WithMany()
                        .HasForeignKey("SeminarCardId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("CourseCard");

                    b.Navigation("Faculty");

                    b.Navigation("FinalExam");

                    b.Navigation("SeminarCard");
                });

            modelBuilder.Entity("OptionModel.Models.Option", b =>
                {
                    b.HasOne("QuestionModel.Models.Question", "Question")
                        .WithMany("Options")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("QuestionModel.Models.QuestionResult", b =>
                {
                    b.HasOne("StudentModel.Models.StudentResult", "StudentResult")
                        .WithMany("ResultDetails")
                        .HasForeignKey("StudentResultId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StudentResult");
                });

            modelBuilder.Entity("SpecializationSubjects", b =>
                {
                    b.HasOne("Authentication.Models.Specialization", null)
                        .WithMany()
                        .HasForeignKey("SpecializationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lesson.Models.Subject", null)
                        .WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StudentSubjects", b =>
                {
                    b.HasOne("Authentication.Models.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lesson.Models.Subject", null)
                        .WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TeacherFaculty", b =>
                {
                    b.HasOne("Authentication.Models.Faculty", null)
                        .WithMany()
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Authentication.Models.Teacher", null)
                        .WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("TeacherSpecializations", b =>
                {
                    b.HasOne("Authentication.Models.Specialization", null)
                        .WithMany()
                        .HasForeignKey("SpecializationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Authentication.Models.Teacher", null)
                        .WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TeacherSubjects", b =>
                {
                    b.HasOne("Lesson.Models.Subject", null)
                        .WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Authentication.Models.Teacher", null)
                        .WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Authentication.Models.Student", b =>
                {
                    b.HasOne("Authentication.Models.EducationType", "EducationType")
                        .WithMany("Students")
                        .HasForeignKey("EducationTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Authentication.Models.Faculty", "Faculty")
                        .WithMany("Students")
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Authentication.Models.Specialization", "Specialization")
                        .WithMany("Students")
                        .HasForeignKey("SpecializationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("EducationType");

                    b.Navigation("Faculty");

                    b.Navigation("Specialization");
                });

            modelBuilder.Entity("Authentication.Models.Teacher", b =>
                {
                    b.HasOne("Authentication.Models.Department", "Department")
                        .WithMany("Teachers")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("Authentication.Models.Department", b =>
                {
                    b.Navigation("Specializations");

                    b.Navigation("Teachers");
                });

            modelBuilder.Entity("Authentication.Models.EducationType", b =>
                {
                    b.Navigation("Specializations");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("Authentication.Models.Faculty", b =>
                {
                    b.Navigation("Specializations");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("Authentication.Models.Specialization", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("Lesson.Models.Subject", b =>
                {
                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("QuestionModel.Models.Question", b =>
                {
                    b.Navigation("Options");
                });

            modelBuilder.Entity("StudentModel.Models.StudentResult", b =>
                {
                    b.Navigation("ResultDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
