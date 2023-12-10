using GameStore.Domain.Entities.Users;

namespace GameStore.Contracts.Users
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ImageUrl { get; set; }

        public UserDTO()
        { }

        public UserDTO(User user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            UserName = user.UserName;
            ImageUrl = user.ImageUrl;
        }

        public User getUser()
        {
            return new User
            {
                Id = this.Id,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email,
                UserName = this.UserName,
                ImageUrl = this.ImageUrl
            };
        }
    }
}
