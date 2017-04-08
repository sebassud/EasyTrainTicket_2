using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyTrainTickets.Common.DTOs
{
    [DataContract(IsReference = true)]
    public class FilterPathsDTO
    {
        [DataMember]
        public List<PathDTO> Paths { get; set; }
        [DataMember]
        public DateTime UserTime { get; set; }
    }
}
