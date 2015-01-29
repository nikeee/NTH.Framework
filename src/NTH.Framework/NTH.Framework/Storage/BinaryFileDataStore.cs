using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace NTH.Framework.Storage
{
    // TODO Test
    public class BinaryFileDataStore<T> : ISerializer<T>
    {
        private static readonly BinaryFormatter _formatter = new BinaryFormatter();
        public string ProposedFileExtension { get { return string.Empty; } }

        public async Task<T> Deserialize(Stream stream)
        {
            var o = await Task.Run(() => _formatter.Deserialize(stream)).ConfigureAwait(false);
            return (T)o;
        }

        public async Task Serialize(Stream stream, T value)
        {
            await Task.Run(() => _formatter.Serialize(stream, value)).ConfigureAwait(false);
        }

        public void Dispose()
        { }
    }
}
