using MyCqrs.Seed;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCqrs.Claims.Events
{
    public class ClaimApprovedEvent:Event
    {
        public readonly string ClaimNo;
        public readonly DateTime ApprovedDate;
        public readonly string ApprovedBy;
        public ClaimApprovedEvent(string claimNo, string approvedBy, DateTime approvedDate)
        {
            ClaimNo = claimNo;
            ApprovedDate = approvedDate;
            ApprovedBy = approvedBy;
        }
    }
}
