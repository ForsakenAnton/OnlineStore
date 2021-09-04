using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Models.ViewModels
{
    public class CategoryViewModel
    {
        public Category Category { get; set; }

        //[Required]
        public IFormFile Image { get; set; }
    }
}
