using System;

namespace NTH.Framework.Storage
{
    public class DataStoreRetrieveException : DataStoreException
    {
        public DataStoreRetrieveException(Exception innerException)
            : base("An error during data store item retrieval occurred.", innerException)
        { }
    }
}
