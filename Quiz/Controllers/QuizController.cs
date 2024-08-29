using Microsoft.AspNetCore.Mvc;
using QuestionModel.Models;
using Quiz.Models;
using QuizDbContext.Services;
using StudentModel.Models;
using System.Collections.Generic;
using System.Linq;

namespace QuizController.Controllers
{
    public class QuizController : Controller
    {
        private readonly IStudentResultService _studentResultService;
        private readonly IQuestionService _questionService;
        private readonly IQuizService _quizService;

        public QuizController(IStudentResultService studentResultService, IQuestionService questionService, IQuizService quizService)
        {
            _studentResultService = studentResultService;
            _questionService = questionService;
            _quizService = quizService;
        }

        // GET: /Quiz/Index
        public async Task<IActionResult> Index()
        {
            var questions = await _quizService.GetAllQuestionsAsync();

            var viewModel = new QuizViewModel
            {
                Questions = questions.Select(q => new QuestionAnswerViewModel
                {
                    QuestionId = q.Id,
                    QuestionText = q.Text,
                    Options = q.Options?.Select(o => new OptionViewModel
                    {
                        Text = o.Text,
                        IsCorrect = o.IsCorrect
                    }).ToList()
                }).ToList()
            };

            return View(viewModel);
        }

        //public IActionResult Index()
        //{
        //    var questions = _questionService.GetAllQuestions();

        //    var viewModel = new QuizViewModel
        //    {
        //        Questions = questions.Select(q => new QuestionAnswerViewModel
        //        {
        //            QuestionId = q.Id,
        //            QuestionText = q.Text,
        //            Options = q.Options?.Select(o => new Quiz.Models.OptionViewModel
        //            {
        //                Value = o.Value,
        //                Text = o.Text
        //            }).ToList()
        //        }).ToList()
        //    };

        //    return View(viewModel);
        //}

        // POST: /Quiz/SubmitQuiz
        [HttpPost]
        public async Task<IActionResult> SubmitQuiz(QuizViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Retrieve all questions from the service
            var questions = await _quizService.GetAllQuestionsAsync();
            decimal score = 0;
            var resultDetails = new List<QuestionResult>();

            foreach (var question in model.Questions)
            {
                // Find the corresponding question in the database
                var dbQuestion = questions.FirstOrDefault(q => q.Id == question.QuestionId);

                if (dbQuestion == null)
                {
                    continue; // Skip if no matching question is found
                }

                // Find the selected option in the current question
                var selectedOption = dbQuestion.Options?.FirstOrDefault(o => o.Text == question.SelectedAnswer);

                // Determine the correct option based on IsCorrect
                var correctOption = dbQuestion.Options?.FirstOrDefault(o => o.IsCorrect);

                // Check if the selected answer is correct
                bool isCorrect = selectedOption != null && selectedOption.IsCorrect;

                if (isCorrect)
                {
                    score += dbQuestion.Points; // Accumulate score based on correct answers
                }

                resultDetails.Add(new QuestionResult
                {
                    QuestionText = dbQuestion.Text,
                    SelectedAnswer = selectedOption?.Text ?? "N/A",
                    CorrectAnswer = correctOption?.Text ?? "N/A",
                    Points = isCorrect ? dbQuestion.Points : 0, // Points if correct, otherwise 0
                });
            }

            var studentResult = new StudentResult
            {
                StudentName = model.StudentName,
                Score = score, // Use the accumulated score
                TotalQuestions = model.Questions.Count,
                ResultDetails = resultDetails // Use the results built in the loop
            };

            await _studentResultService.SaveStudentResultAsync(studentResult); // Ensure SaveStudentResultAsync is implemented

            return RedirectToAction("Index", "Teacher");
        }

        //[HttpPost]
        //public IActionResult SubmitQuiz(QuizViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    var questions = _questionService.GetAllQuestions();
        //    decimal score = 0;
        //    var resultDetails = new List<QuestionResult>();

        //    foreach (var question in model.Questions)
        //    {
        //        // Log the received data
        //        System.Diagnostics.Debug.WriteLine($"QuestionId: {question.QuestionId}, SelectedAnswer: {question.SelectedAnswer}");
        //    }

        //    foreach (var question in questions)
        //    {
        //        var selectedAnswer = model.Questions?
        //            .FirstOrDefault(q => q.QuestionId == question.Id)?
        //            .SelectedAnswer;

        //        var selectedOption = question.Options?.FirstOrDefault(o => o.Value == selectedAnswer);
        //        var correctOption = question.Options?.FirstOrDefault(o => o.Value == question.CorrectAnswer);
        //        var isCorrect = selectedAnswer == question.CorrectAnswer;

        //        if (isCorrect)
        //        {
        //            score += question.Points;
        //        }

        //        resultDetails.Add(new QuestionResult
        //        {
        //            QuestionText = question.Text,
        //            SelectedAnswer = selectedOption?.Text ?? "N/A",
        //            CorrectAnswer = correctOption?.Text ?? "N/A",
        //            Points = isCorrect ? question.Points : 0
        //        });
        //    }

        //    var studentResult = new StudentResult
        //    {
        //        StudentName = model.StudentName,
        //        Score = score,
        //        TotalQuestions = questions.Count,
        //        ResultDetails = resultDetails
        //    };

        //    _studentResultService.SaveStudentResult(studentResult);

        //    return RedirectToAction("Index", "Teacher");
        //}

    }
}
