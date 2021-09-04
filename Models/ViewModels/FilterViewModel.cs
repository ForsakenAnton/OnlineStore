using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Models.ViewModels
{
    public class FilterViewModel
    {
        //public int? SelectedManufacturer { get; private set; }
        public int? ProductId { get; set; }
        public int? CategoryId { get; private set; }
        public string SelectedSearchString { get; private set; }

        public FilterViewModel(/*List<Manufacturer> manufacturers, int? manufacturer,*/int? productId, int? categoryId, string searchString)
        {
            //manufacturers.Insert(0, new Manufacturer { Title = "All", Id = 0 });
            //Manufacturers = new SelectList(manufacturers, "Id", "Title", manufacturer);
            //SelectedManufacturer = manufacturer;
            ProductId = productId;
            CategoryId = categoryId;
            SelectedSearchString = searchString;
        }
        //public SelectList Manufacturers { get; private set; }
    }
}
