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

        public IActionResult Index()
        {
            var questions = _questionService.GetAllQuestions();
            var viewModel = new QuizViewModel
            {
                Questions = questions.Select(q => new QuestionAnswerViewModel
                {
                    QuestionId = q.Id,
                    QuestionText = q.Text,
                    Options = q.Options.Select(o => new OptionViewModel
                    {
                        Value = o.Value,
                        Text = o.Text
                    }).ToList()
                }).ToList()
            };

            return View(viewModel);
        }


        // POST: SubmitQuiz
        [HttpPost]
        public IActionResult SubmitQuiz(string studentName, Dictionary<string, string> questions)
        {
            if (string.IsNullOrEmpty(studentName) || questions == null)
            {
                return View("Error", new { message = "Invalid submission." });
            }

            var questionsList = _questionService.GetAllQuestions();
            decimal score = 0;
            var resultDetails = new List<QuestionResult>();

            foreach (var question in questionsList)
            {
                var questionId = question.Id.ToString();
                var selectedAnswerKey = questions.ContainsKey(questionId) ? questions[questionId] : null;

                var selectedOption = question.Options.FirstOrDefault(o => o.Value == selectedAnswerKey);
                var correctOption = question.Options.FirstOrDefault(o => o.Value == question.CorrectAnswer);
                var isCorrect = selectedAnswerKey == question.CorrectAnswer;

                if (isCorrect)
                {
                    score += question.Points;
                }

                resultDetails.Add(new QuestionResult
                {
                    QuestionText = question.Text,
                    SelectedAnswer = selectedOption?.Text ?? "N/A",
                    CorrectAnswer = correctOption?.Text ?? "N/A",
                    Points = isCorrect ? question.Points : 0
                });
            }

            var studentResult = new StudentResult
            {
                StudentName = studentName,
                Score = score,
                TotalQuestions = questionsList.Count,
                ResultDetails = resultDetails
            };

            _studentResultService.SaveStudentResult(studentResult);

            return RedirectToAction("Index", "Teacher");
        }

    }
}

