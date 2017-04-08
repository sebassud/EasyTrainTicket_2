using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace EasyTrainTickets.Common.DTOs
{
    [DataContract(IsReference = true)]
    public class TicketDTO
    {
        [DataMember]
        public ConnectionPathDTO ConnectionPath { get; set; }
        [DataMember]
        public List<int[]> Seats { get; set; }
        [DataMember]
        public UserDTO User { get; set; }
        [DataMember]
        public Dictionary<DiscountDTO, int> Discounts { get; set; } = new Dictionary<DiscountDTO, int>();
    }
}
