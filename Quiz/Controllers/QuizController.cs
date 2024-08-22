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
        //public IActionResult Index()
        //{
        //    var questions = _questionService.GetAllQuestions();
        //    var viewModel = new QuizViewModel
        //    {
        //        Questions = questions.Select(q => new QuestionAnswerViewModel
        //        {
        //            QuestionId = q.Id,
        //            QuestionText = q.Text,
        //            Options = q.Options.Select(o => new OptionViewModel
        //            {
        //                Value = o.Value,
        //                Text = o.Text
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
                // Handle errors if needed
                return View("Error", new { message = "Invalid submission." });
            }

            var questionsList = _questionService.GetAllQuestions(); // Replace with actual method to get questions
            decimal score = 0;
            var resultDetails = new List<QuestionResult>();

            foreach (var question in questionsList)
            {
                var questionId = question.Id.ToString(); // Convert the question ID to a string
                var selectedAnswer = questions.ContainsKey(questionId) ? questions[questionId] : null;

                var isCorrect = selectedAnswer == question.CorrectAnswer;
                if (isCorrect)
                {
                    score += question.Points;
                }

                resultDetails.Add(new QuestionResult
                {
                    QuestionText = question.Text,
                    SelectedAnswer = selectedAnswer,
                    CorrectAnswer = question.CorrectAnswer,
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




        ////public IActionResult QuizResults()
        ////{
        ////    var studentResults = _studentResultService.GetAllResults().LastOrDefault(); // Retrieve the most recent result
        ////    var resultDetails = studentResults?.ResultDetails ?? new List<QuestionResult>();

        ////    ViewBag.Score = studentResults?.Score ?? 0;
        ////    ViewBag.TotalQuestions = studentResults?.TotalQuestions ?? 0;
        ////    ViewBag.ResultDetails = resultDetails;

        ////    return View(); // Ensure this view expects the correct ViewBag data
        ////}


        //[HttpPost]
        //public IActionResult SubmitQuiz(string studentName, Dictionary<int, string> answers)
        //{
        //    if (string.IsNullOrEmpty(studentName) || answers == null)
        //    {
        //        return View("Error", "No name or answers submitted.");
        //    }

        //    var questions = _questionService.GetAllQuestions();
        //    decimal score = 0; // Use decimal
        //    var resultDetails = new List<QuestionResult>();

        //    foreach (var question in questions)
        //    {
        //        var selectedAnswer = answers.ContainsKey(question.Id) ? answers[question.Id] : null;
        //        var isCorrect = selectedAnswer == question.CorrectAnswer;

        //        if (isCorrect)
        //        {
        //            score += question.Points; // Add points
        //        }

        //        resultDetails.Add(new QuestionResult
        //        {
        //            QuestionText = question.Text,
        //            SelectedAnswer = selectedAnswer,
        //            CorrectAnswer = question.CorrectAnswer,
        //            Points = question.Points // Ensure points are set correctly
        //        });
        //    }

        //    var studentResult = new StudentResult
        //    {
        //        StudentName = studentName,
        //        Score = score, // Use decimal value
        //        TotalQuestions = questions.Count,
        //        ResultDetails = resultDetails
        //    };

        //    _studentResultService.SaveStudentResult(studentResult);

        //    return RedirectToAction("Index", "Teacher");
        //}



    }
}

