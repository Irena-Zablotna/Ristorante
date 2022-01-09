using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ristorante.EmailSender
{
   public interface IEmailSenderService
    {
        Task<bool> Send(string to, string subject, string name);
    }
}
