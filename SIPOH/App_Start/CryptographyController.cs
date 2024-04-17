using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Security.Cryptography;
using System.Text;


    public class CryptographyController
    {
        private static readonly byte[] Key = new byte[] { 0x54, 0x68, 0x69, 0x73, 0x49, 0x73, 0x41, 0x53, 0x65, 0x63, 0x72, 0x65, 0x74, 0x4B, 0x65, 0x79 };
        private static readonly byte[] IV = new byte[] { 0x54, 0x68, 0x69, 0x73, 0x49, 0x76, 0x65, 0x63, 0x74, 0x6F, 0x72, 0x49, 0x56, 0x3F, 0x3F, 0x3F };

        public static string EncryptString(string plainText)
        {
            using (AesManaged aes = new AesManaged())
            {
                aes.Key = Key;
                aes.IV = IV;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }
                    }
                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }

        public static string DecryptString(string cipherText)
        {
            using (AesManaged aes = new AesManaged())
            {
                aes.Key = Key;
                aes.IV = IV;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }

