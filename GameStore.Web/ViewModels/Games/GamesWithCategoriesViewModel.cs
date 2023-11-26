using GameStore.Contracts.Categories;
using GameStore.Contracts.Games;
using GameStore.Web.Helper;

namespace GameStore.Web.ViewModels.Games
{
    public class GamesWithCategoriesViewModel
    {
        public PaginatedList<GameDTO> Games { get; set; }
        public List<CategoryWithStateDTO> Categories { get; set; }
        public string SearchText { get; set; }
    }
}
