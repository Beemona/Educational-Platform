using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Authentication.Models;
using System.Linq;
using System.Threading.Tasks;
using QuizDbContext.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Collections.Generic;
using Lesson.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Quiz.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password)
        {
            var user = await _context.Users
                .Include(u => u.Faculty)
                .Include(u => u.AccessibleSubjects) // Include AccessibleSubjects
                .Where(u => u.UserName == userName && u.PasswordHash == HashPassword(password))
                .FirstOrDefaultAsync();

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim("UserId", user.Id.ToString()),
                    new Claim("FacultyId", user.FacultyId.ToString() ?? string.Empty), // Handle null values
                    new Claim("EducationType", user.EducationType ?? string.Empty),
                    new Claim("Subjects", string.Join(",", user.AccessibleSubjects?.Select(s => s.Title) ?? new List<string>()))
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties { IsPersistent = true };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View();
        }

        // GET: Account/Register
        public IActionResult Register()
        {
            ViewBag.Faculties = _context.Faculties.ToList();
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        public async Task<IActionResult> Register(User model)
        {
            if (ModelState.IsValid)
            {
                // Ensure the necessary properties are set
                if (model.FacultyId.HasValue)
                {
                    var faculty = await _context.Faculties.FindAsync(model.FacultyId.Value);
                    if (faculty != null)
                    {
                        model.Faculty = faculty;
                    }
                }

                // Add the user to the database
                model.PasswordHash = HashPassword(model.PasswordHash); // Adjust as necessary
                _context.Users.Add(model);
                await _context.SaveChangesAsync();

                return RedirectToAction("Login");
            }

            // Fetch faculties and specializations in case of errors
            ViewBag.Faculties = await _context.Faculties.ToListAsync();
            return View(model);
        }



        // GET: Account/Logout
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        // API methods for fetching specializations and subjects
        public JsonResult GetSpecializations(int facultyId)
        {
            var specializations = _context.Specializations
                .Where(s => s.FacultyId == facultyId)
                .Select(s => new { s.Id, s.Name }) // Ensure `Name` is correct in your model
                .ToList();
            return Json(specializations);
        }

        public JsonResult GetSubjects(int facultyId)
        {
            var subjects = _context.Subjects
                .Where(s => s.FacultyId == facultyId)
                .Select(s => new { s.Id, s.Title }) // Changed `Name` to `Title`
                .ToList();
            return Json(subjects);
        }

        public JsonResult GetSubjectsBySpecialization(int specializationId)
        {
            var subjects = _context.Subjects
                .Where(s => s.Specializations.Any(sp => sp.Id == specializationId))
                .Select(s => new { s.Id, s.Title }) // Changed `Name` to `Title`
                .ToList();
            return Json(subjects);
        }

        // Hash password (for demonstration, use a proper hashing mechanism in production)
        private string HashPassword(string password)
        {
            // Simple hash function for demo purposes, replace with a proper one in production
            return password.GetHashCode().ToString();
        }
    }
}
