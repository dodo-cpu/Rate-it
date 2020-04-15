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
            //TODO: DB Get
        }

        #endregion

    }
}
