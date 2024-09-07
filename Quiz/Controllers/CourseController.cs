using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizDbContext.Data;
using Lesson.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CourseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Action to display a list of courses
        public async Task<IActionResult> Index()
        {
            var courses = await _context.Subjects
                .Include(s => s.CourseCard)
                .Include(s => s.SeminarCard)
                .ToListAsync();

            return View(courses); // Return the list of courses to the view
        }

        // Action to display details of a specific course
        public async Task<IActionResult> Details(int id)
        {
            var course = await _context.Subjects
                .Include(s => s.CourseCard)
                .Include(s => s.SeminarCard)
                .Include(s => s.Lessons)
                .Include(s => s.FinalExam)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (course == null)
            {
                return NotFound(); // Return 404 if the course is not found
            }

            return View(course); // Return the single course to the view
        }

        // Action to display lessons for a specific course or seminar
        // Action to display lessons and final exam details for a specific course
        public async Task<IActionResult> CourseLessons(int id, bool isSeminar = false)
        {
            // Fetch the subject along with its lessons
            var subject = await _context.Subjects
                .Include(s => s.Lessons)
                .Include(s => s.CourseCard)
                .Include(s => s.SeminarCard)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (subject == null)
            {
                return NotFound(); // Return 404 if the subject is not found
            }

            // Filter lessons based on whether it is a course or seminar
            var filteredLessons = subject.Lessons.Where(lesson =>
                (isSeminar && lesson.SeminarCardId == subject.SeminarCardId) ||
                (!isSeminar && lesson.CourseCardId == subject.CourseCardId)
            ).ToList();

            var viewModel = new CourseLessonsViewModel
            {
                Subject = subject,
                Lessons = filteredLessons
            };

            return View(viewModel);
        }



        // Action to display details of a specific lesson
        public async Task<IActionResult> LessonDetails(int lessonId)
        {
            var lessonDetails = await _context.LessonPreviews
                .FirstOrDefaultAsync(lp => lp.Id == lessonId);

            if (lessonDetails == null)
            {
                return NotFound(); // Return 404 if the lesson is not found
            }

            // Simulate lesson navigation (you might replace this with real data from your database)
            var lessonProgress = await _context.LessonProgresses
                .FirstOrDefaultAsync(lp => lp.LessonId == lessonId);

            ViewBag.IsLessonFinished = lessonProgress?.IsFinished ?? false;
            ViewBag.PreviousLessonId = lessonId > 1 ? lessonId - 1 : (int?)null;
            ViewBag.NextLessonId = lessonId < 5 ? lessonId + 1 : (int?)null; // Adjust based on your logic

            return View(lessonDetails);
        }


        [HttpPost]
        public async Task<IActionResult> FinishLesson(int lessonId)
        {
            var lessonProgress = await _context.LessonProgresses
                .FirstOrDefaultAsync(lp => lp.LessonId == lessonId);

            if (lessonProgress == null)
            {
                lessonProgress = new LessonProgress
                {
                    LessonId = lessonId,
                    IsFinished = true
                };
                _context.LessonProgresses.Add(lessonProgress);
            }
            else
            {
                lessonProgress.IsFinished = true;
                _context.LessonProgresses.Update(lessonProgress);
            }

            await _context.SaveChangesAsync();

            // Redirect to the lesson details page to refresh state
            return RedirectToAction("LessonDetails", new { lessonId });
        }

        // GET: Course/EditLesson/1
        public async Task<IActionResult> EditLesson(int id)
        {
            var lesson = await _context.LessonPreviews.FindAsync(id);
            if (lesson == null)
            {
                return NotFound(); // Return 404 if the lesson is not found
            }
            return View("EditLesson", lesson); // Return the view with the lesson
        }


        // POST: Course/EditLesson/1
        [HttpPost]
        public async Task<IActionResult> EditLesson(int id, LessonPreview lesson)
        {
            if (!ModelState.IsValid)
            {
                return View(lesson); // Return the view with validation errors
            }

            try
            {
                var existingLesson = await _context.LessonPreviews.FindAsync(id);
                if (existingLesson == null)
                {
                    return NotFound(); // Return 404 if the lesson is not found
                }

                existingLesson.LessonTitle = lesson.LessonTitle;
                existingLesson.LessonText = lesson.LessonText;
                existingLesson.SubjectId = lesson.SubjectId; // Ensure this is a valid ID

                var subjectExists = await _context.Subjects.AnyAsync(s => s.Id == lesson.SubjectId);
                if (!subjectExists)
                {
                    ModelState.AddModelError("", "Invalid SubjectId.");
                    return View(lesson); // Return view with error
                }

                _context.Entry(existingLesson).State = EntityState.Modified;

                await _context.SaveChangesAsync(); // Save changes
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Error: {ex.InnerException?.Message}");
                ModelState.AddModelError("", "Unable to save changes. Try again later.");
                return View(lesson); // Return view with error
            }

            return RedirectToAction("LessonDetails", new { id = id }); // Redirect to the lesson details page
        }



        private bool LessonExists(int id)
        {
            return _context.LessonPreviews.Any(e => e.Id == id);
        }
    }
}
