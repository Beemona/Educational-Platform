using Microsoft.AspNetCore.Mvc;

namespace Quiz.Controllers
{
    public class AllPagesController : Controller
    {
        // GET: /AllPages
        public IActionResult Index()
        {
            return View("AllPages"); // Ensure that the view name matches the file
        }
    }
}
