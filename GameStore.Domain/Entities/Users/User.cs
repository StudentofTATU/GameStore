using Microsoft.AspNetCore.Identity;

namespace GameStore.Domain.Entities.Users
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? ImageUrl { get; set; }
    }
}
