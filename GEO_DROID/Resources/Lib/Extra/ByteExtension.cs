using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEO_DROID.Resources.Lib.Extra
{
    public static class ByteExtension
    {

        public static byte setByte(int value1, int bits1, int value2, int bits2)
        {
            // 1 corregimos que cada valor no se pueda pasar de su tamaño de bits
            value1 = value1 % (int)Math.Pow(2, (double)bits1);
            value2 = value2 % (int)Math.Pow(2, (double)bits2);

            // 2 A multiplicar y sumar
            return (byte)((value2 << bits1) + value1);
        }

        public static int getValue(this byte value, int startPosition, int bits)
        {
            int andOperation = 0;

            switch (bits)
            {
                case 1: andOperation = 0x01; break;
                case 2: andOperation = 0x03; break;
                case 3: andOperation = 0x07; break;
                case 4: andOperation = 0x0F; break;
                case 5: andOperation = 0x1F; break;
                case 6: andOperation = 0x3F; break;
                case 7: andOperation = 0x7F; break;
                case 8: andOperation = 0xFF; break;
            }
            return (value >> startPosition) & andOperation;
        }


        public static byte[] getBytes(this string cadena)
        {
            return Encoding.UTF8.GetBytes(cadena);
        }

        public static string getHexString(this byte[] buffer, int size = 0)
        {
            if (size == 0)
                size = buffer.Length;

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < size; i++)
            {
                sb.Append(buffer[i].ToString("X2"));
                sb.Append(" ");
            }

            return sb.ToString();
        }





    }
}
