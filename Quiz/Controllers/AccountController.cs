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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

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
        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password)
        {
            _logger.LogInformation("Login attempt for user: {UserName}", userName);

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                _logger.LogWarning("Username or password is empty.");
                ModelState.AddModelError("", "Username and password are required.");
                return View();
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            if (user == null)
            {
                _logger.LogWarning("Invalid login attempt. User not found: {UserName}", userName);
                ModelState.AddModelError("", "Invalid login attempt. User not found.");
                return View();
            }

            var passwordVerificationResult = VerifyPassword(user, password);
            if (passwordVerificationResult != PasswordVerificationResult.Success)
            {
                _logger.LogWarning("Invalid login attempt. Incorrect password for user: {UserName}", userName);
                ModelState.AddModelError("", "Invalid login attempt. Incorrect password.");
                return View();
            }

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
        new Claim(ClaimTypes.Role, user.Role ?? string.Empty),
        new Claim("UserId", user.Id.ToString())
    };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties { IsPersistent = true };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            _logger.LogInformation("User {UserName} logged in successfully.", userName);
            return RedirectToAction("MyAccount", "Account");
        }

        // Password verification
        private PasswordVerificationResult VerifyPassword(User user, string password)
        {
            var passwordHasher = new PasswordHasher<User>(); // Use the User type here
            return passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
        }

        // Password verification
        private bool VerifyPassword(string hashedPassword, string password)
        {
            return hashedPassword == HashPassword(password);
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

        // GET: Account/Register (Select Role)
        [HttpGet]
        public IActionResult Register()
        {
            var model = new SelectRoleViewModel();
            return View(model);
        }

        // POST: Account/Register (Handle Role Selection)
        [HttpPost]
        public IActionResult Register(SelectRoleViewModel model)
        {
            if (string.IsNullOrEmpty(model.Role))
            {
                ModelState.AddModelError("", "Please select a role.");
                return View(model); // Return to the same view if no role selected
            }

            // Redirect to the specific registration page based on the role
            switch (model.Role)
            {
                case "Student":
                    return RedirectToAction("StudentRegister");
                case "Teacher":
                    return RedirectToAction("TeacherRegister");
                case "Admin":
                    return RedirectToAction("AdminRegister");
                default:
                    ModelState.AddModelError("", "Invalid role selected.");
                    return View(model);
            }
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
            var specializations = await GetSpecializationsAsync(facultyId, educationTypeId);
            var result = specializations.Select(s => new { id = s.Id, name = s.Name }).ToList();

            return Json(result); // Return filtered specializations as JSON
        }


        // Logout action
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
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
        //[HttpGet]
        //public async Task<IActionResult> RegisterTeacher()
        //{
        //    var departments = await _context.Departments.ToListAsync();
        //    var teacherModel = new TeacherRegistrationViewModel
        //    {
        //        Departments = departments,
        //        Role = "Teacher"
        //    };

        //    return PartialView("_TeacherRegistrationPartial", teacherModel);
        //}

        //NEW REGISTRATION - STANDALONE VIEWS:
        // Updated redirection to standalone views instead of partials
        [HttpGet]
        public IActionResult SelectRole()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RedirectToRoleForm(SelectRoleViewModel model)
        {
            if (string.IsNullOrEmpty(model.Role))
            {
                ModelState.AddModelError("", "Please select a role.");
                return View("SelectRole", model);
            }

            switch (model.Role)
            {
                case "Student":
                    return RedirectToAction("StudentRegister");
                case "Teacher":
                    return RedirectToAction("TeacherRegister");
                case "Admin":
                    return RedirectToAction("AdminRegister");
                default:
                    ModelState.AddModelError("", "Invalid role selected.");
                    return View("SelectRole", model);
            }
        }


        // Student registration GET method
        [HttpGet]
        public async Task<IActionResult> StudentRegister()
        {
            var model = new StudentRegistrationViewModel
            {
                Faculties = await GetFacultiesAsync(),
                EducationTypes = await GetEducationTypesAsync(),
                Specializations = await GetSpecializationsAsync(null, null)
            };
            return View(model);
        }

        // Teacher registration GET method
        [HttpGet]
        public async Task<IActionResult> TeacherRegister()
        {
            var model = new TeacherRegistrationViewModel
            {
                Departments = await _context.Departments.ToListAsync()
            };
            return View(model);
        }

        // Admin registration GET method
        [HttpGet]
        public IActionResult AdminRegister()
        {
            var model = new AdminRegistrationViewModel();
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> RegisterStudent(StudentRegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Log ModelState errors
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    _logger.LogError("ModelState Error: {Error}", error.ErrorMessage);
                }
                return RedirectToAction("Error");
            }

            var student = new Student
            {
                Name = model.Name,
                UserName = model.UserName,
                Email = model.Email,
                FacultyId = model.FacultyId,
                EducationTypeId = model.EducationTypeId,
                SpecializationId = model.SpecializationId,
                Role = "Student",
                IsAdmin = false // Set IsAdmin to false for students
            };

            var passwordHasher = new PasswordHasher<Student>();
            student.PasswordHash = passwordHasher.HashPassword(student, model.Password);

            try
            {
                _context.Users.Add(student);
                await _context.SaveChangesAsync(); // Put a breakpoint here to check student object and exceptions
            }
            catch (DbUpdateException dbEx) // Catch database specific exceptions
            {
                _logger.LogError(dbEx, "Database error occurred while saving the student. User: {UserName}, Email: {Email}", model.UserName, model.Email);
                return RedirectToAction("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while saving the student. User: {UserName}, Email: {Email}", model.UserName, model.Email);
                return RedirectToAction("Error");
            }

            return RedirectToAction("Success");
        }


        public IActionResult Error()
        {
            // You can pass any necessary data to the view, if needed
            return View();
        }


        // POST method for teacher registration
        [HttpPost]
        public async Task<IActionResult> RegisterTeacher(TeacherRegistrationViewModel model)
        {
            // Validate the model state
            if (!ModelState.IsValid)
            {
                // Load departments again if there are validation errors
                model.Departments = await _context.Departments.ToListAsync();
                return View("TeacherRegister", model);
            }

            // Check if a user with the same username or email already exists
            if (await _context.Users.AnyAsync(u => u.UserName == model.UserName || u.Email == model.Email))
            {
                ModelState.AddModelError("", "Username or Email is already in use.");
                model.Departments = await _context.Departments.ToListAsync();
                return View("TeacherRegister", model);
            }

            // Create a new Teacher object
            var teacher = new Teacher
            {
                Name = model.Name,
                UserName = model.UserName,
                Email = model.Email,
                DepartmentId = model.DepartmentId,
                Role = "Teacher",
                IsAdmin = false // Set IsAdmin to false for teachers
            };

            // Hash the password
            var passwordHasher = new PasswordHasher<Teacher>();
            teacher.PasswordHash = passwordHasher.HashPassword(teacher, model.Password);

            // Save the new teacher to the database
            try
            {
                _context.Users.Add(teacher);
                await _context.SaveChangesAsync(); // Save changes to the database
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database error occurred while saving the teacher. User: {UserName}, Email: {Email}", model.UserName, model.Email);
                return RedirectToAction("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while saving the teacher. User: {UserName}, Email: {Email}", model.UserName, model.Email);
                return RedirectToAction("Error");
            }

            return RedirectToAction("Success");
        }


        // POST method for admin registration
        [HttpPost]
        public async Task<IActionResult> RegisterAdmin(AdminRegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("AdminRegister", model);
            }

            // Create an Admin user
            var admin = new Admin
            {
                Name = model.Name,
                UserName = model.UserName,
                Email = model.Email,
                Role = "Admin",
                IsAdmin = true // Admins should have this set to true
            };

            // Hash the password
            var passwordHasher = new PasswordHasher<Admin>();
            admin.PasswordHash = passwordHasher.HashPassword(admin, model.Password);

            try
            {
                _context.Users.Add(admin);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database error occurred while saving the admin. User: {UserName}, Email: {Email}", model.UserName, model.Email);
                return RedirectToAction("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while saving the admin. User: {UserName}, Email: {Email}", model.UserName, model.Email);
                return RedirectToAction("Error");
            }

            return RedirectToAction("Success");
        }

        public IActionResult Success()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> MyAccount()
        {
            // Retrieve the UserId claim
            var userIdString = User.FindFirst("UserId")?.Value;

            // Check if the UserId claim is present and valid
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out var userId))
            {
                // If no valid UserId claim is found, redirect to the login page
                return RedirectToAction("Login", "Account");
            }

            // Retrieve the user from the database
            var user = await _context.Users.FindAsync(userId);

            // Check if the user exists
            if (user == null)
            {
                // Optionally log the issue
                _logger.LogWarning("User with ID {UserId} not found.", userId);
                return NotFound();
            }

            // Prepare the model for the view
            var model = new MyAccountViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Role
            };

            // Load role-specific data
            if (user is Student student)
            {
                model.Faculty = await _context.Faculties.FindAsync(student.FacultyId);
                model.EducationType = await _context.EducationTypes.FindAsync(student.EducationTypeId);
                model.Specialization = await _context.Specializations.FindAsync(student.SpecializationId);
            }
            else if (user is Teacher teacher)
            {
                model.Department = await _context.Departments.FindAsync(teacher.DepartmentId);
            }

            return View(model);
        }



    }
}
