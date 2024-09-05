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
    [Migration("20240904181822_UsersMigr")]
    partial class UsersMigr
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

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

            modelBuilder.Entity("Authentication.Models.Subject", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Subject");
                });

            modelBuilder.Entity("Authentication.Models.User", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<string>("EducationType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FacultyId")
                        .HasColumnType("int");

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

                    b.HasIndex("FacultyId");

                    b.ToTable("Users");
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

            modelBuilder.Entity("SubjectUser", b =>
                {
                    b.Property<int>("SubjectsId")
                        .HasColumnType("int");

                    b.Property<int>("TeachersId")
                        .HasColumnType("int");

                    b.HasKey("SubjectsId", "TeachersId");

                    b.HasIndex("TeachersId");

                    b.ToTable("SubjectUser");
                });

            modelBuilder.Entity("Authentication.Models.User", b =>
                {
                    b.HasOne("Authentication.Models.Faculty", "Faculty")
                        .WithMany("Users")
                        .HasForeignKey("FacultyId");

                    b.Navigation("Faculty");
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

                    b.HasOne("Lesson.Models.FinalExamCard", "FinalExam")
                        .WithMany()
                        .HasForeignKey("FinalExamCardId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Lesson.Models.ClassCard", "SeminarCard")
                        .WithMany()
                        .HasForeignKey("SeminarCardId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("CourseCard");

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

            modelBuilder.Entity("SubjectUser", b =>
                {
                    b.HasOne("Authentication.Models.Subject", null)
                        .WithMany()
                        .HasForeignKey("SubjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Authentication.Models.User", null)
                        .WithMany()
                        .HasForeignKey("TeachersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Authentication.Models.Faculty", b =>
                {
                    b.Navigation("Users");
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
