using GameStore.Domain.Entities.Users;

namespace GameStore.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<bool> Update(User user);
        bool SaveChanges();
    }
}