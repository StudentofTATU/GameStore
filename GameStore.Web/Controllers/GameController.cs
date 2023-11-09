using GameStore.Contracts.Games;
using GameStore.Services.Interfaces;
using GameStore.Web.Helper;
using GameStore.Web.ViewModels.Games;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Web.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameService gameService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public GameController(IGameService gameService, IWebHostEnvironment webHostEnvironment)
        {
            this.gameService = gameService;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index(int? pageNumber)
        {
            List<GameDTO> games = (List<GameDTO>)await gameService.GetAllGamesAsync();

            int pageSize = 6;

            return View(PaginatedList<GameDTO>.Create(games, pageNumber ?? 1, pageSize));
        }

        //Authorized
        [HttpGet]
        public async Task<IActionResult> GamesOfUser()
        {
            List<GameDTO> games = (List<GameDTO>)await gameService.GetAllGamesAsync();
            return View(games);
        }

        [HttpGet]
        public IActionResult Create()
        {

            ViewData["id"] = "c19ec15a-9189-41e3-a9a1-e0c3a30a75d3";// for ownerId 
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

            EditGameViewModel editGameViewModel = new EditGameViewModel();
            editGameViewModel.SetValues(game);
            return View(editGameViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditGameViewModel gameViewModel)
        {
            bool isUpdated = gameService.Update(gameViewModel.GetGameDTO());

            if (gameViewModel.Image != null)
            {
                ChangeImage(gameViewModel);
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
