using GameStore.Contracts.Categories;

namespace GameStore.Contracts.Games
{
    public class SearchDTO
    {
        public string SearchText { get; set; }
        public List<CategoryWithStateDTO> Categories { get; set; }
    }
}
