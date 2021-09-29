using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Models.ViewModels
{
    public class MyOrdersViewModel
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public List<Product> Products { get; set; }
    }
}
