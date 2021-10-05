using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Models
{
    public class Characteristic
    {
        public int Id { get; set; }
        public string SerializedCharactetistics { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
