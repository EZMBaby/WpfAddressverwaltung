using System.IO;
using System.Security.Cryptography;

namespace WpfAddressverwaltung.Classes.DataSaving;

/// <summary>
/// Provides methods for encrypting and decrypting strings using the Advanced Encryption Standard (AES) algorithm.
/// </summary>
public class AesEncrypting
{
    // Note: In a real-world application, these keys should not be hardcoded for security reasons...but who cares rn :D 
    /// <summary>
    /// The encryption key used for AES encryption.
    /// </summary>
    public static byte[] Key { get; } = "bR4K5iDhW3rStP4qL9nVf8XgQ1uZc5Yo"u8.ToArray();

    /// <summary>
    /// The initialization vector (IV) used for AES encryption.
    /// </summary>
    public static byte[] Iv { get; } = "N2r6j8PqX1u5Y8wL"u8.ToArray();

    /// <summary>
    /// Encrypts a string using the AES algorithm.
    /// </summary>
    /// <param name="plainText">The string to be encrypted.</param>
    /// <param name="key">The encryption key.</param>
    /// <param name="iv">The initialization vector.</param>
    /// <returns>The encrypted byte array.</returns>
    public static byte[] EncryptStringToBytes_Aes(string plainText, byte[] key, byte[] iv) {
        // Create an instance of the AES algorithm.
        using Aes aesAlg = Aes.Create();
        
        // Set the encryption key and IV.
        aesAlg.Key = key;
        aesAlg.IV  = iv;

        // Create an encryptor object.
        ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        // Create a memory stream to store the encrypted data.
        using MemoryStream msEncrypt = new MemoryStream();
        
        // Create a crypto stream to perform the encryption.
        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        {
            // Create a stream writer to write the plain text to the crypto stream.
            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
            {
                // Write the plain text to the crypto stream.
                swEncrypt.Write(plainText);
            }
        }

        // Return the encrypted byte array.
        return msEncrypt.ToArray();
    }

    /// <summary>
    /// Decrypts a byte array using the AES algorithm.
    /// </summary>
    /// <param name="cipherText">The byte array to be decrypted.</param>
    /// <param name="key">The encryption key.</param>
    /// <param name="iv">The initialization vector.</param>
    /// <returns>The decrypted string.</returns>
    public static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] key, byte[] iv)
    {
        // Create an instance of the AES algorithm.
        using Aes aesAlg = Aes.Create();

        // Set the encryption key and IV.
        aesAlg.Key = key;
        aesAlg.IV = iv;

        // Create a decryptor object.
        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        // Create a memory stream to store the decrypted data.
        using MemoryStream msDecrypt = new MemoryStream(cipherText);

        // Create a crypto stream to perform the decryption.
        using CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);

        // Create a stream reader to read the decrypted data.
        using StreamReader srDecrypt = new StreamReader(csDecrypt);

        // Return the decrypted string.
        return srDecrypt.ReadToEnd();
    }
}