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
	//By Johann
	public class ShellViewModel : Conductor<object>, IHandle<LoginEvent>, IHandle<LogoutEvent>
    {

		#region Public Methods

		public ShellViewModel()
		{
            AggregatorProvider.Aggregator.Subscribe(this);

			ActivateItem(new LoginViewModel());
		}

		#endregion

		#region Events

		public void Handle(LoginEvent login)
		{
			ActivateItem(new MainViewModel(login.LoggedInUser));
		}

		public void Handle(LogoutEvent logout)
		{
			ActivateItem(new LoginViewModel());
		}

		#endregion

	}
}
