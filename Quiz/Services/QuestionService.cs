using Microsoft.EntityFrameworkCore;
using QuestionModel.Models;
using QuizDbContext.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class QuestionService : IQuestionService
{
    private readonly ApplicationDbContext _context;

    public QuestionService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddQuestionAsync(Question question)
    {
        // Add the question to the context
        _context.Questions.Add(question);

        // Save changes to generate the ID for the question (if needed)
        await _context.SaveChangesAsync();

        // Add options (answers) if they exist
        if (question.Options != null && question.Options.Any())
        {
            foreach (var option in question.Options)
            {
                option.QuestionId = question.Id;  // Ensure the correct QuestionId is set
                _context.Options.Add(option);
            }
        }

        // Save all changes including options
        await _context.SaveChangesAsync();
    }

    public List<Question> GetAllQuestions()
    {
        return _context.Questions.Include(q => q.Options).ToList();
    }

    public Question GetQuestionById(int id)
    {
        var question = _context.Questions.Include(q => q.Options).FirstOrDefault(q => q.Id == id);
        if (question == null)
        {
            throw new KeyNotFoundException($"No question found with ID {id}");
        }
        return question;
    }

    public void UpdateQuestion(Question question)
    {
        var existingQuestion = _context.Questions.Include(q => q.Options).FirstOrDefault(q => q.Id == question.Id);
        if (existingQuestion != null)
        {
            existingQuestion.Text = question.Text;
            existingQuestion.Points = question.Points;
            existingQuestion.Options = question.Options;
            _context.Questions.Update(existingQuestion);
            _context.SaveChanges();
        }
    }
}



//using Microsoft.EntityFrameworkCore;
//using QuestionModel.Models;
//using QuizDbContext.Data;
//using System.Collections.Generic;
//using System.Linq;

//public class QuestionService : IQuestionService
//{
//    protected readonly List<Question> _questions = new List<Question>();

//    public void AddQuestion(Question question)
//    {
//        _questions.Add(question);
//    }

//    public List<Question> GetAllQuestions()
//    {
//        return _questions;
//    }

//    public Question GetQuestionById(int id)
//    {
//        var question = _questions.FirstOrDefault(q => q.Id == id);
//        if (question == null)
//        {
//            throw new KeyNotFoundException($"No question found with ID {id}");
//        }
//        return question;
//    }

//    public void UpdateQuestion(Question question)
//    {
//        var existingQuestion = _questions.FirstOrDefault(q => q.Id == question.Id);
//        if (existingQuestion != null)
//        {
//            existingQuestion.Text = question.Text;
//            existingQuestion.Points = question.Points;
//            existingQuestion.Options = question.Options;
//            // No need to set CorrectAnswer as it's now determined by the IsCorrect property
//        }
//    }

//    private readonly ApplicationDbContext _context;

//    public QuestionService(ApplicationDbContext context)
//    {
//        _context = context;
//    }
//    public async Task AddQuestionAsync(Question question)
//    {
//        _context.Questions.Add(question);
//        await _context.SaveChangesAsync();
//    }
//}


