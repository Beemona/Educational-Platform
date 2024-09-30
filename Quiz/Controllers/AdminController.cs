using Authentication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizDbContext.Data;
using System.Threading.Tasks;

namespace Authentication.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Dashboard
        public IActionResult Dashboard()
        {
            var model = new AdminDashboardViewModel
            {
                Specializations = _context.Specializations.ToList(),
                Departments = _context.Departments.ToList()
            };

            return View(model);
        }

        // GET: Admin/CreateSpecialization
        public IActionResult CreateSpecialization()
        {
            return View();
        }

        // POST: Admin/CreateSpecialization
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSpecialization(Specialization specialization)
        {
            if (ModelState.IsValid)
            {
                _context.Specializations.Add(specialization);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Dashboard));
            }
            return View(specialization);
        }

        // GET: Admin/EditSpecialization/5
        public async Task<IActionResult> EditSpecialization(int id)
        {
            var specialization = await _context.Specializations.FindAsync(id);
            if (specialization == null) return NotFound();
            return View(specialization);
        }

        // POST: Admin/EditSpecialization/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSpecialization(Specialization specialization)
        {
            if (ModelState.IsValid)
            {
                _context.Update(specialization);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Dashboard));
            }
            return View(specialization);
        }

        // GET: Admin/DeleteSpecialization/5
        public async Task<IActionResult> DeleteSpecialization(int id)
        {
            var specialization = await _context.Specializations.FindAsync(id);
            if (specialization == null) return NotFound();
            return View(specialization);
        }

        // POST: Admin/DeleteSpecialization/5
        [HttpPost, ActionName("DeleteSpecialization")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var specialization = await _context.Specializations.FindAsync(id);
            if (specialization != null)
            {
                _context.Specializations.Remove(specialization);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Dashboard));
        }

        // GET: Admin/CreateDepartment
        public IActionResult CreateDepartment()
        {
            return View();
        }

        // POST: Admin/CreateDepartment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDepartment(Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Departments.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Dashboard));
            }
            return View(department);
        }

        // GET: Admin/EditDepartment/5
        public async Task<IActionResult> EditDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null) return NotFound();
            return View(department);
        }

        // POST: Admin/EditDepartment/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDepartment(Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Update(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Dashboard));
            }
            return View(department);
        }

        // GET: Admin/DeleteDepartment/5
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null) return NotFound();
            return View(department);
        }

        // POST: Admin/DeleteDepartment/5
        [HttpPost, ActionName("DeleteDepartment")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Dashboard));
        }
    }
}
