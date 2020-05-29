using DesktopClient.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopClient.ViewModels
{
    public class AuthorizationViewModel : Observable
    {
        private ICommand buttonClickCommand;

        public ICommand ButtonClickCommand => buttonClickCommand ?? (buttonClickCommand = new RelayCommand(ButtonClick));

        private void ButtonClick()
        {
            Debug.WriteLine("Work!!!");
        }

        public AuthorizationViewModel()
        {
        }
    }
}
