using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Windows;
using EasyTrainTickets.DesktopClient.Work;
using EasyTrainTickets.DesktopClient.Models;
using System.Windows.Threading;
using System.Windows.Media;
using System.Net.Http;

namespace EasyTrainTickets.DesktopClient.ViewModels
{
    class MainWindowViewModel : Conductor<object>, IShell, IHandle<BuyTicketViewModel>, IHandle<SearchViewModel>, IHandle<InformationToUser>, IHandle<DiscountViewModel>, IHandle<WelcomeViewModel>
    {
        private IWindowManager windowManager;
        private IEventAggregator eventAggregator;
        private IUnitOfWork unitOfWork;
        private User currentUser;
        private DispatcherTimer timer = new DispatcherTimer();

        public string Title { get; set; } = "Easy Train Tickets";

        private bool isadmin = false;
        public bool IsAdmin
        {
            get
            {
                return isadmin;
            }
            set
            {
                isadmin = value;
                NotifyOfPropertyChange("IsAdmin");
            }
        }
        private bool isuser = false;
        public bool IsUser
        {
            get
            {
                return isuser;
            }
            set
            {
                isuser = value;
                NotifyOfPropertyChange("IsUser");
            }
        }
        private string welcome;
        public string Welcome
        {
            get
            {
                if (currentUser == null) return "";
                return welcome;
            }
            set
            {
                welcome = value;
                NotifyOfPropertyChange("Welcome");
            }
        }

        private InformationToUser information;
        public InformationToUser Information
        {
            get
            {
                return information;
            }
            set
            {
                information = value;
                NotifyOfPropertyChange("Information");
            }
        }


        public MainWindowViewModel(IWindowManager _windowManager, IEventAggregator _eventAggretator, IUnitOfWork _unitOfWork)
        {
            windowManager = _windowManager;
            eventAggregator = _eventAggretator;
            unitOfWork = _unitOfWork;
            eventAggregator.Subscribe(this);
            timer.Interval = new TimeSpan(0, 0, 15);
            timer.Tick += TimerTick;
            ActivateItem(new WelcomeViewModel());


            if(!unitOfWork.Start())
            {
                timer.Stop();
                Information = new InformationToUser()
                {
                    Message = InformationToUser.ServerError,
                    Color = Brushes.Red
                };
                timer.Start();
            }
        }

        public void SignIn()
        {
            LoginViewModel login = new LoginViewModel(unitOfWork);
            windowManager.ShowDialog(login);
            IsAdmin = login.IsAdmin;
            IsUser = login.IsUser;
            currentUser = login.currentUser;
            if (currentUser != null)
            {
                Welcome = "Zalogowany jako:\n" + currentUser.Login;
                eventAggregator.PublishOnUIThread(currentUser);
            }
            if (IsAdmin) ActivateItem(null);
        }

        public void LoginOut()
        {
            IsAdmin = false;
            IsUser = false;
            currentUser = null;
            NotifyOfPropertyChange("Welcome");
            ActivateItem(new WelcomeViewModel());
        }

        public void Registration()
        {
            RegistrationViewModel registration = new RegistrationViewModel(unitOfWork);
            windowManager.ShowDialog(registration);
            IsUser = registration.IsUser;
            currentUser = registration.CurrentUser;
            if (currentUser != null)
            {
                Welcome = "Zalogowany jako:\n" + currentUser.Login;
                eventAggregator.PublishOnUIThread(currentUser);
            }
        }

        public async void AddConnection()
        {
            await Task.Run(() => ActivateItem(new AddConnectionsViewModel(unitOfWork, eventAggregator)));
        }

        public async void Search()
        {
            await Task.Run(() => ActivateItem(new SearchViewModel(unitOfWork, eventAggregator, windowManager, currentUser)));
        }

        public async void Tickets()
        {
            await Task.Run(() => ActivateItem(new MyTicketsViewModel(currentUser, unitOfWork, eventAggregator)));
        }

        public void ChangePassword()
        {
            ChangePasswordViewModel registration = new ChangePasswordViewModel(unitOfWork , currentUser);
            windowManager.ShowDialog(registration);
            currentUser = registration.CurrentUser;
        }

        public void Handle(BuyTicketViewModel message)
        {
            if (currentUser == null)
            {
                this.SignIn();
                return;
            }
            ActivateItem(message);
        }

        public void Handle(SearchViewModel message)
        {
            ActivateItem(message);
        }

        public void Handle(InformationToUser message)
        {
            timer.Stop();
            Information = message;
            timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            timer.Stop();
            Information = new InformationToUser();
        }

        public void Handle(DiscountViewModel message)
        {
            if (currentUser == null)
            {
                this.SignIn();
                return;
            }
            ActivateItem(message);
        }

        public void Handle(WelcomeViewModel message)
        {
            ActivateItem(message);
        }
    }
}
