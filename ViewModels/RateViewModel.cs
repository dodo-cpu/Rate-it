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
	public class RateViewModel : Screen
	{

		private BindableCollection<Criterion> _criteria = new BindableCollection<Criterion>();
		private Topic topic;
		private User user;

		public BindableCollection<Criterion> Criteria
		{
			get { return _criteria; }
			set { _criteria = value; }
		}

		private System.Windows.Visibility _alreadyRated;

		public System.Windows.Visibility AlreadyRated
		{
			get { return _alreadyRated; }
			set 
			{ 
				_alreadyRated = value;
				NotifyOfPropertyChange(() => AlreadyRated);
			}
		}

		private System.Windows.Visibility _showIsRated;

		public System.Windows.Visibility ShowIsRated
		{
			get { return _showIsRated; }
			set 
			{ 
				_showIsRated = value;
				NotifyOfPropertyChange(() => ShowIsRated);
			}
		}

		/// <summary>
		/// Constructs a RateViewModel with the Data loaded
		/// </summary>
		/// <param name="topic"></param>
		/// <param name="user"></param>
		public RateViewModel(Topic topic, User user)
		{
			this.user = user;
			this.topic = topic;
			CheckRated();
			Criteria.AddRange(Criterion.GetCriteriaByTopic(topic, user));
		}

		/// <summary>
		/// Enables and Disables the Button and Label depending on if the user already rated the topic
		/// </summary>
		private void CheckRated()
		{
			if (this.user.istopicrated(topic.Id))
			{
				//topic already rated by user
				AlreadyRated = System.Windows.Visibility.Hidden;
				ShowIsRated = System.Windows.Visibility.Visible;
			}
			else
			{
				//Rateit.Enable = true;
				AlreadyRated = System.Windows.Visibility.Visible;
				ShowIsRated = System.Windows.Visibility.Hidden;
			}
		}

		#region Events

		/// <summary>
		/// Saves the users rating into the DB
		/// </summary>
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
			CheckRated();
			//TODO: Publish event to open overview from main view
			//AggregatorProvider.Aggregator.PublishOnCurrentThread(new RateEvent());
		}

		#endregion

	}
}
