using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Models.Product
{
    public class ListProduct
    {
        public Entities.Category Category { get; set; }
        public Pagination Pagination { get; set; }
    }
}
