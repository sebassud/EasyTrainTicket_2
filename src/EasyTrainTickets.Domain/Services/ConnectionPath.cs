using EasyTrainTickets.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTrainTickets.Domain.Services
{
    public class ConnectionPath
    {
        public List<ConnectionPart> ConnectionsParts { get; set; } = new List<ConnectionPart>();

        public int Changes
        {
            get
            {
                int changes = 0;
                for(int i = 0; i < ConnectionsParts.Count - 1; i++)
                {
                    if (ConnectionsParts[i].Connection.Id != ConnectionsParts[i + 1].Connection.Id)
                        changes++;
                }
                return changes;
            }
        }

        public int JourneyTime
        {
            get
            {
                return (int)(ConnectionsParts.Last().EndTime - ConnectionsParts.First().StartTime).TotalMinutes;
            }
        }
        public void Add(ConnectionPart conPart)
        {
            ConnectionsParts.Add(conPart);
        }

        public ConnectionPart this[int index]
        {
            get
            {
                return ConnectionsParts[index];
            }
            set
            {
                ConnectionsParts[index] = value;
            }
        }
    }
}
