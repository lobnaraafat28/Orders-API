using Orders.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odrer.Services.Services
{
    public class DiscountService
    {
        public static decimal AddDiscount(decimal total)
        {
            if (total < 100)
            {
                return total;
            }
            else if (total >= 100 && total < 200)
            {
                return total * (5 / 100);
            }
            else if (total >= 200)
            {
                return total * (1 / 10);
            }
          return total;
        }
        public static decimal CalculateTotal(List<OrderItem> items)
        {
            decimal total = 0;
            foreach (var item in items)
            {
                total = total + (item.UnitPrice * item.Quantity);
            }
            return total;
        }
    }
}
