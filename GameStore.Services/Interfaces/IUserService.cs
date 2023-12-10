using GameStore.Contracts.Users;
using Microsoft.AspNetCore.Http;

namespace GameStore.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> GetCurrentUserAsync(HttpContext httpContext);
        Task<Status> RegisterAsync(RegisterUserDTO userDTO);
        Task<Status> LoginAsync(LoginUserDTO userDTO);
        Task LogoutAsync();
        Task<bool> UpdateUser(UserDTO userDTO);
    }
}
