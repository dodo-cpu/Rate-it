using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rateit.Models
{
    class Category
    {

        #region Propertiies

        public int Id { get; private set; }

        public string Name { get; private set; }

        public int ParentId { get; private set; }

        #endregion

        #region public Methods

        public Category(int id)
        {
            this.Id = id;

            LoadData();
        }

        #endregion

        #region private methods

        /// <summary>
        /// Reads the data from the database
        /// </summary>
        private void LoadData()
        {
            //TODO: DB Get
        }

        #endregion

    }
}
