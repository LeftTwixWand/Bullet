using DesktopClient.Helpers;
using DesktopClient.Services;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Core.Helpers;
using Windows.Globalization;
using Windows.ApplicationModel.Core;

namespace DesktopClient.ViewModels
{
    public class SettingsViewModel : Observable
    {
        private ElementTheme elementTheme = ThemeSelectorService.Theme;

        public ElementTheme ElementTheme
        {
            get { return elementTheme; }

            set { Set(ref elementTheme, value); }
        }

        private LanguageEnum languageEnum = LanguageSelector.Languages.FirstOrDefault(item => item.Value == ApplicationLanguages.PrimaryLanguageOverride).Key;

        public LanguageEnum LanguageEnum
        {
            get => languageEnum;
            set { Set(ref languageEnum, value); }
        }

        private string _versionDescription;

        public string VersionDescription
        {
            get { return _versionDescription; }

            set { Set(ref _versionDescription, value); }
        }

        private ICommand linkCommand;

        public ICommand LinkCommand => linkCommand ?? (linkCommand = new RelayCommand(LinkButton_Click));

        private ICommand _switchThemeCommand;

        public ICommand SwitchThemeCommand
        {
            get
            {
                if (_switchThemeCommand == null)
                {
                    _switchThemeCommand = new RelayCommand<ElementTheme>(
                        async (param) =>
                        {
                            ElementTheme = param;
                            await ThemeSelectorService.SetThemeAsync(param);
                        });
                }

                return _switchThemeCommand;
            }
        }

        private ICommand switchLanguageCommand;

        public ICommand SwitchLanguageCommand
        {
            get
            {
                if (switchLanguageCommand == null)
                {
                    switchLanguageCommand = new RelayCommand<LanguageEnum>(
                        async (param) =>
                        {
                            string languageKey;
                            LanguageSelector.Languages.TryGetValue(param, out languageKey);
                            ApplicationLanguages.PrimaryLanguageOverride = languageKey;
                            Windows.ApplicationModel.Resources.Core.ResourceContext.GetForCurrentView().Reset();
                            Windows.ApplicationModel.Resources.Core.ResourceContext.GetForViewIndependentUse().Reset();
                            await CoreApplication.RequestRestartAsync("Change language");
                        });
                }
                return switchLanguageCommand;
            }
        }

        public SettingsViewModel()
        {
        }

        private void LinkButton_Click()
        {
            Process.Start(new ProcessStartInfo("https://github.com/LeftTwixWand/Bullet") { UseShellExecute = true, Verb = "open" });
        }

        public async Task InitializeAsync()
        {
            VersionDescription = GetVersionDescription();
            await Task.CompletedTask;
        }

        private static string GetVersionDescription()
        {
            var appName = "AppDisplayName".GetLocalized();
            var package = Package.Current;
            var packageId = package.Id;
            var version = packageId.Version;

            return $"{appName} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }
    }
}