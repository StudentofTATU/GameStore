using GameStore.Contracts.Users;
using GameStore.Services.Interfaces;
using GameStore.Web.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserController(IUserService userService,
            IHttpContextAccessor httpContextAccessor)
        {
            this.userService = userService;
            this.httpContextAccessor = httpContextAccessor;

        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            UserDTO userDTO =
                await userService.GetCurrentUserAsync(HttpContext);

            UserViewModel userViewModel = new UserViewModel(userDTO);

            return View(userViewModel);
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
            if (ModelState.IsValid)
            {
                var result = await userService.RegisterAsync(userVeiwModel.GetUserDTO());
                TempData["message"] = result.Message;
                return RedirectToAction(nameof(Register));
            }
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LogInViewModel logInViewModel)
        {
            if (!ModelState.IsValid) { return View(logInViewModel); }

            var result = await userService.LoginAsync(logInViewModel.GetUserDTO());

            if (result.StatusCode == 1)
            {
                return RedirectToAction("Index", "User");
            }
            else
            {
                TempData["message"] = result.Message;
                return RedirectToAction(nameof(Login));
            }
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await userService.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            UserDTO userDTO =
                await userService.GetCurrentUserAsync(HttpContext);

            UserViewModel userViewModel = new UserViewModel(userDTO);

            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel userViewModel)
        {
            if (!ModelState.IsValid) { return View(userViewModel); }

            bool isUpdated = await userService.
                UpdateUser(userViewModel.GetUserDTO());

            return RedirectToAction("Index");
        }
    }
}
