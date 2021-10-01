using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Models.ViewModels
{
    public class OrderFilterViewModel
    {
        public SelectList SelectStates { get; set; }
        public int OrderState { get; set; }

        public OrderFilterViewModel(int orderState = -1)
        {
            IList<SelectListItem> list = Enum
                .GetValues(typeof(OrderState))
                .Cast<OrderState>()
                .Select(x => new SelectListItem 
                { 
                    Text = x.ToString(), 
                    Value = ((int)x).ToString() 
                }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "All",
                Value = "-1",
                Selected = true
            });

            SelectStates = new SelectList(list, "Value", "Text", orderState);

            OrderState = orderState;
        }
    }
}
