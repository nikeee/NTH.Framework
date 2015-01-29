using System;

namespace NTH.Framework.Storage
{
    public abstract class DataStoreException : Exception
    {
        protected DataStoreException(string message, Exception innerException)
            : base(message + " See innerException for more details.", innerException)
        { }
    }
}
