using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rateit.Models
{
    class ChildCategory : Category
    {

        #region Properties

        public int ParentId { get; set; }

        #endregion

        #region public Methods

        public ChildCategory(int id)
        {
            this.Id = id;

            this.LoadData();
        }

        /// <summary>
        /// Reads the data from the database
        /// </summary>
        public override void LoadData()
        {
            //@todo cateegory name correction in db
            DBConnector connection = new DBConnector();
            if (connection.Open())
            {
                string sql = $"SELECT name, idParent FROM category WHERE idcateegory = {this.Id};";

                connection.Command.CommandText = sql;
                connection.Command.ExecuteReader();

                while (connection.Reader.Read())
                {
                    this.Name = connection.Reader.GetValue(0).ToString();
                    this.ParentId = Convert.ToInt32(connection.Reader.GetValue(1));
                }

                connection.Close();
            }
        }

        #endregion

    }
}
