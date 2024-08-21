using QuestionModel.Models;
using StudentModel.Models;
using System.Collections.Generic;

public class StudentResultService : IStudentResultService
{
    private readonly List<StudentResult> _studentResults = new List<StudentResult>();

    // Method to get all student results
    public List<StudentResult> GetAllResults()
    {
        return _studentResults;
    }

    // Method to save a student result
   public void SaveStudentResult(StudentResult studentResult)
{
    _studentResults.Add(studentResult);
}

}



//using QuestionModel.Models;
//using StudentModel.Models;
//using System.Collections.Generic;
//using System.Linq;

//public class StudentResultService : IStudentResultService
//{
//    private readonly List<StudentResult> _studentResults = new List<StudentResult>();

//    public StudentResultService()
//    {
//        // Initialization or example data, if necessary
//    }

//    public List<StudentResult> GetAllResults()
//    {
//        return _studentResults;
//    }

//    public void SaveStudentResult(string studentName, List<Question> quizQuestions, Dictionary<int, string> submittedAnswers)
//    {
//        var studentResult = new StudentResult
//        {
//            StudentName = studentName,
//            Score = 0,
//            TotalQuestions = quizQuestions.Count,
//            ResultDetails = new List<QuestionResult>()
//        };

//        foreach (var question in quizQuestions)
//        {
//            var selectedAnswer = submittedAnswers.ContainsKey(question.Id) ? submittedAnswers[question.Id] : string.Empty;
//            var isCorrect = selectedAnswer == question.CorrectAnswer;

//            studentResult.ResultDetails.Add(new QuestionResult
//            {
//                QuestionText = question.Text,
//                SelectedAnswer = selectedAnswer,
//                CorrectAnswer = question.CorrectAnswer
//            });

//            if (isCorrect)
//            {
//                studentResult.Score++;
//            }
//        }

//        _studentResults.Add(studentResult);
//    }
//}
