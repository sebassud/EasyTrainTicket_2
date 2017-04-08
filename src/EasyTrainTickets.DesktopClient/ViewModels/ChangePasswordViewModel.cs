using Caliburn.Micro;
using EasyTrainTickets.DesktopClient.Models;
using EasyTrainTickets.DesktopClient.Work;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTrainTickets.DesktopClient.ViewModels
{
    public class ChangePasswordViewModel : Conductor<object>, IDataErrorInfo
    {
        private IUnitOfWork unitOfWork;
        private string oldPassword;
        public string OldPassword
        {
            get
            {
                return oldPassword;
            }
            set
            {
                oldPassword = value;
                NotifyOfPropertyChange("User");
                NotifyOfPropertyChange(() => CanRegistration);
            }
        }

        private string newPassword;
        public string NewPassword
        {
            get
            {
                return newPassword;
            }
            set
            {
                newPassword = value;
                NotifyOfPropertyChange("Password");
                NotifyOfPropertyChange(() => CanRegistration);
            }
        }

        private string repeatNewPassword;
        public string RepeatNewPassword
        {
            get
            {
                return repeatNewPassword;
            }
            set
            {
                repeatNewPassword = value;
                NotifyOfPropertyChange("RepeatPassword");
                NotifyOfPropertyChange(() => CanRegistration);
            }
        }

        private User currentuser;
        public User CurrentUser
        {
            get
            {
                return currentuser;
            }
        }

        public ChangePasswordViewModel(IUnitOfWork _unitOfWork, User user)
        {
            unitOfWork = _unitOfWork;
            currentuser = user;
        }

        private string error;
        public string Error
        {
            get
            {
                return error;
            }
            set
            {
                error = value;
                NotifyOfPropertyChange("Error");
            }
        }

        public string this[string fieldName]
        {
            get
            {
                string error = null;
                if (fieldName == "OldPassword")
                {
                    if (string.IsNullOrEmpty(oldPassword))
                        error = "Hasło nie może być puste!";
                }
                else if (fieldName == "NewPassword")
                {
                    if (string.IsNullOrEmpty(newPassword))
                        error = "Hasło nie może być puste!";
                }
                else if (fieldName == "RepeatNewPassword")
                {
                    if (RepeatNewPassword != NewPassword)
                        error = "Hasło muszą być jednakowe";
                }
                return error;
            }
        }

        public void Cancel()
        {
            this.TryClose();
        }

        public bool CanRegistration
        {
            get
            {
                return Validate();
            }
        }

        private bool Validate()
        {
            if (oldPassword != null && newPassword != null && repeatNewPassword != null && oldPassword.Length != 0 && newPassword.Length != 0 && repeatNewPassword == newPassword) return true;
            else
                return false;
        }

        private void ChangePasswordTask()
        {
            if (CanRegistration == false) return;

            try
            {
                currentuser.Password = OldPassword;
                User user = unitOfWork.ChangePassword(currentuser, newPassword);

                if (user == null)
                {
                    Error = "Błędne hasło";
                    return;
                }
                currentuser = user;

                this.TryClose();
            }
            catch (AggregateException e)
            {
                Error = InformationToUser.ServerError;
            }
        }

        public async void ChangePassword()
        {
            await Task.Run(() => ChangePasswordTask());
        }
    }
}
