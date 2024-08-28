namespace Quiz.Models
{
    public class QuizViewModel
    {
        public string? StudentName { get; set; }
        public List<QuestionAnswerViewModel>? Questions { get; set; }
    }

    public class QuestionAnswerViewModel
    {
        public int QuestionId { get; set; }
        public string? QuestionText { get; set; }
        public List<OptionViewModel>? Options { get; set; }
        public string? SelectedAnswer { get; set; }
    }

    public class OptionViewModel
    {
        public string? Value { get; set; }
        public string? Text { get; set; }
    }

}
