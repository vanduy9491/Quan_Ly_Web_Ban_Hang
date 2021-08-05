using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Entities;
using ProductManagement.Models;
using ProductManagement.Models.Product;
using ProductManagement.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private static int categoryId;
        private static string categoryName;
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductController(ICategoryService categoryService,
                                  IProductService productService,
                                  IWebHostEnvironment webHostEnvironment )
        {
            this.categoryService = categoryService;
            this.productService = productService;
            this.webHostEnvironment = webHostEnvironment;
        }
        //[Route("/Product/Index/{catId=1}/{pageNumber=1}/{pageSize=10}/{keyword=''}")]
        public async Task<IActionResult> Index(int catId, int? pageNumber, int? pageSize, string keyword)
        {
            categoryId = catId;
            var category = await categoryService.GetCategoryById(catId);
            categoryName = category.CategoryName;
            var pagination = new Pagination(category.Products.Count, pageNumber, pageSize, keyword);
            keyword = keyword == "''" ? string.Empty : keyword;
            var books = string.IsNullOrEmpty(keyword) ? category.Products : category.Products.Where(b => b.ProductName.Contains(keyword)).ToList();
            books = books.OrderByDescending(b => b.ProductId).ToList().Skip((pagination.CurrentPage - 1) * pagination.PageSize).Take(pagination.PageSize).ToList();
            category.Products = books;
            var listproduct = new ListProduct()
            {

                Category = category,
                Pagination = pagination
            };
            return View(listproduct);
        }
        public async Task<IActionResult> Create(CreateProduct createProduct)
        {
            
            if (ModelState .IsValid)
            {
                string filename = "No-Image.png";
                if (createProduct.Photo != null)
                {
                    string folderPath = Path.Combine(webHostEnvironment.ContentRootPath, @"wwwroot\Images");
                    filename = $"{DateTime.Now.ToString("ddMMyyyyhhmmss")}_{createProduct.Photo.FileName}";
                    string fullPath = Path.Combine(folderPath, filename);
                    using (var file = new FileStream(fullPath, FileMode.Create))
                    {
                        createProduct.Photo.CopyTo(file);
                    }
                }
                var newProduct = new Products()
                {
                    Photo = $"/Images/{filename}",
                    Infomation = createProduct.Infomation,
                    ProductName = createProduct.ProductName,
                    CategoryId = categoryId,
                    IsDeleted = false,
                    Price = createProduct.Price,
                    Quantity = createProduct.Quantity,
                    PublishYear = createProduct.PublishYear,
                    Discount = createProduct.Discount
                };
                await productService.Create(newProduct);
                return RedirectToAction("Index", "Product", new { catId = categoryId });
            }
            ViewBag.CategoryName = categoryName;
            ViewBag.CategoryId = categoryId;
            return View();
        }
        [HttpGet("/Product/View/{productId}")]
        public async Task<IActionResult> View(int productId)
        {
            var product = await productService.GetProductById(productId);
            var viewproduct = new ViewProduct()
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Infomation = product.Infomation,
                Price = product.Price,
                ExistPhoto = product.Photo,
                Discount = product.Discount,
                PublishYear = product.PublishYear,
                Quantity = product.Quantity,
                Category = product.Category
            };
            ViewBag.CategoryId = categoryId;
            return View(viewproduct);
        }
        
        [HttpGet("/Product/Modify/{productId}")]
        public async Task<IActionResult> Modify(int productId)
        {
            ViewBag.CategoryName = categoryName;
            ViewBag.CategoryId = categoryId;
            var product = await productService.GetProductById(productId);
            var modifyProduct = new ModifyProduct()
            {
                ProductName = product.ProductName,
                Infomation = product.Infomation,
                PublishYear = product.PublishYear,
                Price = product.Price,
                ExistPhoto = product.Photo,
                Quantity = product.Quantity,
                Discount = product.Discount,
                CategoryId = product.CategoryId,
                ProductId = product.ProductId
            };
            return View(modifyProduct);
        }
        [HttpPost]
        public async Task<IActionResult> Modify(ModifyProduct modifyProduct)
        {
            if (ModelState.IsValid)
            {
                var product = await productService.GetProductById(modifyProduct.ProductId);
                if (product != null)
                {
                    string fileName = product.Photo;
                    if (modifyProduct.Photo != null)
                    {
                        //Deleted old photo
                        var oldFileName = fileName.Split("/")[2];
                        if (string.Compare(oldFileName, "No-Image.png") != 0)
                        {
                            System.IO.File.Delete(Path.Combine(webHostEnvironment.ContentRootPath, @"wwwroot\Images\", oldFileName));

                        }
                        string folderPath = Path.Combine(webHostEnvironment.ContentRootPath, @"wwwroot\Images\");
                        fileName = $"{DateTime.Now.ToString("ddMMyyyy")}_{modifyProduct.Photo.FileName}";
                        string fullPath = Path.Combine(folderPath, fileName);
                        using (var file = new FileStream (fullPath, FileMode.Create))
                        {
                            modifyProduct.Photo.CopyTo(file);
                        }
                    }
                    product.Photo = modifyProduct.Photo != null ? $"/Images/{fileName}" : fileName;
                    product.ProductName = modifyProduct.ProductName;
                    product.Infomation = modifyProduct.Infomation;
                    product.Price = modifyProduct.Price;
                    product.Quantity = modifyProduct.Quantity;
                    product.PublishYear = modifyProduct.PublishYear;
                    product.CategoryId = modifyProduct.CategoryId;
                    product.ProductId = modifyProduct.ProductId;

                    await productService.Modify(product);
                    return RedirectToAction("Index", new { catId = categoryId });
                }
            }
            ViewBag.CategoryName = categoryName;
            ViewBag.CategoryId = categoryId;
            return View();
        }
        [HttpGet("/Product/Remove/{productId}")]
        public async Task<IActionResult> Remove(int productId)
        {
            await productService.Remove(productId);
            return RedirectToAction("Index","Product", new { catId = categoryId });
        }
        [HttpGet("/Product/Restore/{productId}")]
        public async Task<IActionResult> Restore(int productId)
        {
            await productService.Restore(productId);
            return RedirectToAction("Index", "Product", new { catId = categoryId });
        }
        protected void SetAlert(string message, int type)
        {
            TempData["AlertMessage"] = message;
            if (type == 1)
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == 2)
            {
                TempData["AlertType"] = "alert-warning";
            }
           
        }

    }
}
