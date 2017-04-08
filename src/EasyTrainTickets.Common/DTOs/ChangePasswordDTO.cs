using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyTrainTickets.Common.DTOs
{
    [DataContract(IsReference = true)]
    public class ChangePasswordDTO : UserDTO
    {
        [DataMember]
        public string NewPassword { get; set; }
    }
}
