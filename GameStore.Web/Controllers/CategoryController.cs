using GameStore.Contracts.Categories;
using GameStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<CategoryDTO> categories = await categoryService.GetAllCategoriesAsync();

            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string Id)
        {
            CategoryDTO categoryDTO = await categoryService.GetCategoryByIdAsync(Id);
            return View(categoryDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryDTO categoryDTO)
        {
            if (ModelState.IsValid)
            {
                bool isUpdated = categoryService.Update(categoryDTO);

                return RedirectToAction("Index", "Category");
            }

            return View(categoryDTO);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryDTO categoryDTO)
        {
            if (ModelState.IsValid)
            {
                categoryService.CreateCategory(categoryDTO);

                return RedirectToAction("Index", "Category");
            }

            return View(categoryDTO);
        }

        public async Task<IActionResult> Delete(string Id)
        {
            bool isDeleted = categoryService.Delete(Id);

            return RedirectToAction("Index");
        }
    }
}
