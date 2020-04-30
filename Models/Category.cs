using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            DBConnector connection = new DBConnector();
            if (connection.Open())
            {
                string sql = $"SELECT * FROM category WHERE idcateegory = {this.Id};";

                connection.Command.CommandText = sql;
                connection.Command.ExecuteReader();

                while (connection.Reader.Read())
                {
                    //TODO: Debug, GetValue ist evtl 0 indexiert
                    this.Name = connection.Reader.GetValue(1).ToString();
                    this.ParentId = Convert.ToInt32(connection.Reader.GetValue(2));
                }

                connection.Close();
            }
        }

        #endregion

    }
}
