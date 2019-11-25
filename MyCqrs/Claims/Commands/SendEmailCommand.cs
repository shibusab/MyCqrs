using MyCqrs.Seed;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCqrs.Claims.Commands
{
    public class SendEmailCommand : Command
    {
        public readonly Domain.ClaimState ClaimState;
        public readonly string ClaimNo;
        public readonly string From;
        public readonly string To;
        public readonly string EmailSubject;
        public readonly string EmailBody;
        public SendEmailCommand(string claimNo, string from, string to, string emailSubject,string emailBody)
        {
            ClaimNo = claimNo;
            From = from;
            To = to;
            EmailSubject = emailSubject;
            EmailBody = emailBody;
        }
    }
}
