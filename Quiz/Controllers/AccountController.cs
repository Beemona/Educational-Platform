using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Authentication.Models;
using QuizDbContext.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Quiz.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AccountController> _logger;

        public AccountController(ApplicationDbContext context, ILogger<AccountController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Account/Login
        public IActionResult Login() => View();

        // POST: Account/Login
        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == userName && u.PasswordHash == HashPassword(password));

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                    new Claim(ClaimTypes.Role, user.Role ?? string.Empty),
                    new Claim("UserId", (user.Id ?? 0).ToString())
                };

                await AddUserClaims(claims, user);

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties { IsPersistent = true };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View();
        }

        // Helper method to add claims based on user role
        private async Task AddUserClaims(List<Claim> claims, User user)
        {
            if (user is Student student)
            {
                await AddStudentClaims(claims, student);
            }
            else if (user is Teacher teacher)
            {
                await AddTeacherClaims(claims, teacher);
            }
        }

        // Add claims for Student
        private async Task AddStudentClaims(List<Claim> claims, Student student)
        {
            if (student.FacultyId.HasValue)
            {
                var faculty = await _context.Faculties.FindAsync(student.FacultyId.Value);
                claims.Add(new Claim("FacultyId", faculty?.Id.ToString() ?? string.Empty));
            }

            if (student.EducationTypeId.HasValue)
            {
                var educationType = await _context.EducationTypes.FindAsync(student.EducationTypeId.Value);
                claims.Add(new Claim("EducationType", educationType?.Name ?? string.Empty));
            }

            if (student.SpecializationId.HasValue)
            {
                var specialization = await _context.Specializations.FindAsync(student.SpecializationId.Value);
                claims.Add(new Claim("Specialization", specialization?.Name ?? string.Empty));
            }

            var accessibleSubjects = await _context.Subjects
                .Where(s => s.Students.Any(st => st.Id == student.Id))
                .ToListAsync();

            claims.Add(new Claim("Subjects", string.Join(",", accessibleSubjects.Select(s => s.Title))));
        }

        // Add claims for Teacher
        private async Task AddTeacherClaims(List<Claim> claims, Teacher teacher)
        {
            var faculties = await _context.Faculties
                .Where(f => teacher.Faculties.Any(tf => tf.Id == f.Id))
                .ToListAsync();

            claims.Add(new Claim("Faculties", string.Join(",", faculties.Select(f => f.Name))));

            var taughtSubjects = await _context.Subjects
                .Where(s => teacher.TaughtSubjects.Any(ts => ts.Id == s.Id))
                .ToListAsync();

            claims.Add(new Claim("TaughtSubjects", string.Join(",", taughtSubjects.Select(s => s.Title))));
        }

        // GET: Account/Register
        [HttpGet]
        public async Task<IActionResult> Register(int? facultyId, int? educationTypeId)
        {
            var model = new UserRegistrationViewModel
            {
                Faculties = await GetFacultiesAsync(),
                EducationTypes = await GetEducationTypesAsync(),
                Specializations = await GetSpecializationsAsync(facultyId, educationTypeId),
                FacultyId = facultyId ?? 0,
                EducationTypeId = educationTypeId ?? 0
            };

            return View(model);
        }

        // POST: Account/Register
        [HttpPost]
        public async Task<IActionResult> Register(string role, int? facultyId, int? educationTypeId)
        {
            if (string.IsNullOrEmpty(role))
            {
                return RedirectToAction("Register");
            }

            var model = await GetRoleSpecificModel(role, facultyId, educationTypeId);
            return View(model);
        }

        // General method to handle role-specific data loading
        private async Task<UserRegistrationViewModel> GetRoleSpecificModel(string role, int? facultyId, int? educationTypeId)
        {
            var model = new UserRegistrationViewModel { Role = role };

            switch (role)
            {
                case "Student":
                    model.Faculties = await GetFacultiesAsync();
                    model.EducationTypes = await GetEducationTypesAsync();
                    model.Specializations = await GetSpecializationsAsync(facultyId, educationTypeId);
                    break;
                case "Teacher":
                    model.Departments = await _context.Departments.ToListAsync();
                    break;
                case "Admin":
                    // No specific data needed for Admin
                    break;
            }

            return model;
        }

        // Helper methods to retrieve data
        private async Task<List<Faculty>> GetFacultiesAsync() => await _context.Faculties.ToListAsync();
        private async Task<List<EducationType>> GetEducationTypesAsync() => await _context.EducationTypes.ToListAsync();
        private async Task<List<Specialization>> GetSpecializationsAsync(int? facultyId, int? educationTypeId)
        {
            return await _context.Specializations
                .Where(s => (!facultyId.HasValue || s.FacultyId == facultyId) &&
                            (!educationTypeId.HasValue || s.EducationTypeId == educationTypeId))
                .ToListAsync();
        }

        [HttpGet]
        public async Task<IActionResult> GetSpecializations(int? facultyId, int? educationTypeId)
        {
            // Use the GetSpecializationsAsync method to retrieve filtered specializations
            var specializations = await GetSpecializationsAsync(facultyId, educationTypeId);

            // Return the specializations as JSON
            var result = specializations.Select(s => new { id = s.Id, name = s.Name }).ToList();

            return Json(result);
        }

        // Logic for loading dynamic fields based on role (preserved as requested)
        [HttpGet]
        public async Task<IActionResult> LoadRoleSpecificFields(string role, int? facultyId, int? educationTypeId)
        {
            switch (role)
            {
                case "Student":
                    var studentModel = new StudentRegistrationViewModel
                    {
                        Faculties = await GetFacultiesAsync(),
                        EducationTypes = await GetEducationTypesAsync(),
                        Specializations = await GetSpecializationsAsync(facultyId, educationTypeId)
                    };
                    return PartialView("_StudentRegistrationPartial", studentModel);

                case "Teacher":
                    var teacherModel = new TeacherRegistrationViewModel
                    {
                        Departments = await _context.Departments.ToListAsync()
                    };
                    return PartialView("_TeacherRegistrationPartial", teacherModel);

                case "Admin":
                    var adminModel = new AdminRegistrationViewModel();
                    return PartialView("_AdminRegistrationPartial", adminModel);

                default:
                    return PartialView("_RegisterErrorPartial");
            }
        }

        // Logout action
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        // Helper method to hash password
        private string HashPassword(string password)
        {
            return password.GetHashCode().ToString(); // Simple hashing for demonstration purposes
        }

        // AJAX method for fetching faculties, education types, and specializations
        [HttpGet]
        public async Task<IActionResult> GetDropdownData(string type, int? facultyId, int? educationTypeId)
        {
            switch (type.ToLower())
            {
                case "faculties":
                    return Json(await GetFacultiesAsync());
                case "educationtypes":
                    return Json(await GetEducationTypesAsync());
                case "specializations":
                    return Json(await GetSpecializationsAsync(facultyId, educationTypeId));
                default:
                    return BadRequest("Invalid data type");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetSubjects(int facultyId)
        {
            var subjects = await _context.Subjects
                .Where(s => s.FacultyId == facultyId)
                .Select(s => new { s.Id, s.Title })
                .ToListAsync();

            return Json(subjects);
        }

        // Teacher-specific registration page (if needed)
        [HttpGet]
        public async Task<IActionResult> RegisterTeacher()
        {
            var departments = await _context.Departments.ToListAsync();
            var teacherModel = new TeacherRegistrationViewModel
            {
                Departments = departments,
                Role = "Teacher"
            };

            return PartialView("_TeacherRegistrationPartial", teacherModel);
        }
    }
}
