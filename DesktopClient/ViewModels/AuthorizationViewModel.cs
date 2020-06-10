using DesktopClient.Helpers;
using DesktopClient.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;

namespace DesktopClient.ViewModels
{
    public class AuthorizationViewModel : Observable
    {
        private ICommand buttonClickCommand;

        public ICommand ButtonClickCommand => buttonClickCommand ?? (buttonClickCommand = new RelayCommand(ButtonClick));

        private async void ButtonClick()
        {
            OrleansClient.InitializeUser("LeftTwixWand");
            Debug.WriteLine(await OrleansClient.User.GetLogin());
            ((App)Application.Current).ActivationService.IsAutherized = true;
        }

        public AuthorizationViewModel()
        {
        }
    }
}
