using GameStore.Contracts.Users;

namespace GameStore.Services.Interfaces
{
    public interface IUserService
    {
        Task<Status> RegisterAsync(RegisterUserDTO userDTO);
    }
}
