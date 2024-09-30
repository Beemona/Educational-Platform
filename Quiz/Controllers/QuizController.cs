using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestionModel.Models;
using Quiz.Models;
using QuizDbContext.Data;
using QuizDbContext.Services;
using StudentModel.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace QuizController.Controllers
{
    public class QuizController : Controller
    {
        private readonly IStudentResultService _studentResultService;
        private readonly IQuestionService _questionService;
        private readonly IQuizService _quizService;
        private readonly ApplicationDbContext _context;

        public QuizController(IStudentResultService studentResultService, IQuestionService questionService, IQuizService quizService, ApplicationDbContext context)
        {
            _studentResultService = studentResultService;
            _questionService = questionService;
            _quizService = quizService;
            _context = context;
        }

        // GET: /Quiz/Index
        //public async Task<IActionResult> Index()
        //{
        //    var questions = await _quizService.GetAllQuestionsAsync();

        //    var viewModel = new QuizViewModel
        //    {
        //        Questions = questions.Select(q => new QuestionAnswerViewModel
        //        {
        //            QuestionId = q.Id,
        //            QuestionText = q.Text,
        //            Options = q.Options?.Select(o => new OptionViewModel
        //            {
        //                Text = o.Text,
        //                IsCorrect = o.IsCorrect
        //            }).ToList()
        //        }).ToList()
        //    };

        //    return View(viewModel);
        //}

        // GET: /Quiz/Index
        [Authorize]
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Error", "Account");
            }
            var questions = await _quizService.GetAllQuestionsAsync();

            // Get the current user's ID and retrieve their name
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _context.Users.FindAsync(userId);

            var viewModel = new QuizViewModel
            {
                StudentName = user?.Name ?? "Guest", // Fallback to "Guest" if user is null
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
                var correctOption = dbQuestion.Options?.FirstOrDefault(o => o.IsCorrect);

                // Determine if the selected answer is correct
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
                    Points = isCorrect ? dbQuestion.Points : 0,
                });
            }

            // Get the current user's name from claims (assuming you have a name claim)
            var studentName = User.FindFirstValue(ClaimTypes.Name);

            // Fetch the user based on their StudentName
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == studentName);
            if (user == null)
            {
                return NotFound(); // Handle case where user isn't found
            }

            // You might need to pass the SubjectId through your model or fetch it from the relevant context
            int subjectId = model.SubjectId; // Make sure SubjectId is available in your model

            // Create a new StudentResult
            var studentResult = new StudentResult
            {
                StudentName = user.Name,
                Score = score, // Use the accumulated score
                TotalQuestions = model.Questions.Count,
                SubjectId = subjectId, // Set the SubjectId here
                ResultDetails = resultDetails // Add the list of QuestionResults
            };

            await _context.StudentResults.AddAsync(studentResult);
            await _context.SaveChangesAsync();

            // Redirect to a confirmation page or results page
            return RedirectToAction("QuizResult", new { id = studentResult.Id });
        }




    }
}
