namespace GuaranteedRate.Common.Crypto
{
    public interface ISymmetricCryptoWrapper
    {
        byte[] GenerateKey();
        byte[] Encrypt(byte[] plaintext, byte[] key, byte[] iv = null);
        byte[] Decrypt(byte[] ciphertext, byte[] key);
        string Decrypt(string ciphertext, string key);
        string Decrypt(string ciphertext, byte[] key);
        string Encrypt(string plaintext, byte[] key);
    }
}