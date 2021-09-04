using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Models
{
    public class CategoryProduct
    {
        
        public int Id { get; set; }
        //public int? CategoryId { get; set; } // у продуктов может и не быть категории
        public int CategoryId { get; set; }
        public int ProductId { get; set; }
        public Category Category { get; set; }
        public Product Product { get; set; }
    }
}
