using MyCqrs.Seed;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCqrs.Claims.Domain
{
    public class Claim : AggregateRoot
    {
        private Guid _id;
        public override Guid Id { get { return _id; } }

        public string ClaimNo { get; set; }
        public DateTime ClaimFiledDate { get; set; }
        public DateTime ClaimAmendDate { get; set; }
        public decimal ClaimAmount { get; set; }
        public ClaimState ClaimStatus { get; set; }
        public string ClaimReviewedBy { get; set; }
        public Claim() { }
        public void FileNewClaim(string claimNo, decimal claimAmount, DateTime filedDate)
        {
            _id = Guid.NewGuid();
            ClaimNo = claimNo;
            ClaimFiledDate = filedDate;
            ClaimAmount = claimAmount;
            ClaimStatus = ClaimState.Filed;

            base.ApplyChange(new Events.ClaimFiledEvent(ClaimNo, ClaimAmount, ClaimFiledDate),true);
        }

        public void RejectClaim(Guid guid, string claimNo,string reviewedBy)
        {
            _id = guid;
            ClaimNo = claimNo;
            ClaimStatus = ClaimState.Canceled;
            
            base.ApplyChange(new Events.ClaimRejectedEvent(ClaimNo,reviewedBy, DateTime.Now), true);
        }

        public void ApproveClaim(Guid guid, string claimNo, string approvedBy)
        {
            _id = guid;
            ClaimNo = claimNo;
            ClaimStatus = ClaimState.Approved;

            base.ApplyChange(new Events.ClaimApprovedEvent(ClaimNo, approvedBy, DateTime.Now), true);
        }

        public void AmendClaim(Guid guid, string claimNo, decimal claimAmount, DateTime amendDate)
        {
            _id = guid;
            ClaimNo = claimNo;
            ClaimAmendDate= amendDate ;
            ClaimAmount = claimAmount;
            ClaimStatus = ClaimState.Amended;
        }
    }
}
