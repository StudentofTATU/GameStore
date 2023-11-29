using GameStore.Data.Configurations;
using GameStore.Domain.Entities.Categories;
using GameStore.Domain.Entities.Games;
using GameStore.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(GameCategoryConfiguration).Assembly);

        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<GameCategory> GameCategories { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
