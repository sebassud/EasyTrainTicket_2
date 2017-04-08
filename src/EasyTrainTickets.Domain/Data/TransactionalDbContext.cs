using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTrainTickets.Domain.Data
{
    public abstract class TransactionalDbContext : DbContext, ITransactionalDbContext
    {
        protected TransactionalDbContext(bool withTransaction = true)
        {
            if (withTransaction)
            {
                this.Database.BeginTransaction();
                this.TransactionStatus = TransactionStatus.Open;
            }
        }

        public bool IsDisposed { get; private set; }

        public TransactionStatus TransactionStatus { get; private set; } = TransactionStatus.None;

        public virtual bool Accept(bool overrideReject = false)
        {
            if (this.TransactionStatus == TransactionStatus.None)
            {
                throw new InvalidOperationException("No transaction to accept");
            }

            if (!overrideReject && this.TransactionStatus == TransactionStatus.Rejected)
            {
                return false;
            }
            this.TransactionStatus = TransactionStatus.Accepted;
            return true;
        }

        ///<inheritdoc />
        public virtual void Reject()
        {
            if (this.TransactionStatus == TransactionStatus.None)
            {
                throw new InvalidOperationException("No transaction to reject");
            }
            this.TransactionStatus = TransactionStatus.Rejected;
        }


        protected override void Dispose(bool disposing)
        {
            if (this.IsDisposed)
            {
                if (disposing)
                {
                    throw new InvalidOperationException("Cannot dispose transactional data context twice");
                }
                return;
            }
            this.IsDisposed = true;

            if (disposing && this.TransactionStatus != TransactionStatus.None)
            {
                try
                {
                    if (this.TransactionStatus == TransactionStatus.Accepted)
                    {
                        this.SaveChanges();
                        this.Database.CurrentTransaction.Commit();
                    }
                    else
                    {
                        this.TransactionStatus = TransactionStatus.Rejected;
                        this.Database.CurrentTransaction.Rollback();
                    }
                }
                catch
                {
                    this.Database.CurrentTransaction.Rollback();
                    this.TransactionStatus = TransactionStatus.Errored;
                    throw;
                }
            }

            base.Dispose(disposing);
        }
    }
}
