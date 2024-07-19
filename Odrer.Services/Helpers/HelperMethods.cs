using Orders.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odrer.Services.Helpers
{
    public class HelperMethods
    {
        public static decimal CalculateTotal(decimal d , int i)
        {
              var sum = (d * i);
            return sum;
        }
    }
}
