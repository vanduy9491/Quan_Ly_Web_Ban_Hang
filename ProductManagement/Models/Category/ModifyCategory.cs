using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Models.Category
{
    public class ModifyCategory
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "The category name is required")]
        [MaxLength(500)]
        public string CategoryName { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
    }
}
