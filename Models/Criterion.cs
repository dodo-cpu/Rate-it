using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rateit.Models
{
    public class Criterion
    {

        #region Properties

        public int Id { get; private set; }

        public string Name { get; private set; }

        public int UserRating { get; private set; }

        //public double publicRating { get; private set; }

        public int UserId { get; private set; }

        public int TopicId { get; private set; }

        #endregion

        #region public methods

        public Criterion(int id, int topicId, int userId)
        {
            this.Id = id;
            this.TopicId = topicId;
            this.UserId = userId;

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
            return true;
        }

        #region public static methods

        public static List<Criterion> GetCriteriaByTopic(Topic topic, User user)
        {
            List<Criterion> results = new List<Criterion>();

            DBConnector connection = new DBConnector();

            if (connection.Open())
            {
                List<int> criteriaIds = new List<int>();
                string sql = $"SELECT idcriterion FROM criterion WHERE category_idcategory = {topic.Id};";

                connection.getResult(sql);

                while (connection.Reader.Read())
                {
                    criteriaIds.Add(connection.Reader.GetInt32(0));
                }

                foreach (int criterionId in criteriaIds)
                {
                    results.Add(new Criterion(criterionId, topic.Id, user.Id));
                }

                connection.Close();
            }

            return results;
        }

        #endregion


        #endregion

        #region private methods

        /// <summary>
        /// Loads the data from the Database
        /// </summary>
        private void LoadData()
        {
            DBConnector connection = new DBConnector();
            if (connection.Open())
            {
                string sql = $"SELECT name, points FROM ratingUser LEFT JOIN criterion ON criterion_idcriterion = idcriterion WHERE user_iduser = {this.UserId} AND topic_idtopic = {this.TopicId} AND criterion_idcriterion = {this.Id};";

                connection.getResult(sql);

                while (connection.Reader.Read())
                {
                    //TODO: Debug, GetValue ist evtl 0 indexiert
                    this.Name = connection.Reader.GetString(0);
                    this.UserRating = connection.Reader.GetInt32(1);
                }

                connection.Close();
            }
        }

        #endregion

    }
}
