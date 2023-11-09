using GameStore.Contracts.Games;
using GameStore.Data.Interfaces;
using GameStore.Domain.Entities.Games;
using GameStore.Services.Interfaces;

namespace GameStore.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
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

        public async Task<GameDTO> GetGameByIdAsync(string gameId)
        {
            Game game = await gameRepository.GetGameByIdAsync(gameId);

            return new GameDTO(game);
        }

        public bool Update(GameDTO gameDTO)
        {
            Game game = new Game
            {
                Id = gameDTO.Id,
                Name = gameDTO.Name,
                Description = gameDTO.Description,
                Price = gameDTO.Price
            };
            return gameRepository.Update(game);
        }
    }
}
