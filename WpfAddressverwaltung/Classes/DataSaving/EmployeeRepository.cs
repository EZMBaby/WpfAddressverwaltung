using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using WpfAddressverwaltung.Classes.EmployeeData;

namespace WpfAddressverwaltung.Classes.DataSaving
{
    /// <summary>
    /// Provides methods for saving and loading employee data from a file.
    /// </summary>
    public class EmployeeRepository
    {
        /// <summary>
        /// The file path where employee data is stored.
        /// </summary>
        private static readonly string FilePath = Path.Combine(GetProjectRoot(), "Data", "employees.json");

        /// <summary>
        /// Gets the project root directory.
        /// </summary>
        /// <returns>The project root directory.</returns>
        private static string GetProjectRoot()
        {
            // Get the base directory (e.g. /bin/Debug/net6.0 or /bin/Release/net6.0)
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;

            // Go up three levels to reach the project root
            return Directory.GetParent(baseDir)!.Parent!.Parent!.Parent!.FullName;
        }

        /// <summary>
        /// Loads employee data from the file.
        /// </summary>
        /// <returns>An ObservableCollection of Employee objects.</returns>
        public static async Task<ObservableCollection<Employee>> LoadEmployeesAsync()
        {
            // Check if the file exists
            if (!File.Exists(FilePath))
            {
                // Return an empty collection if the file does not exist
                return [];
            }

            // Read the encrypted file contents
            byte[] encryptedData = await File.ReadAllBytesAsync(FilePath);

            // Decrypt the data and convert it to a JSON string
            string decryptedJson = AesEncrypting.DecryptStringFromBytes_Aes(encryptedData, AesEncrypting.Key, AesEncrypting.Iv);

            // Deserialize the JSON string into an ObservableCollection of Employee objects
            ObservableCollection<Employee>? employees = JsonSerializer.Deserialize<ObservableCollection<Employee>>(decryptedJson);

            // Return the deserialized collection, or an empty collection if deserialization fails
            return employees ?? [];
        }

        /// <summary>
        /// Saves employee data to the file.
        /// </summary>
        /// <param name="employees">The ObservableCollection of Employee objects to save.</param>
        public static async Task SaveEmployeesAsync(ObservableCollection<Employee> employees)
        {
            // Create a JsonSerializerOptions instance to serialize the data with indentation
            JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };

            // Serialize the employee data to a JSON string
            string json = JsonSerializer.Serialize(employees, options);

            // Encrypt the JSON string using AES encryption
            byte[] encryptedData = AesEncrypting.EncryptStringToBytes_Aes(json, AesEncrypting.Key, AesEncrypting.Iv);

            // Ensure the "Data" directory exists
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath) ?? string.Empty);

            // Write the encrypted data to the file
            await File.WriteAllBytesAsync(FilePath, encryptedData);
        }
    }
}