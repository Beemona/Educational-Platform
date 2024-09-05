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
                .Where(u => u.UserName == userName && u.PasswordHash == HashPassword(password))
                .FirstOrDefaultAsync();

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                    new Claim(ClaimTypes.Role, user.Role ?? string.Empty),
                      new Claim("UserId", (user.Id ?? 0).ToString())
                };

                if (user is Student student)
                {
                    // Load related data for Student
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

                    if (student != null)
                    {
                        var accessibleSubjects = await _context.Subjects
                            .Where(s => s.Students != null && s.Students.Any(st => st.Id == student.Id))
                            .ToListAsync();
                        claims.Add(new Claim("Subjects", string.Join(",", accessibleSubjects.Select(s => s.Title))));
                    }
                }
                else if (user is Teacher teacher)
                {
                    // Ensure Faculties collection is initialized or not null
                    var teacherFaculties = teacher.Faculties ?? new List<Faculty>();

                    // Retrieve the faculties associated with the teacher
                    var faculties = await _context.Faculties
                        .Where(f => teacherFaculties.Any(tf => tf.Id == f.Id))
                        .ToListAsync();

                    claims.Add(new Claim("Faculties", string.Join(",", faculties.Select(f => f.Name))));

                    // Ensure TaughtSubjects collection is initialized or not null
                    var teacherTaughtSubjects = teacher.TaughtSubjects ?? new List<Subject>();

                    // Retrieve the subjects taught by the teacher
                    var taughtSubjects = await _context.Subjects
                        .Where(s => teacherTaughtSubjects.Any(ts => ts.Id == s.Id))
                        .ToListAsync();

                    claims.Add(new Claim("TaughtSubjects", string.Join(",", taughtSubjects.Select(s => s.Title))));
                }
            

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
        [HttpGet]
        public IActionResult Register()
        {
            ViewData["Faculties"] = _context.Faculties.ToList();
            ViewData["Departments"] = _context.Departments.ToList();
            ViewData["Specializations"] = new List<Specialization>(); // Empty initially
            return View(new UserRegistrationViewModel());
        }

        // POST: Account/Register
        [HttpPost]
        [Route("Account/Register/{role}")]
        // POST: Account/Register
        public IActionResult Register(string role)
        {
            switch (role)
            {
                case "Student":
                    var studentModel = new StudentRegistrationViewModel
                    {
                        Faculties = _context.Faculties.ToList(),
                        Specializations = new List<Specialization>(), // Load based on FacultyId dynamically
                        Role = role
                    };
                    return View("RegisterStudent", studentModel); // Make sure this view exists

                case "Teacher":
                    var teacherModel = new TeacherRegistrationViewModel
                    {
                        Departments = _context.Departments.ToList(),
                        Role = role
                    };
                    return View("RegisterTeacher", teacherModel); // Make sure this view exists

                case "Admin":
                    var adminModel = new AdminRegistrationViewModel
                    {
                        Role = role
                    };
                    return View("RegisterAdmin", adminModel); // Make sure this view exists

                default:
                    ModelState.AddModelError("", "Invalid role selected.");
                    return View("Register", new UserRegistrationViewModel());
            }
        }



        //[HttpPost]
        //public async Task<IActionResult> Register(string role, string userName, string email, string name, string passwordHash, int? facultyId, string educationType, int? specializationId, int? departmentId)
        //{
        //    var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        //    Console.WriteLine("Registration started");

        //    if (string.IsNullOrEmpty(role))
        //    {
        //        ModelState.AddModelError("", "Role is required.");
        //        return View();
        //    }

        //    User model = null;

        //    try
        //    {
        //        switch (role)
        //        {
        //            case "Student":

        //                if (!facultyId.HasValue || !await _context.Faculties.AnyAsync(f => f.Id == facultyId))
        //                {
        //                    ModelState.AddModelError("", "Invalid or missing Faculty.");
        //                    return View();
        //                }

        //                model = new Student
        //                {
        //                    UserName = userName,
        //                    Email = email,
        //                    Name = name,
        //                    PasswordHash = HashPassword(passwordHash),
        //                    FacultyId = facultyId,
        //                    SpecializationId = specializationId,
        //                    Role = "Student"
        //                };
        //                break;

        //            case "Teacher":
        //                if (!departmentId.HasValue || await _context.Departments.FindAsync(departmentId) == null)
        //                {
        //                    ModelState.AddModelError("", "Invalid or missing Department.");
        //                    return View();
        //                }

        //                model = new Teacher
        //                {
        //                    UserName = userName,
        //                    Email = email,
        //                    Name = name,
        //                    PasswordHash = HashPassword(passwordHash),
        //                    DepartmentId = departmentId,
        //                    Role = "Teacher"
        //                };
        //                break;

        //            case "Admin":
        //                model = new Admin
        //                {
        //                    UserName = userName,
        //                    Email = email,
        //                    Name = name,
        //                    PasswordHash = HashPassword(passwordHash),
        //                    Role = "Admin"
        //                };
        //                break;

        //            default:
        //                ModelState.AddModelError("", "Invalid role.");
        //                return View();
        //        }

        //        if (ModelState.IsValid)
        //        {
        //            _context.Users.Add(model);
        //            await _context.SaveChangesAsync();
        //            Console.WriteLine("Registration completed successfully");
        //            return RedirectToAction("Login");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error occurred: {ex.Message}");
        //        ModelState.AddModelError("", $"An error occurred while saving the user: {ex.Message}");
        //    }

        //    stopwatch.Stop();
        //    Console.WriteLine($"Registration ended. Duration: {stopwatch.ElapsedMilliseconds} ms");

        //    ViewData["Faculties"] = await _context.Faculties.ToListAsync();
        //    ViewData["Departments"] = await _context.Departments.ToListAsync();

        //    if (role == "Student" && facultyId.HasValue)
        //    {
        //        ViewData["Specializations"] = await _context.Specializations
        //            .Where(s => s.FacultyId == facultyId && s.EducationType != null && s.EducationType.Name == educationType)
        //            .ToListAsync();
        //    }

        //    return View();
        //}


        // GET: Account/Logout
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        // API methods for fetching specializations and subjects
        [HttpGet]
        public JsonResult GetSpecializations(int facultyId, string educationType)
        {
            var specializations = _context.Specializations
                .Where(s => s.FacultyId == facultyId && s.EducationType != null && s.EducationType.Name == educationType)
                .Select(s => new { id = s.Id, name = s.Name })
                .ToList();

            return Json(new { specializations });
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
                .Where(s => s.Specializations != null && s.Specializations.Any(sp => sp.Id == specializationId))
                .Select(s => new { s.Id, s.Title }) // Changed `Name` to `Title`
                .ToList();

            return Json(subjects);
        }

        [HttpGet]
        public IActionResult GetDepartments()
        {
            var departments = _context.Departments
                .Select(d => new { d.Id, d.Name })
                .ToList();

            return Json(new { departments });
        }


        // Hash password (for demonstration, use a proper hashing mechanism in production)
        private string HashPassword(string password)
        {
            // Simple hash function for demo purposes, replace with a proper one in production
            return password.GetHashCode().ToString();
        }
    }
}
