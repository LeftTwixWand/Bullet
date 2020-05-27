using DesktopClient.Helpers;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace DesktopClient.Services
{
    /// <summary>
    /// Class to manage app theme
    /// </summary>
    public static class ThemeSelectorService
    {
        /// <summary>
        /// Theme key from local storage file
        /// </summary>
        private const string SettingsKey = "AppBackgroundTheme";

        /// <summary>
        /// App theme
        /// </summary>
        public static ElementTheme Theme { get; set; } = ElementTheme.Default;

        /// <summary>
        /// Read theme from settings file
        /// </summary>
        /// <returns>Completed task</returns>
        public static async Task InitializeAsync() => Theme = await LoadThemeFromSettingsAsync();

        /// <summary>
        /// Set new app theme
        /// </summary>
        /// <param name="theme">New app theme</param>
        /// <returns>Completed theme</returns>
        public static async Task SetThemeAsync(ElementTheme theme)
        {
            Theme = theme;

            await SetRequestedThemeAsync();
            await SaveThemeInSettingsAsync(Theme);
        }

        /// <summary>
        /// Save theme in local storage
        /// </summary>
        /// <param name="theme">New theme</param>
        /// <returns>Completed task</returns>
        private async static Task SaveThemeInSettingsAsync(ElementTheme theme) => await ApplicationData.Current.LocalSettings.SaveAsync(SettingsKey, theme.ToString());

        /// <summary>
        /// Apply new theme for every element in current active view
        /// </summary>
        /// <returns></returns>
        private static async Task SetRequestedThemeAsync()
        {
            foreach (var view in CoreApplication.Views)
            {
                await view.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    if (Window.Current.Content is FrameworkElement frameworkElement)
                    {
                        frameworkElement.RequestedTheme = Theme;
                    }
                });
            }
        }

        /// <summary>
        /// Load app theme from local storage
        /// </summary>
        /// <returns>Loaded theme</returns>
        private static async Task<ElementTheme> LoadThemeFromSettingsAsync()
        {
            ElementTheme cacheTheme = ElementTheme.Default;

            string themeName = await ApplicationData.Current.LocalSettings.ReadAsync<string>(SettingsKey);

            if (!string.IsNullOrEmpty(themeName))
            {
                Enum.TryParse(themeName, out cacheTheme);
            }

            return cacheTheme;
        }
    }
}