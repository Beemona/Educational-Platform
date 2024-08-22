using OptionModel.Models;
using System.Collections.Generic;

namespace QuestionModel.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        //public List<OptionModel.Models.Option>? Options { get; set; }
        public List<Option> Options { get; set; }
        public string? CorrectAnswer { get; set; } // Can store "A", "B", "C", or "D"
        public decimal Points { get; set; }
    }

    public class QuestionResult
    {
        public string? QuestionText { get; set; }
        public string? SelectedAnswer { get; set; }
        public string? CorrectAnswer { get; set; }
        public decimal? Points { get; set; }
    }
}
