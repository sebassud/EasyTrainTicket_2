using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyTrainTickets.DesktopClient.Work;
using Caliburn.Micro;
using System.Windows;
using EasyTrainTickets.DesktopClient.Models;
using System.Windows.Media;

namespace EasyTrainTickets.DesktopClient.ViewModels
{
    public class BuyTicketViewModel : Screen
    {
        private IUnitOfWork unitOfWork;
        private ConnectionPath connectionpath;
        private DiscountViewModel discountViewModel;
        private IEventAggregator eventAggregator;
        private User currentUser;
        private int to;
        private string sourcePicture;
        public string SourcePicture
        {
            get
            {
                return sourcePicture;
            }
            set
            {
                sourcePicture = value;
                NotifyOfPropertyChange("SourcePicture");
            }
        }
        private List<int> seats = new List<int>();
        public List<int> Seats
        {
            get
            {
                return seats;
            }
            set
            {
                seats = value;
                NotifyOfPropertyChange("Seats");
            }

        }

        private string selectedValue="";
        public string SelectedValue
        {
            get
            {
                return selectedValue;
            }
            set
            {
                selectedValue = value;
                NotifyOfPropertyChange("SelectedValue");
                NotifyOfPropertyChange("CanNext");
            }
        }

        private List<int> selectedSeats = new List<int>();
        public List<int> SelectedSeats
        {
            get
            {
                if (string.IsNullOrEmpty(selectedValue)) return selectedSeats;
                var list = SelectedValue.Split(',').Select(int.Parse).ToList();
                list.Sort();
                return list;
            }
        }
        private string currentReservation;
        public string CurrentReservation
        {
            get
            {
                return currentReservation;
            }
            set
            {
                currentReservation = value;
                NotifyOfPropertyChange("CurrentReservation");
            }
        }
        public int Count
        {
            get
            {
                return Ticket.Count;
            }
        }
        public bool CanNext
        {
            get
            {
                return SelectedSeats.Count == Ticket.NumberOfSeats && Part < Count;
            }
        }

        public bool CanConfirm
        {
            get
            {
                return Part == Count;
            }
        }
        private int part;
        public int Part
        {
            get
            {
                return part;
            }
            set
            {
                part = value;
                NotifyOfPropertyChange("Part");
                NotifyOfPropertyChange("CanNext");
                NotifyOfPropertyChange("CanConfirm");
            }
        }
        private string way;
        public string Way
        {
            get
            {
                return way;
            }
            set
            {
                way = value;
                NotifyOfPropertyChange("Way");
            }
        }
        private Ticket ticket;
        public Ticket Ticket
        {
            get
            {
                return ticket;
            }
            set
            {
                ticket = value;
                NotifyOfPropertyChange("Ticket");
            }
        }

        public bool IsOK { get; set; } = true;

        public BuyTicketViewModel(IUnitOfWork _unitOfWork, ConnectionPath _connectionpath, DiscountViewModel _discountViewModel, IEventAggregator _eventAggregator, User _currentUser, Ticket _ticket, bool randomSeats)
        {
            unitOfWork = _unitOfWork;
            connectionpath = _connectionpath;
            discountViewModel = _discountViewModel;
            eventAggregator = _eventAggregator;
            Way = connectionpath.Way;
            currentUser = _currentUser;
            Ticket = _ticket;
            if (randomSeats)
            {
                RandomNext();
            }
            else
            {
                CalculateReservationTask();
            }
        }
        private void RandomNext()
        {
            while (Part < Count)
            {
                CalculateReservationTask();
                if (Seats.Count < Ticket.NumberOfSeats)
                {
                    IsOK = false;
                    return;
                }
                List<int> seats = Seats.GetRange(0, Ticket.NumberOfSeats);
                for (int i = Part; i <= to; i++)
                {
                    ticket.AddSeat(seats.ToArray());
                }
                Part = to + 1;
                SelectedValue = "";
            }
            NotifyOfPropertyChange("Ticket");
            CurrentReservation = "";
            Seats = null;
            SourcePicture = null;
        }

        public void Cancel()
        {
            eventAggregator.PublishOnUIThreadAsync(discountViewModel);
        }

        private void CalculateReservationTask()
        {
            string fromStation = connectionpath.ConnectionsParts[Part].Route.From;
            to = part;

            while (to + 1 < Count && connectionpath.ConnectionsParts[to].Connection.Id == connectionpath.ConnectionsParts[to + 1].Connection.Id) to++;

            string endStation = connectionpath.ConnectionsParts[to].Route.To;
            if (connectionpath.ConnectionsParts[to].Connection.Train.Type == "Pośpieszny") SourcePicture = "/Resources/pospMsc.png";
            else if (connectionpath.ConnectionsParts[to].Connection.Train.Type == "Ekspres") SourcePicture = "/Resources/exMsc.png";
            try
            {
                Seats = unitOfWork.GetSeats(connectionpath, part, to);
                if (Seats.Count == 0)
                {
                    endStation = connectionpath.ConnectionsParts[part].Route.To;
                    to = part;
                    Seats = unitOfWork.GetSeats(connectionpath, part, to);
                    if (Seats.Count == 0)
                    {
                        SendMessage("Brak miejsc.");
                        IsOK = false;
                        Cancel();                       
                        return;
                    }
                }
                if (Seats.Count < Ticket.NumberOfSeats)
                {
                    SendMessage(string.Format("Zostało miejsc: {0}", Seats.Count));
                    IsOK = false;
                    Cancel();
                    return;
                }
            }
            catch(AggregateException e)
            {
                SendMessage(InformationToUser.ServerError);
                IsOK = false;
                Cancel();
                return;
            }
            Seats.Sort((a, b) => a - b);
            CurrentReservation = String.Format("{0,10} => {1,10}", fromStation, endStation);
        }

        private void SendMessage(string message)
        {
            eventAggregator.PublishOnUIThread(new InformationToUser
            {
                Message = message,
                Color = Brushes.Red
            });
        }

        private async void CalculateReservation()
        {
            await Task.Run(() => CalculateReservationTask());
        }

        public void Next()
        {
            for (int i = Part; i <= to; i++)
            {
                ticket.AddSeat(SelectedSeats.ToArray());
                NotifyOfPropertyChange("Ticket");
            }
            Part = to + 1;
            SelectedValue = "";
            if (Part < Count)
                CalculateReservation();
            else
            {
                CurrentReservation = "";
                Seats = null;
                SourcePicture = null;
            }
        }

        private void ConfirmTask()
        {
            bool result = false;
            try
            {
                result = unitOfWork.BuyTicket(currentUser, Ticket);
            }
            catch(AggregateException e)
            {
                SendMessage(InformationToUser.ServerError);
                Cancel();
                return;
            }
            if (result)
                eventAggregator.PublishOnUIThread(new InformationToUser
                {
                    Message = string.Format("Zakupiono bilet na relację:\n {0} => {1}",
                    Ticket.StartStation, Ticket.EndStation),
                    Color = Brushes.Lime
                });
            else
                SendMessage("Nie udało się zakupić biletu.");
            eventAggregator.PublishOnUIThread(new WelcomeViewModel());
        }
        public async void Confirm()
        {
            await Task.Run(() => ConfirmTask());
        }

    }
}
