using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyTrainTickets.Common.DTOs;
using EasyTrainTickets.DesktopClient.Models;
using System.Net.Http;
using AutoMapper;

namespace EasyTrainTickets.DesktopClient.Work
{
    public static class Services
    {
        public static IEnumerable<Route> GetRoutes(HttpClient client)
        {
            const string request = "api/Routes";
            var response = client.GetAsync(request).Result;
            response.EnsureSuccessStatusCode();
            var resultDTO = response.Content.ReadAsAsync<IEnumerable<RouteDTO>>().Result;

            var result = Mapper.Map<IEnumerable<RouteDTO>, IEnumerable<Route>>(resultDTO);
            return result;
        }

        public static List<ConnectionPath> PostSearchController(HttpClient client, List<Path> paths, DateTime userTime)
        {
            FilterPathsDTO fp = new FilterPathsDTO()
            {
                Paths = Mapper.Map<List<Path>, List<PathDTO>>(paths),
                UserTime = userTime
            };
            const string request = "api/Search";
            var response = client.PostAsJsonAsync(request, fp).Result;
            response.EnsureSuccessStatusCode();
            var resultDTO = response.Content.ReadAsAsync<List<ConnectionPathDTO>>().Result;
            var result = Mapper.Map<List<ConnectionPathDTO>, List<ConnectionPath>>(resultDTO);
            return result;
        }

        public static List<Discount> GetDiscounts(HttpClient client)
        {
            const string request = "api/Discounts";
            var response = client.GetAsync(request).Result;
            response.EnsureSuccessStatusCode();
            var discountDTO = response.Content.ReadAsAsync<IEnumerable<DiscountDTO>>().Result;
            var result = Mapper.Map<IEnumerable<DiscountDTO>, IEnumerable<Discount>>(discountDTO);
            return result.ToList();
        }

        public static User PostLogin(HttpClient client, User user)
        {
            UserDTO userDTO = Mapper.Map<User, UserDTO>(user);
            const string request = "api/Login";
            var response = client.PostAsJsonAsync(request, userDTO).Result;

            if (!response.IsSuccessStatusCode) return null;

            var resultDTO = response.Content.ReadAsAsync<UserDTO>().Result;
            var result = Mapper.Map<UserDTO, User>(resultDTO);
            return result;
        }

        public static User PostRegistration(HttpClient client, User user)
        {
            UserDTO userDTO = Mapper.Map<User, UserDTO>(user);
            const string request = "api/Users";
            var response = client.PostAsJsonAsync(request, userDTO).Result;
            if (!response.IsSuccessStatusCode) return null;
            var resultDTO = response.Content.ReadAsAsync<UserDTO>().Result;
            var result = Mapper.Map<UserDTO, User>(resultDTO);
            return result;
        }

        public static User PutRegistration(HttpClient client, User user, string newPassword)
        {
            ChangePasswordDTO changePasswordDTO = Mapper.Map<User, ChangePasswordDTO>(user);
            changePasswordDTO.NewPassword = newPassword;
            const string request = "api/Users";
            var response = client.PutAsJsonAsync(request, changePasswordDTO).Result;
            if (!response.IsSuccessStatusCode) return null;
            var resultDTO = response.Content.ReadAsAsync<UserDTO>().Result;
            var result = Mapper.Map<UserDTO, User>(resultDTO);
            return result;
        }

        public static List<Train> GetTrains(HttpClient client)
        {
            const string request = "api/Trains";
            var response = client.GetAsync(request).Result;
            response.EnsureSuccessStatusCode();
            var resultDTO = response.Content.ReadAsAsync<IEnumerable<TrainDTO>>().Result;
            var result = Mapper.Map <IEnumerable<TrainDTO>, IEnumerable<Train>>(resultDTO);
            return result.ToList();
        }

        public static IEnumerable<Ticket> GetTickets(HttpClient client, User user)
        {
            string request = "api/Tickets/" + user.Login;
            var response = client.GetAsync(request).Result;
            response.EnsureSuccessStatusCode();
            var resultDTO = response.Content.ReadAsAsync<IEnumerable<TicketDTO>>().Result;
            var result = Mapper.Map<IEnumerable<TicketDTO>, IEnumerable<Ticket>>(resultDTO);
            return result;
        }

        public static ConnectionPart GetConnectionPart(HttpClient client, ConnectionPart part)
        {
            string request = "api/ConnectionParts/" + part.Id;
            var response = client.GetAsync(request).Result;
            response.EnsureSuccessStatusCode();
            var resultDTO = response.Content.ReadAsAsync<ConnectionPartDTO>().Result;
            var result = Mapper.Map<ConnectionPartDTO, ConnectionPart>(resultDTO);
            return result;
        }

        public static bool PutTicket(HttpClient client, User currentUser, Ticket ticket)
        {
            TicketDTO ticketDTO = Mapper.Map<Ticket, TicketDTO>(ticket);
            ticketDTO.User = Mapper.Map<User, UserDTO>(currentUser);
            string request = "api/Tickets";
            var response = client.PutAsXmlAsync(request, ticketDTO).Result;
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }

        public static bool DeleteTicket(HttpClient client, User currentUser, Ticket ticket)
        {
            TicketDTO ticketDTO = Mapper.Map<Ticket, TicketDTO>(ticket);
            ticketDTO.User = Mapper.Map<User, UserDTO>(currentUser);
            string request = "api/Tickets";
            var response = client.PostAsXmlAsync(request, ticketDTO).Result;
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }

        public static void PostConnection(HttpClient client, Connection connection)
        {
            ConnectionDTO connectionDTO = Mapper.Map<Connection, ConnectionDTO>(connection);
            string request = "api/Connections";
            var response = client.PostAsJsonAsync(request, connectionDTO).Result;
            response.EnsureSuccessStatusCode();
        }


    }
}
