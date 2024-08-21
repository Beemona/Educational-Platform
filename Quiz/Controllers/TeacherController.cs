using Microsoft.AspNetCore.Mvc;
using OptionModel.Models;
using QuestionModel.Models;
using QuestionModel.ViewModels;
using StudentModel.Models;
using System.Collections.Generic;
using System.Linq;

namespace Quiz.Controllers
{
    public class TeacherController : Controller
    {
        private readonly IStudentResultService _studentResultService;
        private readonly IQuestionService _questionService;

        public TeacherController(IStudentResultService studentResultService, IQuestionService questionService)
        {
            _studentResultService = studentResultService;
            _questionService = questionService;
        }

        // Display student results
        public IActionResult Index()
        {
            var studentResults = _studentResultService.GetAllResults();

            // Log for debugging
            Console.WriteLine($"Fetched {studentResults.Count} student results");

            return View(studentResults);
        }
        // GET: Teacher/Questions
        public IActionResult Questions()
        {
            var questions = _questionService.GetAllQuestions(); // Fetch all questions

            // Log for debugging
            Console.WriteLine($"Fetched {questions.Count} questions");

            return View(questions); // Pass questions to the view
        }

        // GET: Teacher/AddQuestion
        public IActionResult AddQuestion()
        {
            var viewModel = new QuestionViewModel
            {
                Options = new List<OptionViewModel>
                {
                    new OptionViewModel { Value = "A" },
                    new OptionViewModel { Value = "B" },
                    new OptionViewModel { Value = "C" },
                    new OptionViewModel { Value = "D" }
                }
            };

            return View(viewModel);
        }

        // POST: Teacher/AddQuestion
        [HttpPost]
        public IActionResult AddQuestion(QuestionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var question = new Question
                {
                    Text = model.Text,
                    CorrectAnswer = model.CorrectAnswer,
                    Percentage = model.Percentage,
                    Options = model.Options.Select(o => new Option
                    {
                        Text = o.Text,
                        Value = o.Value
                    }).ToList()
                };

                _questionService.AddQuestion(question); // Save to data store

                return RedirectToAction("Index"); // Redirect to the question list view
            }

            return View(model);
        }


        // GET: Teacher/EditQuestion/5
        public IActionResult EditQuestion(int id)
        {
            var question = _questionService.GetQuestionById(id);

            if (question == null)
            {
                return NotFound();
            }

            var viewModel = new QuestionViewModel
            {
                Id = question.Id, // Make sure this is set
                Text = question.Text,
                CorrectAnswer = question.CorrectAnswer,
                Percentage = question.Percentage,
                Options = question.Options.Select(o => new OptionViewModel
                {
                    Text = o.Text,
                    Value = o.Value
                }).ToList()
            };

            return View(viewModel);
        }


        // POST: Teacher/EditQuestion/5
        [HttpPost]
        public IActionResult EditQuestion(int id, QuestionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var question = new Question
                {
                    Id = id, // Ensure this is correctly assigned
                    Text = model.Text,
                    CorrectAnswer = model.CorrectAnswer,
                    Percentage = model.Percentage,
                    Options = model.Options.Select(o => new Option
                    {
                        Text = o.Text,
                        Value = o.Value
                    }).ToList()
                };

                _questionService.UpdateQuestion(question);

                return RedirectToAction("Index");
            }

            return View(model);
        }

    }
}
