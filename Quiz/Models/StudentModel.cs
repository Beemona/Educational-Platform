namespace StudentModel.Models
{
    public class StudentResult
    {
        public string StudentName { get; set; }
        public int? Score { get; set; }
        public int? TotalQuestions { get; set; }
        public List<QuestionModel.Models.QuestionResult>? ResultDetails { get; set; }
        public StudentResult() => ResultDetails = new List<QuestionModel.Models.QuestionResult>();
    }
}
