using QuestionModel.Models;
using System.Collections.Generic;

namespace StudentModel.Models
{
    public class StudentResult
    {
        public string? StudentName { get; set; }
        public decimal Score { get; set; }
        public int TotalQuestions { get; set; }
        public List<QuestionResult>? ResultDetails { get; set; } = new List<QuestionResult>(); // Initialize to avoid null reference
    }
}
