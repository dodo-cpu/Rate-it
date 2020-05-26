using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rateit.Events
{
    public class LoginEvent
    {

        public Models.User LoggedInUser { get; set; }

        public LoginEvent(Models.User loggedInUser)
        {
            LoggedInUser = loggedInUser;
        }

    }
}
