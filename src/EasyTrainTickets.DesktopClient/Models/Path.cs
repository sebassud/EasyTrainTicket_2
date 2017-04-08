using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTrainTickets.DesktopClient.Models
{
    public class Path
    {
        public List<Route> Track { get; set; } = new List<Route>();

        public int Count
        {
            get
            {
                return Track.Count;
            }
        }
        public void AddPart(Route route)
        {
            Track.Add(route);
        }
       
    }
}
