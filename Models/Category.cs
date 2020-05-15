﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rateit.Models
{
    class Category
    {

        #region Properties

        public int Id { get; set; }

        public string Name { get; set; }

        #endregion

        #region public Methods

        public Category()
        {

        }

        public Category(int id)
        {
            this.Id = id;

            this.LoadData();
        }

        /// <summary>
        /// Reads the data from the database
        /// </summary>
        public virtual void LoadData()
        {
            //@todo cateegory name correction in db
            DBConnector connection = new DBConnector();
            if (connection.Open())
            {
                string sql = $"SELECT name FROM category WHERE idcateegory = {this.Id};";

                connection.Command.CommandText = sql;
                connection.Command.ExecuteReader();

                while (connection.Reader.Read())
                {
                    this.Name = connection.Reader.GetValue(0).ToString();
                }

                connection.Close();
            }
        }

        #endregion

    }
}
