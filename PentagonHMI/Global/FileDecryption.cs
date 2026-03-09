using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PentagonHMI.Global
{
    public class FileDecryption
    {
        public static string DecryptFile(string inputFile, string password)
        {
            string decryptedText = string.Empty;
            try
            {
                using (Aes aesAlg = Aes.Create())
                {
                    using (FileStream fsInput = new FileStream(inputFile, FileMode.Open))
                    {
                        // Read salt from the beginning of the file
                        byte[] salt = new byte[16];
                        fsInput.Read(salt, 0, salt.Length);

                        // Derive key and IV using the same password and salt
                        Rfc2898DeriveBytes keyDerivation = new Rfc2898DeriveBytes(password, salt, 10000);
                        aesAlg.Key = keyDerivation.GetBytes(32);
                        aesAlg.IV = keyDerivation.GetBytes(16);

                        //using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create))
                        using (CryptoStream cs = new CryptoStream(fsInput, aesAlg.CreateDecryptor(), CryptoStreamMode.Read))
                        {
                            //cs.CopyTo(fsOutput);
                            using (StreamReader srDecrypt = new StreamReader(cs))
                            {
                                decryptedText = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
                return decryptedText;
            }
            catch (Exception ex)
            {
                FileLogger.logError("[FileDecryption]", ex.Message.ToString());
                return decryptedText;
            }
        }
    }
}
