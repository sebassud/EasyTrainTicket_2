using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using EasyTrainTickets.DesktopClient.Work;
using EasyTrainTickets.DesktopClient.Models;
using System.Windows;
using System.Windows.Input;
using System.Net.Http;

namespace EasyTrainTickets.DesktopClient.ViewModels
{
    public class LoginViewModel : Conductor<object>
    {
        private IUnitOfWork unitOfWork;
        private string login;
        public string Login
        {
            get
            {
                return login;
            }
            set
            {
                login = value;
                NotifyOfPropertyChange("Login");
            }
        }

        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                NotifyOfPropertyChange("Password");
            }
        }

        private bool isuser = false;
        public bool IsUser
        {
            get
            {
                return isuser;
            }
        }

        private bool isadmin = false;
        public bool IsAdmin
        {
            get
            {
                return isadmin;
            }
        }

        private User currentuser;
        public User currentUser
        {
            get
            {
                return currentuser;
            }
        }

        private string errorBox;
        public string ErrorBox
        {
            get
            {
                return errorBox;
            }
            set
            {
                errorBox = value;
                NotifyOfPropertyChange("ErrorBox");
            }
        }


        public LoginViewModel(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        private void SigInTask()
        {
            try
            {
                User user = unitOfWork.SignIn(login, password);
                if(user == null)
                {
                    ErrorBox = "Błędna nazwa użytkownika lub błędne hasło !";
                    return;
                }
                if (user.IsAdmin) isadmin = true;
                else
                    isuser = true;

                currentuser = user;
                this.TryClose();
            }
            catch(AggregateException e)
            {
                ErrorBox = InformationToUser.ServerError;
            }
        }

        public async void SignIn()
        {
            await Task.Run(() => SigInTask());
        }

        public void Cancel()
        {
            this.TryClose();
        }

        public void ExecuteFilterView(KeyEventArgs keyArgs)
        {
            if (keyArgs.Key == Key.Enter)
            {
                SignIn();
            }
        }

    }
}
