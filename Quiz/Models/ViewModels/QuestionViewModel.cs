using System.ComponentModel.DataAnnotations;

namespace QuestionModel.ViewModels
{
    public class QuestionViewModel
    {
        public int Id { get; set; } // Maps to QuestionId

        [Required(ErrorMessage = "Question text is required.")]
        public string? Text { get; set; } // Maps to QuestionText

        [Required(ErrorMessage = "Points are required.")]
        [Range(1, 10, ErrorMessage = "Points must be between 1 and 10.")]
        public decimal Points { get; set; } // Maps to Points
        //public string? CorrectAnswer { get; set; } OLD!!
        public List<OptionViewModel>? Options { get; set; }  // Options related to the question
    }


    public class OptionViewModel
    {
        [Required(ErrorMessage = "Option text is required.")]
        public string? Text { get; set; } // Maps to OptionText
        //public string? Value { get; set; } // Option value like "A", "B", etc.
        public bool IsCorrect { get; set; }  // Indicates if this option is correct NEW!!!
    }
}

