using MyCqrs.Seed;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCqrs.Claims.Commands
{
   public class ClaimApprovedCommand:Command
    {
        public readonly string ClaimNo;
        public readonly DateTime ApprovedDate;
        public readonly string ApprovedBy;
        public ClaimApprovedCommand(string claimNo, string approvedBy, DateTime approvedDate)
        {
            ClaimNo = claimNo;
            ApprovedDate = approvedDate;
            ApprovedBy = approvedBy;
        }
    }
}
