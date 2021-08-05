using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Entities
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(300)]
        public string CategoryName { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        public ICollection<Products> Products { get; set; }
    }
}
