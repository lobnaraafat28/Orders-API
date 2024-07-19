using Orders.Core.Interfaces;
using Orders.Core.Models;
using Orders.Repo.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Repo.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly OrderManagementDbContext _context;

        public CustomerRepository(OrderManagementDbContext context) : base(context)
        {
           _context = context;
        }
        public IQueryable<OrderModel> GetAllOrders(int id)
        {
            return  _context.Orders.Where(a=>a.CustomerId == id);
        }
    }
}
