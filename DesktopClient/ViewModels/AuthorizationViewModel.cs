using DesktopClient.Helpers;
using DesktopClient.Services;
using System.Windows.Input;
using Windows.UI.Xaml;

namespace DesktopClient.ViewModels
{
    public class AuthorizationViewModel : Observable
    {
        private string login;

        public string Login
        {
            get => login;
            set { Set(ref login, value); }
        }

        private string name;

        public string Name
        {
            get => name;
            set { Set(ref name, value); }
        }

        private string password;

        public string Password
        {
            get => password;
            set { Set(ref password, value); }
        }

        private int selectdPivotIndex;

        public int SelectdPivotIndex
        {
            get => selectdPivotIndex;
            set { Set(ref selectdPivotIndex, value); }
        }

        private ICommand buttonClickCommand;

        public ICommand ButtonClickCommand => buttonClickCommand ?? (buttonClickCommand = new RelayCommand(ButtonClick));

        public AuthorizationViewModel()
        {
        }

        private async void ButtonClick()
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                return;
            }
            
            OrleansClient.InitializeUser(login);

            if (selectdPivotIndex == 0)
            {
                if (await OrleansClient.User.CheckPassword(password))
                {
                    ((App)Application.Current).ActivationService.IsAutherized = true;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(name))
                {
                    if (await OrleansClient.User.Register(Name, password))
                    {
                        ((App)Application.Current).ActivationService.IsAutherized = true;
                    }
                }
            }
        }
    }
}