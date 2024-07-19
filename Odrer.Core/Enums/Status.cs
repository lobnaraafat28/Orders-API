using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Core.Enums
{
    public enum Status
    {
        canceled = 0,
        pending,
        approved,
        shipped,
        delivered
        
    }
}
