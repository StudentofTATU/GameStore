using GameStore.Data.Interfaces;
using GameStore.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext context;

        public UserRepository(AppDbContext context)
        {
            this.context = context;
        }

        public bool Add(User user)
        {
            context.Users.Add(user);

            return SaveChanges();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await context.Users.ToListAsync();
        }

        public bool SaveChanges()
        {
            var saved = context.SaveChanges();

            return saved > 0;
        }
    }
}
