using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Models.ViewModels
{
    public class ManufacturerViewModel
    {
        public Manufacturer Manufacturer { get; set; }
        public IFormFile Image { get; set; }
    }
}
