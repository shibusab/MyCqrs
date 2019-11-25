using MyCqrs.Claims.Commands;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MyCqrs.Persistent
{
   public  interface IClaimsCommandService
    {
        void InsertClaim(Guid clainId, ClaimFileCommand claimFileCommand);
        void RejectClaim(ClaimRejectedCommand command);
        void ApproveClaim(ClaimApprovedCommand command);

    }
}
