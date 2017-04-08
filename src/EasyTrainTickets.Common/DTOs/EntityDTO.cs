using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyTrainTickets.Common.DTOs
{
    [DataContract(IsReference = true)]
    public abstract class EntityDTO
    {
        [DataMember]
        public virtual int Id { get; set; }
        [DataMember]
        public byte[] Version { get; set; }
    }
}
