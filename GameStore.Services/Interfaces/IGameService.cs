using GameStore.Contracts.Games;

namespace GameStore.Services.Interfaces
{
    public interface IGameService
    {
        bool CreateGame(CreateGameDTO createGameDTO);
        Task<IEnumerable<GameDTO>> GetAllGamesAsync();
        IEnumerable<GameDTO> GetSearchedGamesAsync(SearchDTO searchDTO);
        Task<GameDTO> GetGameByIdAsync(string gameId);
        bool Delete(string gameId);
        Task<bool> Update(GameDTO game);
    }
}
