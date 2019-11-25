using MyCqrs.Seed;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCqrs.Claims.Events
{
   public class ClaimFiledEvent :Event
    {
        public readonly string ClaimNo;
        public readonly DateTime FiledDate;
        public readonly decimal ClaimAmount;
        public ClaimFiledEvent(string claimNo, decimal claimAmount, DateTime filedDate)
        {
            ClaimNo = claimNo;
            ClaimAmount = claimAmount;
            FiledDate = filedDate;
        }
    }
}
