using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM
{
    public class DelegateCommand : ICommand
    {
        private Action _Action;

        public DelegateCommand(Action action)
        {
            _Action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

#pragma warning disable
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _Action.Invoke();
        }
#pragma warning restore
    }

}
