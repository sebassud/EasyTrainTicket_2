using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTrainTickets.Domain.Data
{
    public interface ITransactionalData : IEasyTrainTicketsDbEntities, ITransactionalDbContext
    {
    }
}
