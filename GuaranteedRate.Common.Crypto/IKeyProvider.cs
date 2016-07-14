namespace GuaranteedRate.Common.Crypto
{
    public interface IKeyProvider
    {
        string GetKeyAsBase64String(string keyName);
        byte[] GetKey(string keyName);
    }
}