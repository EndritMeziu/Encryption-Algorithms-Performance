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
    class RC4
    {
        public static double encryptionTime;
        public static double decryptionTime;
        public static double encryptionCpuLoad;
        public static double decryptionCpuLoad;
        public static int size;
        public static byte[] GenerateKey(int size)
        {
            byte[] key = new byte[size/8];
            Random r = new Random();
            for (int i=0;i<size/8;i++)
            {
                
                key[i] = (byte)(r.Next()*25);
                
            }
            return key;
        }
        
        public static void Encrypt()
        {
            byte[] key = GenerateKey(size);
            long startTime = nanoTime();
            byte[] inputBytes = File.ReadAllBytes("textfile.txt");
            byte[] encryptedBytes = EncryptOutput(key, inputBytes).ToArray();



            FileStream fs = new FileStream("encryptedfile.txt", FileMode.Create, FileAccess.Write);
            fs.Write(encryptedBytes, 0, encryptedBytes.Length);
            long endTime = nanoTime();
            encryptionTime = (endTime - startTime) / 10;
            encryptionTime /= 100;
            fs.Close();

        }
        

        public static void Decrypt()
        {
            byte[] key = GenerateKey(size);
            long startTime = nanoTime();
            byte[] inputBytes = File.ReadAllBytes("encryptedfile.txt");
            byte[] decryptedBytes = EncryptOutput(key, inputBytes).ToArray();

            FileStream fs = new FileStream("decryptedfile.txt", FileMode.Create, FileAccess.Write);
            fs.Write(decryptedBytes, 0, decryptedBytes.Length);
            long endTime = nanoTime();
            

            decryptionTime = (endTime - startTime) / 10;
            decryptionTime /= 100;
            fs.Close();

        }

        private static byte[] EncryptInitalize(byte[] key)
        {
            byte[] s = Enumerable.Range(0, 256)
              .Select(i => (byte)i)
              .ToArray();

            for (int i = 0, j = 0; i < 256; i++)
            {
                j = (j + key[i % key.Length] + s[i]) & 255;

                Swap(s, i, j);
            }

            return s;
        }

        private static IEnumerable<byte> EncryptOutput(byte[] key, IEnumerable<byte> data)
        {
            byte[] s = EncryptInitalize(key);

            int i = 0;
            int j = 0;

            return data.Select((b) =>
            {
                i = (i + 1) & 255;
                j = (j + s[i]) & 255;

                Swap(s, i, j);

                return (byte)(b ^ s[(s[i] + s[j]) & 255]);
            });
        }

        private static void Swap(byte[] s, int i, int j)
        {
            byte c = s[i];

            s[i] = s[j];
            s[j] = c;
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
