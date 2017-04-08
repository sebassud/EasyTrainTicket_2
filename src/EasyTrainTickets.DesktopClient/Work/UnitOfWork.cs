using EasyTrainTickets.DesktopClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EasyTrainTickets.DesktopClient.Work
{
    public class UnitOfWork : IUnitOfWork
    {
        private HttpClient client; 
        private Graph graph;

        public bool IsConnected
        {
            get
            {
                return graph != null;
            }
        }

        public List<string> Stations
        {
            get
            {
                return graph.Stations;
            }
        }

        public List<Route> GetRoutes
        {
            get
            {
                return Services.GetRoutes(client).ToList();
            }
        }

        public List<Train> GetTrains
        {
            get
            {
                return Services.GetTrains(client).ToList();
            }
        }

        public List<Discount> GetDiscounts
        {
            get
            {
                return Services.GetDiscounts(client).ToList();
            }
        }

        public UnitOfWork()
        {
            AutoMapperConfig.RegisterMappings();
            client = new HttpClient();
            //client.BaseAddress = new Uri("https://easytrainticketsserver20161219105231.azurewebsites.net" + "/");
            client.BaseAddress = new Uri("http://localhost:1210" + "/");
            client.DefaultRequestHeaders.Add("Accept", "application/xml");
        }

        public bool Start()
        {
            if (IsConnected) return true;
            List<Route> routes;
            try
            {
                routes = Services.GetRoutes(client).ToList();
            }
            catch(AggregateException e)
            {
                return false;
            }
            List<string> stations = routes.Select(r => r.From).Distinct().ToList();
            graph = new Graph(stations, routes);
            return true;
        }

        public List<ConnectionPath> Search(string from, string middle, string to, DateTime userTime)
        {
            List<Path> paths;
            if (middle == "")
                paths = graph.SearchPaths(from, to);
            else
                paths = graph.SearchPaths(from, middle, to);
            List<ConnectionPath> conPaths = Services.PostSearchController(client, paths, userTime).ToList();
            //conPaths = conPaths.OrderBy(c => c.StartTime).ThenBy(c => c.EndTime).Take(10).ToList();
            foreach (var conPath in conPaths)
            {
                conPath.Initialize();
            }
            conPaths = conPaths.OrderBy(c => c.StartTime).ThenBy(c => c.EndTime).Take(10).ToList();
            foreach (var conPath in conPaths)
            {
                conPath.WriteConnection();
            }
            //conPaths = conPaths.OrderBy(c => c.StartTime).ThenBy(c => c.EndTime).Take(10).ToList();
            return conPaths;
        }

        public User SignIn(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
                return null;
            string pass = HashMd5(password);
            User user = new User()
            {
                Login = login,
                Password = pass
            };
            user = Services.PostLogin(client, user);
            return user;
        }
        private string HashMd5(string password)
        {
            var salt = System.Text.Encoding.UTF8.GetBytes("susz");
            var pass = System.Text.Encoding.UTF8.GetBytes(password);
            var hmacMD5 = new HMACMD5(salt);
            var saltedHash = hmacMD5.ComputeHash(pass);

            return Convert.ToBase64String(saltedHash);
        }

        public User Registration(string login, string password)
        {
            User user = new User()
            {
                IsAdmin = false,
                Login = login,
                Password = HashMd5(password),
                Tickets = ""
            };
            user = Services.PostRegistration(client, user);
            return user;
        }

        public User ChangePassword(User user, string newPassword)
        {
            user.Password = HashMd5(user.Password);
            newPassword = HashMd5(newPassword);
            return Services.PutRegistration(client, user, newPassword);
        }

        public List<Ticket> CreateTickets(User user)
        {
            List<Ticket> tickets = Services.GetTickets(client, user).ToList();
            return InitializeTickets(tickets);
        }

        private List<Ticket> InitializeTickets(List<Ticket> tickets)
        {
            for (int i = 0; i < tickets.Count; i++)
            {
                tickets[i].ConnectionPath.Initialize();
                tickets[i].Initialize();
            }
            return tickets;
        }

        public List<int> GetSeats(ConnectionPath conpath, int from, int to)
        {
            List<int> freeseats = new List<int>();

            List<ConnectionPart> conparts = new List<ConnectionPart>();
            conparts.AddRange(conpath.ConnectionsParts.GetRange(from, to - from + 1));
            List<int[]> allseats = new List<int[]>();
            foreach (var part in conparts)
            {
                string seats = Services.GetConnectionPart(client, part).Seats;
                if (seats == "") return freeseats;
                allseats.Add(seats.Split(';').Select(Int32.Parse).ToArray());
            }

            int[] table = new int[120];
            foreach (var list in allseats)
                foreach (var s in list)
                    table[s]++;

            int count = to - from + 1;
            for (int i = 0; i < table.Count(); i++)
            {
                if (table[i] == count) freeseats.Add(i);
            }
            return freeseats;
        }

        public bool BuyTicket(User currentUser, Ticket ticket)
        {
            return Services.PutTicket(client, currentUser, ticket);
        }

        public bool DeleteTicket(User user, Ticket ticket)
        {
            return Services.DeleteTicket(client, user, ticket);
        }

        public void AddConnection(Connection connection)
        {
            Services.PostConnection(client, connection);
        }

    }
}
