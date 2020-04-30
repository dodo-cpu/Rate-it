using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Rateit.Models
{
    public class User : INotifyPropertyChanged
    {

        #region fields

        private int _Id;

        private string _Name;

        private string _Password;

        private int _adminflag;

        #endregion

        #region Properties

        public int Id 
        {
            get { return _Id; }
            set { _Id = value; RaisePropertyChanged("Id"); } 
        }

        public string Name 
        {
            get { return _Name; } 
            set { _Name = value; RaisePropertyChanged("Name"); }
        }

        public string Password
        {
            get { return _Password; }
            set { _Password = value; RaisePropertyChanged("Password"); }
        }

        public int adminflag
        {
            get { return _adminflag; }
            set { _adminflag = value; RaisePropertyChanged("adminflag"); }
        }

        #endregion

        #region public Methods

        public User(int id)
        {
            this.Id = id;

            LoadData();
        }

        #region public static methods

        /// <summary>
        /// Registers a new user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>User, Null if error</returns>
        public static User Register(string username, string password)
        {
            User user = null;

            //If the user hasnt entered sql commands in the username
            if (!SecurityHelper.ContainsSQLCommands(username))
            {
                DBConnector connection = new DBConnector();

                if (connection.Open())
                {
                    bool usernameTaken = false;
                    string sql;
                    //check if username is already taken
                    sql = $"SELECT COUNT(*) FROM user WHERE username = {username};";

                    connection.Command.CommandText = sql;
                    connection.Command.ExecuteScalar();

                    while (connection.Reader.Read())
                    {
                        if (!(connection.Reader.GetInt32(0) == 0))
                        {
                            usernameTaken = true;
                        }
                    }

                    //If usernmae is not taken
                    if (!usernameTaken)
                    {
                        //Create new user in the Database
                        sql = $"INSERT INTO user (username, password) VALUES ({username}, {SecurityHelper.GetStringSha256Hash(password)});";

                        connection.Command.CommandText = sql;
                        connection.Command.ExecuteNonQuery();

                        //Get the new user from the Database
                        sql = $"SELECT iduser FROM user WHERE username = {username} AND password = {SecurityHelper.GetStringSha256Hash(password)};";

                        connection.Command.CommandText = sql;
                        connection.Command.ExecuteReader();

                        while (connection.Reader.Read())
                        {
                            user = new User(connection.Reader.GetInt32(0));
                        }
                    }

                    connection.Close();
                }
            }

            return user;
        }

        /// <summary>
        /// Logs a user in
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>User, Null if error</returns>
        public static User Login(string username, string password)
        {
            User user = null;

            //If user hasnt entered sql commands as username
            if (!SecurityHelper.ContainsSQLCommands(username))
            {
                DBConnector connection = new DBConnector();

                if (connection.Open())
                {
                    //Get the user with specified name and password
                    string sql = $"SELECT * FROM user WHERE username = '{username}' AND password = '{SecurityHelper.GetStringSha256Hash(password)}';";

                    connection.getResult(sql);
                    //connection.Command.CommandText = sql;
                    //connection.Command.ExecuteReader();
                    int userid = -1;
                    while (connection.Reader.Read())
                    {
                        //TODO: Debug, could cause error because index out of range if user doesnt exist 
                         userid = Convert.ToInt32(connection.Reader.GetValue(0));
                        //user = new User(connection.Reader.GetInt32(0));
                    }
                    user = new User(userid);
             

                }

            }

            return user;
        }

        #endregion

        #endregion

        #region private methods

        private void LoadData()
        {
            //TODO: DB Get

            DBConnector connection = new DBConnector();

            if (connection.Open())
            {
                //Get the user with specified name and password
                string sql = $"SELECT * FROM user WHERE iduser ={this._Id}";

                connection.getResult(sql);
                while (connection.Reader.Read())
                {

                    this.Name = connection.Reader.GetValue(1).ToString();
                    //user = new User(connection.Reader.GetInt32(0));
                    this.Id = Convert.ToInt32(connection.Reader.GetValue(0));

                }
            }
        }

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
