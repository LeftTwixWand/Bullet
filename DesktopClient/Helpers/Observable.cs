using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DesktopClient.Helpers
{
    /// <summary>
    /// Class to notify xaml code changes
    /// </summary>
    public class Observable : INotifyPropertyChanged
    {
        /// <summary>
        /// Main event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Property changed event method
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// Calls on property changed event method
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="storage">Xaml context</param>
        /// <param name="value">Value</param>
        /// <param name="propertyName">Property name, which method gets automatically</param>
        protected void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }
    }
}