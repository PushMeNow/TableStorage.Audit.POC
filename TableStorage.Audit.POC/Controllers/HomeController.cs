using Microsoft.AspNetCore.Mvc;

namespace TableStorage.Audit.POC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Users()
        {
            return View();
        }
    }
}