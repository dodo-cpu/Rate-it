using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace Rateit.Models
{
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

        public MySqlDataAdapter Adapter {get;set;}

        public MySqlDataReader Reader { get; set; }

        public bool Connected { get; set; }

        #endregion

        #region public methods

        /// <summary>
        /// Returns a DBConnector with connection to the connection details in the Text file, or else the default connection
        /// </summary>
        public DBConnector()
        {

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
                return this.Connected = true;
            }
            else
            {
                return this.Connected = false;
            }
        }

        /// <summary>
        /// Closes the connection to the Database
        /// </summary>
        /// <returns>true if closing succeeded</returns>
        public bool Close()
        {
            this.Connection.Close();

            if (this.Connection.State == System.Data.ConnectionState.Open)
            {
                return this.Connected = true;
            }
            else
            {
                return this.Connected = false;
            }
        }

        public MySqlDataReader getResult(string sql)
        {
          
            MySqlCommand myCommand = new MySqlCommand(sql, this.Connection);

            return this.Reader = myCommand.ExecuteReader();
        }
        #region public static methods

        //@todo move to fitting classes
        /// <summary>
        /// Returns all Topics that are of the given category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        /*public static List<Topic> GetTopicsByCategory(int categoryId)
        {
            List<Topic> topics = new List<Topic>();

            //TODO: check sql string column/tabelnames 
            string sql = $"SELECT idtopic FROM topic WHERE category_idcategory = {categoryId};";

            DBConnector connection = new DBConnector();

            if (connection.Open())
            {
                connection.Command.CommandText = sql;

                while (connection.Reader.Read())
                {
                    topics.Add(new Topic(connection.Reader.GetInt32(0)));
                }
            }

            return topics;
        }

        /// <summary>
        /// Returns the categories that have the given parentId. If parentId = null it returns all categories where parentId is NULL
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public static List<Category> GetCategoriesByParent(int? parentId)
        {
            List<Category> categories = new List<Category>();

            string sql;
            //TODO: check sql string column/tabelnames 
            if (parentId == null)
            {
                sql = "SELECT idcategory FROM category WHERE idParent IS NULL;";
            }
            else
            {
                sql = $"SELECT idcategory FROM category WHERE idParent = {parentId};";
            }

            DBConnector connection = new DBConnector();

            if (connection.Open())
            {
                connection.Command.CommandText = sql;

                while (connection.Reader.Read())
                {
                    categories.Add(new Category(connection.Reader.GetInt32(0)));
                }
            }

            return categories;
        }*/


        #endregion

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
