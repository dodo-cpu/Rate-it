using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Rateit.Models
{
    class Topic
    {

        #region Properties

        public int Id { get; private set; }

        public string Name { get; private set; }

        private List<Criterion> Criteria { get; set; }

        public int UserId { get; private set; }

        public int CategoryId { get; private set; }

        #endregion

        #region public methods

        public Topic(int id, int userId)
        {
            this.Id = id;
            this.UserId = userId;

            LoadData();
        }

        /// <summary>
        /// Returns the ratings of the Topic made from the public
        /// </summary>
        /// <returns></returns>
        public List<double> GetPublicRatings()
        {
            List<double> ratings = new List<double>();

            foreach (Criterion criterion in this.Criteria)
            {
                ratings.Add(criterion.publicRating);
            }

            return ratings;
        }

        /// <summary>
        /// Returns the ratings of the Topic made from the current user
        /// </summary>
        /// <returns></returns>
        public List<double> GetUserRatings()
        {
            List<double> ratings = new List<double>();

            foreach (Criterion criterion in this.Criteria)
            {
                ratings.Add(criterion.userRating);
            }

            return ratings;
        }

        /// <summary>
        /// writes the user ratings to the Database
        /// </summary>
        /// <param name="userRatings">the users ratings of the Criteria</param>
        /// <returns></returns>
        public bool Rate(List<double> userRatings)
        {
            for (int i = 0; i < userRatings.Count; i++)
            {
              this.Criteria[i].Rate(userRatings[i]);
            }

            //TODO return
        }

        #endregion

        #region private methods

        /// <summary>
        /// Loads the Data from the Database
        /// </summary>
        private void LoadData()
        {
            //@todo set List<Criterion>
            MySqlConnection connection = new MySqlConnection("SERVER=127.0.0.1;" +
                    "DATABASE=rateit;" +
                    "UID=root;PASSWORD=;");
            connection.Open();

            string sql = "SELECT * FROM topic" +
                         "WHERE idtopic ='" + this.Id + "'";

            MySqlCommand command = new MySqlCommand(sql, connection);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                this.CategoryId = Convert.ToInt32(reader.GetValue(1));
                this.Name = reader.GetValue(2).ToString();
            }

            connection.Close();
        }

        #endregion

    }
}
