using EasyTrainTickets.Domain.Data;
using EasyTrainTickets.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EasyTrainTickets.Domain.Services
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

        public List<ConnectionPath> SecondSearch(DateTime userTime, IEasyTrainTicketsDbEntities _dbContext)
        {
            List<ConnectionPath> conPaths = new List<ConnectionPath>();
            List<ConnectionPart> startParts = ListOfConnections(0, userTime, _dbContext).
                Where(cp => SqlMethods.DateDiffMinute(userTime, cp.StartTime) >= 0 && 
                    SqlMethods.DateDiffHour(userTime, cp.StartTime) < 20).ToList();
            foreach (var startPart in startParts)
            {
                userTime = startPart.EndTime;
                ConnectionPath conPath = new ConnectionPath();
                conPath.Add(startPart);
                for (int i = 1; i < Count; i++)
                {
                    var conpart = FindBestConnectionPart(i, userTime, _dbContext);
                    if (conpart == null)
                    {
                        conPath = null;
                        break;
                    }
                    conPath.Add(conpart);
                    Connection con = conpart.Connection;
                    List<ConnectionPart> list = con.Parts.ToList();
                    int k = list.FindIndex(cp => cp.Id == conpart.Id);
                    while (i + 1 < Count && k + 1 < list.Count && list[k + 1].Route.Id == Track[i + 1].Id)
                    {
                        conPath.Add(list[k + 1]);
                        k++;
                        i++;
                    }
                    userTime = list[k].EndTime;
                }
                if (conPath != null)
                {
                    if (conPath.Changes < 5)
                        if (TimeSpan.Compare(conPath.ConnectionsParts.Last().EndTime - conPath.ConnectionsParts.First().StartTime, new TimeSpan(25, 0, 0)) < 0)
                            conPaths.Add(conPath);
                }
            }
            return conPaths;
        }

        private int Min(int i, DateTime userTime, IEasyTrainTicketsDbEntities _dbContext)
        {
            var list = ListOfConnections(i, userTime, _dbContext);
            if (list.Count == 0) return int.MaxValue;
            return list.Min(cp => SqlMethods.DateDiffMinute(userTime, cp.StartTime) >= 0 ? 
                SqlMethods.DateDiffMinute(userTime, cp.StartTime) : int.MaxValue);
        }

        private ConnectionPart FindBestConnectionPart(int i, DateTime userTime, IEasyTrainTicketsDbEntities dbContext)
        {
            int minTime = Min(i, userTime, dbContext);
            if (minTime > 300)
                return null;
            ConnectionPart conPart = ListOfConnections(i, userTime, dbContext).Where(cp =>
                        SqlMethods.DateDiffMinute(userTime, cp.StartTime) == minTime).First();
            return conPart;

        }
        private List<ConnectionPart> ListOfConnections(int i, DateTime userTime, IEasyTrainTicketsDbEntities _dbContext)
        {
            Route route = Track[i];
            return _dbContext.ConnectionParts.Where(cp => cp.Route.Id == route.Id && 
                (cp.StartTime.Day == userTime.Day || cp.StartTime.Day == userTime.Day + 1) && 
                    cp.StartTime.Month == userTime.Month).ToList();
               
        }
    }
}
