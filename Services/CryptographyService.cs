using System.Security.Cryptography;
using Yota_backend.Services.Interface;

namespace Yota_backend.Services;

public class CryptographyService : ICryptographyService
{
    private static string _key = "";
    private readonly Aes _aes; 
    
    public CryptographyService()
    {
        _key = File.ReadAllText("EncryptKey.csv");
        _aes = Aes.Create();
        _aes.KeySize = 256;
        _aes.IV = new byte[16];
        _aes.Mode = CipherMode.CBC;
        _aes.Padding = PaddingMode.PKCS7;
        GenerateKeyIfNull();
    }

    public string AesEncrypt(string plainText)
    {
        if (string.IsNullOrEmpty(plainText)) throw new ArgumentNullException(nameof(plainText));
        
        var encryptor = _aes.CreateEncryptor(_aes.Key, _aes.IV);

        using var msEncrypt = new MemoryStream();
        using var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
        using var swEncrypt = new StreamWriter(csEncrypt);
        swEncrypt.Write(plainText);
        var encrypted = msEncrypt.ToArray();
        
        return Convert.ToBase64String(encrypted);
    }

    public string AesDecrypt(string cipherText)
    {
        if (string.IsNullOrEmpty(cipherText)) throw new ArgumentNullException(nameof(cipherText));

        var cipherBytes = Convert.FromBase64String(cipherText);
        var decryptor = _aes.CreateDecryptor(_aes.Key, _aes.IV);

        using var msDecrypt = new MemoryStream(cipherBytes);
        using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        using var srDecrypt = new StreamReader(csDecrypt);
        
        return srDecrypt.ReadToEnd();;
    }

    private void GenerateKeyIfNull()
    {
        if (_aes.Key.Length != 0 || !string.IsNullOrEmpty(_key)) return;
        _aes.GenerateKey();
        File.WriteAllText("EncryptKey.csv", BitConverter.ToString(_aes.Key));
    }
}


