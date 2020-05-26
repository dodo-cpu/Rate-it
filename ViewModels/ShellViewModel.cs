﻿using Caliburn.Micro;
using Rateit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rateit.ViewModels
{
    public class ShellViewModel : Screen
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
			set { _user = value; }
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


		#endregion

		#region Public Methods

		public ShellViewModel()
		{
			LoadParentCategories();

			User = new User(1);

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
			List<Category> categories = Category.GetCategoriesByParent(SelectedParentCategory.Id);
			foreach (Category category in categories)
			{
				ChildCategories.Add(category);
			}
		}

		private void LoadTopics()
		{
			Topics.Clear();
			List<Topic> topics = Topic.GetTopicsByCategoryId(SelectedChildCategory.Id);
			foreach (Topic topic in topics)
			{
				Topics.Add(topic);
			}

		}

		#endregion

	}
}