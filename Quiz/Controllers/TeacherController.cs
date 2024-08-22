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
        //public IActionResult Index()
        //{
        //    var studentResults = _studentResultService.GetAllResults();
        //    return View(studentResults); // Ensure this view expects a List<StudentResult>
        //}

        public IActionResult Index()
        {
            var studentResults = _studentResultService.GetAllResults();
            return View(studentResults); // Ensure this view expects a List<StudentResult>
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
        // POST: Teacher/AddQuestion
        //[HttpPost]
        //public IActionResult AddQuestion(QuestionViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var question = new Question
        //        {
        //            Text = model.Text,
        //            CorrectAnswer = model.CorrectAnswer,
        //            Points = model.Points,  // Changed from Percentage to Points
        //            Options = model.Options.Select(o => new Option
        //            {
        //                Text = o.Text,
        //                Value = o.Value
        //            }).ToList()
        //        };

        //        _questionService.AddQuestion(question);

        //        return RedirectToAction("Index");
        //    }

        //    // If the model is invalid, return the same view with the model to display validation errors
        //    return View(model);
        //}



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
                    Points = model.Points,
                    Options = model.Options.Select(o => new Option
                    {
                        Text = o.Text,
                        Value = o.Value
                    }).ToList()
                };

                _questionService.AddQuestion(question);

                return RedirectToAction("Index");
            }

            // If the model is invalid, return the same view with the model to display validation errors
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
                Options = question.Options.Select(o => new QuestionModel.ViewModels.OptionViewModel
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


///////////////////////////////////////////////////////////////


