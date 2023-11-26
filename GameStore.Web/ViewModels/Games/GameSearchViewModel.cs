using GameStore.Contracts.Categories;

namespace GameStore.Web.ViewModels.Games
{
    public class GameSearchViewModel
    {
        public string SearchText { get; set; }
        public List<CategoryDTO> Categories { get; set; }
    }
}
