using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Rateit.Views;
using Rateit.Models;

namespace Rateit.ViewModels
{
    class MainViewModel
    {

        #region fields

        private ICommand _buttonCommand;
        private ObservableCollection<Category> _parentCategories;
        private ObservableCollection<Category> _childCategories;
        private ObservableCollection<Topic> _topics;

        #endregion

        #region properties

        public ICommand ButtonCommand
        {
            get
            {
                if (_buttonCommand == null)
                {
                    _buttonCommand = new Commands.RelayCommand(c => OnButton(), c => true, false);
                }
                return _buttonCommand;
            }
        }

        private ObservableCollection<Category> ParentCategories { get => _parentCategories; set => _parentCategories = value; }

        private ObservableCollection<Category> ChildtCategories { get => _childCategories; set => _childCategories = value; }

        private ObservableCollection<Topic> Topics { get => _topics; set => _topics = value; }

        #endregion

        #region public methods

        public MainViewModel()
        {
            this.ParentCategories = new ObservableCollection<Category>(Category.GetCategoriesByParent(null));
            //this.LoginView = new Views.LoginView();
            //this.LoginView.Show();
        }

        public void OnButton()
        {
            //RaiseHide();
        }

        #endregion

        #region private methods



        #endregion

        //#region events

        //public event PropertyChangedEventHandler PropertyChanged;

        //private void RaisePropertyChanged(string property)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        //}

        //public event HideEventHandler Hiding;
        //public delegate void HideEventHandler();
        //private void RaiseHide()
        //{
        //    Hiding?.Invoke();
        //}

        //public event ShowEventHandler Showing;
        //public delegate void ShowEventHandler();
        //private void RaiseShow()
        //{
        //    Showing?.Invoke();
        //}

        //#endregion

    }
}
