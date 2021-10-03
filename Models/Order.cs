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
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid OrderNumber { get; set; } // int, string..?

        [Display(Name = "Order's date")]
        [DataType(DataType.Date)]
        public DateTime DateOfOrder { get; set; }
        public OrderState OrderState { get; set; }

        [DisplayFormat(DataFormatString = "{0}$")]
        [Column(TypeName = "decimal(9, 2)")]
        public decimal TotalPrice { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
        public Delivery Delivery { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }
    }

    public enum OrderState
    {
        None = 0,     // нет заказа
        Sent = 1,     // отправлен
        Received = 2, // доставлен
        Paid = 3,     // оплачен 
        Returned = 4, // возвращен
        Cancelled = 5 // отменен
    }
}
