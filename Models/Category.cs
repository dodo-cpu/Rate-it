using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Rateit.Models
{
    class Category
    {

        #region Propertiies

        public int Id { get; private set; }

        public string Name { get; private set; }

        public int ParentId { get; private set; }

        #endregion

        #region public Methods

        public Category(int id)
        {
            this.Id = id;

            LoadData();
        }

        #endregion

        #region private methods

        /// <summary>
        /// Reads the data from the database
        /// </summary>
        private void LoadData()
        {
            //@todo cateegory name correction in db
            MySqlConnection connection = new MySqlConnection("SERVER=127.0.0.1;" +
                    "DATABASE=rateit;" +
                    "UID=root;PASSWORD=;");
            connection.Open();

            string sql = "SELECT * FROM category" +
                         "WHERE idcateegory ='" + this.Id + "'";

            MySqlCommand command = new MySqlCommand(sql, connection);
            MySqlDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                this.Name = reader.GetValue(1).ToString();
                this.ParentId = Convert.ToInt32(reader.GetValue(2));
            }

            connection.Close();
        }

        #endregion

    }
}
