using GameStore.Data.Interfaces;
using GameStore.Domain.Entities.Games;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data.Repository
{
    public class GameRepository : IGameRepository
    {
        private readonly AppDbContext context;

        public GameRepository(AppDbContext appDbContext)
        {
            this.context = appDbContext;
        }

        public async Task<IEnumerable<Game>> GetAllGamesAsync()
        {
            return await context.Games.ToListAsync();
        }

        public async Task<Game> GetGameByIdAsync(string gameId)
        {
            return await context.Games
                .FirstOrDefaultAsync(i => i.Id.ToString().Equals(gameId));
        }

        public bool Add(Game game)
        {
            context.Games.Add(game);

            return SaveChanges();
        }

        public bool Update(Game game)
        {
            Game gameUpdate = context.Games.FirstOrDefault(i => i.Id.ToString().Equals(game.Id.ToString()));
            gameUpdate.Name = game.Name;
            gameUpdate.Description = game.Description;
            gameUpdate.Price = game.Price;

            return SaveChanges();
        }

        public bool Delete(string gameId)
        {
            Game gameDelete = context.Games
                .FirstOrDefault(i => i.Id.ToString().Equals(gameId));
            context.Games.Remove(gameDelete);

            return SaveChanges();
        }

        private bool SaveChanges()
        {
            var saved = context.SaveChanges();

            return saved > 0;
        }
    }
}
