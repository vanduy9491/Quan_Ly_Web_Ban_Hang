using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductManagement.Models.Cart;
using ProductManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;

        public const string CARTKEY = "SCN";
        public HomeController(ICategoryService categoryService, IProductService productService)
        {
            this.categoryService = categoryService;
            this.productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await categoryService.GetCategories());
        }
        [Route("/Home/AddToCart/{productId:int}", Name = "Index")]

        public async Task<IActionResult> AddToCart(int productId)
        {
            var product = await productService.GetProductById(productId);
            if (product == null)
            {
                return NotFound("Không có sản phẩm");
            }

            var productItem = new ProductItem()
            {
                ProductId = product.ProductId,
                Quantity = product.Quantity,
                ProductName = product.ProductName,
                Price = product.Price,
                Infomation = product.Infomation,
                Photo = product.Photo,
                Discount = product.Discount,
                PublishYear = product.PublishYear,
                CategoryId = product.CategoryId
            };

            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.Product.ProductId == productId);
            if (cartitem != null)
            {
                cartitem.Quantity++;
            }
            else
            {
                cart.Add(new CartItem() { Quantity = 1, Product = productItem });
            }
            SaveCartSession(cart);
            return RedirectToAction("Cart");
        }
        [Route("Home/Cart")]
        public IActionResult Cart()
        {
            return View(GetCartItems());
        }

        [Route("Home/UpdateCart", Name = "updatecart")]
        [HttpPost]
        public IActionResult UpdateCart([FromForm]int productId, [FromForm]int quantity)
        {
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.Product.ProductId == productId);
            if (cartitem != null)
            {
                cartitem.Quantity = quantity;
            }
            SaveCartSession(cart);
            return Ok();
        }

        [Route("/Home/RemoveCart/{productId:int}")]
        public IActionResult RemoveCart([FromRoute]int productId)
        {
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.Product.ProductId == productId);
            if (cartitem != null)
            {
                cart.Remove(cartitem);
            }
            SaveCartSession(cart);
            return RedirectToAction(nameof(Cart));
        }

        List<CartItem> GetCartItems()
        {
            var session = HttpContext.Session;
            string jsoncart = session.GetString(CARTKEY);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<CartItem>>(jsoncart);
            }
            return new List<CartItem>();
        }
        void SaveCartSession(List<CartItem> ls)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString(CARTKEY, jsoncart);
        }
    }
}
