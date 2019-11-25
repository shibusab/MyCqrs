using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dapper;


namespace MyCqrs.Persistent
{
    public class ClaimsQueryService: IClaimsQueryService
    {
        private readonly IUnitOfWork unitOfWork;
        public ClaimsQueryService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ClaimDto GetClaim(string claimNo)
        {
            var claim = new ClaimDto();
            claim = GetClaim(claimNo, unitOfWork.DbConnection);
            return claim;
        }

        public ClaimDto GetClaim(string claimNo, IDbConnection connection)
        {
            var claim= new ClaimDto();
            string sql = "SELECT TOP 1 * from Distribution.Claims where ClaimNo=@claimNoParameter";
            var parameters = new  { claimNoParameter = claimNo };
            
            if(unitOfWork.HasTransactions)
            {
               claim= connection.QueryFirstOrDefault<ClaimDto>(sql, parameters, unitOfWork.DbTransaction);
            }
            else
            {
                claim= connection.QueryFirstOrDefault<ClaimDto>(sql, parameters);
            }

            return claim;
        }
    }
}
