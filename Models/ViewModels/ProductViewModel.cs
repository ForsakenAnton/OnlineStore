using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Models.ViewModels
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public IFormFile Image { get; set; } // без required. Логика будет прописана в контроллере метода Create по типу если null, то AddModelError("Image", "Не добавлено изображение"). 
        public SelectList ManufacturersList { get; set; } // required. Так как value пойдет в Product.ManufacturerId
        public SelectList CategoriesList { get; set; } // без required. Категорию можно не выбирать(логика в контроллере)
        public int[] categoriesId { get; set; }
        public int[] oldCategoriesId { get; set; }
    }
}
