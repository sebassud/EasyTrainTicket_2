using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using EasyTrainTickets.DesktopClient.Work;
using EasyTrainTickets.DesktopClient.Models;
using System.Windows.Media;
using System.Windows;

namespace EasyTrainTickets.DesktopClient.ViewModels
{
    public class MyTicketsViewModel : Screen
    {
        private User currentuser;
        private IUnitOfWork unitOfWork;

        private BindableCollection<Ticket> tickets;
        public BindableCollection<Ticket> Tickets
        {
            get
            {
                return tickets;
            }
            set
            {
                tickets = value;
                NotifyOfPropertyChange("Tickets");
            }
        }

        private Ticket selectedTicket;
        private IEventAggregator eventAggregator;

        public Ticket SelectedTicket
        {
            get
            {
                return selectedTicket;
            }
            set
            {
                selectedTicket = value;
                NotifyOfPropertyChange("SelectedTicket");
                NotifyOfPropertyChange("CanDelete");
            }
        }

        public bool CanDelete
        {
            get
            {
                if (SelectedTicket == null) return false;
                if (TimeSpan.Compare(SelectedTicket.StartTime - DateTime.Now, new TimeSpan(0, 0, 0)) > 0) return true;
                return false;
            }
        }

        public MyTicketsViewModel(User _currentuser, IUnitOfWork _unitOfWork, IEventAggregator _eventAggregator)
        {
            currentuser = _currentuser;
            unitOfWork = _unitOfWork;
            eventAggregator = _eventAggregator;
            Initialize();
        }

        private void Initialize()
        {
            tickets = new BindableCollection<Ticket>();
            try
            {
                Tickets.AddRange(unitOfWork.CreateTickets(currentuser));
            }    
            catch(AggregateException e)
            {
                SendMessage(InformationToUser.ServerError);
                return;
            }    
            var list = Tickets.OrderByDescending(t => t.StartTime).ThenByDescending(t => t.EndTime).ToList();
            Tickets.Clear();
            Tickets.AddRange(list);
        }

        private void SendMessage(string message)
        {
            eventAggregator.PublishOnUIThread(new InformationToUser
            {
                Message = message,
                Color = Brushes.Red
            });
        }

        private void DeleteTicketTask()
        {
            bool result = false;
            try
            {
                result = unitOfWork.DeleteTicket(currentuser, selectedTicket);
            }
            catch (AggregateException e)
            {
                SendMessage(InformationToUser.ServerError);
                return;
            }
            if (result)
                eventAggregator.PublishOnUIThread(new InformationToUser
                {
                    Message = string.Format("Usunięto bilet:\n{0} => {1}", selectedTicket.StartStation, selectedTicket.EndStation),
                    Color = Brushes.Lime
                });
            else
                SendMessage("Bilet został już usunięty.");
            Tickets.Remove(selectedTicket);
        }
        public async void Delete()
        {
            await Task.Run(() => DeleteTicketTask());
        }
    }
}
