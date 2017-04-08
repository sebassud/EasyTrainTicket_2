using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTrainTickets.DesktopClient.Models
{
    public class Ticket
    {
        public ConnectionPath ConnectionPath { get; set; }
        public List<int[]> Seats { get; set; }
        public List<int> ConPartsId { get; set; }
        public Dictionary<Discount, int> Discounts { get; set; } = new Dictionary<Discount, int>();

        public int NumberOfSeats
        {
            get
            {
                int sum = 0;
                foreach (var key in Discounts.Keys) sum += Discounts[key];
                return sum;
            }
        }
        
        public int Count
        {
            get
            {
                return Math.Max(ConPartsId.Count, ConnectionPath.Count);
            }
        }
        private int change;
        public int Change
        {
            get
            {
                return change;
            }
        }
        private decimal price;
        public decimal Price
        {
            get
            {
                return price;
            }
        }
        private DateTime startTime;
        public DateTime StartTime
        {
            get
            {
                return startTime;
            }
        }
        private DateTime endTime;
        public DateTime EndTime
        {
            get
            {
                return endTime;
            }
        }

        private List<string> sourcePictures;
        public List<string> SourcePictures
        {
            get
            {
                return sourcePictures;
            }
        }
        private string startStation;
        public string StartStation
        {
            get
            {
                return startStation;
            }
        }
        private string endStation;
        public string EndStation
        {
            get
            {
                return endStation;
            }
        }

        public string DiscountView
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (var key in Discounts.Keys)
                {
                    if (Discounts[key] > 0)
                        sb.Append(string.Format("{0}: {1}\n", key.Type, Discounts[key]));
                }
                sb.Remove(sb.Length - 1, 1);
                return sb.ToString();
            }
        }

        public Ticket()
        {
            ConnectionPath = new ConnectionPath();
            Seats = new List<int[]>();
            ConPartsId = new List<int>();
            sourcePictures = new List<string>();
        }
        public Ticket(List<ConnectionPart> conParts) : this()
        {
            ConnectionPath.ConnectionsParts = conParts;
            ConnectionPath.Initialize();
            this.Initialize();
        }
        public void AddSeat(int[] seat)
        {
            Seats.Add(seat);
        }

        public int this[int i]
        {
            get
            {
                return ConPartsId[i];
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Seats.Count; i++)
            {
                sb.AppendFormat("{0,-15} => {1,-15} {2,10} {3,-15} {4,10} {5,5}\n", ConnectionPath[i].Route.From, ConnectionPath[i].Route.To, "Pociąg: ", ConnectionPath[i].Connection.Name, "Zarezerwowane miejsca:", string.Join(",",Seats[i]));
            }

            return sb.ToString();
        }

        public void Initialize()
        {
            startTime = ConnectionPath[0].StartTime;
            endTime = ConnectionPath.ConnectionsParts.Last().EndTime;
            startStation = ConnectionPath[0].Route.From;
            endStation = ConnectionPath.ConnectionsParts.Last().Route.To;
            price = 0;
            decimal count = 0;
            foreach(var key in Discounts.Keys)
            {
                count += (decimal)(Discounts[key] * key.Percent);
            }
            price = Math.Round(count * ConnectionPath.Price, 2);
            change = ConnectionPath.Change;
            for (int i = 0; i < Count - 1; i++)
            {
                if (ConnectionPath[i].Connection.Id != ConnectionPath[i + 1].Connection.Id)
                {
                    if (ConnectionPath[i].Connection.Train.Type == "Pośpieszny") sourcePictures.Add("/Resources/pospImg.png");
                    else if (ConnectionPath[i].Connection.Train.Type == "Ekspres") sourcePictures.Add("/Resources/exImg.png");
                }
            }
            if (ConnectionPath[Count - 1].Connection.Train.Type == "Pośpieszny") sourcePictures.Add("/Resources/pospImg.png");
            else if (ConnectionPath[Count - 1].Connection.Train.Type == "Ekspres") sourcePictures.Add("/Resources/exImg.png");
        }
    }
}
