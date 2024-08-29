using Microsoft.EntityFrameworkCore;
using QuestionModel.Models;
using OptionModel.Models;
using StudentModel.Models;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Question and Option Relationship
            modelBuilder.Entity<Question>()
                .HasMany(q => q.Options)
                .WithOne(o => o.Question)
                .HasForeignKey(o => o.QuestionId);

            // StudentResult and QuestionResult Relationship
            modelBuilder.Entity<StudentResult>()
               .HasMany(sr => sr.ResultDetails)
               .WithOne(qr => qr.StudentResult)
               .HasForeignKey(qr => qr.StudentResultId);

            modelBuilder.Entity<Question>()
                .HasKey(q => q.Id);

            modelBuilder.Entity<Option>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<StudentResult>()
                .HasKey(sr => sr.Id);

            modelBuilder.Entity<QuestionResult>()
                .HasKey(qr => qr.Id);

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

            modelBuilder.Entity<Question>()
                .Property(q => q.Id)
                .ValueGeneratedOnAdd(); // This indicates that the database generates the value

            modelBuilder.Entity<Option>()
                .Property(o => o.Id)
                .ValueGeneratedOnAdd(); // This indicates that the database generates the value
        }
    }
}
