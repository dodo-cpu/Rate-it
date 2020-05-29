using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace Rateit.Models
{
    //By Johann
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

        //By Dorina
        /// <summary>
        /// Writes a user rating into the Database
        /// </summary>
        /// <returns>returns true if successful</returns>
        public bool Rate()
        {
            bool rated = false;
            DBConnector connection = new DBConnector();

            if (connection.Open())
            {
                string sql = $"INSERT INTO ratingUser (user_iduser,topic_idtopic, criterion_idcriterion, points) VALUES ({UserId}, {TopicId}, {Id}, {UserRating});";

                connection.getResult(sql);

                //if row found set rated true
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

        //By Johann
        /// <summary>
        /// Get Criterias based on topic
        /// </summary>
        /// <param name="topic">Object of Class Topic</param>
        /// <param name="user">Object of Class User</param>
        /// <returns>returns List(Criterion)</returns>
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

        //By Dorina
        /// <summary>
        /// Loads the data from the Database
        /// </summary>
        private void LoadData()
        {
            DBConnector connection = new DBConnector();
            if (connection.Open())
            {
                string sqlname = $"SELECT name FROM criterion WHERE idcriterion ={this.Id}";

                connection.getResult(sqlname);

                while (connection.Reader.Read())
                {
                    this.Name = connection.Reader.GetValue(0).ToString();
                }

                string sqluserrating = $"SELECT ratingUser.points FROM ratingUser where ratinguser.user_iduser = {this.UserId} AND ratinguser.topic_idtopic = {this.TopicId} AND ratinguser.criterion_idcriterion = {this.Id}";

                connection.getResult(sqluserrating);

                //default if it isnt rated by user yet
                this.UserRating = 3;

                while (connection.Reader.Read())
                {
                  this.UserRating = connection.Reader.GetInt32(0);
                }
                        
                connection.Close();
            }
        }

        #endregion

    }
}
