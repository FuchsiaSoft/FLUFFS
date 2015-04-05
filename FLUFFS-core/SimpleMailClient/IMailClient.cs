using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMailClient
{
    public interface IMailClient
    {
        void SendEmail(IEnumerable<string> recipients, string subject,
            string body, string sender);
    }
}
