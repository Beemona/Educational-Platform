using QuestionModel.Models;

public interface IQuestionService
{
    void AddQuestion(Question question);
    List<Question> GetAllQuestions();
    Question GetQuestionById(int id);
    void UpdateQuestion(Question question);
}
