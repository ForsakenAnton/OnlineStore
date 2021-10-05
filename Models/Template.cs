using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Models
{
    public class Template
    {
        public int Id { get; set; }
        public string SerializedTemplates { get; set; }
        public string Title { get; set; }

        //public IEnumerable<Product> Products { get; set; }
        public ICollection<Category> Categories { get; set; }
        //public int? CategoryId  { get; set; }
        //public Category Category { get; set; }
        
    }
}
