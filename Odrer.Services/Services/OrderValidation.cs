using Orders.Core.Interfaces;
using Orders.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odrer.Services.Services
{
    public class OrderValidation
    {
        private readonly IGenericRepository<Product> proRepo;
        private readonly IGenericRepository<OrderModel> orderRepo;

        public OrderValidation(IGenericRepository<Product> proRepo, IGenericRepository<OrderModel> orderRepo )
        {
            this.proRepo = proRepo;
            this.orderRepo = orderRepo;
        }
        public async Task<int> ItemCountValidation(int productId ,int Quantity)
        {
            var product = await proRepo.GetByIdAsync(productId);
            if (product.Stock < Quantity) return 0;
            else return 1;
        }
        public async Task CheckStatusAndChangeStock(int orderId)
        {
           var order = await orderRepo.GetByIdAsync(orderId);
            foreach(var i in order.OrderItems)
            if (await ItemCountValidation(i.ProductId, i.Quantity) !=0)
            {
                if (order.Status == Orders.Core.Enums.Status.approved)
                {
                   var product = await proRepo.GetByIdAsync(i.ProductId);
                    product.Stock -= i.Quantity;
                    proRepo.Update(product);
                }
            }
        }
        public async Task<int> CheckStock(OrderItem item, int quantity)
        {
            var product = await proRepo.GetByIdAsync(item.ProductId);
            if (product.Stock < quantity) return product.Stock;
            else return -1;
        }
    }
}
