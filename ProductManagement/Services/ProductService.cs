using Microsoft.EntityFrameworkCore;
using ProductManagement.DBContexts;
using ProductManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductDBContext context;

        public ProductService(ProductDBContext context)
        {
            this.context = context;
        }
        public async Task<Products> Create(Products creatProduct)
        {
            try
            {
                context.Add(creatProduct);
                var productId = await context.SaveChangesAsync();
                creatProduct.ProductId = productId;
                return creatProduct;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Products> GetProductById(int productId)
        {
            return await context.Products.Include(c => c.Category).FirstOrDefaultAsync(p => p.ProductId == productId);
            }

        public async Task<Products> Modify(Products product)
        {
            try
            {
                context.Attach(product);
                context.Entry<Products>(product).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return product;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Products> Remove(int productId)
        {
            try
            {
                var product = await GetProductById(productId);
                product.IsDeleted = true;
                context.Attach(product);
                context.Entry<Products>(product).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return product;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Products> Restore(int productId)
        {
            try
            {
                var product = await GetProductById(productId);
                product.IsDeleted = false;
                context.Attach(product);
                context.Entry<Products>(product).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return product;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
