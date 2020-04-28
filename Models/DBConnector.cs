using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Rateit.Models
{
    class DBConnector
    {

        #region constants

        private const string defaultConnectionString = "SERVER=127.0.0.1;DATABASE=rateit;UID=root;PASSWORD=;";

        #endregion

        #region fields

        private SqlConnection _connection;

        public SqlCommand _command;

        #endregion


        #region properties

        public SqlConnection Connection
        {
            get { return _connection; }
            set { _connection = value; }
        }

        public SqlCommand Command 
        {
            get { return _command; } 
            set { _command = value; Command.Connection = _connection; } 
        }

        public SqlDataAdapter Adapter { get; set; }

        public SqlDataReader Reader { get; set; }

        public bool Connected { get; set; }

        #endregion

        #region public methods

        /// <summary>
        /// Returns a DBConnector with connection to the connection details in the Text file, or else the default connection
        /// </summary>
        public DBConnector()
        {
            //TODO: parse text, if not working use default
            //this.Connection = new SqlConnection(ParseCSVConnectionString());
            this.Connection = new SqlConnection(defaultConnectionString);
            this.Command = new SqlCommand();
        }

        /// <summary>
        /// Returns a DBConnector with connection to the given connectionString
        /// </summary>
        /// <param name="connectionString"></param>
        public DBConnector(string connectionString)
        {
            this.Connection = new SqlConnection(connectionString);
            this.Command = new SqlCommand();
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

            return this.Connected;
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
                this.Connected = true;
            }
            else
            {
                this.Connected = false;
            }

            return !this.Connected;
        }

        #region public static methods

        /// <summary>
        /// Returns all Topics that are of the given category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public static List<Topic> GetTopicsByCategory(int categoryId)
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
        }


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
