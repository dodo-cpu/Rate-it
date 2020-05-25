using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rateit.Models
{
    public class Topic
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
        /// Returns the ratings of the Topic made by the current user
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
            return true;
        }

        #region public static methods

        /// <summary>
        /// Returns all Topics that have the given CategoryId
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public static List<Topic> GetTopicsByCategoryId(int categoryId)
        {
            List<Topic> results = new List<Topic>();
            List<int> ids = new List<int>();

            DBConnector connection = new DBConnector();
            if (connection.Open())
            {
                string sql = $"SELECT idtopic FROM topic WHERE category_idcategory = {categoryId};";

                connection.getResult(sql);

                while (connection.Reader.Read())
                {
                    ids.Add(connection.Reader.GetInt32(0));
                }

                connection.Close();
            }

            foreach (int id in ids)
            {
                results.Add(new Topic(id));
            }

            return results;
        }

        #endregion

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
                string sql = $"SELECT category_idcategory, name FROM topic WHERE idtopic = {this.Id};";

                connection.getResult(sql);

                while (connection.Reader.Read())
                {
                    //TODO: Debug, GetValue ist evtl 0 indexiert
                    this.CategoryId = Convert.ToInt32(connection.Reader.GetValue(0));
                    this.Name = connection.Reader.GetValue(1).ToString();
                }

                connection.Close();
            }
        }

        #endregion

    }
}
