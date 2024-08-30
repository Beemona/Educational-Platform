using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuestionModel.Models;
using Quiz.Models; // Assuming this is where your ViewModel and Test model are located
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Controllers
{
    public class QuizCreationController : Controller
    {
        // Static questions
        private static readonly List<Question> FreudQuestions = new List<Question>
        {
            new Question { Id = 1, Text = "What is Freud's theory of the unconscious?", Points = 1 },
            new Question { Id = 2, Text = "What are Freud's stages of psychosexual development?", Points = 1 }
            // Add more simulated questions as needed
        };

        private static readonly List<Question> CarlRogersQuestions = new List<Question>
        {
            new Question { Id = 1, Text = "What is Carl Rogers' concept of unconditional positive regard?", Points = 1 },
            new Question { Id = 2, Text = "How does Carl Rogers define self-actualization?", Points = 1 }
            // Add more simulated questions as needed
        };

        private static readonly List<Question> JungQuestions = new List<Question>
        {
            new Question { Id = 1, Text = "What are Jung's archetypes?", Points = 1 },
            new Question { Id = 2, Text = "What is Jung's concept of the collective unconscious?", Points = 1 }
            // Add more simulated questions as needed
        };

        // GET: QuizCreation/Create
        public IActionResult Create()
        {
            var subjectTables = new Dictionary<string, int>
            {
                {"Freud", 0},
                {"Carl Rogers", 0},
                {"Jung", 0}
                // Add more tables as needed
            };

            var model = new QuizCreationViewModel
            {
                SubjectTables = subjectTables
            };

            return View(model);
        }

        // POST: Quiz/Create
        [HttpPost]
        public IActionResult Create(QuizCreationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var selectedQuestions = new List<Question>();

                foreach (var table in model.SubjectTables)
                {
                    if (table.Value > 0)
                    {
                        var questions = GetQuestionsFromTable(table.Key, table.Value, model.IsRandomized);
                        selectedQuestions.AddRange(questions);
                    }
                }

                // Serialize the list of questions and store it in TempData
                TempData["SelectedQuestions"] = JsonConvert.SerializeObject(selectedQuestions);

                // Redirect to the SelectedQuestions view
                return RedirectToAction("SelectedQuestions");
            }

            return View(model);
        }



        private List<Question> GetQuestionsFromTable(string tableName, int numberOfQuestions, bool isRandomized)
        {
            IEnumerable<Question> questions;

            switch (tableName)
            {
                case "Freud":
                    questions = FreudQuestions;
                    break;
                case "Carl Rogers":
                    questions = CarlRogersQuestions;
                    break;
                case "Jung":
                    questions = JungQuestions;
                    break;
                default:
                    return new List<Question>();
            }

            if (isRandomized)
            {
                questions = questions.OrderBy(q => Guid.NewGuid()).Take(numberOfQuestions);
            }
            else
            {
                questions = questions.Take(numberOfQuestions);
            }

            return questions.ToList();
        }


        // GET: QuizCreation/SelectedQuestions
        public IActionResult SelectedQuestions()
        {
            // Deserialize the list of questions from TempData
            var questionsJson = TempData["SelectedQuestions"] as string;
            var selectedQuestions = string.IsNullOrEmpty(questionsJson)
                ? new List<Question>()
                : JsonConvert.DeserializeObject<List<Question>>(questionsJson);

            var model = new SelectedQuestionsViewModel
            {
                Questions = selectedQuestions
            };

            return View(model);
        }


        // POST: QuizCreation/SaveQuestions
        [HttpPost]
        public IActionResult SaveQuestions(SelectedQuestionsViewModel model)
        {
            // Process and save the questions here
            // For now, just redirect to the Index action
            return RedirectToAction("Index");
        }
    }
}
