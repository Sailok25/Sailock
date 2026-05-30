using System;
using System.IO;
using System.Text.Json;
using Sailock.Models;

namespace Sailock.Services
{
    public class StorageService
    {
        private readonly CryptoService _crypto = new CryptoService();

        private static readonly string AppFolder =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Sailock");

        private static readonly string FilePath =
            Path.Combine(AppFolder, "data.slock");

        /// <summary>
        /// True si ya existe un archivo .slock (usuario registrado).
        /// </summary>
        public bool DataFileExists() => File.Exists(FilePath);

        /// <summary>
        /// Intenta cargar y descifrar los datos.
        /// Devuelve null si la contraseña es incorrecta o el archivo está corrupto.
        /// </summary>
        public AppData Load(string password)
        {
            if (!DataFileExists())
                return new AppData();

            try
            {
                string cipherText = File.ReadAllText(FilePath);
                string json = _crypto.Decrypt(cipherText, password);
                return JsonSerializer.Deserialize<AppData>(json) ?? new AppData();
            }
            catch
            {
                // Contraseña incorrecta o archivo corrupto
                return null;
            }
        }

        /// <summary>
        /// Cifra y guarda los datos en disco.
        /// </summary>
        public void Save(AppData data, string password)
        {
            Directory.CreateDirectory(AppFolder);

            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            string cipherText = _crypto.Encrypt(json, password);

            File.WriteAllText(FilePath, cipherText);
        }

        /// <summary>
        /// Importa un archivo .slock externo usando la contraseña actual.
        /// Devuelve null si la contraseña no coincide.
        /// </summary>
        public AppData Import(string filePath, string password)
        {
            try
            {
                string cipherText = File.ReadAllText(filePath);
                string json = _crypto.Decrypt(cipherText, password);
                return JsonSerializer.Deserialize<AppData>(json) ?? new AppData();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Exporta los datos actuales a la ruta indicada.
        /// </summary>
        public void Export(AppData data, string filePath, string password)
        {
            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            string cipherText = _crypto.Encrypt(json, password);
            File.WriteAllText(filePath, cipherText);
        }

        /// <summary>
        /// Elimina el archivo .slock del disco.
        /// </summary>
        public void DeleteAll()
        {
            if (File.Exists(FilePath))
                File.Delete(FilePath);
        }
    }
}