using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyTrainTickets.Common.DTOs
{
    [DataContract(IsReference = true)]
    public class UserDTO : EntityDTO
    {
        [DataMember]
        public virtual string Login { get; set; }
        [DataMember]
        public virtual string Password { get; set; }
        [DataMember]
        public virtual string Tickets { get; set; }
        [DataMember]
        public virtual bool IsAdmin { get; set; }
    }
}
