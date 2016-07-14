using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace GuaranteedRate.Common.Crypto
{
    public class AESCryptoWrapper : ISymmetricCryptoWrapper
    {
        private readonly int KEY_SIZE = 256;

        /// <summary>
        ///     generates a valid key
        /// </summary>
        /// <returns>the key as a byte array</returns>
        public byte[] GenerateKey()
        {
            using (var aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.GenerateKey();
                return aes.Key;
            }
        }

        /// <summary>
        ///     Encrypts a byte array
        /// </summary>
        /// <param name="plaintext"></param>
        /// <param name="key"></param>
        /// <param name="iv">Optional.  Will generate IV if needed.</param>
        /// <returns></returns>
        public byte[] Encrypt(byte[] plaintext, byte[] key, byte[] iv = null)
        {
            using (var aes = Aes.Create())
            {
                if (iv == null)
                {
                    aes.GenerateIV();
                    iv = aes.IV;
                }
                aes.KeySize = KEY_SIZE;
                aes.Padding = PaddingMode.PKCS7;
                aes.BlockSize = 128;
                aes.IV = iv;
                aes.Key = key;
                aes.Mode = CipherMode.CBC;

                using (var enc = aes.CreateEncryptor())
                {
                    var result = new List<byte>();
                    result.AddRange(iv);
                    result.AddRange(enc.TransformFinalBlock(plaintext, 0, plaintext.Length));

                    return result.ToArray();
                }
            }
        }

        /// <summary>
        ///     Decrypts a byte array
        /// </summary>
        /// <param name="ciphertext"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public byte[] Decrypt(byte[] ciphertext, byte[] key)
        {
            var iv = new byte[16];
            Array.Copy(ciphertext, 0, iv, 0, iv.Length);
            var foo = new byte[ciphertext.Length - iv.Length];
            Array.Copy(ciphertext, iv.Length, foo, 0, ciphertext.Length - iv.Length);
            ciphertext = foo;

            using (var aes = Aes.Create())
            {
                aes.BlockSize = 128;
                aes.KeySize = KEY_SIZE;
                aes.Padding = PaddingMode.PKCS7;
                aes.IV = iv;
                aes.Key = key;
                aes.Mode = CipherMode.CBC;

                using (var enc = aes.CreateDecryptor())
                {
                    return enc.TransformFinalBlock(ciphertext, 0, ciphertext.Length);
                }
            }
        }

        /// <summary>
        ///     Decrypts
        /// </summary>
        /// <param name="ciphertext">The cyphertext as a base 64 string</param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Decrypt(string ciphertext, byte[] key)
        {
            var bytes = Convert.FromBase64String(ciphertext);
            var plaintext = Decrypt(bytes, key);

            return Encoding.UTF8.GetString(plaintext);
        }

        /// <summary>
        /// </summary>
        /// <param name="ciphertext">The cyphertext as a base 64 string</param>
        /// <param name="key">The key as a base 64 string</param>
        /// <returns></returns>
        public string Decrypt(string ciphertext, string key)
        {
            var bytes = Convert.FromBase64String(ciphertext);
            var plaintext = Decrypt(bytes, Convert.FromBase64String(key));

            return Encoding.UTF8.GetString(plaintext);
        }

        /// <summary>
        ///     encrypts a string.
        /// </summary>
        /// <param name="plaintext">The plaintext as UTF8</param>
        /// <param name="key"></param>
        /// <returns>A base64 encoded string. </returns>
        public string Encrypt(string plaintext, byte[] key)
        {
            var bytes = Encoding.UTF8.GetBytes(plaintext);
            var cipher = Encrypt(bytes, key);
            return Convert.ToBase64String(cipher);
        }

   

        /// <summary>
        /// </summary>
        /// <param name="plaintext">The plaintext as UTF8</param>
        /// <param name="key">The key as a base 64 string</param>
        /// <returns>A base64 encoded string</returns>
        public string Encrypt(string plaintext, string key)
        {
            var bytes = Encoding.UTF8.GetBytes(plaintext);
            var cipher = Encrypt(bytes, Convert.FromBase64String(key));
            return Convert.ToBase64String(cipher);
        }

      
    }
}