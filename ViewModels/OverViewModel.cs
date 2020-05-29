using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Rateit.Models;

namespace Rateit.ViewModels
{
    public class OverViewModel : Screen
    {
		private BindableCollection<Topic> _topics = new BindableCollection<Topic>();
		public BindableCollection<Topic> Topics
		{
			get { return _topics; }
			set { _topics = value; }
		}

		/// <summary>
		/// Creates an OverView with the topics of the given category ranked by average points
		/// </summary>
		/// <param name="categoryId"></param>
		public OverViewModel(int? categoryId)
		{
			Topics.AddRange(Topic.GetTopicsByRanking(categoryId));
		}

	}
}
