using GameStore.Domain.Entities.Games;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<Game> Games { get; set; }
    }
}
