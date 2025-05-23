using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEO_DROID.Resources.Lib.Extra
{
    public class CryptoTn4
    {
        private byte[] TN4Key = new byte[256];
        private byte indexI, indexJ;

        private Random rnd;
        private Semaphore sem;

        public CryptoTn4()
        {
            rnd = new Random(Environment.TickCount);
            sem = new Semaphore(1, 1);
            Init("CryptoTn4");
        }

        public void WaitOne() { sem.WaitOne(); }
        public void Release() { sem.Release(); }

        public byte randomByte
        {
            get { return (byte)rnd.Next(0xFF); }
        }

        public void Init(string key)
        {
            byte[] keyBytes = Encoding.ASCII.GetBytes(key);
            Init(keyBytes);
        }

        public void Init(byte[] key)
        {
            byte j = 0, h = 0;
            byte t;

            for (int i = 0; i < 256; i++)
                TN4Key[i] = (byte)i;

            for (int i = 0; i < 256; i++)
            {
                j = (byte)((j - (key[h] + TN4Key[i])) & 255);
                t = TN4Key[i];
                if (++h >= key.Length)
                    h = 0;
                TN4Key[i] = TN4Key[j];
                TN4Key[j] = t;
            }

            Reset();
        }

        public void Reset()
        {
            indexI = 0;
            indexJ = 0;
        }

        public byte Crypt(byte x)
        {
            indexI++;
            byte h = (byte)(x ^ TN4Key[(TN4Key[indexI] - TN4Key[indexJ]) & 255]);
            indexJ -= TN4Key[x];
            return h;
        }

        public byte Decrypt(byte x)
        {
            indexI++;
            byte h = (byte)(x ^ TN4Key[(TN4Key[indexI] - TN4Key[indexJ]) & 255]);
            indexJ -= TN4Key[h];
            return h;
        }

    }
}
