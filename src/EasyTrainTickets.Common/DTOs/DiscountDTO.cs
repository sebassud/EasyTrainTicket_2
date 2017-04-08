using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyTrainTickets.Common.DTOs
{
    [DataContract(IsReference = true)]
    public class DiscountDTO : EntityDTO
    {
        [DataMember]
        public virtual string Type { get; set; }
        [DataMember]
        public virtual double Percent { get; set; }
    }
}
