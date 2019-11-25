using MyCqrs.Claims.Domain;
using MyCqrs.Persistent;
using MyCqrs.Seed;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCqrs.Claims.Commands
{
    public class ClaimsCommandHandler
    {
        private readonly IEventPublisher _eventPublisher;
        private IClaimsQueryService queryService;
        private IClaimsCommandService commandService;
        private readonly IRepository<Claim> _eventStoreRepository;
        private readonly IEmail _emailProvider;
        public ClaimsCommandHandler(IEventPublisher eventPublisher,  IClaimsQueryService queryService, IClaimsCommandService commandService, IRepository<Claim> eventStoreRepository, IEmail emailProvider)
        {
            _eventPublisher = eventPublisher;
            this.queryService = queryService;
            this.commandService = commandService;
            _eventStoreRepository = eventStoreRepository;
            _emailProvider = emailProvider;
        }

        public void Handle(ClaimFileCommand command)
        {
            var existingClaim = queryService.GetClaim(command.ClaimNo);
            if(null != existingClaim)
            {
                throw new Exception("Claim Already Exists");
            }

            var claim = new Claim();
            claim.FileNewClaim(command.ClaimNo, command.ClaimAmount, command.FiledDate);
            commandService.InsertClaim( claim.Id, command);
            _eventStoreRepository.Save(claim, -1);

            // Throwing event in Aggregate, not in CommandHandler. 
           // _eventPublisher.Publish(new Events.ClaimFiledEvent(command.ClaimNo, command.ClaimAmount, command.FiledDate));
        }

        public void Handle(ClaimRejectedCommand command)
        {
            var existingClaim = queryService.GetClaim(command.ClaimNo);
            if (null == existingClaim)
            {
                throw new Exception("Missing Claim "+ command.ClaimNo);
            }

            var claim = new Claim();
            claim.RejectClaim(existingClaim.ClaimId, command.ClaimNo, command.ReviewedBy);
            commandService.RejectClaim(command);
            _eventStoreRepository.Save(claim, -1);

            // Throwing event in Aggregate, not in CommandHandler. 
            // _eventPublisher.Publish(new Events.ClaimRejectedEvent(command.ClaimNo, command.ReviewedBy, command.RejectedDate));
        }

        public void Handle(ClaimApprovedCommand command)
        {
            var existingClaim = queryService.GetClaim(command.ClaimNo);
            if (null == existingClaim)
            {
                throw new Exception("Missing Claim " + command.ClaimNo);
            }

            var claim = new Claim();
            claim.ApproveClaim(existingClaim.ClaimId, command.ClaimNo, command.ApprovedBy);
            commandService.ApproveClaim(command);
            _eventStoreRepository.Save(claim, -1);

            // Throwing event in Aggregate, not in CommandHandler. 
            // _eventPublisher.Publish(new Events.ClaimApprovedEvent(command.ClaimNo, command.ApprovedBy, command.ApprovedDate));
        }

        public void Handle(SendEmailCommand command)
        {
            _emailProvider.Send(command.From, command.To,string.Empty, command.EmailSubject,command.EmailBody);
        }
    }
}
