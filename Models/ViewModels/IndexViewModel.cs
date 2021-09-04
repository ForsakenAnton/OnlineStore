using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Models.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Manufacturer> Manufacturers { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public List<ProductsGroupByCategoryViewModel> ProductsGroupByCategoryViewModel {get; set; }
        //public IEnumerable<Category> Categories { get; set; }
        //public IEnumerable<CategoryProduct> CategoryProducts { get; set; }
    }
}
