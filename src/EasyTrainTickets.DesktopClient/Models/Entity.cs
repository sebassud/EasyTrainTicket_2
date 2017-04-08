using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTrainTickets.DesktopClient.Models
{
    public abstract class Entity
    {
        public virtual int Id { get; set; }
        public virtual byte[] Version { get; set; }
    }
}
