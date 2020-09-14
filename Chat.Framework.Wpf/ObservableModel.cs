using System.ComponentModel;

namespace Chat.Framework.Wpf
{
    /// <summary>
    /// View model base class.
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
