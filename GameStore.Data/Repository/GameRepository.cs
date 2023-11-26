using GameStore.Data.Interfaces;
using GameStore.Domain.Entities.Categories;
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

        public bool Update(Game game, List<Guid> removedCategories, List<Guid> addedCategories)
        {
            Game gameUpdate = context.Games.FirstOrDefault(i => i.Id.ToString().Equals(game.Id.ToString()));
            gameUpdate.Name = game.Name;
            gameUpdate.Description = game.Description;
            gameUpdate.Price = game.Price;

            if (removedCategories != null)
                context.GameCategories.RemoveRange(
                    GetGameCategories(game.Id.ToString(), removedCategories));

            if (addedCategories != null)
                AddGameCategories(game.Id, addedCategories);


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
        private List<GameCategory> GetGameCategories(string gameId, List<Guid> categoryIds)
        {
            List<GameCategory> categories = new List<GameCategory>();

            foreach (var c in categoryIds)
            {
                GameCategory gameCategory = context.GameCategories
                    .Where(g => g.CategoryId.ToString().Equals(c.ToString())).
                        FirstOrDefault(g => g.GameId.ToString().Equals(gameId));

                categories.Add(gameCategory);
            }

            return categories;
        }

        private void AddGameCategories(Guid gameId, List<Guid> categoryIds)
        {
            List<GameCategory> categories = new List<GameCategory>();

            foreach (var categoryId in categoryIds)
            {
                categories.Add(new GameCategory
                {
                    GameId = gameId,
                    CategoryId = categoryId
                });
            }

            context.GameCategories.AddRange(categories);
        }

        public List<Game> GetAllGamesAsync(string searchText, List<Guid> categoryIds)
        {
            IQueryable<Game> games;

            if (searchText == null || searchText.Length < 3)
            {
                games = context.Games.Include(g => g.GameCategories)
                    .ThenInclude(g => g.Category);
            }
            else
            {
                games = context.Games.Include(g => g.GameCategories)
                    .ThenInclude(g => g.Category).Where(g => g.Name.Contains(searchText));
            }


            if (categoryIds.Count < 1)
                return games.ToList();

            List<Game> selectedGames = new List<Game>();

            foreach (var game in games)
            {
                if (game.GameCategories.Where(c =>
                    categoryIds.Contains(c.CategoryId)).Count() > 0)
                {
                    selectedGames.Add(game);
                }
            }
            return selectedGames;
        }
    }
}
