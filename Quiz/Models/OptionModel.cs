namespace OptionModel.Models
{
    public class Option
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public string? Value { get; set; } // e.g., "A", "B", "C", "D"
        public int QuestionId { get; set; } // Foreign key to the associated question
    }
}
