using ProductManagement.Entities;
using ProductManagement.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Models.Dashboard
{
    public class DashboardView
    {
        public Entities.Category Category { get; set; }

        public Task<List<Entities.Category>> Categorys { get; set; }


        public Task<List<AppIdentityUser>> appIdentityUser { get; set; }

    }
}
