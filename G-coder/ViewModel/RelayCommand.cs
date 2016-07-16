using System;
using System.Windows.Input;

namespace G_coder.ViewModel
{
    internal class RelayCommand : ICommand
    {
        private Func<bool> _canExecute;
        private Action _execute;

        public RelayCommand(Action execute)
            : this(execute, null)
        {
            
        }

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            if(execute == null)
                throw new ArgumentNullException(nameof(execute));
            this._execute = execute;
            this._canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute();
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
    }
}