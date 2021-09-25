using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Models.ViewModels
{
    public class OrderPlacementViewModel
    {
        public ShopCart ShopCart { get; set; }
        public OrderDataUserViewModel OrderDataUser { get; set; }
        public Delivery Delivery { get; set; }
        public int QuantityOrdersOfUser { get; set; }
        public string ReturnUrl { get; set; }
    }
}
