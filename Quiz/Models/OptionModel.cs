using QuestionModel.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OptionModel.Models
{
    public class Option
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Maps to OptionId
        public string? Text { get; set; } // Maps to OptionText
        public int QuestionId { get; set; } // Foreign key to the associated question
        public bool IsCorrect { get; set; }  // Maps to IsCorrect (1 for true, 0 for false) NEW!!

        public Question? Question { get; set; } // Navigation property
    }
}
