using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MyCqrs.Persistent
{
    public interface IClaimsQueryService
    {
        ClaimDto GetClaim(string claimNo);
        ClaimDto  GetClaim(string claimNo, IDbConnection connection);
    }
}
