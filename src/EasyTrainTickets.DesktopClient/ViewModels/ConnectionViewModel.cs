using Caliburn.Micro;
using EasyTrainTickets.DesktopClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTrainTickets.DesktopClient.ViewModels
{
    public class ConnectionViewModel : Conductor<object>
    {
        private Connection connection;
        public string NameTrain
        {
            get
            {
                return connection.Name;
            }
        }

        public string Title { get; set; } = "Easy Train Tickets";

        public string Picture
        {
            get
            {
                if (connection.Train.Type == "Ekspres") return "/Resources/exImg.png";
                else
                    return "/Resources/pospImg.png";
            }
        }

        private BindableCollection<string> itinerary = new BindableCollection<string>();
        public BindableCollection<string> Itinerary
        {
            get { return itinerary; }
        }
        public ConnectionViewModel(Connection _connection)
        {
            connection = _connection;
            Initialize();
        }

        private void Initialize()
        {
            Itinerary.Add(string.Format("{0,-15} {1,15} {2,15} {3,15}", "Stacja", "Przyjazd", "Odjazd", "Frekwencja"));
            int count = 80;
            if (connection.Train.Type == "Ekspres") count = 60;
            connection.Parts.Sort((p1, p2) => DateTime.Compare(p1.StartTime, p2.StartTime));
            for (int i = 0; i <= connection.Parts.Count; i++)
            {
                double fr=0;
                if(i<connection.Parts.Count)
                {
                    fr = connection.Parts[i].FreeSeats / (double)count;
                }
                if (i == 0)
                    Itinerary.Add(string.Format("{0,-15} {1,15} {2,15}      {3:P0}", connection.Parts[i].Route.From, "", connection.Parts[i].StartTime.ToShortTimeString(), 1-fr));
                else if (i == connection.Parts.Count)
                    Itinerary.Add(string.Format("{0,-15} {1,15} {2,15}", connection.Parts[i - 1].Route.To, connection.Parts[i - 1].EndTime.ToShortTimeString(), ""));
                else
                    Itinerary.Add(string.Format("{0,-15} {1,15} {2,15}      {3:P0}", connection.Parts[i].Route.From, connection.Parts[i - 1].EndTime.ToShortTimeString(), connection.Parts[i].StartTime.ToShortTimeString(), 1 -fr));
            }
        }
    }
}
