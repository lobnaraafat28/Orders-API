using Orders.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Core.Interfaces
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        IQueryable<OrderModel> GetAllOrders(int id);
    }
}
