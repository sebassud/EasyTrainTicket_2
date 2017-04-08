using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyTrainTickets.Domain.Model;

namespace EasyTrainTickets.Domain.Data
{
    public partial class EasyTrainTicketsDbEntities : TransactionalDbContext, ITransactionalData
    {
        public IDbSet<Connection> Connections { get; set; }
        public IDbSet<ConnectionPart> ConnectionParts { get; set; }
        public IDbSet<Route> Routes { get; set; }
        public IDbSet<Train> Trains { get; set; }
        public IDbSet<User> Users { get; set; }
        public IDbSet<Discount> Discounts { get; set; }

#if Debug
        static EasyTrainTicketsDbEntities()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<EasyTrainTicketsDbEntities>());
        }
#endif
        public EasyTrainTicketsDbEntities(bool withTransaction)
            : base(withTransaction)
        {
            Database.SetInitializer(new EasyTrainTicketsDBInitializer());
        }
    }
}
