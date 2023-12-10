using System.ComponentModel.DataAnnotations;
using GameStore.Contracts.Users;

namespace GameStore.Web.ViewModels.Users
{
    public class UserViewModel
    {
        public string Id { get; set; }
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string? Password { get; set; }

        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }

        public UserViewModel()
        {

        }
        public UserViewModel(UserDTO userDTO)
        {
            Id = userDTO.Id;
            FirstName = userDTO.FirstName;
            LastName = userDTO.LastName;
            Email = userDTO.Email;
            UserName = userDTO.UserName;
            Password = userDTO.Password;
            ImageUrl = userDTO.ImageUrl;
        }

        public UserDTO GetUserDTO()
        {
            return new UserDTO
            {
                Id = this.Id,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email,
                UserName = this.UserName,
                Password = this.Password,
                ImageUrl = this.ImageUrl
            };
        }
    }
}
