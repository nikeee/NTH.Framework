using NTH.Framework.IO;
using System.IO;
using System.Threading.Tasks;

namespace NTH.Framework.Storage
{
    public class JsonSerializer<T> : ISerializer<T>
    {
        public string ProposedFileExtension { get { return ".json"; } }

        public async Task<T> Deserialize(Stream stream)
        {
            return await stream.DeserializeFromJsonStreamAsync<T>().ConfigureAwait(false);
        }

        public async Task Serialize(Stream stream, T value)
        {
            await stream.SerializeToJsonStreamAsync(value).ConfigureAwait(false);
        }

        public void Dispose()
        { }
    }
}
