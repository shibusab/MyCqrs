using Dapper;
using MyCqrs.Claims.Commands;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace MyCqrs.Persistent
{
    public class ClaimsCommandService : IClaimsCommandService
    {
        private readonly IUnitOfWork unitOfWork;

        public ClaimsCommandService(IUnitOfWork unitofWork)
        {
            this.unitOfWork = unitofWork;
        }

        public void InsertClaim(Guid claimId, ClaimFileCommand command)
        {
            string sql = "INSERT INTO [Distribution].[Claims] ( [ClaimId] , [ClaimNo], [ClaimFiledDate], [ClaimAmount], [ClaimStatus], [ClaimReviewedBy]) VALUES ( @ClaimId, @ClaimNo, @ClaimFiledDate, @ClaimAmount, @ClaimStatus, @ClaimReviewedBy)";
            var parameters = new
            {
                ClaimId = claimId,
                ClaimNo = command.ClaimNo,
                ClaimFiledDate = command.FiledDate,
                ClaimAmount = command.ClaimAmount,
                ClaimStatus = Claims.Domain.ClaimState.Filed.ToString(),
                ClaimReviewedBy = string.Empty
            };

            if (unitOfWork.HasTransactions)
            {
                unitOfWork.DbConnection.Execute(sql, parameters, unitOfWork.DbTransaction);
            }
            else
            {
                unitOfWork.DbConnection.Execute(sql, parameters);
            }
        }

        public void InsertClaim(IDbConnection dbConnection, ClaimFileCommand command)
        {
            
        }

        public void RejectClaim(ClaimRejectedCommand command)
        {
            string sql = "UPDATE [Distribution].[Claims] SET [ClaimStatus] = @ClaimStatus, [ClaimReviewedBy] = @ClaimReviewedBy WHERE [ClaimNo] = @ClaimNo ";
            var parameters = new
            {
                ClaimNo = command.ClaimNo,
                ClaimStatus = Claims.Domain.ClaimState.Rejected.ToString(),
                ClaimReviewedBy = command.ReviewedBy
            };
            if (unitOfWork.HasTransactions)
            {
                unitOfWork.DbConnection.Execute(sql, parameters, unitOfWork.DbTransaction);
            }
            else
            {
                unitOfWork.DbConnection.Execute(sql, parameters);
            }

        }

        public void ApproveClaim(ClaimApprovedCommand command)
        {
            string sql = "UPDATE [Distribution].[Claims] SET [ClaimStatus] = @ClaimStatus, [ClaimReviewedBy] = @ClaimReviewedBy WHERE [ClaimNo] = @ClaimNo ";
            var parameters = new
            {
                ClaimNo = command.ClaimNo,
                ClaimStatus = Claims.Domain.ClaimState.Approved.ToString(),
                ClaimReviewedBy = command.ApprovedBy
            };

            if (unitOfWork.HasTransactions)
            {
                unitOfWork.DbConnection.Execute(sql, parameters, unitOfWork.DbTransaction);
            }
            else
            {
                unitOfWork.DbConnection.Execute(sql, parameters);
            }

        }
    }
}
