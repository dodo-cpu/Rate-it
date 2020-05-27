using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Rateit.Models;

namespace Rateit.ViewModels
{
    class OverViewModel
    {

		private BindableCollection<Topic> _topics = new BindableCollection<Topic>();
		public BindableCollection<Topic> Topics
		{
			get { return _topics; }
			set { _topics = value; }
		}

		public OverViewModel(int categoryId)
		{
			Topics.AddRange(Topic.GetTopicsByRanking(categoryId));
		}

	}
}
