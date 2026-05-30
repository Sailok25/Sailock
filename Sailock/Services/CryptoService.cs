using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Sailock.Services
{
    public class CryptoService
    {
        // Iteraciones PBKDF2 — coste computacional deliberado
        private const int Iterations = 100_000;
        private const int KeySize = 32; // 256 bits
        private const int SaltSize = 16; // 128 bits
        private const int IVSize = 16; // 128 bits

        /// <summary>
        /// Cifra texto plano con AES-256.
        /// Devuelve: salt (16) + IV (16) + ciphertext, todo en Base64.
        /// </summary>
        public string Encrypt(string plainText, string password)
        {
            // Generar salt e IV aleatorios en cada cifrado
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] iv = RandomNumberGenerator.GetBytes(IVSize);
            byte[] key = DeriveKey(password, salt);

            using var aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using var encryptor = aes.CreateEncryptor();
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] cipherBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

            // Concatenar: salt + IV + ciphertext
            byte[] result = new byte[SaltSize + IVSize + cipherBytes.Length];
            Buffer.BlockCopy(salt, 0, result, 0, SaltSize);
            Buffer.BlockCopy(iv, 0, result, SaltSize, IVSize);
            Buffer.BlockCopy(cipherBytes, 0, result, SaltSize + IVSize, cipherBytes.Length);

            return Convert.ToBase64String(result);
        }

        /// <summary>
        /// Descifra texto cifrado por Encrypt().
        /// Lanza CryptographicException si la contraseña es incorrecta.
        /// </summary>
        public string Decrypt(string cipherBase64, string password)
        {
            byte[] data = Convert.FromBase64String(cipherBase64);

            // Extraer salt, IV y ciphertext
            byte[] salt = new byte[SaltSize];
            byte[] iv = new byte[IVSize];
            byte[] cipherBytes = new byte[data.Length - SaltSize - IVSize];

            Buffer.BlockCopy(data, 0, salt, 0, SaltSize);
            Buffer.BlockCopy(data, SaltSize, iv, 0, IVSize);
            Buffer.BlockCopy(data, SaltSize + IVSize, cipherBytes, 0, cipherBytes.Length);

            byte[] key = DeriveKey(password, salt);

            using var aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using var decryptor = aes.CreateDecryptor();
            byte[] plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

            return Encoding.UTF8.GetString(plainBytes);
        }

        private byte[] DeriveKey(string password, byte[] salt)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(
                password,
                salt,
                Iterations,
                HashAlgorithmName.SHA256);

            return pbkdf2.GetBytes(KeySize);
        }
    }
}