using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Models
{
    public class Delivery
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter the first delivery address")]
        [Display(Name = "First address")]
        public string Line1 { get; set; }

        [Display(Name = "Second address")]
        public string Line2 { get; set; }

        [Display(Name = "Third address")]
        public string Line3 { get; set; }

        [Required(ErrorMessage = "Specify city")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Specify country")]
        [Display(Name = "Country")]
        public string Country { get; set; }


        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
