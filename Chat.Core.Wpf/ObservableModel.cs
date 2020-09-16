using System.ComponentModel;

namespace Chat.Core.Wpf
{
    /// <summary>
    /// WPF MVVM view model base.
    /// </summary>
    internal class ObservableModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
