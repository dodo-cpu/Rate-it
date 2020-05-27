using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Controls;
using Caliburn.Micro;
using Rateit.Events;
using Rateit.Models;

namespace Rateit.ViewModels
{
    public class LoginViewModel
    {

        #region fields

        private User _user;
        private string _name;

        #endregion

        #region properties

        public User User
        {
            get { return _user; }
            set { _user = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }


        #endregion

        #region public methods

        public void Login()
        {
            User = User.Login(Name, Password);
            if (User != null)
            {
                AggregatorProvider.Aggregator.PublishOnCurrentThread(new LoginEvent(User));
            }
            else
            {
                //TODO: signal to user didnt work
            }
        }


        public void Register()
        {
            User = User.Register(Name, Password);
            if (User != null)
            {
                AggregatorProvider.Aggregator.PublishOnCurrentThread(new LoginEvent(User));
            }
            else
            {
                //TODO: signal to user didnt work
            }
        }

        public void PasswordChanged(PasswordBox source)
        {
            Password = source.Password;
        }

        #endregion

        #region private methods

        //private void OnLogin(string name, object password)
        //{
        //    PasswordBox pw = password as PasswordBox;
        //    this.User = Models.User.Login(name, pw.Password);
        //    if (this.User != null)
        //    {
        //        OnLoggedIn();
        //    }
        //    else
        //    {
        //        //TODO: Signal to the user that input was incorrect
        //    }
        //}

        //private bool CanLogin()
        //{
        //    //TODO: Input check / Exceptions
        //    return true;
        //}

        //private void OnRegister(string name, object password)
        //{
        //    PasswordBox pw = password as PasswordBox;
        //    this.User = Models.User.Register(name, pw.Password);
        //    if (this.User != null)
        //    {
        //        OnLoggedIn();
        //    }
        //    else
        //    {
        //        //TODO: Signal to the user that input was incorrect
        //    }
        //}

        #endregion

        #region events and delegates

        //public event PropertyChangedEventHandler PropertyChanged;

        //public event LoggedInEventHandler LoggedIn;

        //public delegate void LoggedInEventHandler(object source, EventArgs args);

        //private void RaisePropertyChanged(string property)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        //}

        //protected virtual void OnLoggedIn()
        //{
        //    LoggedIn?.Invoke(this, EventArgs.Empty);
        //}

        #endregion

    }
}