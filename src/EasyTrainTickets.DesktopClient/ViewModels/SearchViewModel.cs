using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Windows;
using EasyTrainTickets.DesktopClient.Work;
using EasyTrainTickets.DesktopClient.Models;
using System.Windows.Media;

namespace EasyTrainTickets.DesktopClient.ViewModels
{
    public class SearchViewModel : Screen, IHandle<User>
    {
        private IUnitOfWork unitOfWork;
        private IEventAggregator eventAggregator;
        private IWindowManager windowManager;
        private User currentUser;
        private List<ConnectionPath> originalPaths = new List<ConnectionPath>();

        private List<string> fromStation;
        public List<string> FromStation
        {
            get
            {
                return fromStation;
            }
            set
            {
                fromStation = value;
                NotifyOfPropertyChange("FromStation");
            }
        }

        private string selectedFromStation;
        public string SelectedFromStation
        {
            get
            {
                return selectedFromStation;
            }
            set
            {
                selectedFromStation = value;
                NotifyOfPropertyChange("SelectedFromStation");
                NotifyOfPropertyChange("CanSearch");
            }
        }

        private List<string> middleStation = new List<string>();
        public List<string> MiddleStation
        {
            get
            {
                return middleStation;
            }
            set
            {
                middleStation = value;
                NotifyOfPropertyChange("MiddleStation");
            }
        }

        private string selectedMiddleStation;
        public string SelectedMiddleStation
        {
            get
            {
                return selectedMiddleStation;
            }
            set
            {
                selectedMiddleStation = value;
                NotifyOfPropertyChange("SelectedMiddleStation");
                NotifyOfPropertyChange("CanSearch");
            }
        }

        private List<string> endStation;
        public List<string> EndStation
        {
            get
            {
                return endStation;
            }
            set
            {
                endStation = value;
                NotifyOfPropertyChange("EndStation");
            }
        }

        private string selectedEndStation;
        public string SelectedEndStation
        {
            get
            {
                return selectedEndStation;
            }
            set
            {
                selectedEndStation = value;
                NotifyOfPropertyChange("SelectedEndStation");
                NotifyOfPropertyChange("CanSearch");
            }
        }

        private DateTime startDate = DateTime.Now;
        public DateTime StartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                startDate = value;
                NotifyOfPropertyChange("StartDate");
            }
        }
        public DateTime MinDate
        {
            get
            {
                return DateTime.Now;
            }
        }

        private BindableCollection<ConnectionPath> paths = new BindableCollection<ConnectionPath>();
        public BindableCollection<ConnectionPath> Paths
        {
            get { return paths; }
            set
            {
                paths = value;
                NotifyOfPropertyChange("Paths");
            }
        }

        private ConnectionPath selectedPath;
        public ConnectionPath SelectedPath
        {
            get
            {
                return selectedPath;
            }

            set
            {
                selectedPath = value;
                NotifyOfPropertyChange("SelectedPath");
                NotifyOfPropertyChange("CanBuyTicket");
            }
        }
        private bool canSearch = true;
        public bool CanSearch
        {
            get
            {
                return !string.IsNullOrEmpty(SelectedFromStation) && !string.IsNullOrEmpty(SelectedEndStation) && SelectedFromStation != SelectedEndStation && SelectedFromStation != SelectedMiddleStation && SelectedEndStation != SelectedMiddleStation && canSearch;
            }
            set
            {
                canSearch = value;
                NotifyOfPropertyChange("CanSearch");
            }
        }
        private bool isExpress = true;
        public bool IsExpress
        {
            get
            {
                return isExpress;
            }
            set
            {
                isExpress = value;
                NotifyOfPropertyChange("IsExpress");
                Filter();
            }
        }

        private bool isDirect;
        public bool IsDirect
        {
            get
            {
                return isDirect;
            }
            set
            {
                isDirect = value;
                NotifyOfPropertyChange("IsDirect");
                Filter();
            }
        }

        public SearchViewModel(IUnitOfWork _unitOfWork, IEventAggregator _eventAggregator, IWindowManager _windowManager, User _currentUser)
        {
            unitOfWork = _unitOfWork;
            windowManager = _windowManager;
            eventAggregator = _eventAggregator;
            if (!unitOfWork.Start())
            {
                SendInfo();
                return;
            } 
            FromStation = unitOfWork.Stations;
            SelectedMiddleStation = "";
            MiddleStation.Add("");
            MiddleStation.AddRange(unitOfWork.Stations);
            EndStation = unitOfWork.Stations;
            currentUser = _currentUser;
            eventAggregator.Subscribe(this);

        }

        private void SendInfo()
        {
            var Information = new InformationToUser()
            {
                Message = InformationToUser.ServerError,
                Color = Brushes.Red
            };
            eventAggregator.PublishOnUIThreadAsync(Information);
        }

        private void SearchTask()
        {
            Paths.Clear();
            originalPaths.Clear();
            CanSearch = false;
            try
            {
                originalPaths.AddRange(unitOfWork.Search(SelectedFromStation, SelectedMiddleStation, SelectedEndStation, StartDate));
            }
            catch (AggregateException e)
            {
                SendInfo();
            }
            CanSearch = true;
            Filter();
        }

        public async void Search()
        {
            await Task.Run(() => SearchTask());
        }

        public void ConnectionView(int index)
        {
            windowManager.ShowWindow(new ConnectionViewModel(SelectedPath.Connections[index]));
        }

        public bool CanBuyTicket
        {
            get
            {
                return SelectedPath != null;
            }
        }

        public void BuyTicket()
        {
            eventAggregator.PublishOnUIThreadAsync(new DiscountViewModel(unitOfWork, selectedPath, this, eventAggregator, currentUser));
        }

        public void Handle(User message)
        {
            currentUser = message;
        }

        public void Filter()
        {
            Paths.Clear();
            Paths.AddRange(originalPaths);
            if (IsDirect)
            {
                for (int i = 0; i < Paths.Count; i++)
                {
                    if (Paths[i].Change > 0)
                    {
                        Paths.RemoveAt(i);
                        i--;
                    }
                }
            }
            if (!IsExpress)
            {
                for(int i = 0; i < Paths.Count; i++)
                {
                    foreach(var con in Paths[i].Connections)
                    {
                        if(con.Train.Type=="Ekspres")
                        {
                            Paths.RemoveAt(i);
                            i--;
                            break;
                        }
                    }
                }
            }
        }
    }
}
