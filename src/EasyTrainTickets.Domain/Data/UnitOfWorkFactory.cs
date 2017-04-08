using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTrainTickets.Domain.Data
{
    public interface IUnitOfWorkFactory
    {
        IEasyTrainTicketsDbEntities CreateUnitOfWork();
        ITransactionalData CreateTransactionalUnitOfWork();
    }


    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        public virtual IEasyTrainTicketsDbEntities CreateUnitOfWork()
        {
            return new EasyTrainTicketsDbEntities(false);
        }

        public virtual ITransactionalData CreateTransactionalUnitOfWork()
        {
            return new EasyTrainTicketsDbEntities(true);
        }
    }
}
