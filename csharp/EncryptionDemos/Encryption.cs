using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Reflection;

namespace EncryptionDemos
{
    class Encryption
    {
        // This is the base path of this demo, outside both C# and Python areas
        private static string basePath = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(GetWorkingDirectory()).ToString()).ToString()).ToString()).ToString();

        // These are for generating new keys in the "basePath" area
        private static string generatedPrivateKeyPath = Path.Combine(basePath, "PrivateKey.txt");
        private static string generatedPublicKeyPath = Path.Combine(basePath, "PublicKey.txt");

        // This is where the file, encrypted using the Python encryptor, should be
        private static string encryptedStringPath = Path.Combine(basePath, "encrypted.txt");

        // This is the actual private key that will be used for decryption
        // The file itself will NOT be automatically overwritten when generating keys
        private static string privateKeyPath = Path.Combine(GetWorkingDirectory(), "PrivateKey.txt");


        public static string GetWorkingDirectory()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }


        static void DecryptString()
        {
            // Load encrypted string from file
            Console.WriteLine("Loading encrypted string from " + encryptedStringPath + "...");
            string encryptedString = Regex.Replace(System.IO.File.ReadAllText(encryptedStringPath), @"\t|\n|\r", "");

            // Load private key from file
            Console.WriteLine("Loading private key from " + privateKeyPath + "...");
            string privateKeyXML = System.IO.File.ReadAllText(privateKeyPath);
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privateKeyXML);

            // Decrypt string, convert to correct encoding
            Console.WriteLine("Decrypting...");
            byte[] encryptedBytes = Convert.FromBase64String(encryptedString);
            var decryptedBytes = rsa.Decrypt(encryptedBytes, false);
            var decryptedString = Encoding.UTF8.GetString(decryptedBytes);

            Console.Write("Decrypted string: ");
            Console.WriteLine(decryptedString);
        }

        static void GenerateKeys()
        {
            // 1024 length encryption
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1024);

            // Write to files. 
            // This will NOT automatically overwrite the python and C# public/private keys
            Console.WriteLine("Writing private key to " + privateKeyPath + "...");
            System.IO.File.WriteAllText(generatedPrivateKeyPath, rsa.ToXmlString(true));
            Console.WriteLine("Writing public key to " + privateKeyPath + "...");
            System.IO.File.WriteAllText(generatedPublicKeyPath, rsa.ToXmlString(false));
        }

        static void Main(string[] args)
        {
            // Menu
            Console.WriteLine("Please choose from:");
            Console.WriteLine("1. Generate Keys");
            Console.WriteLine("2. Decrypt String");
            Console.Write("Enter your choice: ");

            int input = Console.Read();
            char choice = Convert.ToChar(input);

            switch (choice)
            {
                case '1':
                    GenerateKeys();
                    break;
                case '2':
                    DecryptString();
                    break;
                default:
                    break;
            }

            HoldForExit();
        }

        static void HoldForExit()
        {
            // Hold console open
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
