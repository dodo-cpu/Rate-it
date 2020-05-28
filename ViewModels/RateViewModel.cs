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
		private Topic topic;

		public BindableCollection<Criterion> Criteria
		{
			get { return _criteria; }
			set { _criteria = value; }
		}


		public RateViewModel(Topic topic, User user)
		{
			Criteria.AddRange(Criterion.GetCriteriaByTopic(topic, user));
			this.topic = topic;
		}

		#region Events

		public void Rateit()
		{
			int totalpoints = 0;
			foreach (Criterion criterion in Criteria)
			{
				totalpoints = totalpoints + criterion.UserRating;
				criterion.Rate();
				topic.updateCriterionRate(criterion.Id, criterion.UserRating);
			}		
			topic.updateAfterRate(totalpoints);
			AggregatorProvider.Aggregator.PublishOnCurrentThread(new RateEvent());
		}

		#endregion

	}
}
