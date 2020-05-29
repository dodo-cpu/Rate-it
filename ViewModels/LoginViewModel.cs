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
        private string _password;

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


        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }


        #endregion

        #region public methods

        /// <summary>
        /// Logs in the User and signals to ShellViewModel that Login occured
        /// </summary>
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

        /// <summary>
        /// Creates a new user, logs him in and signals to ShellViewModel that Login occured
        /// </summary>
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

        /// <summary>
        /// Helper function to be able to bind to the Text of a PasswordBox
        /// </summary>
        /// <param name="source"></param>
        public void PasswordChanged(PasswordBox source)
        {
            Password = source.Password;
        }

        #endregion

    }
}