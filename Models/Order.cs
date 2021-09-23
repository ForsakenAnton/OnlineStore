using OnlineStore.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Display(Name = "Your order's number")]
        public Guid OrderNumber { get; set; } // int, string..?

        [Display(Name = "Order's date")]
        [DataType(DataType.Date)]
        public DateTime DateOfOrder { get; set; }
        public State State { get; set; }

        [Column(TypeName = "decimal(9, 2)")]
        public decimal TotalPrice { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }
    }

    public enum State
    {
        None = 0,     // нет заказа
        Received = 1, // доставлен
        Paid = 2,     // оплачен
        Sent = 3,     // отправлен
        Returned = 4, // возвращен
        Cancelled = 5 // отменен
    }
}
