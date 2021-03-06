﻿using System;
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

        public List<Criterion> Criteria { get; set; }

        public int CategoryId { get; private set; }

        public double AvrgScore { get; private set; }

        public int totalpoints { get; private set; }

        public int totalrankings { get; private set; }

        #endregion

        #region public methods

        public Topic(int id)
        {
            this.Id = id;

            LoadData();
        }

        #region public static methods

        //By Johann
        /// <summary>
        /// Returns all Topics that have the given CategoryId
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns>List of Objects from Class Topic</returns>
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

        //By Dorina
        /// <summary>
        /// add totalpoints from last user rating to topic.totalpoints
        /// </summary>
        /// <param name="points">total points from last user rate</param>
        public void updateAfterRate(int points)
        {
            DBConnector connection = new DBConnector();
            if (connection.Open())
            {
                //sums points
                this.totalpoints = this.totalpoints + points;
                //update ranking count
                this.totalrankings = this.totalrankings + 1;

                string sql = $"UPDATE topic SET totalpoints ={this.totalpoints}, totalrankings ={this.totalrankings} WHERE idtopic = {this.Id}";

                connection.getResult(sql);

                connection.Close();
            }
        }

        //By Dorina
        /// <summary>
        /// add specific criterion points to ratingtopic
        /// </summary>
        /// <param name="criterionId">criterion ID</param>
        /// <param name="points">points from last user rate</param>
        public void updateCriterionRate(int criterionId, int points)
        {
            DBConnector connection = new DBConnector();
            if (connection.Open())
            {
                string sqlselect = $"SELECT points FROM ratingtopic WHERE topic_idtopic = {this.Id} AND criterion_idcriterion = {criterionId}";

                connection.getResult(sqlselect);
            
                while (connection.Reader.Read())
                {
                    points = Convert.ToInt32(connection.Reader.GetValue(0)) + points;
                }

                string sqlupdate = $"UPDATE ratingtopic SET points ={points} WHERE topic_idtopic = {this.Id} AND criterion_idcriterion = {criterionId}";

                connection.getResult(sqlupdate);

                connection.Close();
            }
        }

        //By Johann
        /// <summary>
        /// Returns all Topics (that are of the given Category) ordered by average score
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns>Topics ordered by average score</returns>
        public static List<Topic> GetTopicsByRanking(int? categoryId)
        {
            List<Topic> results = new List<Topic>();
            List<int> topicIds = new List<int>();
            DBConnector connection = new DBConnector();

            if (connection.Open())
            {
                List<int> categoryIds = new List<int>();
                int? parentId = null;
                string sql;

                if (categoryId == null)
                {
                    sql = "SELECT idtopic FROM topic ORDER BY totalpoints / totalrankings DESC, idtopic ASC;";
                }
                else
                {
                    //Get ParentCategoryId
                    sql = $"SELECT idParent FROM category WHERE idcategory = {categoryId};";

                    connection.getResult(sql);

                    while (connection.Reader.Read())
                    {
                        if (!connection.Reader.IsDBNull(0))
                        {
                            parentId = connection.Reader.GetInt32(0);
                        }
                    }

                    //If ParentId is null, get all childCategoryIds
                    if (parentId == null)
                    {
                        sql = $"SELECT idcategory FROM category WHERE idParent = {categoryId};";

                        connection.getResult(sql);

                        while (connection.Reader.Read())
                        {
                            categoryIds.Add(connection.Reader.GetInt32(0));
                        }

                    }
                    else
                    {
                        categoryIds.Add((int)categoryId);
                    }

                    //Get all TopicsIds from the category/-ies
                    sql = "SELECT idtopic FROM topic WHERE category_idcategory IN (";

                    foreach (int category in categoryIds)
                    {
                        sql += category.ToString() + ", ";
                    }

                    sql = sql.Remove(sql.Length - 2) + ") ORDER BY totalpoints / totalrankings DESC, idtopic ASC;";
                }

                connection.getResult(sql);

                while (connection.Reader.Read())
                {
                    topicIds.Add(connection.Reader.GetInt32(0));
                }

                connection.Close();
            }

            //Create Topics from TopicIds
            foreach (int id in topicIds)
            {
                results.Add(new Topic(id));
            }

            return results;
        }

        #endregion

        #endregion

        #region private methods

        //By Dorina
        /// <summary>
        /// Loads the Data from the Database
        /// </summary>
        private void LoadData()
        {
            ////@todo set List<Criterion>
            DBConnector connection = new DBConnector();
            if (connection.Open())
            {
                string sql = $"SELECT category_idcategory, name, totalpoints / totalrankings , totalpoints, totalrankings FROM topic WHERE idtopic = {this.Id};";

                connection.getResult(sql);

                while (connection.Reader.Read())
                {
                    //TODO: Debug, GetValue ist evtl 0 indexiert
                    this.CategoryId = Convert.ToInt32(connection.Reader.GetValue(0));
                    this.Name = connection.Reader.GetValue(1).ToString(); 
                    if (!connection.Reader.IsDBNull(2))
                    {

                        this.AvrgScore = (double)connection.Reader.GetDecimal(2);
                    }
                    else
                    {
                        this.AvrgScore = 0;
                    }
                    this.totalpoints = Convert.ToInt32(connection.Reader.GetValue(3));
                    this.totalrankings = Convert.ToInt32(connection.Reader.GetValue(4));
                }

                connection.Close();
            }
        }

        #endregion

    }
}
