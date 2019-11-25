using MyCqrs.Seed;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCqrs.Claims.Commands
{
    public class ClaimRejectedCommand:Command
    {
        public readonly string ClaimNo;
        public readonly DateTime RejectedDate;
        public readonly string ReviewedBy;
        public ClaimRejectedCommand(string claimNo, string reviewedBy, DateTime rejectedDate)
        {
            ClaimNo = claimNo;
            RejectedDate = rejectedDate;
            ReviewedBy = reviewedBy;
        }
    }
}
