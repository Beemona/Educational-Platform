using Microsoft.EntityFrameworkCore;
using QuestionModel.Models;
using OptionModel.Models;
using StudentModel.Models;
using Lesson.Models;
using Authentication.Models;

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
        public DbSet<ClassCard> CourseCards { get; set; }
        public DbSet<ClassCard> SeminarCards { get; set; }
        public DbSet<FinalExamCard> FinalExamCards { get; set; }
        public DbSet<LessonPreview> LessonPreviews { get; set; }
        public DbSet<LessonProgress> LessonProgresses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<EducationType> EducationTypes { get; set; }
        public DbSet<Department> Departments { get; set; }

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
            modelBuilder.Entity<Lesson.Models.Subject>()
                 .HasOne(s => s.CourseCard)
                 .WithMany()
                 .HasForeignKey(s => s.CourseCardId)
                 .OnDelete(DeleteBehavior.Restrict);


            // Subject and ClassCard Relationship (Seminar)
            modelBuilder.Entity<Lesson.Models.Subject>()
                .HasOne(s => s.SeminarCard)
                .WithMany()
                .HasForeignKey(s => s.SeminarCardId)
                .OnDelete(DeleteBehavior.Restrict);

            // Subject and FinalExamCard Relationship
            modelBuilder.Entity<Lesson.Models.Subject>()
                .HasOne(s => s.FinalExam)
                .WithMany()
                .HasForeignKey(s => s.FinalExamCardId)
                .OnDelete(DeleteBehavior.Restrict); 

            // Subject and LessonCard Relationship
            modelBuilder.Entity<Lesson.Models.Subject>()
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
            modelBuilder.Entity<Lesson.Models.Subject>()
                .HasMany(s => s.Lessons)
                .WithOne(lc => lc.Subject)
                .HasForeignKey(lc => lc.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // Specialization and Faculty
            modelBuilder.Entity<Specialization>()
                .HasOne(s => s.Faculty)
                .WithMany(f => f.Specializations)
                .HasForeignKey(s => s.FacultyId)
                .OnDelete(DeleteBehavior.Restrict);

            // Specialization and EducationType
            modelBuilder.Entity<Specialization>()
               .HasOne(s => s.EducationType)
               .WithMany(e => e.Specializations)
               .HasForeignKey(s => s.EducationTypeId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<Student>("Student")
                .HasValue<Teacher>("Teacher")
                .HasValue<Admin>("Admin");


            // SpecializationSubjects
            modelBuilder.Entity<Specialization>()
            .HasMany(s => s.Subjects)
            .WithMany(sub => sub.Specializations)
            .UsingEntity<Dictionary<string, object>>(
                "SpecializationSubjects",
                j => j.HasOne<Subject>().WithMany().HasForeignKey("SubjectId"),
                j => j.HasOne<Specialization>().WithMany().HasForeignKey("SpecializationId")
            );

            // Specialization and Student
            modelBuilder.Entity<Student>()
               .HasOne(s => s.Specialization)
               .WithMany(s => s.Students)
               .HasForeignKey(s => s.SpecializationId)
               .OnDelete(DeleteBehavior.Restrict);

            // Configuring the relationship between Student and EducationType
            modelBuilder.Entity<Student>()
                .HasOne(s => s.EducationType)
                .WithMany(e => e.Students)
                .HasForeignKey(s => s.EducationTypeId)
                .OnDelete(DeleteBehavior.Restrict); // Change to Restrict to avoid cascade issues

            // Configuring the relationship between Student and Faculty
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Faculty)
                .WithMany(f => f.Students)
                .HasForeignKey(s => s.FacultyId)
                .OnDelete(DeleteBehavior.Restrict); // Change to Restrict to avoid cascade issues

            // Teacher and Department
            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.Department)
                .WithMany(d => d.Teachers)
                .HasForeignKey(t => t.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);


            // Student and Subjects relationship
            modelBuilder.Entity<Student>()
                .HasMany(s => s.AccessibleSubjects)
                .WithMany(sub => sub.Students)
                .UsingEntity<Dictionary<string, object>>(
                    "StudentSubjects",
                    j => j.HasOne<Subject>().WithMany().HasForeignKey("SubjectId"),
                    j => j.HasOne<Student>().WithMany().HasForeignKey("StudentId")
            );

            modelBuilder.Entity<Subject>()
                .HasOne(s => s.Faculty)
                .WithMany()
                .HasForeignKey(s => s.FacultyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Specialization>()
                .HasOne(s => s.Faculty)
                .WithMany(f => f.Specializations)
                .HasForeignKey(s => s.FacultyId);

            modelBuilder.Entity<Faculty>()
                .HasMany(f => f.Specializations)
                .WithOne(s => s.Faculty)
                .HasForeignKey(s => s.FacultyId)
                .OnDelete(DeleteBehavior.Cascade);

            // Teacher and Specializations
            modelBuilder.Entity<Teacher>()
                .HasMany(t => t.Specializations)
                .WithMany(s => s.Teachers)
                .UsingEntity<Dictionary<string, object>>(
                    "TeacherSpecializations",
                    j => j.HasOne<Specialization>().WithMany().HasForeignKey("SpecializationId"),
                    j => j.HasOne<Teacher>().WithMany().HasForeignKey("TeacherId")
                );

            // Teacher and TaughtSubjects Relationship
            modelBuilder.Entity<Teacher>()
                .HasMany(t => t.TaughtSubjects)
                .WithMany(sub => sub.Teachers)
                .UsingEntity<Dictionary<string, object>>(
                    "TeacherSubjects",
                    j => j.HasOne<Subject>().WithMany().HasForeignKey("SubjectId"),
                    j => j.HasOne<Teacher>().WithMany().HasForeignKey("TeacherId")
                );

            // Configuring the relationship between Teacher and Faculty
            modelBuilder.Entity<Teacher>()
                .HasMany(t => t.Faculties)
                .WithMany(f => f.Teachers)
                .UsingEntity<Dictionary<string, object>>(
                    "TeacherFaculty",
                    j => j
                        .HasOne<Faculty>()
                        .WithMany()
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Restrict),
                    j => j
                        .HasOne<Teacher>()
                        .WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Restrict));

            modelBuilder.Entity<LessonCard>()
                .HasOne(l => l.Subject)
                .WithMany(s => s.Lessons)
                .HasForeignKey(l => l.SubjectId);

            modelBuilder.Entity<LessonCard>()
                .HasOne<ClassCard>()
                .WithMany()
                .HasForeignKey(l => l.CourseCardId);

            modelBuilder.Entity<LessonCard>()
                .HasOne<ClassCard>()
                .WithMany()
                .HasForeignKey(l => l.SeminarCardId);

            modelBuilder.Entity<FinalExamCard>()
                .HasOne(s => s.Subject)
                .WithMany()
                .HasForeignKey(s => s.SubjectId);

            // Primary Key Configuration
            modelBuilder.Entity<Question>()
                .HasKey(q => q.Id);

            modelBuilder.Entity<Option>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<StudentResult>()
                .HasKey(sr => sr.Id);

            modelBuilder.Entity<QuestionResult>()
                .HasKey(qr => qr.Id);

            modelBuilder.Entity<Lesson.Models.Subject>()
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

            modelBuilder.Entity<Specialization>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<EducationType>()
            .HasKey(e => e.Id);

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

            modelBuilder.Entity<Lesson.Models.Subject>()
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
