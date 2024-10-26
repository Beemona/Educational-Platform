using Microsoft.AspNetCore.Mvc;
using StudentModel.Models;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using QuizDbContext.Data;
using Microsoft.EntityFrameworkCore;


namespace Quiz.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }


        // Dashboard main view with cards
        public IActionResult TeacherDashboard()
        {
            return View();
        }

        // Subjects view
        public async Task<IActionResult> Subjects()
        {
            // Fetch subjects from the database
            var subjects = await _context.Subjects
                .Select(s => new
                {
                    s.Id,
                    s.Title // Ensure the title matches your model's property name
                })
                .ToListAsync();

            ViewBag.Subjects = subjects; // Pass the subjects to the ViewBag
            return View(); // Return the Subjects view
        }

        private List<string> subjects = new List<string>
        {
            "Forensic Psychology",
            "Clinical Psychology",
            "Trauma Therapy",
            "Profiling"
        };

        // Exams view
        public IActionResult Exams()
        {
            var exams = new List<string>
            {
               "Published Exams",
               "Saved Exams",
               "Create Quiz",
               "Create Grade Formula"
            };

            ViewBag.Exams = exams;
            return View(); // Create an Exams view for this action
        }

        // Feedback view
        public IActionResult Feedback()
        {
            var feedback = new List<string>
            {
                "Math: Good understanding.",
                "Physics: Needs improvement in theory.",
                "Chemistry: Excellent work.",
                "Biology: Improve practical skills."
            };

            ViewBag.Feedback = feedback;
            return View(); // Create a Feedback view for this action
        }

        // Student Grades Details view
        //        public IActionResult StudentGradesDetails()
        //        {
        //            // Hardcoded student results for display
        //            var studentResults = new List<StudentResult>
        //            {
        //             // Bob
        //new StudentResult { Id = 6, StudentName = "Bob", Score = 90, Grade = 9, TotalQuestions = 20, SubjectId = 1, UserId = 2 }, // Forensic Psychology
        //new StudentResult { Id = 7, StudentName = "Bob", Score = 70, Grade = 7, TotalQuestions = 20, SubjectId = 3, UserId = 2 }, // Trauma Therapy
        //new StudentResult { Id = 8, StudentName = "Bob", Score = 88, Grade = 9, TotalQuestions = 20, SubjectId = 2, UserId = 2 }, // Clinical Psychology
        //new StudentResult { Id = 9, StudentName = "Bob", Score = 60, Grade = 6, TotalQuestions = 20, SubjectId = 3, UserId = 2 }, // Trauma Therapy
        //new StudentResult { Id = 10, StudentName = "Bob", Score = 82, Grade = 8, TotalQuestions = 20, SubjectId = 1, UserId = 2 }, // Forensic Psychology

        //// Charlie
        //new StudentResult { Id = 11, StudentName = "Charlie", Score = 75, Grade = 8, TotalQuestions = 20, SubjectId = 2, UserId = 3 }, // Clinical Psychology
        //new StudentResult { Id = 12, StudentName = "Charlie", Score = 85, Grade = 9, TotalQuestions = 20, SubjectId = 1, UserId = 3 }, // Forensic Psychology
        //new StudentResult { Id = 13, StudentName = "Charlie", Score = 80, Grade = 8, TotalQuestions = 20, SubjectId = 3, UserId = 3 }, // Trauma Therapy
        //new StudentResult { Id = 14, StudentName = "Charlie", Score = 90, Grade = 9, TotalQuestions = 20, SubjectId = 1, UserId = 3 }, // Forensic Psychology
        //new StudentResult { Id = 15, StudentName = "Charlie", Score = 70, Grade = 7, TotalQuestions = 20, SubjectId = 2, UserId = 3 }, // Clinical Psychology

        //// David
        //new StudentResult { Id = 16, StudentName = "David", Score = 65, Grade = 6, TotalQuestions = 20, SubjectId = 3, UserId = 4 }, // Trauma Therapy
        //new StudentResult { Id = 17, StudentName = "David", Score = 90, Grade = 9, TotalQuestions = 20, SubjectId = 1, UserId = 4 }, // Forensic Psychology
        //new StudentResult { Id = 18, StudentName = "David", Score = 80, Grade = 8, TotalQuestions = 20, SubjectId = 2, UserId = 4 }, // Clinical Psychology
        //new StudentResult { Id = 19, StudentName = "David", Score = 72, Grade = 7, TotalQuestions = 20, SubjectId = 3, UserId = 4 }, // Trauma Therapy
        //new StudentResult { Id = 20, StudentName = "David", Score = 88, Grade = 9, TotalQuestions = 20, SubjectId = 1, UserId = 4 }, // Forensic Psychology

        //// Emma
        //new StudentResult { Id = 21, StudentName = "Emma", Score = 91, Grade = 9, TotalQuestions = 20, SubjectId = 2, UserId = 5 }, // Clinical Psychology
        //new StudentResult { Id = 22, StudentName = "Emma", Score = 77, Grade = 8, TotalQuestions = 20, SubjectId = 3, UserId = 5 }, // Trauma Therapy
        //new StudentResult { Id = 23, StudentName = "Emma", Score = 84, Grade = 9, TotalQuestions = 20, SubjectId = 1, UserId = 5 }, // Forensic Psychology
        //new StudentResult { Id = 24, StudentName = "Emma", Score = 69, Grade = 6, TotalQuestions = 20, SubjectId = 2, UserId = 5 }  // Clinical Psychology

        //            };

        //            ViewBag.StudentResults = studentResults;
        //            return View();
        //        }

        [HttpGet]
        public IActionResult Grades()
        {
            // Populate ViewBag with data for the dropdowns
            ViewBag.Subjects = new List<string> { "Forensic Psychology", "Clinical Psychology", "Trauma Therapy", "Profiling" };
            ViewBag.Years = new List<string> { "1", "2", "3" };
            ViewBag.Groups = new List<string> { "A", "B", "C" };

            return View();
        }
        private List<StudentGrade> studentGrades = new List<StudentGrade>
        {
            // Bob - Group A, Year 1
            new StudentGrade { Name = "Bob", Year = 1, Group = "A", Subject = "Forensic Psychology", CoursePoints = 90, SeminarPoints = 90, FinalExamPoints = 70, BonusPoints = 5 },
            new StudentGrade { Name = "Bob", Year = 1, Group = "A", Subject = "Trauma Therapy", CoursePoints = 25, SeminarPoints = 20, FinalExamPoints = 20, BonusPoints = 5 },
            new StudentGrade { Name = "Bob", Year = 1, Group = "A", Subject = "Clinical Psychology", CoursePoints = 30, SeminarPoints = 20, FinalExamPoints = 38, BonusPoints = 5 },
            new StudentGrade { Name = "Bob", Year = 1, Group = "A", Subject = "Profiling", CoursePoints = 20, SeminarPoints = 20, FinalExamPoints = 20, BonusPoints = 0},

            // Charlie - Group B, Year 1
            new StudentGrade { Name = "Charlie", Year = 1, Group = "B", Subject = "Clinical Psychology", CoursePoints = 28, SeminarPoints = 18, FinalExamPoints = 29, BonusPoints = 0},
            new StudentGrade { Name = "Charlie", Year = 1, Group = "B", Subject = "Forensic Psychology", CoursePoints = 30, SeminarPoints = 20, FinalExamPoints = 35, BonusPoints = 0 },
            new StudentGrade { Name = "Charlie", Year = 1, Group = "B", Subject = "Trauma Therapy", CoursePoints = 30, SeminarPoints = 20, FinalExamPoints = 30, BonusPoints = 0 },
            new StudentGrade { Name = "Charlie", Year = 1, Group = "B", Subject = "Profiling", CoursePoints = 30, SeminarPoints = 20, FinalExamPoints = 40, BonusPoints = 0 },

            // David - Group C, Year 1
            new StudentGrade { Name = "David", Year = 1, Group = "C", Subject = "Trauma Therapy", CoursePoints = 20, SeminarPoints = 20, FinalExamPoints = 25, BonusPoints = 0 },
            new StudentGrade { Name = "David", Year = 1, Group = "C", Subject = "Forensic Psychology", CoursePoints = 30, SeminarPoints = 20, FinalExamPoints = 40, BonusPoints = 0 },
            new StudentGrade { Name = "David", Year = 1, Group = "C", Subject = "Clinical Psychology", CoursePoints = 30, SeminarPoints = 20, FinalExamPoints = 30, BonusPoints = 0 },
            new StudentGrade { Name = "David", Year = 1, Group = "C", Subject = "Profiling", CoursePoints = 30, SeminarPoints = 20, FinalExamPoints = 38, BonusPoints = 0 },

            // Emma - Group A, Year 2
            new StudentGrade { Name = "Emma", Year = 2, Group = "A", Subject = "Clinical Psychology", CoursePoints = 30, SeminarPoints = 20, FinalExamPoints = 40, BonusPoints = 1},
            new StudentGrade { Name = "Emma", Year = 2, Group = "A", Subject = "Trauma Therapy", CoursePoints = 25, SeminarPoints = 20, FinalExamPoints = 30, BonusPoints = 2 },
            new StudentGrade { Name = "Emma", Year = 2, Group = "A", Subject = "Forensic Psychology", CoursePoints = 30, SeminarPoints = 20, FinalExamPoints = 34, BonusPoints = 0 },
            new StudentGrade { Name = "Emma", Year = 2, Group = "A", Subject = "Profiling", CoursePoints = 20, SeminarPoints = 20, FinalExamPoints = 29, BonusPoints = 0 }
        };

        [HttpPost]
        public IActionResult ShowGrades(string subject, string year, string group)
        {
            // Filter the student results based on selected subject, year, and group.
            var filteredResults = studentGrades
                .Where(s => s.Subject == subject && s.Year.ToString() == year && s.Group == group)
                .ToList();

            var gradeFormula = GetGradeFormulaFromCookie(subject);

            foreach (var student in filteredResults)
            {
                student.Grade = student.CalculateGrade(gradeFormula);
            }

            ViewBag.FilteredResults = filteredResults;
            ViewBag.Subject = subject;
            ViewBag.Year = year;
            ViewBag.Group = group;
            ViewBag.GradeFormula = gradeFormula;

            return View("FilteredResults"); // Ensure you have a view named FilteredResults
        }

        // This action will load the filter page with dropdowns
        public IActionResult FilterStudentGrades()
        {
            ViewBag.Subjects = new List<string> { "Forensic Psychology", "Clinical Psychology", "Trauma Therapy" };
            ViewBag.Years = new List<int> { 1, 2, 3 };
            ViewBag.Groups = new List<string> { "A", "B", "C" };

            return View(); // Create a view for filtering
        }

        // This action handles the filtering based on the selected values
        [HttpPost]
        public IActionResult FilteredResults(string subject, int year, string group)
        {
            var filteredResults = studentGrades
                .Where(s => s.Subject == subject && s.Year == year && s.Group == group)
                .ToList();

            ViewBag.FilteredResults = filteredResults;

            return View("FilteredResults"); // Create a view to show the filtered results
        }

        // Action to display the Grade Formula creation page
        public IActionResult CreateGradeFormula()
        {
            ViewBag.Subjects = subjects;
            return View();
        }

        [HttpPost]
        public IActionResult CreateGradeFormula(GradeFormula model)
        {
            if (ModelState.IsValid)
            {
                // Calculate the total weight
                int totalWeight = model.SeminarWeight + model.CourseWeight + model.FinalExamWeight;

                // Check if the total weight is 100
                if (totalWeight != 100)
                {
                    ModelState.AddModelError("", "The total weight must equal 100%.");
                    ViewBag.Subjects = subjects; // Re-pass subjects to the view
                    return View(model);
                }

                // Calculate grades for students based on the formula
                var calculatedGrades = CalculateGrades(model);

                // Save the grade formula in a cookie
                SaveGradeFormulaInCookie(model);

                ViewBag.CalculatedGrades = calculatedGrades;
                ViewBag.Message = "Grade formula saved successfully!";
            }

            ViewBag.Subjects = subjects; // Re-pass subjects to the view if validation fails
            return View(model);
        }

        private void SaveGradeFormulaInCookie(GradeFormula formula)
        {
            var options = new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(30),
                HttpOnly = true,
                Secure = true
            };

            // Serialize the formula to JSON and store it with a key that includes the subject name
            string sanitizedSubject = formula.Subject.Replace(" ", "_");
            string jsonFormula = JsonSerializer.Serialize(formula);
            string cookieKey = $"GradeFormula_{sanitizedSubject}"; // Use sanitized subject
            Response.Cookies.Append(cookieKey, jsonFormula, options);
        }

        private List<StudentGrade> CalculateGrades(GradeFormula formula)
        {
            var calculatedGrades = new List<StudentGrade>();

            foreach (var student in studentGrades)
            {
                if (student.Subject == formula.Subject)
                {
                    double seminarPoints = formula.SeminarWeight > 0 ? (student.SeminarPoints * formula.SeminarWeight) / 100.0 : 0;
                    double coursePoints = formula.CourseWeight > 0 ? (student.CoursePoints * formula.CourseWeight) / 100.0 : 0;
                    double finalExamPoints = formula.FinalExamWeight > 0 ? (student.FinalExamPoints * formula.FinalExamWeight) / 100.0 : 0;

                    // Calculate total score
                    double totalScore = seminarPoints + coursePoints + finalExamPoints;

                    // Calculate final grade (assuming a default max grade of 10)
                    double finalGrade = Math.Round((totalScore / formula.DefaultPoints) * 10 + 0.5); // Round to the nearest integer
                    finalGrade = Math.Clamp(finalGrade, 1, 10); // Ensure the grade is between 1 and 10

                    calculatedGrades.Add(new StudentGrade
                    {
                        Name = student.Name,
                        Subject = student.Subject,
                        Grade = (int)finalGrade // Cast to int since grades are whole numbers
                    });
                }
            }

            return calculatedGrades;
        }

        private GradeFormula GetGradeFormulaFromCookie(string subject)
        {
            // Sanitize subject name for the cookie key
            string sanitizedSubject = subject.Replace(" ", "_");
            string cookieKey = $"GradeFormula_{sanitizedSubject}";

            if (Request.Cookies.TryGetValue(cookieKey, out var jsonFormula))
            {
                return JsonSerializer.Deserialize<GradeFormula>(jsonFormula);
            }

            return null; // Return null if a formula for the specific subject doesn't exist
        }
    }
}
