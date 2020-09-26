using System;
using System.Windows.Input;

namespace Chat.Start
{
    /// <summary>
    /// WPF MVVM command base class.
    /// </summary>
    internal class ActionCommand : ICommand
    {
        private readonly Action<object> action;
        private readonly Predicate<object> canExecute;

        public ActionCommand(Action<object> action) : this(action, null) { }

        public ActionCommand(Action<object> action, Predicate<object> canExecute)
        {
            this.action = action;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) => canExecute is null || canExecute(parameter);

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                action.Invoke(parameter);
            }
        }
    }
}
