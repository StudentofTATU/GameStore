using System.ComponentModel.DataAnnotations;
using GameStore.Contracts.Users;

namespace GameStore.Web.ViewModels.Users
{
    public class LogInViewModel
    {
        [Required]
        public string UserName { get; set; }

        public bool Remember { get; set; } = false;

        [Required]
        [RegularExpression("^.{6,}$", ErrorMessage =
            "Minimum length 6.")]
        public string Password { get; set; }

        public LoginUserDTO GetUserDTO()
        {
            return new LoginUserDTO
            {
                UserName = UserName,
                Password = Password,
                Remember = Remember
            };
        }
    }
}
