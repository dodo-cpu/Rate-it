using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Rateit.Commands
{
    //Source 27.04.2020: https://www.youtube.com/watch?v=0IKOphciSZo
    class RelayCommand : ICommand
    {
        Action<object> executeAction;
        Func<object, bool> canExecute;
        bool CanExecuteCache;

        public RelayCommand(Action<object> executeAction, Func<object,bool> canExecute, bool canExecuteCache)
        {
            this.canExecute = canExecute;
            this.executeAction = executeAction;
            this.CanExecuteCache = canExecuteCache;
        }

        public bool CanExecute(object parameters)
        {
            if (canExecute == null)
            {
                return true;
            }
            else
            {
                return canExecute(parameters);
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public void Execute(object parameter)
        {
            executeAction(parameter);
        }

    }
}
