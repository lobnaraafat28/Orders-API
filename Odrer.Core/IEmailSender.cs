using Orders.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odrer.Core
{
    public interface IEmailSender
    {
        void SendEmail(string email, string subject, Status status);
    }
}
