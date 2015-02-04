using System.IO;
using System.IO.Compression;

namespace NTH.Framework.Storage
{
    class GZipCompressor : ICompressor
    {
        public string ProposedFileExtension { get { return ".gz"; } }

        public Stream DecompressProxy(Stream stream)
        {
            System.Diagnostics.Debug.Assert(stream != null);

            return new GZipStream(stream, CompressionMode.Decompress, true);
        }

        public Stream CompressProxy(Stream stream)
        {
            System.Diagnostics.Debug.Assert(stream != null);

            return new GZipStream(stream, CompressionMode.Compress, true);
        }

        public void Dispose()
        { }
    }
}
