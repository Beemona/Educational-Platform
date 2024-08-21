namespace QuestionModel.ViewModels
{
    public class QuestionViewModel
    {
        public int Id { get; set; } // Add this property if it's missing
        public string? Text { get; set; }
        public string? CorrectAnswer { get; set; }
        public decimal Percentage { get; set; } // Assuming Percentage is an integer
        public List<OptionViewModel> Options { get; set; }
    }

    public class OptionViewModel
    {
        public string? Text { get; set; }
        public string? Value { get; set; }
    }
}
