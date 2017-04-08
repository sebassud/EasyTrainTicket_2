using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyTrainTickets.DesktopClient.Models;

namespace EasyTrainTickets.DesktopClient.Work
{
    public interface IUnitOfWork
    {
        bool Start();
        List<Route> GetRoutes { get; }
        List<Train> GetTrains { get; }
        List<Discount> GetDiscounts { get; }
        List<ConnectionPath> Search(string from, string middle, string to, DateTime userTime);
        List<string> Stations { get; }
        User SignIn(string login, string password);
        User Registration(string login, string password);
        User ChangePassword(User user, string newPassword);
        List<Ticket> CreateTickets(User user);
        List<int> GetSeats(ConnectionPath conpath, int from, int to);
        bool BuyTicket(User currentUser, Ticket ticket);
        bool DeleteTicket(User user, Ticket ticket);
        void AddConnection(Connection connection);
    }
}
