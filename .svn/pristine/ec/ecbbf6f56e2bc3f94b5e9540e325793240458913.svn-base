using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEO_DROID.Services.SincroService
{
    public class UtilsGeneric
    {
        public static byte[] Chop(byte[] b, int size)
        {
            byte[] caracteres = new byte[size];
            for (int i = 0; i < size; i++)
                caracteres[i] = b[i];
            return caracteres;
        }

        public static CultureInfo GetCultureInfo()
        {
            CultureInfo ci = new CultureInfo("es-ES");
            ci.NumberFormat.CurrencyDecimalSeparator = ",";
            ci.NumberFormat.CurrencyGroupSeparator = ".";
            ci.NumberFormat.CurrencyGroupSizes = new int[1];
            ci.NumberFormat.CurrencyGroupSizes[0] = 3;
            return ci;
        }

        public static bool CreaDirectorioEnRuta(string path)
        {
            if (string.IsNullOrEmpty(path)) return false;

            bool existeUnidad = false, existeDirectorio = false;
            existeDirectorio = Directory.Exists(path);
            if (!existeDirectorio)
            {
                string directoryRoot = Directory.GetDirectoryRoot(path);
                DriveInfo[] listDrives = DriveInfo.GetDrives();
                foreach (DriveInfo di in listDrives)
                {
                    if (di.RootDirectory.Name == directoryRoot)
                    {
                        existeUnidad = true;
                        break;
                    }
                }
                if (existeUnidad)
                {
                    Directory.CreateDirectory(path);
                    existeDirectorio = Directory.Exists(path);
                }
            }
            return existeDirectorio;
        }

        public static byte[] GzipString(string datos)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(datos);
            buffer = GzipBuffer(buffer);
            return buffer;
        }

        public static byte[] GzipBuffer(byte[] buffer)
        {
            byte[] bufferAux = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (GZipStream gStream = new GZipStream(ms, CompressionMode.Compress))
                {
                    gStream.Write(buffer, 0, buffer.Length);
                    gStream.Close();
                }
                bufferAux = ms.ToArray();

                // Eliminamos los 0's del final del buffer (podemos descomprimir sin ellos 
                // y nos fastidian en la comprobación del buffer ya que nos pueden llegar sueltos 
                // después de poder descomprimir sin haber excepción)
                //int numCeros = 0;
                //for (int i = bufferAux.Length - 1; i > 0; i--)
                //{
                //    if (bufferAux[i] == 0)
                //        numCeros++;
                //    else
                //        break;
                //}
                //bufferAux = UtilsGeneric.Chop(bufferAux, bufferAux.Length - numCeros);
                //bufferAux = UtilsGeneric.Chop(bufferAux, bufferAux.Length - 4);
            }
            return bufferAux;
        }

        public static byte[] UnGzipBuffer(byte[] buffer)
        {
            byte[] bufferAux = null;
            using (MemoryStream ms = new MemoryStream(buffer))
            {
                // Calculamos el tamaño del buffer de destino
                int leidosTotal = 0;
                using (GZipStream gStream = new GZipStream(ms, CompressionMode.Decompress, true))
                {
                    int leidos = 0;
                    do
                    {
                        byte[] bufferDesc = new byte[64];
                        leidos = gStream.Read(bufferDesc, 0, bufferDesc.Length);
                        leidosTotal += leidos;
                    }
                    while (leidos != 0);
                }

                // Descomprimimos en el buffer de destino
                if (leidosTotal != 0)
                {
                    ms.Position = 0;
                    bufferAux = new byte[leidosTotal];
                    using (GZipStream gStream = new GZipStream(ms, CompressionMode.Decompress, false))
                    {
                        gStream.Read(bufferAux, 0, bufferAux.Length);
                    }
                }
            }
            return bufferAux;
        }
    }
}
