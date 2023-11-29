using GameStore.Domain.Entities.Users;

namespace GameStore.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        bool Add(User user);
        bool SaveChanges();
    }
}