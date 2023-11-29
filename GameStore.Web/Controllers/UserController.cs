using GameStore.Web.ViewModels.Users;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Web.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterViewModel userVeiwModel)
        {
            // TODO write code
            return View();
        }

        public IActionResult Login() { return View(); }

        [HttpPost]
        public async Task<IActionResult> Login(LogInViewModel logInViewModel)
        {
            return View();
        }
    }
}
