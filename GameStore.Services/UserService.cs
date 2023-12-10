using GameStore.Contracts.Users;
using GameStore.Domain.Entities.Users;
using GameStore.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace GameStore.Services
{
    public partial class UserService : IUserService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public UserService(UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<Status> LoginAsync(LoginUserDTO userDTO)
        {

            var user = await userManager.FindByNameAsync(userDTO.UserName);

            Status status = await IsUserValid(user, userDTO);

            if (status.StatusCode == 1)
            {
                status = await SignInUser(user, userDTO);
            }

            return status;
        }

        public async Task<Status> RegisterAsync(RegisterUserDTO userDTO)
        {
            var status = await IsUserExists(userDTO);

            if (status.StatusCode == 1) { return status; }

            status = await SaveUser(userDTO);

            return status;
        }

        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<UserDTO> GetCurrentUserAsync(HttpContext httpContext)
        {
            string userId = userManager.GetUserId(httpContext.User);

            User user = await userManager.FindByIdAsync(userId);

            return new UserDTO(user);
        }

        public async Task<bool> UpdateUser(UserDTO userDTO)
        {
            var userUpdate = await userManager.FindByIdAsync(userDTO.Id);

            userUpdate.FirstName = userDTO.FirstName;
            userUpdate.LastName = userDTO.LastName;
            userUpdate.Email = userDTO.Email;
            userUpdate.UserName = userDTO.UserName;


            IdentityResult identityResult =
                await userManager.UpdateAsync(userUpdate);

            return identityResult.Succeeded;
        }
    }
}
