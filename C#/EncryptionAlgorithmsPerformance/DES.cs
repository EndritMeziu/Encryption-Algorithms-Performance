using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionAlgorithmsPerformance
{
    class DES
    {
        public static DESCryptoServiceProvider objDes = new DESCryptoServiceProvider();
        public static double encryptionTime;
        public static double decryptionTime;
        public static double encryptionCpuLoad;
        public static double decryptionCpuLoad;

        DES()
        {

        }



        public static void encrypt()
        {


           
            ICryptoTransform encryptor = objDes.CreateEncryptor();
            objDes.Padding = PaddingMode.Zeros;
            objDes.Mode = CipherMode.ECB;


            byte[] inputBytes = File.ReadAllBytes("textfile.txt");
            long startTime = nanoTime();
            byte[] encryptedBytes = encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);


            long endTime = nanoTime();
            FileStream fs = new FileStream("encryptedfile.txt", FileMode.Create, FileAccess.Write);
            fs.Write(encryptedBytes, 0, encryptedBytes.Length);
           
            encryptionTime = (endTime - startTime) / 10;
            encryptionTime /= 100;
            fs.Close();




        }

        public static void decrypt()
        {


            
            ICryptoTransform decryptor = objDes.CreateDecryptor();
            objDes.Padding = PaddingMode.Zeros;
            objDes.Mode = CipherMode.ECB;



            byte[] inputBytes = File.ReadAllBytes("encryptedfile.txt");
            long startTime = nanoTime();
            byte[] decryptedBytes = decryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);


            long endTime = nanoTime();
            FileStream fs = new FileStream("decryptedfile.txt", FileMode.Create, FileAccess.Write);
            fs.Write(decryptedBytes, 0, decryptedBytes.Length);
            

            decryptionTime = (endTime - startTime) / 10;
            decryptionTime /= 100;
            fs.Close();



        }
        public static long nanoTime()
        {
            long nano = 10000L * Stopwatch.GetTimestamp();
            nano /= TimeSpan.TicksPerMillisecond;
            nano *= 100L;
            return nano;
        }

    }
}
