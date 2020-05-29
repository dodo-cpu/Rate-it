using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rateit.Models
{
    //By Johann
    public class Category
    {

        #region Properties

        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private int? _parentId;

        public int? ParentId
        {
            get { return _parentId; }
            set { _parentId = value; }
        }

        #endregion

        #region Methods

        public Category(int id)
        {
            this.Id = id;

            this.LoadData();
        }

        //By Johann
        /// <summary>
        /// Reads the data from the database
        /// </summary>
        public virtual void LoadData()
        {
            DBConnector connection = new DBConnector();
            if (connection.Open())
            {
                string sql = $"SELECT name, idParent FROM category WHERE idcategory = {this.Id};";

                connection.getResult(sql);

                while (connection.Reader.Read())
                {
                    this.Name = connection.Reader.GetString(0);
                    if (connection.Reader.IsDBNull(1))
                    {
                        this.ParentId = null;
                    }
                    else
                    {
                        this.ParentId = (int?)connection.Reader.GetValue(1);
                    }
                }

                connection.Close();
            }
        }

        //By Johann
        /// <summary>
        /// Returns all Categories that have the parentId, returns all parent categories if parentId = NULL
        /// </summary>
        /// <param name="parentId">Id of parent category or NULL</param>
        /// <returns></returns>
        public static List<Category> GetCategoriesByParent(int? parentId)
        {
            List<Category> results = new List<Category>();
            List<int> ids = new List<int>();

            DBConnector connection = new DBConnector();
            if (connection.Open())
            {
                string sql;
                if (parentId is null)
                {
                    sql = "SELECT idcategory FROM category WHERE idParent IS NULL;";
                }
                else
                {
                    sql = $"SELECT idcategory FROM category WHERE idParent = {parentId};";
                }

                connection.getResult(sql);

                while (connection.Reader.Read())
                {
                    ids.Add(connection.Reader.GetInt32(0));
                }

                connection.Close();
            }

            foreach (int id in ids)
            {
                results.Add(new Category(id));
            }

            return results;
        }

        #endregion

    }
}
