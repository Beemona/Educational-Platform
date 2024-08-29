using System.Collections.Generic;
using System.Threading.Tasks;
using QuestionModel.Models;

namespace QuizDbContext.Services
{
    public interface IQuizService
    {
        Task<List<Question>> GetAllQuestionsAsync();
        Task<Question> GetQuestionByIdAsync(int id);
    }
}
