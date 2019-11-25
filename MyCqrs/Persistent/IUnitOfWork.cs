using System;
using System.Collections.Generic;
using System.Text;

namespace MyCqrs.Persistent
{
    public interface IUnitOfWork
    {
        void Commit();
        void Rollback();
        void Dispose();

        bool HasTransactions{get; }
        System.Data.IDbTransaction DbTransaction { get; }
        System.Data.IDbConnection DbConnection { get; }

    }
}
