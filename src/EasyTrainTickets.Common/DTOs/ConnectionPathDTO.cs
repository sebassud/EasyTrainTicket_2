using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyTrainTickets.Common.DTOs
{
    [DataContract(IsReference = true)]
    public class ConnectionPathDTO
    {
        [DataMember]
        public List<ConnectionPartDTO> ConnectionsParts { get; set; }
    }
}
