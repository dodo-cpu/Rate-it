using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Rateit.Models
{
    public class User
    {

        #region fields

        private int _Id;

        private string _Name;

        //private int _adminflag;

        #endregion

        #region Properties

        public int Id 
        {
            get { return _Id; }
            set { _Id = value; } 
        }

        public string Name 
        {
            get { return _Name; } 
            set { _Name = value; }
        }

        //public int adminflag
        //{
        //    get { return _adminflag; }
        //    set { _adminflag = value; }
        //}

        #endregion

        #region public Methods

        /// <summary>
        /// Creates a new User and Loads the corresponding data from The DB
        /// </summary>
        /// <param name="id">DB Id of the User</param>
        public User(int id)
        {
            this.Id = id;

            LoadData();
        }

        /// <summary>
        /// Checks weather a topic was already rated by the User
        /// </summary>
        /// <param name="topicid">DB Id of the Topic</param>
        /// <returns></returns>
        public bool istopicrated(int topicid)
        {
            bool israted = false;
            DBConnector connection = new DBConnector();

            if (connection.Open())
            {
                //Get the user with specified name and password
                string sql = $"SELECT * FROM ratinguser WHERE user_iduser = {this._Id} AND topic_idtopic = {topicid}";

                connection.getResult(sql);
                while (connection.Reader.Read())
                {
                    israted = true;
                }
            }
            return israted;
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
                    //check if username is already taken
                    string sql = $"SELECT COUNT(*) FROM user WHERE username = '{username}';";

                    connection.getResult(sql);

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
                        sql = $"INSERT INTO user (username, password) VALUES ('{username}', '{SecurityHelper.GetStringSha256Hash(password)}');";

                        connection.getResult(sql);

                        //Get the new user from the Database
                        sql = $"SELECT iduser FROM user WHERE username = '{username}' AND password = '{SecurityHelper.GetStringSha256Hash(password)}';";

                        connection.getResult(sql);

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
        /// <returns>User, Null if credentials incorrect or error</returns>
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

                    //If no matching user was found, user stays null
                    while (connection.Reader.Read())
                    {
                        user = new User(connection.Reader.GetInt32(0));
                    }

                    connection.Close();
                }
            }

            return user;
        }

        #endregion

        #endregion

        #region private methods

        /// <summary>
        /// Loads the data from the Database
        /// </summary>
        private void LoadData()
        {
            DBConnector connection = new DBConnector();

            if (connection.Open())
            {
                //Get the user with specified name and password
                string sql = $"SELECT * FROM user WHERE iduser = {this._Id}";

                connection.getResult(sql);
                while (connection.Reader.Read())
                {

                    this.Name = connection.Reader.GetValue(1).ToString();
                    this.Id = Convert.ToInt32(connection.Reader.GetValue(0));

                }
            }
        }

        #endregion


    }
}
