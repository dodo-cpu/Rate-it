using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace Rateit.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {

        #region fields

        //public ICommand _loginCommand;

        //private ICommand _registerCommand;

        #endregion

        #region properties

        //public ICommand LoginCommand
        //{
        //    get
        //    {
        //        if (_loginCommand == null)
        //        {
        //           // _loginCommand = new Commands.RelayCommand(c => OnLogin(), c => CanLogin(), false);
        //        }
        //        return _loginCommand;
        //    }
        //}

        //public ICommand RegisterCommand
        //{
        //    get
        //    {
        //        if (_registerCommand == null)
        //        {
        //           // _registerCommand = new Commands.RelayCommand(c => OnRegister(), c => CanLogin(), false);
        //        }
        //        return _registerCommand;
        //    }
        //}

        private Views.LoginView LoginView { get; set; }

        public System.Windows.Visibility Visibility { get; set; }

        private ObservableCollection<Models.Category> ParentCategories { get; set; }

        #endregion

        #region public methods

        public MainViewModel()
        {
            this.LoginView = new Views.LoginView();
            this.LoginView.Show();
            System.Threading.Thread.Sleep(5000);
            this.LoginView.Hide();
        }

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
