using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace Rateit.Models
{
    public class Criterion
    {

        #region Properties

        public int Id { get; private set; }

        public string Name { get; private set; }


        private int _userRating;

        public int UserRating
        {
            get { return _userRating; }
            set 
            { 
                _userRating = value;
            }
        }

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
        public bool Rate()
        {
            bool rated = false;
            DBConnector connection = new DBConnector();

            if (connection.Open())
            {
                string sql = $"INSERT INTO ratingUser (user_iduser,topic_idtopic, criterion_idcriterion, points) VALUES ({UserId}, {TopicId}, {Id}, {UserRating});"; //ON DUPLICATE KEY UPDATE points = VALUES({UserRating});"; @todo errorfix

                connection.getResult(sql);

                while (connection.Reader.Read())
                {
                    if (connection.Reader.GetInt16(0) == 1)
                    {
                        rated = true;
                    }
                }

                connection.Close();
            }

            return rated;
        }

        #region public static methods

        public static List<Criterion> GetCriteriaByTopic(Topic topic, User user)
        {
            List<Criterion> results = new List<Criterion>();

            DBConnector connection = new DBConnector();

            if (connection.Open())
            {
                List<int> criteriaIds = new List<int>();
                string sql = $"SELECT idcriterion FROM criterion WHERE category_idcategory = {topic.CategoryId};";

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
                string sql = $"SELECT criterion.name, ratingUser.points FROM topic left JOIN criterion ON topic.category_idcategory = criterion.category_idcategory LEFT JOIN ratingUser ON ratingUser.topic_idtopic = topic.idtopic WHERE (ratingUser.user_iduser = {UserId} OR ratingUser.user_iduser IS NULL) AND topic.idtopic = {TopicId} AND criterion.idcriterion = {Id}; ";

                connection.getResult(sql);

                while (connection.Reader.Read())
                {
                    this.Name = connection.Reader.GetString(0);
                    if (connection.Reader.IsDBNull(1))
                    {
                        //If the user hasnt already rated, take 3 as default
                        this.UserRating = 3;
                    }
                    else
                    {
                        this.UserRating = connection.Reader.GetInt32(1);
                    }
                }

                connection.Close();
            }
        }

        #endregion

    }
}
