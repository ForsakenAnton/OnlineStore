using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Models.ViewModels
{
    public class OrderPlacementViewModel
    {
        public ShopCart ShopCart { get; set; }
        public OrderDataUserAndDeliveryViewModel OrderData { get; set; }
    }
}
