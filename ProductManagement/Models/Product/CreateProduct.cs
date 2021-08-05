using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Models.Product
{
    public class CreateProduct
    {
        [Required(ErrorMessage ="The product name is required")]
        [MaxLength(500)]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "The infomation is required")]
        [MaxLength(1000)]
        public string Infomation { get; set; }
        [Required(ErrorMessage = "The publish year is required")]
        [Range(minimum: 2010, maximum: 2021)]
        public int PublishYear { get; set; }
        [Required(ErrorMessage = "The quantity is required")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "The price is required")]
        public int Price { get; set; }
        public IFormFile Photo { get; set; }
        public int CategoryId { get; set; }
        public double Discount { get; set; }

    }
}
