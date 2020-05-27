using Windows.ApplicationModel.Resources;

namespace DesktopClient.Helpers
{
    /// <summary>
    /// Localization helper class
    /// </summary>
    internal static class RecourceExtensions
    {
        /// <summary>
        /// Resource loader - load current resw file from "Strings" folder.
        /// </summary>
        private static ResourceLoader resourceLoader = new ResourceLoader();

        /// <summary>
        /// Localize your string
        /// </summary>
        /// <param name="resourceKey">String you got to translate</param>
        /// <returns>Localized string</returns>
        internal static string GetLocalized(this string resourceKey) => resourceLoader.GetString(resourceKey);
    }
}