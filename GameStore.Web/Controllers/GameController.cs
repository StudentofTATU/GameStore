using GameStore.Contracts.Games;
using GameStore.Services.Interfaces;
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

        public async Task<IActionResult> Index()
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

            return View(game);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GameDTO game)
        {

            // write code
            return View(game);
        }

        public IActionResult Delete(string Id)
        {
            bool isDeleted = gameService.Delete(Id);

            return RedirectToAction("Index");
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
    }
}
