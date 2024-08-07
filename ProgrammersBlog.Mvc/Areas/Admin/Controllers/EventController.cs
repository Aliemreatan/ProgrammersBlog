using Microsoft.AspNetCore.Mvc;

namespace MyProject.WebUi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EventController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
