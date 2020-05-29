using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Rateit.Models
{
    //By Johann & Dorina
    class DBConnector
    {

        #region constants

        private const string defaultConnectionString = "SERVER=127.0.0.1;DATABASE=rateit;UID=root;PASSWORD=;";

        #endregion

        #region fields

        private MySqlConnection _connection;

        public MySqlCommand _command;

        #endregion


        #region properties

        public MySqlConnection Connection
        {
            get { return _connection; }
            set { _connection = value; }
        }

        public MySqlCommand Command
        {
            get { return _command; }
            set { _command = value; Command.Connection = _connection; }
        }

        public MySqlDataAdapter Adapter { get; set; }

        public MySqlDataReader Reader { get; set; }

        public bool Connected { get; set; }

        #endregion

        #region public methods

        /// <summary>
        /// Returns a DBConnector with connection to the connection details in the Text file, or else the default connection
        /// </summary>
        public DBConnector()
        {
            //TODO: parse text, if not working use default
            this.Connection = new MySqlConnection(defaultConnectionString);
        }

        /// <summary>
        /// Returns a DBConnector with connection to the given connectionString
        /// </summary>
        /// <param name="connectionString"></param>
        public DBConnector(string connectionString)
        {
            this.Connection = new MySqlConnection(connectionString);
        }

        /// <summary>
        /// Opens the connection to the Database
        /// </summary>
        /// <returns>true if opening succeeded</returns>
        public bool Open()
        {
            this.Connection.Open();

            if (this.Connection.State == System.Data.ConnectionState.Open)
            {
                this.Connected = true;
            }
            else
            {
                this.Connected = false;
            }
            //return true;
            //@todo check 
            return this.Connected;
        }

        /// <summary>
        /// Closes the connection to the Database
        /// </summary>
        /// <returns>true if closing succeeded</returns>
        public bool Close()
        {
            this.Connection.Close();

            if (this.Connection.State != System.Data.ConnectionState.Open)
            {
                this.Connected = true;
            }
            else
            {
                this.Connected = false;
            }
            //return true;
            //@todo check 
            return !this.Connected;
        }

        /// <summary>
        /// execute sql string
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>Returns MySqlDataReader</returns>
        public MySqlDataReader getResult(string sql)
        {

            MySqlCommand myCommand = new MySqlCommand(sql, this.Connection);

            //TODO: ByJohann: We need alseo a nonQuery() execution method to write to the DB i think
            if (this.Reader != null)
            {
                this.Reader.Close();
            }
            return this.Reader = myCommand.ExecuteReader();
        }

        #endregion

        #region public static methods

        #endregion

        #region private methods

        /// <summary>
        /// Parses the Text file for the connection details
        /// </summary>
        /// <returns>Connection string</returns>
        private string ParseCSVConnectionString()
        {
            string connectionString = string.Empty;

            //TODO: Parse CSV/Text

            return connectionString;
        }

        #endregion
    }
}