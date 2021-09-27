using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Models.ViewModels
{
    public class MyOrdersViewModel
    {
        public Tuple<int, Order> IdAndOrderTupple { get; set; }
        public List<Product> Products { get; set; }
    }
}
