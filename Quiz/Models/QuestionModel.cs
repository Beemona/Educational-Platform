using OptionModel.Models;
using StudentModel.Models;
using System.Collections.Generic;

namespace QuestionModel.Models
{
    public class Question
    {
        public int Id { get; set; } // Maps to QuestionId
        public string? Text { get; set; } // Maps to QuestionText
        public List<Option>? Options { get; set; } // Options related to the question
        // public string? CorrectAnswer { get; set; } // Can store "A", "B", "C", or "D" OLD!!!
        public decimal Points { get; set; } // Maps to Points
        public int CategoryId { get; set; }  // Foreign key to the associated category NEW!!!
    }


    public class QuestionResult
    {
        public int Id { get; set; } // Primary key
        public string? QuestionText { get; set; }
        public string? SelectedAnswer { get; set; } // Text of the selected answer
        public string? CorrectAnswer { get; set; }  // Text of the correct answer
        public decimal? Points { get; set; }

        // Foreign key
        public int StudentResultId { get; set; }
        public StudentResult? StudentResult { get; set; }
    }

}
