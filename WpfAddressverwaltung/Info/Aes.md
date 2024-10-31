# Einführung in AES (Advanced Encryption Standard)

AES, kurz für Advanced Encryption Standard, ist ein weit verbreitetes und sicheres Verschlüsselungsverfahren, das zur
Verschlüsselung und zum Schutz von Daten verwendet wird. AES ist der weltweit am häufigsten verwendete
Verschlüsselungsalgorithmus und wird z. B. von Regierungen und in kommerziellen Anwendungen eingesetzt.

## Merkmale von AES

1. **Symmetrische Verschlüsselung:** AES ist ein symmetrischer Verschlüsselungsalgorithmus. Das bedeutet, dass derselbe
   Schlüssel sowohl für die Verschlüsselung als auch für die Entschlüsselung der Daten verwendet wird. Dadurch ist AES
   schneller als asymmetrische Verschlüsselungsverfahren.
2. **Blockverschlüsselung:** AES verarbeitet Daten in Blöcken (16 Byte oder 128 Bit) und verschlüsselt diese Blöcke
   separat.
3. **Schlüssellänge:** AES unterstützt drei verschiedene Schlüssellängen, was unterschiedliche Sicherheitsstufen bietet:
    - **AES-128:** 128-Bit-Schlüssel
    - **AES-192:** 192-Bit-Schlüssel
    - **AES-256:** 256-Bit-Schlüssel (meistens die sicherste Wahl)
4. **Sicherheit:** AES ist als sicher anerkannt und wird weltweit verwendet. Ein gut geschützter Schlüssel und eine
   ordnungsgemäße Implementierung bieten starke Sicherheit gegen die meisten Angriffe.

## Wie AES funktioniert

AES arbeitet in mehreren Runden, die abhängig von der Schlüssellänge variieren (10 Runden für AES-128, 12 für AES-192,
14 für AES-256). Jede Runde besteht aus den folgenden Schritten:

1. **SubBytes:** Ersetzt jedes Byte im Block durch ein anderes Byte, um die Daten zu verschleiern.
2. **ShiftRows:** Verschiebt die Zeilen des Blocks, um die Position der Daten zu ändern.
3. **MixColumns:** Mischt die Spalten des Blocks, um die Daten weiter zu verwürfeln.
4. **AddRoundKey:** Kombiniert den Block mit dem Schlüssel, was eine exklusive Oder-Operation (XOR) auf den Datenblock
   und den Rundenschlüssel anwendet.
   Durch diese Schritte wird der ursprüngliche Klartext in verschlüsselten Text umgewandelt, der nur mit dem richtigen
   Schlüssel entschlüsselt werden kann.

## Anwendung von AES

Ein gängiges Anwendungsszenario für AES ist die Verschlüsselung von Dateien oder sensiblen Daten in einer Datenbank. Um
Daten sicher zu verschlüsseln, benötigt AES zwei Dinge:

- **Schlüssel:** Ein geheimes Passwort, das aus 16, 24 oder 32 Byte bestehen kann (für 128, 192 oder 256 Bit).
- **Initialisierungsvektor (IV):** Ein zusätzlicher Wert, der die Verschlüsselung für gleiche Datenblöcke
  unterschiedlich gestaltet, was bei der Sicherheit hilft.

## Beispiel in C#

Hier ist ein einfaches Beispiel, wie AES in C# verwendet wird, um einen Text zu verschlüsseln und wieder zu
entschlüsseln:

```csharp
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Provides a simple example of how to use the Advanced Encryption Standard (AES) for encryption and decryption.
/// </summary>
public static class AesExample
{
    // The secret key used for encryption and decryption. This should be a 32-byte long key for AES-256 encryption.
    private static readonly byte[] key = Encoding.UTF8.GetBytes("your-32-byte-long-key-for-aes-encrypt");

    // The initialization vector (IV) used for encryption and decryption. This should be a 16-byte long IV.
    private static readonly byte[] iv = Encoding.UTF8.GetBytes("your-16-byte-init-vector");

    /// <summary>
    /// Encrypts the given plain text using the AES algorithm.
    /// </summary>
    /// <param name="plainText">The text to be encrypted.</param>
    /// <returns>The encrypted text as a byte array.</returns>
    public static byte[] Encrypt(string plainText)
    {
        // Create a new instance of the Aes class.
        using (Aes aes = Aes.Create())
        {
            // Set the key and IV for the Aes instance.
            aes.Key = key;
            aes.IV = iv;

            // Create an encryptor using the key and IV.
            using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
            {
                // Create a memory stream to store the encrypted data.
                using (var msEncrypt = new MemoryStream())
                {
                    // Create a crypto stream to perform the encryption.
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        // Create a stream writer to write the plain text to the crypto stream.
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            // Write the plain text to the crypto stream.
                            swEncrypt.Write(plainText);

                            // Return the encrypted data as a byte array.
                            return msEncrypt.ToArray();
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Decrypts the given cipher text using the AES algorithm.
    /// </summary>
    /// <param name="cipherText">The text to be decrypted.</param>
    /// <returns>The decrypted text as a string.</returns>
    public static string Decrypt(byte[] cipherText)
    {
        // Create a new instance of the Aes class.
        using (Aes aes = Aes.Create())
        {
            // Set the key and IV for the Aes instance.
            aes.Key = key;
            aes.IV = iv;

            // Create a decryptor using the key and IV.
            using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
            {
                // Create a memory stream to store the decrypted data.
                using (var msDecrypt = new MemoryStream(cipherText))
                {
                    // Create a crypto stream to perform the decryption.
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        // Create a stream reader to read the decrypted data from the crypto stream.
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Return the decrypted data as a string.
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
```

## Sicherheitstipps

- **Schlüssel und IV sicher speichern:** Halten Sie den AES-Schlüssel und IV geheim und speichern Sie sie sicher.
- **Verwendung von AES-256:** Wenn möglich, verwenden Sie die AES-256-Verschlüsselung für erhöhte Sicherheit.
- **Nicht denselben IV wiederverwenden:** Verwenden Sie für jede Verschlüsselungssitzung einen neuen IV, um die
  Sicherheit zu maximieren.

AES bietet starke und effiziente Verschlüsselung, solange der Schlüssel und die Implementierung sicher sind. Es bleibt
eine der besten Optionen zur Sicherung sensibler Daten.