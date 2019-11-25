using System;
using System.Collections.Generic;
using System.Text;

namespace MyCqrs.Claims.Domain
{
    public enum ClaimState
    {
        Filed,Amended,Canceled,Rejected,Approved,IssuedCheck,Closed
    }
}
