using System;
using System.IO;
using System.Threading.Tasks;

namespace NTH.Framework.Storage
{
    // TODO: synclocks

    public class FileDataStore<T> : IDataStore<T>, IDisposable
    {
        private readonly string _directory;
        private readonly string _extension;
        private readonly ISerializer<T> _serializer;
        private readonly ICompressor _compressor;

        public string Directory { get { return _directory; } }
        public string Extension { get { return _extension; } }
        public ISerializer<T> Serializer { get { return _serializer; } }
        public ICompressor Compressor { get { return _compressor; } }

        public int ItemCount { get { return System.IO.Directory.GetFiles(_directory).Length; } }

        public FileDataStore(ISerializer<T> serializer, string directory)
            : this(serializer, directory, string.Empty)
        { }
        public FileDataStore(ISerializer<T> serializer, string directory, string extension)
            : this(serializer, directory, extension, null)
        { }
        public FileDataStore(ISerializer<T> serializer, string directory, string extension, ICompressor compressor)
        {
            if (serializer == null)
                throw new ArgumentNullException("serializer");
            _serializer = serializer;
            _directory = directory;
            _extension = extension;
            _compressor = compressor;
        }

        public Task<DirectoryInfo> EnsureDirectoryAsync()
        {
            //if (!Directory.Exists(_directory))
            // http://stackoverflow.com/a/6925583/785210
            return Task.Run(() => System.IO.Directory.CreateDirectory(_directory));
        }

        private string GetFileNameForKey(string key)
        {
            return Path.Combine(_directory, key + _extension);
        }

        public async Task<T> RetrieveAsync(string key)
        {
            var fileName = GetFileNameForKey(key);
            try
            {
                using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    if (_compressor != null)
                    {
                        using (var decompressedStream = _compressor.DecompressProxy(fs))
                            return await _serializer.Deserialize(decompressedStream).ConfigureAwait(false);
                    }
                    return await _serializer.Deserialize(fs).ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                throw new DataStoreRetrieveException(e);
            }
        }

        public async Task StoreAsync(string key, T value)
        {
            var fileName = GetFileNameForKey(key);
            try
            {
                using (var fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    if (_compressor != null)
                    {
                        using (var compressedStream = _compressor.CompressProxy(fs))
                            await _serializer.Serialize(compressedStream, value).ConfigureAwait(false);
                    }
                    else
                    {
                        await _serializer.Serialize(fs, value).ConfigureAwait(false);
                    }
                }
            }
            catch (Exception e)
            {
                throw new DataStoreStoreException(e);
            }
        }

        public Task<bool> ExistsAsync(string key)
        {
            return Task.Run(() =>
            {
                var fileName = GetFileNameForKey(key);
                return File.Exists(fileName);
            });
        }

        private bool _disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (!_disposed)
            {

            }
            _disposed = true;
        }

        ~FileDataStore()
        {
            Dispose(false);
        }
    }
}
