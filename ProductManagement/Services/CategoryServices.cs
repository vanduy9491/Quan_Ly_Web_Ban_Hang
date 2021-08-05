using Microsoft.EntityFrameworkCore;
using ProductManagement.DBContexts;
using ProductManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Services
{
    public class CategoryServices : ICategoryService
    {
        private readonly ProductDBContext context;

        public CategoryServices(ProductDBContext context)
        {
            this.context = context;
        }

        public async Task<Category> Create(Category createCategory)
        {
            try
            {
                context.Add(createCategory);
                var cateId = await context.SaveChangesAsync();
                createCategory.CategoryId = cateId;
                return createCategory;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Category>> GetCategories()
        {
            return await context.Categories.Include(p => p.Products).Where(c => c.IsDeleted == false).ToListAsync();
        }

        public async Task<Category> GetCategoryById(int categoryId)
        {
            return await context.Categories.Include(p => p.Products).FirstOrDefaultAsync(c => c.CategoryId == categoryId);
        }

        public async Task<Category> Remove(int categoryId)
        {
            try
            {
                var cat = await GetCategoryById(categoryId);
                cat.IsDeleted = true;
                context.Attach(cat);
                context.Entry<Category>(cat).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return cat;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Category> Restore(int catId)
        {
            try
            {
                var cat = await GetCategoryById(catId);
                cat.IsDeleted = false;
                context.Attach(cat);
                context.Entry<Category>(cat).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return cat;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
