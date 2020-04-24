using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rateit.Models
{
    class Topic
    {

        #region Properties

        public int Id { get; private set; }

        public string Name { get; private set; }

        private List<Criterion> Criteria { get; set; }

        public int CategoryId { get; private set; }

        #endregion

        #region public methods

        public Topic(int id)
        {
            this.Id = id;

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

            //TODO: return
        }

        #endregion

        #region private methods

        /// <summary>
        /// Loads the Data from the Database
        /// </summary>
        private void LoadData()
        {
            ////@todo set List<Criterion>
            DBConnector connection = new DBConnector();
            if (connection.Open())
            {
                string sql = $"SELECT * FROM topic WHERE idtopic = {this.Id};";

                connection.Command.CommandText = sql;
                connection.Command.ExecuteReader();

                while (connection.Reader.Read())
                {
                    //TODO: Debug, GetValue ist evtl 0 indexiert
                    this.CategoryId = Convert.ToInt32(connection.Reader.GetValue(1));
                    this.Name = connection.Reader.GetValue(2).ToString();
                }

                connection.Close();
            }
        }

        #endregion

    }
}
