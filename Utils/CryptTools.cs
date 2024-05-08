using System.Security.Cryptography;
using System.Text;

namespace Yota_backend.Utils;

public class CryptTools
{
    public static async Task<string> FromFileToStringByBytes(IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        byte[] bytes = memoryStream.ToArray();
        return Encoding.UTF8.GetString(bytes).ToString();
    }

    public static string AESEncrypt(string plainText)
    {
        if (string.IsNullOrEmpty(plainText))
            throw new ArgumentNullException("plainText");

        byte[] encrypted;
        using (Aes aesAlg = Aes.Create())
        {

            aesAlg.KeySize = 256;
            aesAlg.IV = new byte[16];
            aesAlg.Mode = CipherMode.CBC;
            aesAlg.Padding = PaddingMode.PKCS7;

            aesAlg.GenerateKey();

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }

            File.WriteAllText("EncryptKey.csv", BitConverter.ToString(aesAlg.Key).Replace("-", ""));
        }
        return Convert.ToBase64String(encrypted);
    }

    public static string AESDecrypt(string cipherText)
    {
        var key = File.ReadAllText("EncryptKey.csv");

        if (string.IsNullOrEmpty(cipherText))
            throw new ArgumentNullException("cipherText");
        if (string.IsNullOrEmpty(key))
            throw new ArgumentNullException("key");

        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        string plaintext = null;

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(key);
            aesAlg.IV = new byte[16];
            aesAlg.Mode = CipherMode.CBC;
            aesAlg.Padding = PaddingMode.PKCS7;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msDecrypt = new MemoryStream(cipherBytes))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
        }
        return plaintext;
    }
}
