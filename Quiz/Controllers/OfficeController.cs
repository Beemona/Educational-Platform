using Microsoft.AspNetCore.Mvc;

namespace Quiz.Controllers
{
    public class OfficeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
