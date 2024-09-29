using Authentication.Models;
using Lesson.Models;
using QuestionModel.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentModel.Models
{
    [Table("StudentResults")]
    public class StudentResult 
    {
        public int? Id { get; set; } // Identity column for StudentResult
        public decimal Score { get; set; }
        public int? Grade { get; set; }
        public int? TotalQuestions { get; set; }
        public string? StudentName { get; set; } // Add this line

        // Foreign key to associate with the Student (User)
        public int? UserId { get; set; }
        public User? User { get; set; }

        // Foreign key to associate with the Subject
        public int? SubjectId { get; set; } // Reference to the Subject
        public Subject? Subject { get; set; } // Navigation property for the subject

        public ICollection<QuestionResult>? ResultDetails { get; set; } = new List<QuestionResult>();
    }
}
