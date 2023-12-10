using System.ComponentModel.DataAnnotations;
using GameStore.Contracts.Users;

namespace GameStore.Web.ViewModels.Users
{
    public class UserRegisterViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression("^.{6,}$", ErrorMessage =
            "Minimum length 6.")]
        public string Password { get; set; }

        public RegisterUserDTO GetUserDTO()
        {
            return new RegisterUserDTO
            {
                FirstName = FirstName,
                LastName = LastName,
                UserName = UserName,
                Email = Email,
                Password = Password
            };
        }
    }
}
