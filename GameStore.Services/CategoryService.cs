using GameStore.Contracts.Categories;
using GameStore.Data;
using GameStore.Domain.Entities.Categories;
using GameStore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext context;

        public CategoryService(AppDbContext appDbContext)
        {
            this.context = appDbContext;
        }

        public bool CreateCategory(CategoryDTO categoryDTO)
        {
            Category category = new Category
            {
                Id = categoryDTO.Id,
                Name = categoryDTO.Name,
            };

            context.Categories.Add(category);

            return SaveChanges();
        }

        public bool Delete(string categoryId)
        {
            Category category = context.Categories
                .FirstOrDefault(c => c.Id.ToString().Equals(categoryId));

            context.Categories.Remove(category);

            return SaveChanges();
        }

        public async Task<List<CategoryDTO>> GetGameCategoriesAsync(string gameId)
        {
            var gameCategories = context.Games.Include(g => g.GameCategories)
                .ThenInclude(g => g.Category).Single(g => g.Id.ToString().Equals(gameId));

            List<CategoryDTO> gameCategoryDTOs = new List<CategoryDTO>();

            foreach (var category in gameCategories.GameCategories)
            {
                gameCategoryDTOs.Add(new CategoryDTO
                {
                    Id = category.Category.Id,
                    Name = category.Category.Name
                });
            }

            return gameCategoryDTOs;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
        {
            IEnumerable<Category> categories = await context.Categories.ToListAsync();
            List<CategoryDTO> categoryDTOs = new List<CategoryDTO>();

            foreach (var category in categories)
            {
                categoryDTOs.Add(new CategoryDTO
                {
                    Id = category.Id,
                    Name = category.Name
                });
            }

            return categoryDTOs;
        }

        public async Task<CategoryDTO> GetCategoryByIdAsync(string categoryId)
        {
            Category category = await context.Categories
                .FirstOrDefaultAsync(c => c.Id.ToString().Equals(categoryId));

            return new CategoryDTO(category.Id, category.Name);
        }

        public bool Update(CategoryDTO categoryDTO)
        {
            Category category = context.Categories.FirstOrDefault(
                c => c.Id.ToString().Equals(categoryDTO.Id.ToString()));

            category.Name = categoryDTO.Name;

            return SaveChanges();
        }

        private bool SaveChanges()
        {
            var saved = context.SaveChanges();

            return saved > 0;
        }

        public async Task<List<CategoryWithStateDTO>>
            GetAllCategoriesWithStateAsync(List<CategoryDTO> checkedCategories)
        {
            IEnumerable<CategoryDTO> categoryDtos = await GetAllCategoriesAsync();
            List<CategoryWithStateDTO> categoryWithStateDTOs = new List<CategoryWithStateDTO>();

            foreach (CategoryDTO categoryDTO in categoryDtos)
            {
                categoryWithStateDTOs.Add(new CategoryWithStateDTO
                {
                    Id = categoryDTO.Id,
                    Name = categoryDTO.Name,
                    IsChecked = IsItemExist(categoryDTO.Id, checkedCategories)
                });
            }

            return categoryWithStateDTOs;
        }

        public bool IsItemExist(Guid Id, List<CategoryDTO> checkedCategories)
        {
            if (checkedCategories == null) return false;

            foreach (var item in checkedCategories)
            {
                if (item.Id.ToString().Equals(Id.ToString())) return true;
            }

            return false;
        }
    }
}
