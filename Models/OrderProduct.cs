using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Models
{
    public class OrderProduct
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(9, 2)")]
        public decimal PriceOfOneProduct { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}