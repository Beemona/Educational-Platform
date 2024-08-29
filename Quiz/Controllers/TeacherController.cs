using Microsoft.AspNetCore.Mvc;
using OptionModel.Models;
using QuestionModel.Models;
using QuestionModel.ViewModels;
using StudentModel.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Quiz.Controllers
{
    [Route("Teacher")]
    public class TeacherController : Controller
    {
        private readonly IStudentResultService _studentResultService;
        private readonly IQuestionService _questionService;

        public TeacherController(IStudentResultService studentResultService, IQuestionService questionService)
        {
            _studentResultService = studentResultService;
            _questionService = questionService;
        }

        // GET: Teacher/Index
        [HttpGet("Index")]
        public IActionResult Index()
        {
            List<StudentResult> studentResults = _studentResultService.GetAllResults();
            return View(studentResults);
        }

        // GET: Teacher/Questions
        [HttpGet("Questions")]
        public IActionResult Questions()
        {
            var questions = _questionService.GetAllQuestions();
            return View(questions);
        }

        // GET: Teacher/AddQuestion
        public IActionResult AddQuestion()
        {
            var viewModel = new QuestionViewModel
            {
                Options = new List<OptionViewModel>
        {
            new OptionViewModel(),
            new OptionViewModel(),
            new OptionViewModel(),
            new OptionViewModel()
        }
            };

            return View(viewModel);
        }


        // POST: Teacher/AddQuestion
        [HttpPost]
        public async Task<IActionResult> AddQuestion(QuestionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var question = new Question
                {
                    Text = model.Text,
                    Points = model.Points,
                    Options = model.Options.Select(o => new Option
                    {
                        Text = o.Text,
                        IsCorrect = o.IsCorrect
                    }).ToList()
                };

                await _questionService.AddQuestionAsync(question);
                return RedirectToAction("Index");
            }

            return View(model);
        }



        // GET: Teacher/EditQuestion/5
        [HttpGet("EditQuestion/{id}")]
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
                Points = question.Points,
                Options = question.Options?.Select(o => new QuestionModel.ViewModels.OptionViewModel
                {
                    Text = o.Text,
                    IsCorrect = o.IsCorrect
                }).ToList()
            };

            return View(viewModel);
        }

        // POST: Teacher/EditQuestion/5
        [HttpPost("EditQuestion/{id}")]
        public IActionResult EditQuestion(int id, QuestionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var question = new Question
                {
                    Id = id,
                    Text = model.Text,
                    Points = model.Points,
                    Options = model.Options?.Select(o => new Option
                    {
                        Text = o.Text,
                        IsCorrect = o.IsCorrect
                    }).ToList()
                };

                _questionService.UpdateQuestion(question);
                return RedirectToAction("Questions");
            }

            return View(model);
        }
    }
}
