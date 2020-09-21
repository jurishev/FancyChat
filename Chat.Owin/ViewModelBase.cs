using System.ComponentModel;

namespace Chat.Owin
{
    /// <summary>
    /// MVVM View Model base class.
    /// </summary>
    internal class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
