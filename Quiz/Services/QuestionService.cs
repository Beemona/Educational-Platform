using QuestionModel.Models;

public class QuestionService : IQuestionService
{
    private readonly List<Question> _questions = new List<Question>();

    public void AddQuestion(Question question)
    {
        _questions.Add(question);
    }

    public List<Question> GetAllQuestions()
    {
        return _questions;
    }

    public Question GetQuestionById(int id)
    {
        return _questions.FirstOrDefault(q => q.Id == id);
    }

    public void UpdateQuestion(Question question)
    {
        var existingQuestion = _questions.FirstOrDefault(q => q.Id == question.Id);
        if (existingQuestion != null)
        {
            existingQuestion.Text = question.Text;
            existingQuestion.CorrectAnswer = question.CorrectAnswer;
            existingQuestion.Options = question.Options;
        }
    }
}
