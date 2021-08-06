using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TableStorage.Audit.BLL.Interfaces;

namespace TableStorage.Audit.POC.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            return Json(await _userService.GetAll());
        }
    }
}