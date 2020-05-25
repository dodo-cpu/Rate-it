using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Controls;
using Caliburn.Micro;

namespace Rateit.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {

        #region fields

        public ICommand _loginCommand;

        private ICommand _registerCommand;

        #endregion

        #region properties

        public string Name { private get; set; }

        public Models.User User { get; private set; }

        public ICommand LoginCommand
        {
            get
            {
                if (_loginCommand == null)
                {
                    _loginCommand = new Commands.RelayCommand(c => OnLogin(this.Name, c), c => CanLogin(),false);
                }
                return _loginCommand;
            }
        }

        public ICommand RegisterCommand
        {
            get
            {
                if (_registerCommand == null)
                {
                    _registerCommand = new Commands.RelayCommand(c => OnRegister(this.Name, c), c => CanLogin(), false);
                }
                return _registerCommand;
            }
        }

        #endregion

        #region private methods

        private void OnLogin(string name, object password)
        {
            PasswordBox pw = password as PasswordBox;
            this.User = Models.User.Login(name, pw.Password);
            if (this.User != null)
            {
                OnLoggedIn();
            }
            else
            {
                //TODO: Signal to the user that input was incorrect
            }
        }

        private bool CanLogin()
        {
            //TODO: Input check / Exceptions
            return true;
        }

        private void OnRegister(string name, object password)
        {
            PasswordBox pw = password as PasswordBox;
            this.User = Models.User.Register(name, pw.Password);
            if (this.User != null)
            {
                OnLoggedIn();
            }
            else
            {
                //TODO: Signal to the user that input was incorrect
            }
        }

        #endregion

        #region events and delegates

        public event PropertyChangedEventHandler PropertyChanged;

        public event LoggedInEventHandler LoggedIn;

        public delegate void LoggedInEventHandler(object source, EventArgs args);

        private void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        protected virtual void OnLoggedIn()
        {
            LoggedIn?.Invoke(this, EventArgs.Empty);
        }

        #endregion

    }
}