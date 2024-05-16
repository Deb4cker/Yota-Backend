namespace Yota_backend.Services.Interface;

public interface ICryptographyService
{
    string AesEncrypt(string plaintext);
    string AesDecrypt(string cypherText);
}