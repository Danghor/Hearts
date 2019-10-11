using System;
using System.Windows.Input;

namespace HeartsClient
{
    class RelayCommand : ICommand
    {
        Action Action { get; }

        public event EventHandler CanExecuteChanged;

        internal RelayCommand(Action action)
        {
            Action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            Action.Invoke();
        }
    }
}
