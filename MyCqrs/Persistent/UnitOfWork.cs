using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MyCqrs.Persistent
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection dbConnection;
        private IDbTransaction dbTransaction;
        private bool isDisposed = false;
        private bool hasTransactions = false;
        public UnitOfWork(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public UnitOfWork WithTransaction()
        {
            dbTransaction = dbConnection.BeginTransaction();
            hasTransactions = true;
            return this;
        }
        
        public bool HasTransactions { get { return hasTransactions; } }

        public IDbTransaction DbTransaction { get { return dbTransaction; } }
        public IDbConnection DbConnection { get { return dbConnection; } }
        public void Commit()
        {
            if(isDisposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }
            else 
            {
                if (hasTransactions)
                {
                    dbTransaction.Commit();
                }
            }
        }

        public void Rollback()
        {
            if (isDisposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }
            else
            {
                if (hasTransactions)
                {
                    dbTransaction.Rollback();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (isDisposed) return;
            
            if(disposing && dbConnection != null)
            {
                dbConnection.Dispose();
            }
            isDisposed = true;
        }

    }
}
