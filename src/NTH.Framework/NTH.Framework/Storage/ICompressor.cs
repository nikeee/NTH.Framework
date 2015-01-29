using System;
using System.IO;

namespace NTH.Framework.Storage
{
    public interface ICompressor : IDisposable
    {
        string ProposedFileExtension { get; }
        Stream DecompressProxy(Stream stream);
        Stream CompressProxy(Stream stream);
    }
}
