using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Models
{
    public class BankCredit
    {
        public int Id { get; set; }
        public string BankName { get; set; }
        public int CountOfMonths { get; set; }
        public float FirstPayment { get; set; }
        public float Percent { get; set; }

        public int MinMonth { get; set; }
        public int MaxMonth { get; set; }
        public int ProductId { get; set; }

        // Navigation
        public Product Product { get; set; }
    }
}
