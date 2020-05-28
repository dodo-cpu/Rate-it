using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Rateit.Models;
using Rateit.Events;

namespace Rateit.ViewModels
{
    public class RateViewModel
    {

		private BindableCollection<Criterion> _criteria = new BindableCollection<Criterion>();

		public BindableCollection<Criterion> Criteria
		{
			get { return _criteria; }
			set { _criteria = value; }
		}


		public RateViewModel(Topic topic, User user)
		{
			Criteria.AddRange(Criterion.GetCriteriaByTopic(topic, user));
		}

		#region Events

		public void Rateit()
		{
			foreach (Criterion criterion in Criteria)
			{
				criterion.Rate(1);
			}
			AggregatorProvider.Aggregator.PublishOnCurrentThread(new RateEvent());
		}

		#endregion

	}
}
