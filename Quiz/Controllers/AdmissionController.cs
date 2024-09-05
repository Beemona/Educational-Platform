using Microsoft.AspNetCore.Mvc;
using Admission.Model; // Replace with your actual namespace

public class AdmissionController : Controller
{
    // GET: Admission
    public IActionResult Index()
    {
        var model = new AdmissionViewModel
        {
            Faculties = new List<string>
            {
                "Faculty of Psychology",
                "Faculty of Math",
                "Faculty of Engineering"
            },
            Degrees = new List<string>
            {
                "Bachelor",
                "Master",
                "Doctorate"
            },
            Programs = new List<string>(),
            LearningTypes = new List<string>
            {
                "Distance Learning",
                "On Campus"
            },
            ApplicationTypes = new List<string>
            {
                "Budget",
                "Tax",
                "Both"
            }
        };

        return View(model);
    }

    // POST: Admission
    [HttpPost]
    public IActionResult Index(AdmissionViewModel model)
    {
        // Handle form submission, save to database, etc.
        // For now, just redirect or return a view with the submitted data
        return View(model); // For demonstration purposes
    }

    [HttpPost]
    public IActionResult UpdatePrograms(string faculty, string degree)
    {
        var programs = GetPrograms(faculty, degree);
        return Json(programs);
    }

    private List<string> GetPrograms(string faculty, string degree)
    {
        var programs = new List<string>();

        if (faculty == "Faculty of Psychology")
        {
            if (degree == "Bachelor")
            {
                programs.Add("Bachelor of Psychology");
            }
            else if (degree == "Master")
            {
                programs.Add("Clinical Psychology");
                programs.Add("Psychotherapy");
                programs.Add("Forensic Psychology");
                programs.Add("Human Resources");
            }
        }
        else if (faculty == "Faculty of Math")
        {
            if (degree == "Bachelor")
            {
                programs.Add("Bachelor of Mathematics");
            }
            else if (degree == "Master")
            {
                programs.Add("Master of Mathematics");
                programs.Add("Educational Master");
            }
        }
        else if (faculty == "Faculty of Engineering")
        {
            if (degree == "Bachelor")
            {
                programs.Add("Bachelor of Computer Science");
                programs.Add("Bachelor of Electronics");
                programs.Add("Bachelor of Electrical Engineering");
            }
            else if (degree == "Master")
            {
                programs.Add("Master in Web Development");
                programs.Add("Master in AI");
                programs.Add("Master in Software Engineering");
            }
        }

        return programs;
    }
}
