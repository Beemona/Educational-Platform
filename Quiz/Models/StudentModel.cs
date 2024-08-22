using QuestionModel.Models;

namespace StudentModel.Models
{
    public class StudentResult
    {
        public string StudentName { get; set; }
        public decimal Score { get; set; } // Make sure Score is a decimal or double
        public int TotalQuestions { get; set; }
        public List<QuestionResult> ResultDetails { get; set; } // This should match your actual model
    }
}
