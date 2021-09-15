
using OnlineStore.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Models.ViewModels
{
    public class IndexProductsViewModel//<T>
    {
        public IEnumerable<Product> Products { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
        public PageViewModel PageListViewModel { get; set; }
        public int? ProductId { get; set; }
        public IEnumerable<FavoriteProduct> FavoriteProducts { get; set; }
        public User User { get; set; }
        public ShopCart ShopCart { get; set; }
    }
}
