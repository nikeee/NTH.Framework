
namespace NTH.Framework.Security.Cryptography
{
    // TODO: Maybe use a generic approach and constrain T to HashAlgorithm?
    internal interface IHashValue
    {
        int Size { get; }

        byte[] GetBytes();
    }
}
