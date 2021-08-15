using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Models.Dashboard;
using ProductManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Controllers
{
    [Authorize(Roles = "SystemAdmin")]
    public class DashboardController : Controller
    {
        private readonly IUserService userService;
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;

        public DashboardController(IUserService userService, ICategoryService categoryService, IProductService productService)
        {
            this.userService = userService;
            this.categoryService = categoryService;
            this.productService = productService;
        }
        public IActionResult Index()
        {
           
            return View();
        }
    }
}
