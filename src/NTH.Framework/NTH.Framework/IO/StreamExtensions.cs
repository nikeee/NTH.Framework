using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace NTH.Framework.IO
{
    internal static class StreamExtensions
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();
        public static T DeserializeFromJsonStream<T>(this Stream stream)
        {
            using (var streamReader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(streamReader))
                return Serializer.Deserialize<T>(jsonReader);
        }

        public static async Task<T> DeserializeFromJsonStreamAsync<T>(this Stream stream)
        {
            return await Task.Run(() => stream.DeserializeFromJsonStream<T>()).ConfigureAwait(false);
        }

        public static void SerializeToJsonStream<T>(this Stream stream, T value)
        {
            using (var streamWriter = new StreamWriter(stream))
            using (var jsonWriter = new JsonTextWriter(streamWriter))
                Serializer.Serialize(jsonWriter, value);
        }
        public static async Task SerializeToJsonStreamAsync<T>(this Stream stream, T value)
        {
            await Task.Run(() => stream.SerializeToJsonStream(value)).ConfigureAwait(false);
        }
    }
}
