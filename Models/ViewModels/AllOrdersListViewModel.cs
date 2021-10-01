using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Models.ViewModels
{
    public class AllOrdersListViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
        public PageViewModel PageListViewModel { get; set; }
        public OrderFilterViewModel OrderFilterViewModel { get; set; }
    }
}
