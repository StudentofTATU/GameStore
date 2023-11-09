using GameStore.Contracts.Categories;

namespace GameStore.Services.Interfaces
{
    public interface ICategoryService
    {
        bool CreateCategory(CategoryDTO categoryDTO);
        Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();
        Task<CategoryDTO> GetCategoryByIdAsync(string categoryId);
        bool Delete(string categoryId);
        bool Update(CategoryDTO categoryDTO);
    }
}
