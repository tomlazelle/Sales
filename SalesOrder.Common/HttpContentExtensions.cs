using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Raven.Imports.Newtonsoft.Json;

namespace SalesOrder.Common
{
    public static class HttpContentExtensions
    {
        public static async Task<T> ReadAsAsync<T>(this HttpContent httpContent)
        {
            var serializer = new JsonSerializer();

            var contentStream = await httpContent.ReadAsStreamAsync();

            using (var sr = new StreamReader(contentStream))
            using (var jsonTextReader = new JsonTextReader(sr))
            {
                return serializer.Deserialize<T>(jsonTextReader);
            }
        }
    }
}