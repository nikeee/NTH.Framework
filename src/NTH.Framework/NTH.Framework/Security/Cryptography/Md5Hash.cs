using System;
using System.IO;
using System.Security.Cryptography;

namespace NTH.Framework.Security.Cryptography
{
    internal class Md5Hash : IHashValue
    {
        private const int HashSize = 16; // in bytes
        public int Size { get { return HashSize; } }

        private readonly byte[] _data;

        public Md5Hash(byte[] hashData)
        {
            if (hashData == null)
                hashData = new byte[HashSize];
            if (hashData.Length != HashSize)
                throw new ArgumentException("Invalid hash size.");
            _data = hashData;
        }

        #region Static From*

        public static Md5Hash FromFile(FileInfo file)
        {
            return FromFile(file.FullName);
        }
        public static Md5Hash FromFile(string fileName)
        {
            using (var stream = File.OpenRead(fileName))
                return FromStream(stream);
        }
        public static Md5Hash FromStream(Stream stream)
        {
            using (var csp = new MD5CryptoServiceProvider())
            {
                var byteData = csp.ComputeHash(stream);
                return new Md5Hash(byteData);
            }
        }

        #endregion

        #region Equality

        public static bool operator ==(Md5Hash a, Md5Hash b)
        {
            var aData = a == null ? new byte[HashSize] : a._data;
            var bData = b == null ? new byte[HashSize] : b._data;

            for (int i = 0; i < HashSize; ++i)
                if (aData[i] != bData[i]) // TODO: Compiler flag for no optimization?
                    return false;
            return true;
        }
        public static bool operator !=(Md5Hash a, Md5Hash b)
        {
            return !(a == b);
        }

        #region Equals

        protected bool Equals(Md5Hash other)
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
            return Equals((Md5Hash)obj);
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