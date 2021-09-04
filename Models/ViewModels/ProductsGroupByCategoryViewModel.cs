using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Models.ViewModels
{
    public class ProductsGroupByCategoryViewModel
    {
        public Tuple<int, string> CategoryTitle { get; set; }
        public List<Product> Products { get; set; }
    }
}
