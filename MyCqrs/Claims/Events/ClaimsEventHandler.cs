using MyCqrs.Claims.Commands;
using MyCqrs.Claims.Domain;
using MyCqrs.Seed;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCqrs.Claims.Events
{
    public class ClaimsEventHandler :IHandles<ClaimFiledEvent>, IHandles<ClaimRejectedEvent>, IHandles<ClaimApprovedEvent>
    {
        private readonly ICommandSender _bus;
        public ClaimsEventHandler(ICommandSender bus)
        {
            _bus = bus;
        }

        public void Handle(ClaimFiledEvent @event)
        {
            var from = "test@test.com";
            var to = "test1@test.com";
            var cc = string.Empty;
            var subject = string.Format("Claim {0} was set to {1}", @event.ClaimNo, ClaimState.Filed.ToString());
            var body = string.Format("Claim {0} was {1} on {2}", @event.ClaimNo, ClaimState.Filed.ToString(), @event.FiledDate);

            var command = new SendEmailCommand(@event.ClaimNo, from, to, subject, body);
            _bus.Send(command);
        }

        public void Handle(ClaimRejectedEvent @event)
        {
           var from = "test@test.com";
           var to = "test1@test.com";
           var cc = string.Empty;
           var subject = string.Format("Claim {0} was set to {1}", @event.ClaimNo,ClaimState.Rejected.ToString());
           var body = string.Format("Claim {0} was {1} on {2}", @event.ClaimNo, ClaimState.Rejected.ToString(), @event.RejectedDate);
           
            var command = new SendEmailCommand(@event.ClaimNo, from,to,subject,body);
            _bus.Send(command);
        }

        public void Handle(ClaimApprovedEvent @event)
        {
            var from = "test@test.com";
            var to = "test1@test.com";
            var cc = string.Empty;
            var subject = string.Format("Claim {0} was set to {1}", @event.ClaimNo, ClaimState.Approved.ToString());
            var body = string.Format("Claim {0} was {1} on {2}", @event.ClaimNo, ClaimState.Approved.ToString(), @event.ApprovedDate);

            var command = new SendEmailCommand(@event.ClaimNo, from, to, subject, body);
            _bus.Send(command);
        }
    }
}
