using MyCqrs.Seed;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCqrs.Claims.Events
{
    public class ClaimRejectedEvent:Event
    {
        public readonly string ClaimNo;
        public readonly DateTime RejectedDate;
        public readonly string ReviewedBy;
        public ClaimRejectedEvent(string claimNo, string reviewedBy, DateTime rejectedDate)
        {
            ClaimNo = claimNo;
            RejectedDate = rejectedDate;
            ReviewedBy = reviewedBy;
        }
    }
}
