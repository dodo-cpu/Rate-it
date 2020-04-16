using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

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

        private void LoadData()
        {
            //@todo change collums names
            MySqlConnection connection = new MySqlConnection("SERVER=127.0.0.1;" +
                    "DATABASE=rateit;" +
                    "UID=root;PASSWORD=;");
            connection.Open();

            string sql = "SELECT * FROM assesmentcriteria" +
                         "WHERE idassesmentCriteria ='" + this.Id + "'";

            MySqlCommand command = new MySqlCommand(sql, connection);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                this.Name = reader.GetValue(2).ToString();
            }

            connection.Close();
        }

        #endregion

    }
}
