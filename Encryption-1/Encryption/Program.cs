 using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Security;
using System.Security.Cryptography;
using System.Threading;

namespace Encryption
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath, sKey, sInputFile, sOutputFile;
            int inputFileSize;
            
            Console.WriteLine("Passphrase: ");
            sKey = Console.ReadLine();

            Console.WriteLine("Please carefully enter file name you want to encrypt: ");
            sInputFile = Console.ReadLine();

            inputFileSize = sInputFile.Length;

            sOutputFile = sInputFile.Substring(0,inputFileSize-4)+"Encrypt";

            EncryptFile(sInputFile, "tushar.txt", sKey);
            Console.WriteLine("Your file has been sucessfully encrypted.");
            //DecryptFile(sOutputFile, "decrypted.txt", sKey);
        }

        //--------------- Encryption ----------------//
        static void EncryptFile(string sInputFile, string sOutputFile, string sKey)
        {
            FileStream fileInput = new FileStream(sInputFile, FileMode.Open, FileAccess.Read);
            FileStream fileEncrypted = new FileStream(sOutputFile, FileMode.Create, FileAccess.Write);
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            try
            {
                DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            }
            catch (Exception)
            {
                Console.WriteLine("Error! Length of the key must be 8 char.");
                Console.WriteLine("Encryption Unsuccessfull.");
                System.Environment.Exit(0);
       
            }            
            ICryptoTransform desencrypt = DES.CreateEncryptor();
            CryptoStream cryptostream = new CryptoStream(fileEncrypted, desencrypt, CryptoStreamMode.Write);
            byte[] byteOutput = new byte[fileInput.Length - 1];
            fileInput.Read(byteOutput,0, byteOutput.Length);
            cryptostream.Write(byteOutput, 0, byteOutput.Length);
            fileEncrypted.Close();
            fileInput.Close();
            Console.WriteLine("Please wait while your file is being encrypted...");
            Thread.Sleep(5000);
        }
        //--------------- Decryption ---------------//
        static void DecryptFile(string sInputFile, string sOutputFile, string sKey)
        {
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);

            FileStream eFileInput = new FileStream(sInputFile, FileMode.Open, FileAccess.Read);
            FileStream fileDecrypted = new FileStream(sOutputFile, FileMode.Create, FileAccess.Write); 

            ICryptoTransform desdecrypt = DES.CreateDecryptor();
            CryptoStream cryptostreamDecrypt = new CryptoStream(fileDecrypted, desdecrypt, CryptoStreamMode.Write);
            byte[] byteOutput = new byte[eFileInput.Length - 1];
            eFileInput.Read(byteOutput, 0, byteOutput.Length);
            cryptostreamDecrypt.Write(byteOutput, 0, byteOutput.Length);

        }
    }
}
