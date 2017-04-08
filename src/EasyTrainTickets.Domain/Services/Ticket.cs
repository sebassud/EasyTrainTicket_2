using EasyTrainTickets.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTrainTickets.Domain.Services
{
    public class Ticket
    {
        public ConnectionPath ConnectionPath { get; set; } = new ConnectionPath();
        public List<int[]> Seats { get; set; } = new List<int[]>();

        public List<int> conPartsId = new List<int>();
        public Dictionary<Discount, int> Discounts { get; set; } = new Dictionary<Discount, int>();
        public List<int> discountsId = new List<int>();

        public int this[int i]
        {
            get
            {
                return conPartsId[i];
            }
        }

        public int Count
        {
            get
            {
                return ConnectionPath.ConnectionsParts.Count;
            }
        }
        
    }
}
