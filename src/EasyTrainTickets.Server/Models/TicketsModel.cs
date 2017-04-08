using EasyTrainTickets.Common.DTOs;
using EasyTrainTickets.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyTrainTickets.Domain.Data;
using EasyTrainTickets.Domain.Services;
using System.Text;

namespace EasyTrainTickets.Server.Models
{
    public class TicketsModel
    {
        public List<TicketDTO> GetTicketsUser(string loginUser, IEasyTrainTicketsDbEntities dbContext)
        {
            var list = dbContext.Users.Where(u => u.Login == loginUser).ToList();
            if (list.Count == 0)
            {
                return null;
            }
            User user = list.First();
            List<Ticket> listTickets = CreateTickets(user);
            listTickets = InitializeTickets(listTickets, dbContext);

            return AutoMapper.Mapper.Map<List<Ticket>, List<TicketDTO>>(listTickets);
        }

        private List<Ticket> CreateTickets(User user)
        {
            string[] tickets = user.Tickets.Split(';');
            if (user.Tickets.Length == 0) return new List<Ticket>();
            Ticket[] Tickets = new Ticket[tickets.Length];
            for (int i = 0; i < tickets.Length; i++)
            {
                string[] discount = tickets[i].Split(':');
                tickets[i] = discount[1];
                Tickets[i] = new Ticket();
                string[] conParts = tickets[i].Split('|');
                foreach (var conPart in conParts)
                {
                    string[] part = conPart.Split('-');
                    Tickets[i].conPartsId.Add(int.Parse(part[0]));
                    string[] seats = part[1].Split(',');
                    Tickets[i].Seats.Add(seats.Select(int.Parse).ToArray());
                }
                string[] ids = discount[0].Split(',');
                Tickets[i].discountsId.AddRange(ids.Select(int.Parse).ToList());
            }
            return Tickets.ToList();
        }

        private List<Ticket> InitializeTickets(List<Ticket> tickets, IEasyTrainTicketsDbEntities dbContext)
        {
            for (int i = 0; i < tickets.Count; i++)
            {
                HashSet<int> ids = new HashSet<int>();
                foreach (var id in tickets[i].conPartsId)
                {
                    ids.Add(id);
                }
                List<ConnectionPart> parts = dbContext.ConnectionParts.Where(cp => ids.Contains(cp.Id)).ToList();
                for (int j = 0; j < tickets[i].Seats.Count; j++)
                {
                    tickets[i].ConnectionPath.Add(parts.Find(p => p.Id == tickets[i][j]));
                }
                Dictionary<int, int> dict = new Dictionary<int, int>();
                foreach (var id in tickets[i].discountsId)
                {
                    if (dict.ContainsKey(id)) dict[id]++;
                    else
                        dict.Add(id, 1);
                }
                foreach(var id in dict.Keys)
                {
                    Discount discount = dbContext.Discounts.Where(d => d.Id == id).First();
                    tickets[i].Discounts.Add(discount, dict[id]);
                }
            }
            return tickets;
        }

        public bool BuyTicket(TicketDTO ticketDTO, ITransactionalData dbContext)
        {
            var list = dbContext.Users.Where(u => u.Id == ticketDTO.User.Id && u.Password == ticketDTO.User.Password).ToList();
            if (list.Count == 0)
            {
                return false;
            }
            User currentUser = list.First();
            Ticket ticket = AutoMapper.Mapper.Map<TicketDTO, Ticket>(ticketDTO);

            for (int i = 0; i < ticket.ConnectionPath.ConnectionsParts.Count; i++)
            {
                var tmp = ticket.ConnectionPath[i];
                ticket.ConnectionPath[i] = dbContext.ConnectionParts.Where(cp => cp.Id == tmp.Id).First();

                int[] userSeat = ticket.Seats[i];

                foreach(var s in userSeat)
                {
                    string seat = s.ToString();
                    string seats = ticket.ConnectionPath[i].Seats;
                    int index = seats.IndexOf(seat);
                    string newSeats;
                    if (index < 0)
                    {
                        dbContext.Reject();
                        return false;
                    }
                    if (seats.Length == seat.Length)
                    {
                        newSeats = "";
                    }
                    else if (index == seats.Length - seat.Length) newSeats = seats.Remove(index - 1, seat.Length + 1);
                    else
                    {
                        newSeats = seats.Remove(index, seat.Length + 1);
                    }

                    ticket.ConnectionPath[i].Seats = newSeats;
                    ticket.ConnectionPath[i].FreeSeats--;
                }

            }
            if (currentUser.Tickets.Length == 0)
                currentUser.Tickets = CreateStringTicket(ticket);
            else
                currentUser.Tickets = string.Format(currentUser.Tickets + ";" + CreateStringTicket(ticket));
            dbContext.SaveChanges();
            return true;
        }
        private string CreateStringTicket(Ticket ticket)
        {
            StringBuilder sb = new StringBuilder();
            ticket.discountsId.Clear();
            foreach(var key in ticket.Discounts.Keys)
            {
                for (int i = 0; i < ticket.Discounts[key]; i++) ticket.discountsId.Add(key.Id);

            }
            sb.Append(string.Join(",", ticket.discountsId));
            sb.Append(":");
            for (int i = 0; i < ticket.Count; i++)
            {
                sb.Append(string.Format("{0}-{1}", ticket.ConnectionPath[i].Id, string.Join(",", ticket.Seats[i])));
                if (i < ticket.Count - 1)
                    sb.Append("|");
            }
            return sb.ToString();
        }

        public bool DeleteTicket(TicketDTO ticketDTO, IEasyTrainTicketsDbEntities dbContext)
        {
            var list = dbContext.Users.Where(u => u.Id == ticketDTO.User.Id && u.Password == ticketDTO.User.Password).ToList();
            if (list.Count == 0)
            {
                return false;
            }
            User user = list.First();
            Ticket ticket = AutoMapper.Mapper.Map<TicketDTO, Ticket>(ticketDTO);

            string ticketString = CreateStringTicket(ticket);
            string userTickets = user.Tickets;
            int index = userTickets.IndexOf(ticketString);
            string newTickets;

            if (index < 0) return false;
            else if (ticketString.Length == userTickets.Length) newTickets = "";
            else if (index == userTickets.Length - ticketString.Length) newTickets = userTickets.Remove(index - 1, ticketString.Length + 1);
            else
            {
                newTickets = userTickets.Remove(index, ticketString.Length + 1);
            }
            user.Tickets = newTickets;

            for (int i = 0; i < ticket.Count; i++)
            {
                var tmp = ticket.ConnectionPath[i];
                ticket.ConnectionPath[i] = dbContext.ConnectionParts.Where(cp => cp.Id == tmp.Id).First();
                if (ticket.ConnectionPath[i].Seats == "") ticket.ConnectionPath[i].Seats = string.Join(",", ticket.Seats[i]).Replace(',',';');
                else
                {
                    ticket.ConnectionPath[i].Seats = String.Format("{0};{1}", ticket.ConnectionPath[i].Seats, string.Join(",",ticket.Seats[i]).Replace(',', ';'));
                }
                ticket.ConnectionPath[i].FreeSeats += ticket.Seats[i].Length;
            }

            dbContext.SaveChanges();
            return true;
        }
    }
}