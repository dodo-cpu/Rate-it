using Caliburn.Micro;
using Rateit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rateit.Events;

namespace Rateit.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LoginEvent>
    {

        #region Fields

        private User _user;
		private BindableCollection<Category> _parentCategories = new BindableCollection<Category>();
		private Category _selectedParentCategory;
		private BindableCollection<Category> _childCategories = new BindableCollection<Category>();
		private Category _selectedChildCategory;
		private BindableCollection<Topic> _topics = new BindableCollection<Topic>();
		private Topic _selectedTopic;
		private System.Windows.Visibility _logoutVisibility = System.Windows.Visibility.Hidden;

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
				NotifyOfPropertyChange(() => SelectedParentCategory);
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
				NotifyOfPropertyChange(() => SelectedChildCategory);
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
			set { _selectedTopic = value; }
		}

		public System.Windows.Visibility LogoutVisibility
		{
			get { return _logoutVisibility; }
			private set 
			{
				_logoutVisibility = value;
				NotifyOfPropertyChange(() => LogoutVisibility);
			}
		}


		#endregion

		#region Public Methods

		public ShellViewModel()
		{
            //LoadParentCategories();
            AggregatorProvider.Aggregator.Subscribe(this);

			ActivateItem(new LoginViewModel());

		}

        public void Logout()
        {
            User = null;
			LogoutVisibility = System.Windows.Visibility.Hidden;
            ActivateItem(new LoginViewModel());
        }

		#endregion

		#region Private Methods

		private void LoadParentCategories()
		{
			ParentCategories.Clear();
			List<Category> categories = Category.GetCategoriesByParent(null);
			foreach (Category category in categories)
			{
				ParentCategories.Add(category);
			}
		}

		private void LoadChildCategories()
		{
			ChildCategories.Clear();
            if (SelectedParentCategory != null)
            {
                List<Category> categories = Category.GetCategoriesByParent(SelectedParentCategory.Id);
                foreach (Category category in categories)
                {
                    ChildCategories.Add(category);
                }
            }
		}

		private void LoadTopics()
		{
			Topics.Clear();
            if (SelectedChildCategory != null)
            {
                List<Topic> topics = Topic.GetTopicsByCategoryId(SelectedChildCategory.Id);
                foreach (Topic topic in topics)
                {
                    Topics.Add(topic);
                }
            }
		}

        #endregion

        #region Events

        public void Handle(LoginEvent message)
        {
            User = message.LoggedInUser;
			LogoutVisibility = System.Windows.Visibility.Visible;
			LoadParentCategories();
            //TODO: ActivateItem(new RatingsViewModel);
        }

        #endregion

    }
}
