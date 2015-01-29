using System;

namespace NTH.Framework.Storage
{
    public class DataStoreStoreException : DataStoreException
    {
        public DataStoreStoreException(Exception innerException)
            : base("An error during data store item storage occurred.", innerException)
        { }
    }
}
