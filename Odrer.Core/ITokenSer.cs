using Microsoft.AspNetCore.Identity;
using Orders.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odrer.Core
{
    public interface ITokenSer
    {
            Task<string> CreateTokenAsync(User user, UserManager<User> userManager);
    }
}
