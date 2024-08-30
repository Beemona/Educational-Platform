using Microsoft.EntityFrameworkCore;
using QuestionModel.Models;
using OptionModel.Models;
using StudentModel.Models;
using Lesson.Models;

namespace QuizDbContext.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<StudentResult> StudentResults { get; set; }
        public DbSet<QuestionResult> QuestionResults { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<ClassCard> ClassCards { get; set; }
        public DbSet<LessonCard> LessonCards { get; set; }
        public DbSet<FinalExamCard> FinalExamCards { get; set; }
        public DbSet<LessonPreview> LessonPreviews { get; set; }
        public DbSet<LessonProgress> LessonProgresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Question and Option Relationship
            modelBuilder.Entity<Question>()
                .HasMany(q => q.Options)
                .WithOne(o => o.Question)
                .HasForeignKey(o => o.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            // StudentResult and QuestionResult Relationship
            modelBuilder.Entity<StudentResult>()
                .HasMany(sr => sr.ResultDetails)
                .WithOne(qr => qr.StudentResult)
                .HasForeignKey(qr => qr.StudentResultId)
                .OnDelete(DeleteBehavior.Cascade);

            // Subject and ClassCard Relationship (Course)
            modelBuilder.Entity<Subject>()
                 .HasOne(s => s.CourseCard)
                 .WithMany()
                 .HasForeignKey(s => s.CourseCardId)
                 .OnDelete(DeleteBehavior.Restrict);


            // Subject and ClassCard Relationship (Seminar)
            modelBuilder.Entity<Subject>()
                .HasOne(s => s.SeminarCard)
                .WithMany()
                .HasForeignKey(s => s.SeminarCardId)
                .OnDelete(DeleteBehavior.Restrict);

            // Subject and FinalExamCard Relationship
            modelBuilder.Entity<Subject>()
                .HasOne(s => s.FinalExam)
                .WithMany()
                .HasForeignKey(s => s.FinalExamCardId)
                .OnDelete(DeleteBehavior.Restrict); 

            // Subject and LessonCard Relationship
            modelBuilder.Entity<Subject>()
                .HasMany(s => s.Lessons)
                .WithOne(lc => lc.Subject)
                .HasForeignKey(lc => lc.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // FinalExamCard and Subject Relationship
            modelBuilder.Entity<FinalExamCard>()
                .HasOne(fe => fe.Subject)
                .WithOne()
                .HasForeignKey<FinalExamCard>(fe => fe.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);

            // LessonPreview and LessonProgress Relationship
            modelBuilder.Entity<Subject>()
                .HasMany(s => s.Lessons)
                .WithOne(lc => lc.Subject)
                .HasForeignKey(lc => lc.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // Primary Key Configuration
            modelBuilder.Entity<Question>()
                .HasKey(q => q.Id);

            modelBuilder.Entity<Option>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<StudentResult>()
                .HasKey(sr => sr.Id);

            modelBuilder.Entity<QuestionResult>()
                .HasKey(qr => qr.Id);

            modelBuilder.Entity<Subject>()
                .HasKey(s=> s.Id);

            modelBuilder.Entity<ClassCard>()
                .HasKey(cc => cc.Id);

            modelBuilder.Entity<LessonCard>()
                .HasKey(lc => lc.Id);

            modelBuilder.Entity<LessonPreview>()
                .HasKey(lp => lp.Id);

            modelBuilder.Entity<FinalExamCard>()
                .HasKey(fe => fe.Id);

            modelBuilder.Entity<LessonProgress>()
                .HasKey(lp => lp.Id);

            // Decimal and Bit Type Configuration
            modelBuilder.Entity<Question>()
                .Property(q => q.Points)
                .HasColumnType("decimal(5, 2)");

            modelBuilder.Entity<Option>()
                .Property(o => o.IsCorrect)
                .HasColumnType("bit");

            modelBuilder.Entity<QuestionResult>()
                .Property(qr => qr.Points)
                .HasColumnType("decimal(5, 2)");

            modelBuilder.Entity<StudentResult>()
                .Property(sr => sr.Score)
                .HasColumnType("decimal(5, 2)");

            // Identity Columns Configuration
            modelBuilder.Entity<Question>()
                .Property(q => q.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Option>()
                .Property(o => o.Id)
                .ValueGeneratedOnAdd();

            //modelBuilder.Entity<StudentResult>()
            //    .Property(sr => sr.Id)
            //    .ValueGeneratedOnAdd();

            //modelBuilder.Entity<QuestionResult>()
            //    .Property(qr => qr.Id)
            //    .ValueGeneratedOnAdd();

            modelBuilder.Entity<Subject>()
                .Property(s => s.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<ClassCard>()
                .Property(cc => cc.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<LessonCard>()
                .Property(lc => lc.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<LessonPreview>()
                .Property(lp => lp.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<FinalExamCard>()
                .Property(fe => fe.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<LessonProgress>()
               .Property(lp => lp.Id)
               .ValueGeneratedOnAdd();
        }
    }
}
