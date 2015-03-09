using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace NTH.Framework.Security.Cryptography
{
    internal class Sha1Hash : IHashValue
    {
        private const int HashSize = 20; // in bytes
        public int Size { get { return HashSize; } }

        private readonly byte[] _data;

        public Sha1Hash(byte[] hashData)
        {
            if (hashData == null)
                hashData = new byte[HashSize];
            if (hashData.Length != HashSize)
                throw new ArgumentException("Invalid hash size.");
            _data = hashData;
        }

        #region Static From*

        public static Sha1Hash FromFile(FileInfo file)
        {
            return FromFile(file.FullName);
        }
        public static Sha1Hash FromFile(string fileName)
        {
            using (var stream = File.OpenRead(fileName))
                return FromStream(stream);
        }
        public static Sha1Hash FromStream(Stream stream)
        {
            using (var csp = new SHA1CryptoServiceProvider())
            {
                var byteData = csp.ComputeHash(stream);
                return new Sha1Hash(byteData);
            }
        }

        #region Async

        public static Task<Sha1Hash> FromFileAsync(string fileName, CancellationToken cancellationToken, IProgress<int> progress)
        {
            // TODO: Test

            const int bufferSize = 4096;

            return Task.Run(() =>
                            {
                                using (var csp = new SHA1CryptoServiceProvider())
                                using (var stream = File.OpenRead(fileName))
                                {
                                    var buffer = new byte[bufferSize];
                                    long totalSize = stream.Length;
                                    int bytesRead;
                                    long totalBytesRead = 0;

                                    do
                                    {
                                        cancellationToken.ThrowIfCancellationRequested();

                                        bytesRead = stream.Read(buffer, 0, bufferSize);
                                        totalBytesRead += bytesRead;

                                        if (bytesRead == 0)
                                        {
                                            csp.TransformFinalBlock(buffer, 0, bytesRead);
                                        }
                                        else
                                        {
                                            csp.TransformBlock(buffer, 0, bytesRead, null, 0);
                                        }

                                        if (progress != null)
                                            progress.Report(checked((int)(totalBytesRead * 100 / totalSize)));
                                    } while (bytesRead != 0);

                                    return new Sha1Hash(csp.Hash);
                                }
                            }, cancellationToken);
        }

        #endregion

        #endregion

        #region Equality

        public static bool operator ==(Sha1Hash a, Sha1Hash b)
        {
            var aData = a == null ? new byte[HashSize] : a._data;
            var bData = b == null ? new byte[HashSize] : b._data;

            for (int i = 0; i < HashSize; ++i)
                if (aData[i] != bData[i]) // TODO: Compiler flag for no optimization?
                    return false;
            return true;
        }
        public static bool operator !=(Sha1Hash a, Sha1Hash b)
        {
            return !(a == b);
        }

        #region Equals

        protected bool Equals(Sha1Hash other)
        {
            return Equals(_data, other._data);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            return Equals((Sha1Hash)obj);
        }

        #endregion

        #endregion

        public override int GetHashCode()
        {
            return (_data != null ? _data.GetHashCode() : 0);
        }

        public byte[] GetBytes()
        {
            throw new NotImplementedException();
        }
    }
}
