using System;
using GuaranteedRate.Common.Configuration;

namespace GuaranteedRate.Common.Crypto
{
    public class ConfigKeyProvider : IKeyProvider
    {
        public byte[] GetKey(string keyName)
        {
            return Convert.FromBase64String(
                GetKeyAsBase64String(keyName));
        }

        /// <summary>
        /// looks for a key in app.config/web.config called [keyName] or Crypto.[keyName]
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public string GetKeyAsBase64String(string keyName)
        {
            if (ConfigHelper.GetAppSetting<string>(keyName, "not found") != "not found")
            {
                return ConfigHelper.GetAppSetting<string>(keyName, "not found");
            }

            if (ConfigHelper.GetAppSetting<string>($"Crypto.Key.{keyName}", "not found") != "not found")
            {
                return ConfigHelper.GetAppSetting<string>($"Crypto.Key.{keyName}", "");
            }

            throw new ArgumentException($"Key not found with name of {keyName}");
        }
    }
}