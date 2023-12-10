using GameStore.Contracts.Categories;
using GameStore.Contracts.Games;
using GameStore.Contracts.Users;
using GameStore.Services.Interfaces;
using GameStore.Web.Helper;
using GameStore.Web.ViewModels.Games;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Web.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameService gameService;
        private readonly ICategoryService categoryService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IUserService userService;
        public GameController(IGameService gameService,
            IWebHostEnvironment webHostEnvironment,
            IUserService userService,
            ICategoryService categoryService)
        {
            this.gameService = gameService;
            this.webHostEnvironment = webHostEnvironment;
            this.categoryService = categoryService;
            this.userService = userService;
        }

        public async Task<IActionResult> Index(int? pageNumber,
            GamesWithCategoriesViewModel gamesWithCategoriesViewModel)
        {
            SearchDTO searchDTO = new SearchDTO
            {
                SearchText = gamesWithCategoriesViewModel.SearchText,
                Categories = gamesWithCategoriesViewModel.Categories
            };

            List<GameDTO> games = gameService.GetSearchedGamesAsync(searchDTO).ToList();

            List<CategoryWithStateDTO> categories =
                await categoryService.GetAllCategoriesWithStateAsync(null);

            int pageSize = 6;

            GamesWithCategoriesViewModel gamesWithCategories = new GamesWithCategoriesViewModel
            {
                Games = PaginatedList<GameDTO>.Create(games, pageNumber ?? 1, pageSize),
                Categories = categories,
                SearchText = gamesWithCategoriesViewModel.SearchText,
            };

            return View(gamesWithCategories);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GamesOfUser()
        {
            UserDTO userDTO =
                await userService.GetCurrentUserAsync(HttpContext);

            List<GameDTO> games = await gameService.GetAllUserGamesAsync(userDTO.Id);

            return View(games);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            UserDTO userDTO =
                await userService.GetCurrentUserAsync(HttpContext);

            //ViewData["id"] = "c19ec15a-9189-41e3-a9a1-e0c3a30a75d3";// for ownerId 
            ViewData["id"] = userDTO.Id;
            return View();
        }

        [HttpGet]
        public IActionResult Test()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> View(string Id)
        {
            GameDTO gameDTO = await gameService.GetGameByIdAsync(Id);
            return View(gameDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string Id)
        {
            GameDTO game = await gameService.GetGameByIdAsync(Id);

            List<CategoryWithStateDTO> categoryWithStateDTOs =
                await categoryService.GetAllCategoriesWithStateAsync(game.Categories);

            List<Category> categories = new List<Category>();

            foreach (var c in categoryWithStateDTOs)
            {
                categories.Add(new Category
                {
                    Id = c.Id,
                    Name = c.Name,
                    IsChecked = c.IsChecked
                });
            }

            EditGameViewModel editGameViewModel = new EditGameViewModel();
            editGameViewModel.SetValues(game);

            EditGameCategoryViewModel editGameCategoryViewModel =
                new EditGameCategoryViewModel
                {
                    EditGameViewModel = editGameViewModel,
                    Categories = categories
                };

            return View(editGameCategoryViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditGameCategoryViewModel gameCategoryViewModel)
        {
            List<CategoryDTO> gameCategoryDTOs = new List<CategoryDTO>();

            foreach (var c in gameCategoryViewModel.Categories)
            {
                if (c.IsChecked)
                {
                    gameCategoryDTOs.Add(new CategoryDTO
                    {
                        Id = c.Id,
                        Name = c.Name
                    });
                }
            }

            bool isUpdated = await gameService.Update(
                gameCategoryViewModel.EditGameViewModel.GetGameDTO(gameCategoryDTOs));

            if (gameCategoryViewModel.EditGameViewModel.Image != null)
            {
                ChangeImage(gameCategoryViewModel.EditGameViewModel);
            }

            return RedirectToAction("GamesOfUser");
        }

        public async Task<IActionResult> Delete(string Id)
        {
            GameDTO gameDTO = await gameService.GetGameByIdAsync(Id);
            bool isDeleted = gameService.Delete(Id);
            if (isDeleted)
            {
                DeleteImage(gameDTO.ImageUrl);
            }
            return RedirectToAction("GamesOfUser");
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateGameViewModel gameViewModel)
        {
            if (ModelState.IsValid)
            {
                var fileName = await SaveImage(gameViewModel);

                gameService.CreateGame(gameViewModel.GetGameDTO(fileName));
                return RedirectToAction("Index", "Home");
            }
            return View(gameViewModel);
        }

        private async Task<string> SaveImage(CreateGameViewModel gameViewModel)
        {
            var fileName = "";
            var folders = "images/games/";
            if (gameViewModel.Image != null)
            {
                fileName = DateTime.Now.ToFileTime() + "_" + gameViewModel.Image.FileName;
                string path = folders + fileName;
                string pathToStore = Path.Combine(webHostEnvironment.WebRootPath, path);

                await gameViewModel.Image.CopyToAsync(new FileStream(pathToStore, FileMode.Create));
            }

            return fileName;
        }

        private async Task<string> ChangeImage(EditGameViewModel gameViewModel)
        {
            var fileName = "";
            var folders = "images/games/";
            if (gameViewModel.Image != null)
            {
                string path = folders + gameViewModel.ImageUrlName;
                string pathToStore = Path.Combine(webHostEnvironment.WebRootPath, path);

                await gameViewModel.Image.CopyToAsync(new FileStream(pathToStore, FileMode.Create));
            }

            return fileName;
        }

        private void DeleteImage(string imageUrl)
        {
            var path = Path.Combine(webHostEnvironment.WebRootPath,
                "images/games", imageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
}
