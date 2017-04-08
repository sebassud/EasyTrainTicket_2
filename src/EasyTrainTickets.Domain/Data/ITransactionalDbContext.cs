using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTrainTickets.Domain.Data
{
    public enum TransactionStatus
    {
        None,
        Open,
        Accepted,
        Rejected,
        Errored
    }
    public interface ITransactionalDbContext : IDbContext
    {
        TransactionStatus TransactionStatus { get; }
        bool Accept(bool overrideReject = false);

        ///<inheritdoc />
        void Reject();
    }
}
