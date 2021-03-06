using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Entities;
using ProductManagement.Models;
using ProductManagement.Models.Category;
using ProductManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Controllers
{
    [Authorize(Roles = "SystemAdmin")]

    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        public async Task<IActionResult> Index(int? pageNumber, int? pageSize, string keyword)
        {
            var categories = await categoryService.GetCategories();
            var pagination = new Pagination(categories.Count, pageNumber, pageSize, null);
            keyword = keyword == "''" ? string.Empty : keyword;
            var cats = string.IsNullOrEmpty(keyword) ? categories : categories.Where(b => b.CategoryName.Contains(keyword)).ToList();
            var catView = new CategoryView()
            {
                Categories = cats.Skip((pagination.CurrentPage - 1) * pagination.PageSize).Take(pagination.PageSize).ToList(),
                Pagination = pagination
            };
            return View(catView);
        }
        public async Task<IActionResult> Create(CreateCategory createCategory )
        {
            if (ModelState.IsValid)
            {
                var newCategory = new Category()
                {
                    CategoryName = createCategory.CategoryName,
                    IsDeleted = false
                };
                await categoryService.Create(newCategory);
                return RedirectToAction("Index", "Category");
            }
            return View();
        }
        [HttpGet("/Category/Modify/{categoryId}")]

        public async Task<IActionResult> Modify(int categoryId)
        {
            var cat = await categoryService.GetCategoryById(categoryId);
            var modifyCat = new ModifyCategory()
            {
                CategoryName = cat.CategoryName
            };
            return View(modifyCat);
        }
        [HttpPost("/Category/Modify/{categoryId}")]
        public async Task<IActionResult> Modify(ModifyCategory modifyCategory)
        {
            if (ModelState.IsValid)
            {
                var category = await categoryService.GetCategoryById(modifyCategory.CategoryId);
                if (category != null)
                {
                    category.CategoryName = modifyCategory.CategoryName;
                    await categoryService.Modify(category);
                    return RedirectToAction("Index","Category");
                }
            }
            return View();
        }


        [HttpGet("/Category/Remove/{categoryId}")]
        public async Task<IActionResult> Remove(int categoryId)
        {
            await categoryService.Remove(categoryId);
            return RedirectToAction("Index");
        }
        [HttpGet("/Category/Restore/{categoryId}")]
        public async Task<IActionResult> Restore(int categoryId)
        {
            await categoryService.Restore(categoryId);
            return RedirectToAction("Index");;
        }
    }
}
