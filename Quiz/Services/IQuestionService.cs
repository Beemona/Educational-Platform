using QuestionModel.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IQuestionService
{
    Task AddQuestionAsync(Question question);
    List<Question> GetAllQuestions();
    Question GetQuestionById(int id);
    void UpdateQuestion(Question question);
}


//using QuestionModel.Models;

//public interface IQuestionService
//{
//    void AddQuestion(Question question);
//    List<Question> GetAllQuestions();
//    Question GetQuestionById(int id);
//    void UpdateQuestion(Question question);
//    Task AddQuestionAsync(Question question);
//}
