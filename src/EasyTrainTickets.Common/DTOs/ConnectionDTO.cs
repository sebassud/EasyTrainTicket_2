using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyTrainTickets.Common.DTOs
{
    [DataContract(IsReference = true)]
    public class ConnectionDTO : EntityDTO
    {   
        [DataMember]
        public virtual string StartPlace { get; set; }
        [DataMember]
        public virtual string EndPlace { get; set; }
        [DataMember]
        public virtual string Name { get; set; }
        [DataMember]
        public virtual TrainDTO Train { get; set; }
        [DataMember]
        public virtual List<ConnectionPartDTO> Parts { get; set; }
    }
}
