using Orders.Core.Models;

namespace Order.PL.API.DTOs
{
    public class InvoiceDTO
    {
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int OrderId { get; set; }
    }
}
