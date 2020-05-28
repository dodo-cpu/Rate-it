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
				//NotifyOfPropertyChange(() => SelectedParentCategory);
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
				//NotifyOfPropertyChange(() => SelectedChildCategory);
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

		private void LoadParentCategories()
		{
			ParentCategories.Clear();
			ParentCategories.AddRange(Category.GetCategoriesByParent(null));
		}

		private void LoadChildCategories()
		{
			ChildCategories.Clear();
			if (SelectedParentCategory != null)
			{
				ChildCategories.AddRange(Category.GetCategoriesByParent(SelectedParentCategory.Id));
			}
		}

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

		public void Logout()
		{
			AggregatorProvider.Aggregator.PublishOnCurrentThread(new LogoutEvent());
		}

		public void Handle(RateEvent rate)
		{
			ActivateItem(new OverViewModel(SelectedChildCategory.Id));
		}

		#endregion

	}
}
