using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OnlineStore.Models
{
    [Display(Name = "Product")]
    public class Product
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Feature { get; set; }

        [DisplayFormat(DataFormatString = "{0}$")]
        [Column(TypeName = "decimal(9, 2)")]
        public decimal Price { get; set; }

        [Display(Name = "Top status")]
        public bool IsTop { get; set; }

        [DisplayFormat(DataFormatString = "{0}$")]
        [Column(TypeName = "decimal(9, 2)")]
        public decimal Discount { get; set; }
        public int Count { get; set; }
        
        [Display(Name = "Commodity code")]
        public string CommodityCode { get; set; }
        public byte[] Image { get; set; }
        public int? ManufacturerId { get; set; }

        //////////////////////////////////////////////////
        //public int? CategoryId { get; set; }
        //public ICollection<Category> Categories { get; set; }
        //////////////////////////////////////////////////

         
        // Navigation
        public Manufacturer Manufacturer { get; set; }
        public ICollection<CategoryProduct> CategoryProducts { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }

        /// ////////////////////////////////////////////////////
        //public IEnumerable<Template> Templates { get; set; }
        public Characteristic Characteristic { get; set; }
        /// ////////////////////////////////////////////////////
        //public BankCredit BankCredit { get; set; }
    }
}
