using Microsoft.AspNetCore.Mvc;
using QuestionModel.Models;
using StudentModel.Models;
using System.Collections.Generic;
using System.Linq;

namespace QuizController.Controllers
{
    public class QuizController : Controller
    {
        private readonly IStudentResultService _studentResultService;
        private readonly IQuestionService _questionService;

        public QuizController(IStudentResultService studentResultService, IQuestionService questionService)
        {
            _studentResultService = studentResultService;
            _questionService = questionService;
        }

        // GET: Quiz
        public IActionResult Index()
        {
            var questions = _questionService.GetAllQuestions();
            return View(questions); // Pass the list of questions to the view
        }

        // POST: SubmitQuiz
        [HttpPost]
        public IActionResult SubmitQuiz(string studentName, Dictionary<int, string> answers)
        {
            if (string.IsNullOrEmpty(studentName) || answers == null)
            {
                return View("Error", "No name or answers submitted.");
            }

            var questions = _questionService.GetAllQuestions(); // Fetch questions again here
            int score = 0;
            var resultDetails = new List<QuestionResult>();

            foreach (var question in questions)
            {
                var selectedAnswer = answers.ContainsKey(question.Id) ? answers[question.Id] : null;
                var isCorrect = selectedAnswer == question.CorrectAnswer;

                if (isCorrect)
                {
                    score++;
                }

                resultDetails.Add(new QuestionResult
                {
                    QuestionText = question.Text,
                    SelectedAnswer = selectedAnswer,
                    CorrectAnswer = question.CorrectAnswer
                });
            }

            var studentResult = new StudentResult
            {
                StudentName = studentName,
                Score = score,
                TotalQuestions = questions.Count,
                ResultDetails = resultDetails
            };

            _studentResultService.SaveStudentResult(studentResult);

            return RedirectToAction("Index", "Teacher");
        }
    }
}
