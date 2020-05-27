using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Core.Helpers
{
    /// <summary>
    /// Helper class, which allows to work with JSON type.
    /// </summary>
    public static class Json
    {
        /// <summary>
        /// Convert data from JSON to Type asynchronously
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="value">Object</param>
        /// <returns>Type</returns>
        public static async Task<T> ToObjectAsync<T>(string value) => await Task.Run(() => JsonConvert.DeserializeObject<T>(value));

        /// <summary>
        /// Convert data from JSON to string asynchronously
        /// </summary>
        /// <param name="value"></param>
        /// <returns>String text from object</returns>
        public static async Task<string> StringifyAsync(object value) => await Task.Run(() => JsonConvert.SerializeObject(value));
    }
}