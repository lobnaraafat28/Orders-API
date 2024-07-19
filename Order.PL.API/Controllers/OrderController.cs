using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Odrer.Core;
using Odrer.Services.Helpers;
using Odrer.Services.Services;
using Order.PL.API.DTOs;
using Orders.API.DTOs;
using Orders.Core.Enums;
using Orders.Core.Interfaces;
using Orders.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Orders.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IGenericRepository<OrderModel> repo;
        private readonly UserManager<User> user;
        private readonly IGenericRepository<Product> proRepo;
        private readonly IGenericRepository<Invoice> invRepo;
        private readonly IEmailSender emailSender;

        public OrderController(IGenericRepository<OrderModel> repo, UserManager<User> user, IGenericRepository<Product> proRepo,IGenericRepository<Invoice> invRepo ,IEmailSender emailSender)
        {
            this.repo = repo;
            this.user = user;
            this.proRepo = proRepo;
            this.invRepo = invRepo;
            this.emailSender = emailSender;
        }
        [HttpGet("GetAllOrders")]
        [Authorize(Roles = "Admin")]

        public async Task<IEnumerable<OrderModel>> GetAllOrders()
        {
            return await repo.GetAllAsync();
        }
        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder(CreateOrderDTO orderdto)
        {
           
            var orderCreated = new OrderModel()
            {
                OrderDate = DateTime.Now,
                Status = Core.Enums.Status.pending,
                PaymentMethod = orderdto.PaymentMethod,
                CustomerId = orderdto.CustomerId,
                OrderItems = orderdto.OrderItems.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                }).ToList(),
                TotalAmount = DiscountService.AddDiscount(DiscountService.CalculateTotal(orderdto.OrderItems.ToList()))  
            };
            var validator = new OrderValidation(proRepo, repo);
            foreach (var i in orderCreated.OrderItems)
            {
                var stock = await validator.CheckStock(i, i.Quantity);
                if (stock != -1)
                {

                    i.Quantity = stock;
                }
                
            }
            await repo.Add(orderCreated);
            var invoice = new Invoice()
            {
                InvoiceDate = DateTime.Now,
                OrderId = orderCreated.OrderId,
                TotalAmount = orderCreated.TotalAmount
            };
            await invRepo.Add(invoice); 

            return Ok(orderCreated);

        }
        [HttpPut("UpdateStatus")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> UpdateOrderStatus(int orderId, Status status)
        {
            var validator = new OrderValidation(proRepo, repo);
            var order = await repo.GetByIdAsync(orderId);
            order.Status = status;
            await validator.CheckStatusAndChangeStock(orderId);
            repo.Update(order);

            emailSender.SendEmail(order.Customer.Email, "Track Order", status);
            return Ok();

        }
        [HttpGet("{id}")]
        public async Task<OrderDTO> GetDetailsOfOrder(int orderId)
        {
            var order = await repo.GetByIdAsync(orderId);
            var specOrder = new OrderDTO()
            {
                CustomerId = order.CustomerId,
                OrderId = orderId,
                Status = order.Status,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                OrderItems = order.OrderItems,
                PaymentMethod = order.PaymentMethod
            };
            return specOrder;
        }

       


    }

}
