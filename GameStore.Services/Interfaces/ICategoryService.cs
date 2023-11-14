using GameStore.Contracts.Categories;

namespace GameStore.Services.Interfaces
{
    public interface ICategoryService
    {
        bool CreateCategory(CategoryDTO categoryDTO);
        Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();
        Task<List<CategoryWithStateDTO>>
            GetAllCategoriesWithStateAsync(List<CategoryDTO> checkedCategories);
        Task<CategoryDTO> GetCategoryByIdAsync(string categoryId);
        Task<List<CategoryDTO>> GetGameCategoriesAsync(string gameId);
        bool Delete(string categoryId);
        bool Update(CategoryDTO categoryDTO);
    }
}
