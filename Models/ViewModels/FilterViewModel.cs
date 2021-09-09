using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Models.ViewModels
{
    public class FilterViewModel
    {
        public int? ProductId { get; set; }
        public int? CategoryId { get; private set; }
        public int? ManufacturerId { get; private set; }
        public string SelectedSearchString { get; private set; }
        public SelectList Categories { get; set; }
        public SelectList Manufacturers { get; set; }

        public FilterViewModel(int? productId, string searchString, int? categoryId, List<Category> categories, int? manufacturerId, List<Manufacturer> manufacturers)
        {
            if (manufacturers != null)
            {
                manufacturers.Insert(0, new Manufacturer { Title = "All manufacturers", Id = 0 });
                Manufacturers = new SelectList(manufacturers, "Id", "Title", manufacturerId);
            }

            if (categories != null)
            {
                categories.Insert(0, new Category { Title = "All categories", Id = 0 });
                Categories = new SelectList(categories, "Id", "Title", categoryId);
            }

            ProductId = productId;
            CategoryId = categoryId;
            ManufacturerId = manufacturerId;
            SelectedSearchString = searchString;        
        }
    }
}
