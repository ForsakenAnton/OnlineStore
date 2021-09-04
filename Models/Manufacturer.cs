using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Models
{
    public class Manufacturer
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public byte[] Image { get; set; }
        
        // Navigation
        public ICollection<Product> Products { get; set; }
    }
}
