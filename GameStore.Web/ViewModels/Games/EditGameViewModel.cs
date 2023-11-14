using GameStore.Contracts.Categories;
using GameStore.Contracts.Games;

namespace GameStore.Web.ViewModels.Games
{
    public class EditGameViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public IFormFile? Image { get; set; }
        public string ImageUrlName { get; set; }

        public void SetValues(GameDTO gameDTO)
        {
            this.Id = gameDTO.Id;
            this.Name = gameDTO.Name;
            this.Description = gameDTO.Description;
            this.Price = gameDTO.Price;
            this.ImageUrlName = gameDTO.ImageUrl;
        }

        public GameDTO GetGameDTO()
        {
            return new GameDTO
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description,
                Price = this.Price
            };
        }

        public GameDTO GetGameDTO(List<CategoryDTO> categories)
        {
            return new GameDTO
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description,
                Price = this.Price,
                Categories = categories
            };
        }
    }
}
