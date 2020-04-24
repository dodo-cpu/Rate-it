using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rateit.Models
{
    class Criterion
    {

        #region Properties

        public int Id { get; private set; }

        public string Name { get; private set; }

        public double userRating { get; private set; }

        public double publicRating { get; private set; }

        public int userId { get; private set; }

        public int topicId { get; private set; }

        #endregion

        #region public methods

        public Criterion(int id, int topicId, int userId)
        {
            this.Id = id;
            this.topicId = topicId;
            this.userId = userId;

            LoadData();
        }

        /// <summary>
        /// Writes a user rating into the Database
        /// </summary>
        /// <param name="rating">The users rating</param>
        /// <returns></returns>
        public bool Rate(double rating)
        {
            //TODO: DB Set
        }


        #endregion

        #region private methods

        /// <summary>
        /// Loads the data from the Database
        /// </summary>
        private void LoadData()
        {
            ////@todo change collums names
            DBConnector connection = new DBConnector();
            if (connection.Open())
            {
                string sql = $"SELECT * FROM assesmentcriteria WHERE idassesmentCriteria = {this.Id};";

                connection.Command.CommandText = sql;
                connection.Command.ExecuteReader();

                while (connection.Reader.Read())
                {
                    //TODO: Debug, GetValue ist evtl 0 indexiert
                    this.Name = connection.Reader.GetValue(2).ToString();
                }

                connection.Close();
            }
        }

        #endregion

    }
}
