using QuestionModel.Models;

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
        public string? Value { get; set; } // Maps to OptionText
        public string? Text { get; set; } // Maps to OptionText

        public bool IsCorrect { get; set; }  // Indicates if this option is correct
    }

    public class QuizCreationViewModel
    {
        public string? SelectedFormat { get; set; } // Quiz format
        public int NumberOfQuestions { get; set; } // Number of questions
        public bool IsRandomized { get; set; } // Randomized or handpicked
        public Dictionary<string, int>? SubjectTables { get; set; } // Tables and number of questions from each table
    }
    public class Test
    {
        public int Id { get; set; }
        public List<Question>? Questions { get; set; }
    }

    public class SelectedQuestionsViewModel
    {
        public List<Question>? Questions { get; set; }
    }
}
