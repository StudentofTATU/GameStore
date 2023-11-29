using GameStore.Contracts.Users;
using GameStore.Services.Interfaces;

namespace GameStore.Services
{
    public class UserService : IUserService
    {

        public Task<Status> RegisterAsync(RegisterUserDTO userDTO)
        {
            throw new NotImplementedException();
        }
    }
}
