using ProductManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Services
{
    public interface IProductService
    {
        Task<Products> Create(Products creatProduct);
        Task<Products> GetProductById(int productId);
        Task<Products> Modify(Products product);
        Task<Products> Remove(int productId);
        Task<Products> Restore(int productId);
    }
}
