# Common.Crypto
Symmetric encryption library for .NET Clients


Right now, we only have a key provider interface, a key provider that can store keys in config, and an AES symmetric encryption wrapper.

```var myKeyAsBytes = new ConfigKeyProvider().GetKey("my-key-name");```

var myKeyAsString = new ConfigKeyProvider().GetKeyAsBase64String("my-key-name");

using(enc = new AESCryptoWrapper()){
	var result = enc.Encrypt("my big secret", myKeyAsString);
}```


All result strings come out as base64 encoded strings.  