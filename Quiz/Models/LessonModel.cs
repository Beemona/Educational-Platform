// Models/CourseModel.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lesson.Models
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Code { get; set; }
        public string? CourseProfessor { get; set; }
        public string? SeminarProfessor { get; set; }
        public string? IconUrl { get; set; }
        public string? BackgroundColor { get; set; }

        public int? CourseCardId { get; set; }
        public ClassCard? CourseCard { get; set; }

        public int? SeminarCardId { get; set; }
        public ClassCard? SeminarCard { get; set; }

        public int? FinalExamCardId { get; set; }
        public FinalExamCard? FinalExam { get; set; }

        public ICollection<LessonCard>? Lessons { get; set; }
    }


    public class ClassCard {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Professor { get; set; }
        public int NumberOfLessonsPosted { get; set; }
        public int WeeksPassed { get; set; }

        // Foreign Key
        //public int SubjectId { get; set; }
        //public Subject? Subject { get; set; }
    }

        public class LessonCard
        {
            [Key]
            public int Id { get; set; } // Add this line
            public string LessonName { get; set; } = string.Empty;
            public string LessonTitle { get; set; } = string.Empty;
            public int WeekNumber { get; set; }
            public string DateRange { get; set; } = string.Empty;
            public int Year { get; set; }
            public string Summary { get; set; } = string.Empty;

            // Foreign Key
            public int SubjectId { get; set; }
            public Subject? Subject { get; set; }

            public int? CourseCardId { get; set; } // This should be included if lessons are tied to course cards
            public int? SeminarCardId { get; set; } // This should be included if lessons are tied to seminar cards

    }

        public class FinalExamCard
        {
            [Key]
            public int Id { get; set; } // Add this line
            public string ExamName { get; set; } = string.Empty;
            public int NumberOfQuestions { get; set; }
            public int TotalPoints { get; set; }

            // Foreign Key
            public int SubjectId { get; set; }
            public Subject? Subject { get; set; }

        }

    public class LessonPreview
    {
        [Key]
        public int Id { get; set; } // Add this line
        public string? LessonTitle { get; set; }
        public string? ProfessorName { get; set; }
        public string? LessonText { get; set; }

        // Foreign Key
        public int SubjectId { get; set; }
        public Subject? Subject { get; set; }

    }

    public class LessonProgress
    {
        [Key]
        public int Id { get; set; } // Primary Key
        public int LessonId { get; set; }
        public bool IsFinished { get; set; }

        public LessonPreview? LessonPreview { get; set; }

    }

}
