using Orders.Core.Enums;
using Orders.Core.Models;

namespace Order.PL.API.DTOs
{
    public class CreateOrderDTO
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string PaymentMethod { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
