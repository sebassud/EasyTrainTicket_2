using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyTrainTickets.Common.DTOs
{
    [DataContract(IsReference = true)]
    public class ConnectionPartDTO : EntityDTO
    {
        [DataMember]
        public virtual RouteDTO Route { get; set; }
        [DataMember]
        public virtual ConnectionDTO Connection { get; set; }
        [DataMember]
        public virtual DateTime StartTime { get; set; }
        [DataMember]
        public virtual DateTime EndTime { get; set; }
        [DataMember]
        public virtual string Seats { get; set; }
        [DataMember]
        public virtual int FreeSeats { get; set; }
    }
}
