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

        public async Task<bool> Update(User user)
        {
            User userUpdate = context.Users.
                FirstOrDefault(i => i.Id.Equals(user.Id));

            userUpdate.FirstName = user.FirstName;
            userUpdate.LastName = user.LastName;
            userUpdate.Email = user.Email;
            userUpdate.UserName = user.UserName;

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
