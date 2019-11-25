using System;
using System.Collections.Generic;
using System.Text;

namespace MyCqrs.Seed
{
    public interface IEmail
    {
        void Send(string from, string to, string cc, string subject, string emailBody);
    }
}
