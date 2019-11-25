using MyCqrs.Seed;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCqrs.Claims.Commands
{
    public class ClaimFileCommand:Command
    {
        public readonly string ClaimNo;
        public readonly DateTime FiledDate;
        public readonly decimal ClaimAmount;
        public ClaimFileCommand(string claimNo, decimal claimAmount, DateTime filedDate)
        {
            ClaimNo = claimNo;
            ClaimAmount = claimAmount;
            FiledDate = filedDate;
        }
    }
}
