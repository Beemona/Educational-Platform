using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Admission.Model;
using QuizDbContext.Data; // Adjust namespace as necessary
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class AdmissionController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdmissionController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Admission
    public async Task<IActionResult> Index()
    {
        var model = new AdmissionViewModel
        {
            Faculties = await _context.Faculties
                .Select(f => new AdmissionViewModel.Faculty
                {
                    Id = f.Id,
                    Name = f.Name
                })
                .ToListAsync(),

            EducationTypes = await _context.EducationTypes
                .Select(e => new AdmissionViewModel.EducationType
                {
                    Id = e.Id,
                    Name = e.Name
                })
                .ToListAsync(),

            Specializations = await _context.Specializations
                .Select(s => new AdmissionViewModel.Specialization
                {
                    Id = s.Id,
                    Name = s.Name,
                    FacultyId = s.FacultyId ?? 0,
                    EducationTypeId = s.EducationTypeId ?? 0
                })
                .ToListAsync(),

            // Example data for ApplicationTypes and LearningTypes
            ApplicationTypes = new List<string> { "Type1", "Type2", "Type3" },
            LearningTypes = new List<string> { "Full-time", "Part-time" }
        };

        return View(model);
    }

    // POST: Admission
    [HttpPost]
    public async Task<IActionResult> Index(AdmissionViewModel model)
    {
        // Handle form submission, save to database, etc.
        // Example: Save model to the database
        // await _context.SaveChangesAsync();

        return View(model); // For demonstration purposes
    }

    [HttpGet]
    public async Task<IActionResult> GetSpecializations(int educationTypeId, int facultyId)
    {
        var specializations = await _context.Specializations
           .Where(s => s.FacultyId == facultyId && s.EducationTypeId == educationTypeId)
           .Select(s => new
           {
               Id = s.Id,
               Name = s.Name
           })
           .ToListAsync();

        return Json(specializations);
    }
}
