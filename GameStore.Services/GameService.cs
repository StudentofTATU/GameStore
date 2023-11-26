using GameStore.Contracts.Categories;
using GameStore.Contracts.Games;
using GameStore.Data.Interfaces;
using GameStore.Domain.Entities.Games;
using GameStore.Services.Interfaces;

namespace GameStore.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository gameRepository;
        private readonly ICategoryService categoryService;

        public GameService(IGameRepository gameRepository, ICategoryService categoryService)
        {
            this.gameRepository = gameRepository;
            this.categoryService = categoryService;
        }

        public bool CreateGame(CreateGameDTO createGameDTO)
        {
            Game game = new Game
            {
                Id = Guid.NewGuid(),
                Name = createGameDTO.Name,
                Description = createGameDTO.Description,
                Price = createGameDTO.Price,
                ImageUrl = createGameDTO.ImageUrl,
                CreatedDateTime = DateTimeOffset.Now,
                OwnerId = createGameDTO.OwnerId
            };

            return gameRepository.Add(game);
        }

        public bool Delete(string gameId)
        {
            return gameRepository.Delete(gameId);
        }

        public async Task<IEnumerable<GameDTO>> GetAllGamesAsync()
        {
            IEnumerable<Game> games = await gameRepository.GetAllGamesAsync();
            List<GameDTO> gameDTOs = new List<GameDTO>();

            foreach (Game game in games)
            {
                gameDTOs.Add(new GameDTO(game));
            }
            return gameDTOs;
        }

        public IEnumerable<GameDTO> GetSearchedGamesAsync(SearchDTO searchDTO)
        {
            List<Guid> categoryIds = new List<Guid>();
            if (searchDTO.Categories != null)
            {
                categoryIds = searchDTO.Categories.Where(c => c.IsChecked)
                .Select(c => c.Id).ToList();
            }




            IEnumerable<GameDTO> games = gameRepository
                .GetAllGamesAsync(searchDTO.SearchText, categoryIds).Select(
                g => new GameDTO(g));


            return games;
        }
        public async Task<GameDTO> GetGameByIdAsync(string gameId)
        {
            Game game = await gameRepository.GetGameByIdAsync(gameId);


            GameDTO gameDTO = new GameDTO(game);
            gameDTO.Categories = await GetOneGameCategoriesList(gameId);

            return gameDTO;
        }

        public async Task<bool> Update(GameDTO gameDTO)
        {
            Game game = new Game
            {
                Id = gameDTO.Id,
                Name = gameDTO.Name,
                Description = gameDTO.Description,
                Price = gameDTO.Price
            };

            List<CategoryDTO> beforeUpdateGameCategories =
                await GetOneGameCategoriesList(gameDTO.Id.ToString());

            // gameDTO.Categories this new afrer update list
            List<Guid> removedCategories, addedCategories;

            removedCategories = GetOmittedItemIds(beforeUpdateGameCategories, gameDTO.Categories);
            addedCategories = GetOmittedItemIds(gameDTO.Categories, beforeUpdateGameCategories);


            return gameRepository.Update(game, removedCategories, addedCategories);
        }

        private async Task<List<CategoryDTO>> GetOneGameCategoriesList(string gameId)
        {
            return await categoryService.GetGameCategoriesAsync(gameId);
        }

        private List<Guid> GetOmittedItemIds(List<CategoryDTO> mainList, List<CategoryDTO> innerList)
        {
            List<Guid> categoryIds = new List<Guid>();

            foreach (var category in mainList)
            {
                if (!IsItemExist(category.Id, innerList))
                {
                    categoryIds.Add(category.Id);
                }
            }

            return categoryIds;
        }

        public bool IsItemExist(Guid Id, List<CategoryDTO> checkedCategories)
        {
            if (checkedCategories == null) return false;

            foreach (var item in checkedCategories)
            {
                if (item.Id.ToString().Equals(Id.ToString())) return true;
            }

            return false;
        }
    }
}