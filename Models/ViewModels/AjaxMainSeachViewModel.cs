using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Models.ViewModels
{
    public class AjaxMainSeachViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public List<string> Tags { get; set; }
    }
}
