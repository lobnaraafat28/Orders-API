using Microsoft.AspNetCore.Mvc;
using Orders.API.DTOs;
using Orders.Core.Interfaces;
using Orders.Core.Models;

namespace Orders.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository repo;

        public CustomerController(ICustomerRepository Repo)
        {
            repo = Repo;
        }
        [HttpPost("CreateCustomer")]
        public  Task CreateCustomer(CustomerDTO customerdto)
        {
            var addedCustomer = new Customer()
            {
                CustomerId = customerdto.CustomerId,
                Name = customerdto.Name,
                Email = customerdto.Email,
            };
            repo.Add(addedCustomer);
            return  Task.CompletedTask;
        }
        [HttpGet("GetAllOrdersOfCustomer")]
        public IEnumerable<OrderDTO> GetAllOdersOfCustomer(int id)
        {
          var orders =  repo.GetAllOrders(id);
            var orderstoShow = new List<OrderDTO>();
            if (orders != null)
            {
                foreach (var order in orders)
                {
                    var o = new OrderDTO()
                    {
                        OrderItems = order.OrderItems,
                        OrderDate = order.OrderDate,
                        OrderId= order.OrderId,
                        CustomerId = id,
                        PaymentMethod= order.PaymentMethod,
                        Status= order.Status,
                        TotalAmount = order.TotalAmount,
                    };
                    orderstoShow.Add(o);
                  }
            }
            return orderstoShow;
        }

    }
}
