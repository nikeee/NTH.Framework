using System;
using System.Threading.Tasks;

namespace NTH.Framework.Storage
{
    public interface IDataStore<T> : IDisposable
    {
        ISerializer<T> Serializer { get; }
        ICompressor Compressor { get; }
        Task StoreAsync(string key, T value);
        Task<T> RetrieveAsync(string key);
        Task<bool> ExistsAsync(string key);
    }
}
