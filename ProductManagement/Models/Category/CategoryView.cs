using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Models
{
    public class CategoryView
    {
        public List<Entities.Category> Categories { get; set; }
        public Pagination Pagination { get; set; }
    }
}
