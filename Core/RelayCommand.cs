using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace SnakeGame.Core
{
    public class RelayCommand : ICommand
    {
        private Action<object> _command;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<object> command)
        {
            _command = command;
        }

        public bool CanExecute(object parameter)
        {
            if (_command is not null)
                return true;
            else
                return false;
        }

        public void Execute(object parameter) => _command?.Invoke(parameter);
        
    }
}
