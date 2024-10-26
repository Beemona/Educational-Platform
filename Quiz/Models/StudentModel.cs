using Authentication.Models;
using Lesson.Models;
using QuestionModel.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public int? Year => User is Student student ? student.Year : null; // Using the Student Year
        public int? Group => User is Student student ? student.Group : null; // Using the Student Group

        // Foreign key to associate with the Subject
        public int? SubjectId { get; set; } // Reference to the Subject
        public Subject? Subject { get; set; } // Navigation property for the subject

        public ICollection<QuestionResult>? ResultDetails { get; set; } = new List<QuestionResult>();
    }
    public class StudentGrade
    {
        public string Name { get; set; }
        public int CoursePoints { get; set; }
        public int SeminarPoints { get; set; }
        public int FinalExamPoints { get; set; }
        public int BonusPoints { get; set; }
        public double Grade { get; set; }
        public int Year { get; set; }   // Year the student is in
        public string Group { get; set; } // Student's group
        public string Subject { get; set; } // Subject of the grade
        public double CalculateGrade(GradeFormula formula)
        {
            // Weighted points
            var weightedSeminar = SeminarPoints * (formula.SeminarWeight / 100.0);
            var weightedCourse = CoursePoints * (formula.CourseWeight / 100.0);
            var weightedFinalExam = FinalExamPoints * (formula.FinalExamWeight / 100.0);

            // Calculate the total score and add bonus points
            var totalPoints = weightedSeminar + weightedCourse + weightedFinalExam + BonusPoints;

            return Math.Round(totalPoints, 2); // Round to 2 decimal places for better readability
        }

        public string Status => Grade >= 50 ? "Passed" : "Failed";
    }

    public class GradeFormula
    {
        public string Subject { get; set; }
        public int SeminarWeight { get; set; } // Weight for seminar points
        public int CourseWeight { get; set; } // Weight for course points
        public int FinalExamWeight { get; set; } // Weight for final exam points
        public int DefaultPoints { get; set; } = 10; // Default points for grading
    }

}
