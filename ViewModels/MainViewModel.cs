using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;

namespace Rateit.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {

        #region fields

        private string _name;

        private string _password;

        private Models.User _user;

        public ICommand _loginCommand;

        private ICommand _registerCommand;

        #endregion

        #region properties

        public Models.User User
        {
            get { return _user; }
            set { _user = value; }//RaisePropertyChanged("User"); }
        }

        public ICommand LoginCommand
        {
            get
            {
                if (_loginCommand == null)
                {
                   // _loginCommand = new Commands.RelayCommand(c => OnLogin(), c => CanLogin(), false);
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
                   // _registerCommand = new Commands.RelayCommand(c => OnRegister(), c => CanLogin(), false);
                }
                return _registerCommand;
            }
        }

        #endregion

        #region public methods



        #endregion

        #region private methods



        #endregion

        #region events

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        #endregion

    }
}
