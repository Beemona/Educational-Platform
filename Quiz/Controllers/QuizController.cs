using Microsoft.AspNetCore.Mvc;
using QuestionModel.Models;
using Quiz.Models;
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

        // GET: /Quiz/Index
        //public IActionResult Index()
        //{
        //    var questions = _questionService.GetAllQuestions();
        //    var viewModel = new QuizViewModel
        //    {
        //        Questions = questions.Select(q => new QuestionAnswerViewModel
        //        {
        //            QuestionId = q.Id,
        //            QuestionText = q.Text!,
        //            Options = q.Options?.Select(o => new OptionViewModel
        //            {
        //                Value = o.Value!,
        //                Text = o.Text!
        //            }).ToList()
        //        }).ToList()
        //    };

        //    return View(viewModel);
        //}

        public IActionResult Index()
        {
            var questions = _questionService.GetAllQuestions();

            var viewModel = new QuizViewModel
            {
                Questions = questions.Select(q => new QuestionAnswerViewModel
                {
                    QuestionId = q.Id,
                    QuestionText = q.Text,
                    Options = q.Options?.Select(o => new Quiz.Models.OptionViewModel
                    {
                        Value = o.Value,
                        Text = o.Text
                    }).ToList()
                }).ToList()
            };

            return View(viewModel);
        }

        // POST: /Quiz/SubmitQuiz
        [HttpPost]
        public IActionResult SubmitQuiz(QuizViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var questions = _questionService.GetAllQuestions();
            decimal score = 0;
            var resultDetails = new List<QuestionResult>();

            foreach (var question in model.Questions)
            {
                // Log the received data
                System.Diagnostics.Debug.WriteLine($"QuestionId: {question.QuestionId}, SelectedAnswer: {question.SelectedAnswer}");
            }

            foreach (var question in questions)
            {
                var selectedAnswer = model.Questions?
                    .FirstOrDefault(q => q.QuestionId == question.Id)?
                    .SelectedAnswer;

                var selectedOption = question.Options?.FirstOrDefault(o => o.Value == selectedAnswer);
                var correctOption = question.Options?.FirstOrDefault(o => o.Value == question.CorrectAnswer);
                var isCorrect = selectedAnswer == question.CorrectAnswer;

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
                StudentName = model.StudentName,
                Score = score,
                TotalQuestions = questions.Count,
                ResultDetails = resultDetails
            };

            _studentResultService.SaveStudentResult(studentResult);

            return RedirectToAction("Index", "Teacher");
        }

    }
}
