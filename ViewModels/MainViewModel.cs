using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Rateit.Views;
using Rateit.Models;
using Caliburn.Micro;
using Rateit.Events;

namespace Rateit.ViewModels
{
	public class MainViewModel : Conductor<object>, IHandle<RateEvent>
	{

		#region Fields

		private User _user;
		private BindableCollection<Category> _parentCategories = new BindableCollection<Category>();
		private Category _selectedParentCategory;
		private BindableCollection<Category> _childCategories = new BindableCollection<Category>();
		private Category _selectedChildCategory;
		private BindableCollection<Topic> _topics = new BindableCollection<Topic>();
		private Topic _selectedTopic;

		#endregion

		#region Properties

		public User User
		{
			get { return _user; }
			set
			{
				_user = value;
				NotifyOfPropertyChange(() => User);
			}
		}

		public BindableCollection<Category> ParentCategories
		{
			get { return _parentCategories; }
			set { _parentCategories = value; }
		}

		public Category SelectedParentCategory
		{
			get { return _selectedParentCategory; }
			set
			{
				_selectedParentCategory = value;
				LoadChildCategories();
				ActivateItem(new OverViewModel(SelectedParentCategory.Id));
			}
		}

		public BindableCollection<Category> ChildCategories
		{
			get { return _childCategories; }
			set { _childCategories = value; }
		}

		public Category SelectedChildCategory
		{
			get { return _selectedChildCategory; }
			set
			{
				_selectedChildCategory = value;
				LoadTopics();
				ActivateItem(new OverViewModel(SelectedChildCategory.Id));
			}
		}

		public BindableCollection<Topic> Topics
		{
			get { return _topics; }
			set { _topics = value; }
		}

		public Topic SelectedTopic
		{
			get { return _selectedTopic; }
			set 
			{ 
				_selectedTopic = value;
				ActivateItem(new RateViewModel(SelectedTopic, User));
			}
		}

		#endregion

		#region Public Methods

		public MainViewModel(User loggedInUser)
		{
			User = loggedInUser;
			LoadParentCategories();
			//TODO: specify starting category
			ActivateItem(new OverViewModel(null));

		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Loads the parent categories into the BindableCollection
		/// </summary>
		private void LoadParentCategories()
		{
			ParentCategories.Clear();
			ParentCategories.AddRange(Category.GetCategoriesByParent(null));
		}

		/// <summary>
		/// Loads the child categories of the selected parent category into the BindableCollection
		/// </summary>
		private void LoadChildCategories()
		{
			ChildCategories.Clear();
			if (SelectedParentCategory != null)
			{
				ChildCategories.AddRange(Category.GetCategoriesByParent(SelectedParentCategory.Id));
			}
		}

		/// <summary>
		/// Loads the topics of the selected child category into the BindableCollection
		/// </summary>
		private void LoadTopics()
		{
			Topics.Clear();
			if (SelectedChildCategory != null)
			{
				Topics.AddRange(Topic.GetTopicsByCategoryId(SelectedChildCategory.Id));
			}
		}

		#endregion

		#region Events

		/// <summary>
		/// Signals to ShellViewModel that Logout occured
		/// </summary>
		public void Logout()
		{
			AggregatorProvider.Aggregator.PublishOnCurrentThread(new LogoutEvent());
		}

		//TODO: Get the event from RateViewModel to here
		///// <summary>
		///// Opens the OverView after rating occured
		///// </summary>
		///// <param name="rate"></param>
		//public void Handle(RateEvent rate)
		//{
		//	ActivateItem(new OverViewModel(SelectedChildCategory.Id));
		//}

		#endregion

	}
}
