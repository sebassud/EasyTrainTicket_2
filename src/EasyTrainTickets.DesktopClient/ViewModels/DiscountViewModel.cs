using Caliburn.Micro;
using EasyTrainTickets.DesktopClient.Models;
using EasyTrainTickets.DesktopClient.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace EasyTrainTickets.DesktopClient.ViewModels
{
    public class DiscountViewModel : Screen
    {
        private IUnitOfWork unitOfWork;
        private ConnectionPath connectionpath;
        private SearchViewModel searchViewModel;
        private IEventAggregator eventAggregator;
        private User currentUser;

        private BindableCollection<Discount> discounts = new BindableCollection<Discount>();
        public BindableCollection<Discount> Discounts
        {
            get
            {
                return discounts;
            }
            set
            {
                discounts = value;
                NotifyOfPropertyChange("Discounts");
                NotifyOfPropertyChange("Price");
            }
        }

        public string Way
        {
            get
            {
                return connectionpath.Way;
            }
        }
        private bool isRandomSeats;
        public bool IsRandomSeats
        {
            get
            {
                return isRandomSeats;
            }
            set
            {
                isRandomSeats = value;
                NotifyOfPropertyChange("IsRandomSeats");
            }
        }

        private decimal price;
        public decimal Price
        {
            get
            {
                decimal count=0;
                for (int i = 0; i < discounts.Count; i++) 
                {
                    count += (decimal)(discounts[i].Count * discounts[i].Percent);
                }
                return Math.Round(count * connectionpath.Price, 2);
            }
            set
            {
                price = value;
                NotifyOfPropertyChange("Price");
            }
        }

        private bool canBuyTicket = true;
        public bool CanBuyTicket
        {
            get
            {
                return Price > 0 && canBuyTicket;
            }
            set
            {
                canBuyTicket = value;
                NotifyOfPropertyChange("CanBuyTicket");
            }
        }

        public DiscountViewModel(IUnitOfWork _unitOfWork, ConnectionPath _connectionpath, SearchViewModel _searchViewModel, IEventAggregator _eventAggregator, User _currentUser)
        {
            unitOfWork = _unitOfWork;
            connectionpath = _connectionpath;
            searchViewModel = _searchViewModel;
            eventAggregator = _eventAggregator;
            currentUser = _currentUser;
            try
            {
                Discounts.AddRange(unitOfWork.GetDiscounts);
            }
            catch (AggregateException e)
            {
                SendMessage(InformationToUser.ServerError);
                Cancel();
                return;
            }

        }

        private void SendMessage(string message)
        {
            eventAggregator.PublishOnUIThread(new InformationToUser
            {
                Message = message,
                Color = Brushes.Red
            });
        }
        public void Cancel()
        {
            eventAggregator.PublishOnUIThreadAsync(searchViewModel);
        }

        public async void BuyTicket()
        {
            await Task.Run(() => BuyTicketTask());
        }

        public void BuyTicketTask()
        {
            CanBuyTicket = false;
            Ticket ticket = new Ticket(connectionpath.ConnectionsParts);

            foreach (var discount in discounts)
            {
                ticket.Discounts.Add(discount, discount.Count);
            }
            var model = new BuyTicketViewModel(unitOfWork, connectionpath, this, eventAggregator, currentUser, ticket, IsRandomSeats);
            CanBuyTicket = true;
            if (model.IsOK)
                eventAggregator.PublishOnUIThread(model);
        }

        public void Change()
        {
            NotifyOfPropertyChange("Price");
            NotifyOfPropertyChange("CanBuyTicket");
        }
    }
}
