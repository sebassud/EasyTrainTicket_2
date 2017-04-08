using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyTrainTickets.Domain.Model;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace EasyTrainTickets.Domain.Data
{
    public interface IEasyTrainTicketsDbEntities : IDbContext
    {
        IDbSet<Connection> Connections { get; set; }
        IDbSet<ConnectionPart> ConnectionParts { get; set; }
        IDbSet<Route> Routes { get; set; }
        IDbSet<Train> Trains { get; set; }
        IDbSet<User> Users { get; set; }
        IDbSet<Discount> Discounts { get; set; }

    }
}
