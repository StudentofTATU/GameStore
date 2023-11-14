using GameStore.Domain.Entities.Games;

namespace GameStore.Data.Interfaces
{
    public interface IGameRepository
    {
        Task<IEnumerable<Game>> GetAllGamesAsync();
        Task<Game> GetGameByIdAsync(string gameId);
        bool Add(Game game);
        bool Update(Game game, List<Guid> RemovedCategories, List<Guid> AddedCategories);
        bool Delete(string gameId);
    }
}
