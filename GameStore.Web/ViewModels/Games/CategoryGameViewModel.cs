using GameStore.Contracts.Categories;

namespace GameStore.Web.ViewModels.Games
{
    public class CategoryGameViewModel
    {
        public EditGameViewModel editGameViewModel { get; set; }
        public IEnumerable<CategoryDTO> categories { get; set; }
    }
}
