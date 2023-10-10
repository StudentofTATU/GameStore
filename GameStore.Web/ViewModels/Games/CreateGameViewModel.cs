using System.ComponentModel.DataAnnotations;
using GameStore.Contracts.Games;

namespace GameStore.Web.ViewModels.Games
{
    public class CreateGameViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        public string OwnerId { get; set; }

        public IFormFile Image { get; set; }


        public CreateGameDTO GetGameDTO(string ImageUrl)
        {
            return new CreateGameDTO
            {
                Name = Name, //TODO change title to name
                Description = Description,
                Price = Price,
                ImageUrl = ImageUrl,
                OwnerId = Guid.Parse(OwnerId)
            };
        }

    }
}
