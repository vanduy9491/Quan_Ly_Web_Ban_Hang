using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Entities
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [MaxLength(500)]
        public string ProductName { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Infomation { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        [MaxLength(300)]
        public string Photo { get; set; }
        [Required]
        public int PublishYear { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        [Required]
        public double Discount { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        
    }
}
