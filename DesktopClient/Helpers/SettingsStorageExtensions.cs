using Core.Helpers;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace DesktopClient.Helpers
{
    /// <summary>
    /// Class, helps to work with app storage
    /// </summary>
    public static class SettingsStorageExtensions
    {
        /// <summary>
        /// File extension
        /// </summary>
        private const string FileExtension = ".json";

        /// <summary>
        /// Check if app storrage is available
        /// </summary>
        /// <param name="appData">App data storage</param>
        /// <returns></returns>
        public static bool IsRoamingStorageAvailable(this ApplicationData appData) => appData?.RoamingStorageQuota == 0;

        /// <summary>
        /// Save data to .json file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="folder">App folder</param>
        /// <param name="name">Name</param>
        /// <param name="content">Data type</param>
        /// <returns></returns>
        public static async Task SaveAsync<T>(this StorageFolder folder, string name, T content)
        {
            var file = await folder?.CreateFileAsync(GetFileName(name), CreationCollisionOption.ReplaceExisting);

            var fileContent = await Json.StringifyAsync(content);

            await FileIO.WriteTextAsync(file, fileContent);
        }

        /// <summary>
        /// Deserialize data from .json file
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="folder">App folder</param>
        /// <param name="name">File name key</param>
        /// <returns></returns>
        public static async Task<T> ReadAsync<T>(this StorageFolder folder, string name)
        {
            if (!File.Exists(Path.Combine(folder.Path, GetFileName(name))))
            {
                return default;
            }

            var file = await folder.GetFileAsync($"{name}.json");
            var fileContent = await FileIO.ReadTextAsync(file);

            return await Json.ToObjectAsync<T>(fileContent);
        }

        /// <summary>
        /// Save data to .json file
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="settings">Application data container</param>
        /// <param name="key">Value key</param>
        /// <param name="value">Object to save</param>
        /// <returns>Completed task</returns>
        public static async Task SaveAsync<T>(this ApplicationDataContainer settings, string key, T value) => settings.SaveString(key, await Json.StringifyAsync(value));

        /// <summary>
        /// Save data to .json file
        /// </summary>
        /// <param name="settings">Application data container</param>
        /// <param name="key">Value key</param>
        /// <param name="value">String to save</param>
        public static void SaveString(this ApplicationDataContainer settings, string key, string value) => settings.Values[key] = value;

        /// <summary>
        /// Read data from json with specific key
        /// </summary>
        /// <typeparam name="T">Data type to return</typeparam>
        /// <param name="settings">Application data container</param>
        /// <param name="key">Value key</param>
        /// <returns>Converted data</returns>
        public static async Task<T> ReadAsync<T>(this ApplicationDataContainer settings, string key)
        {
            object obj = null;

            if (settings.Values.TryGetValue(key, out obj))
            {
                return await Json.ToObjectAsync<T>((string)obj);
            }

            return default;
        }

        /// <summary>
        /// Save byte data to .json file
        /// </summary>
        /// <param name="folder">Folder with file</param>
        /// <param name="content">Byte data</param>
        /// <param name="fileName">JSON file name</param>
        /// <param name="options">Save options</param>
        /// <returns>Storage file with new data</returns>
        public static async Task<StorageFile> SaveFileAsync(this StorageFolder folder, byte[] content, string fileName, CreationCollisionOption options = CreationCollisionOption.ReplaceExisting)
        {
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("ExceptionSettingsStorageExtensionsFileNameIsNullOrEmpty".GetLocalized(), nameof(fileName));
            }

            var storageFile = await folder.CreateFileAsync(fileName, options);
            await FileIO.WriteBytesAsync(storageFile, content);
            return storageFile;
        }

        /// <summary>
        /// Return file as byte array
        /// </summary>
        /// <param name="folder">Folder</param>
        /// <param name="fileName">File name</param>
        /// <returns>Byte array</returns>
        public static async Task<byte[]> ReadFileAsync(this StorageFolder folder, string fileName)
        {
            var item = await folder.TryGetItemAsync(fileName).AsTask().ConfigureAwait(false);

            if ((item != null) && item.IsOfType(StorageItemTypes.File))
            {
                var storageFile = await folder.GetFileAsync(fileName);
                byte[] content = await storageFile.ReadBytesAsync();
                return content;
            }

            return null;
        }

        /// <summary>
        /// Read byre from file
        /// </summary>
        /// <param name="file">Storrage file</param>
        /// <returns>Byte array</returns>
        public static async Task<byte[]> ReadBytesAsync(this StorageFile file)
        {
            if (file != null)
            {
                using (IRandomAccessStream stream = await file.OpenReadAsync())
                {
                    using (var reader = new DataReader(stream.GetInputStreamAt(0)))
                    {
                        await reader.LoadAsync((uint)stream.Size);
                        var bytes = new byte[stream.Size];
                        reader.ReadBytes(bytes);
                        return bytes;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Concat file name and file format
        /// </summary>
        /// <param name="name">file name</param>
        /// <returns>Full file name</returns>
        private static string GetFileName(string name) => string.Concat(name, FileExtension);
    }
}