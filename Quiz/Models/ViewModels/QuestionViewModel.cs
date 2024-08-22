namespace QuestionModel.ViewModels
{
    public class QuestionViewModel
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public decimal Points { get; set; } // Use decimal here
        public string? CorrectAnswer { get; set; }
        public List<OptionViewModel> Options { get; set; }
    }


    public class OptionViewModel
    {
        public string? Text { get; set; }
        public string? Value { get; set; }
    }
}
