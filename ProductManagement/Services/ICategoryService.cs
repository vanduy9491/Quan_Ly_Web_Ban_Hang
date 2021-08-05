using ProductManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategories();
        Task<Category> GetCategoryById(int categoryId);
        Task<Category> Create(Category createCategory);
        Task<Category> Remove(int catId);
        Task<Category> Restore(int catId);
    }
}
