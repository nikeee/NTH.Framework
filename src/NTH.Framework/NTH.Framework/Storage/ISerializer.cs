using System;
using System.IO;
using System.Threading.Tasks;

namespace NTH.Framework.Storage
{
    public interface ISerializer<T> : IDisposable
    {
        string ProposedFileExtension { get; }

        Task<T> Deserialize(Stream stream);
        Task Serialize(Stream stream, T value);
    }
}
