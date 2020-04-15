using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //TODO: DB Get
        }

        #endregion

    }
}
