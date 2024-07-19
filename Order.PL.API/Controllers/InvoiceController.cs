using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.PL.API.DTOs;
using Orders.Core.Interfaces;
using Orders.Core.Models;
using System.Data;

namespace Orders.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IGenericRepository<Invoice> repo;

        public InvoiceController(IGenericRepository<Invoice> repo)
        {
            this.repo = repo;
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<InvoiceDTO> GetInvoiceDetails(int id)
        {
            var invoice = await repo.GetByIdAsync(id);
            var invoicedto = new InvoiceDTO
            {
                InvoiceDate = invoice.InvoiceDate,
                OrderId = invoice.OrderId,
                TotalAmount = invoice.TotalAmount,
            };
            return invoicedto;
        }
        [HttpGet("GetAllInvoices")]
        [Authorize(Roles = "Admin")]

        public async Task<IEnumerable<Invoice>> GetAllInvoices()
        {
            var invoices = await repo.GetAllAsync();
            return invoices;
            
        }
    }
}
