using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Models.Cart
{
    public class ProductItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Infomation { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string Photo { get; set; }
        public int PublishYear { get; set; }
        public bool IsDeleted { get; set; }
        public double Discount { get; set; }
        public int CategoryId { get; set; }

    }
}
