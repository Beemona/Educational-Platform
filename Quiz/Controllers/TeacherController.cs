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
            // Retrieve all student results from the service
            List<StudentResult> studentResults = _studentResultService.GetAllResults();

            // Pass the student results to the view
            return View(studentResults);
        }


        // GET: Teacher/Questions
        public IActionResult Questions()
        {
            var questions = _questionService.GetAllQuestions();
            return View(questions); // Pass questions to the view
        }


        // GET: Teacher/AddQuestion
        public IActionResult AddQuestion()
        {
            var viewModel = new QuestionModel.ViewModels.QuestionViewModel
            {
                Options = new List<QuestionModel.ViewModels.OptionViewModel>
        {
            new QuestionModel.ViewModels.OptionViewModel { Value = "A" },
            new QuestionModel.ViewModels.OptionViewModel { Value = "B" },
            new QuestionModel.ViewModels.OptionViewModel { Value = "C" },
            new QuestionModel.ViewModels.OptionViewModel { Value = "D" }
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
                    Points = model.Points,
                    Options = model.Options?.Select((option, index) => new Option
                    {
                        Text = option.Text,
                        Value = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"[index].ToString()
                    }).ToList(),
                    CorrectAnswer = model.CorrectAnswer // This should match the selected value
                };

                _questionService.AddQuestion(question);
                return RedirectToAction("Index");
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
                Id = question.Id,
                Text = question.Text,
                CorrectAnswer = question.CorrectAnswer,
                Points = question.Points,
                Options = question.Options?.Select(o => new QuestionModel.ViewModels.OptionViewModel
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
                    Id = id,
                    Text = model.Text,
                    CorrectAnswer = model.CorrectAnswer,
                    Points = model.Points,
                    Options = model.Options?.Select(o => new Option
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
