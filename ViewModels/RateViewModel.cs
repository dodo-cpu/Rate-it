using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Rateit.Models;
using Rateit.Events;
using System.Reflection.Emit;

namespace Rateit.ViewModels
{
	public class RateViewModel
	{

		private BindableCollection<Criterion> _criteria = new BindableCollection<Criterion>();
		private Topic topic;
		private Label _messageAlreadyRate;
		private bool _LableVisible = false;
		private System.Windows.Visibility _lablevis = System.Windows.Visibility.Hidden;

		public BindableCollection<Criterion> Criteria
		{
			get { return _criteria; }
			set { _criteria = value; }
		}

		public Label messageAlreadyRate
		{
			get { return _messageAlreadyRate; }
			set { _messageAlreadyRate = value; }
		}

		public System.Windows.Visibility lablevis
		{
			get { return _lablevis; }
			set { _lablevis = value; }
		}

		public RateViewModel(Topic topic, User user)
		{
			if (user.istopicrated(topic.Id)) {
				//topic already rated by user
				//Rateit.Enable = false;
				_lablevis = System.Windows.Visibility.Visible;
			}
			else
			{
				//Rateit.Enable = true;
				//messageAlreadyRate.Hide();
			}
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
