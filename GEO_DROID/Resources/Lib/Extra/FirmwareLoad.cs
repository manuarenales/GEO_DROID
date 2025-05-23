using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GEO_DROID.Resources.Lib.Extra
{
    public class FirmwareLoad
    {
        public string filename = string.Empty;
        public string name = string.Empty;

        public uint ModeloHardware { get; private set; }
        public uint Protocolo { get; private set; }
        public uint Version { get; private set; }

        public uint Tramas { get; private set; }
        public uint Size { get; private set; }

        private Dictionary<int, byte[]> BufferTramas = new Dictionary<int, byte[]>();

        public FirmwareLoad(string filename)
        {
            this.filename = filename;
            this.name = Path.GetFileName(filename);
            this.ModeloHardware = 0;
            this.Protocolo = 0;
            this.Version = 0;
            this.Tramas = 0;
            this.Size = 0;
        }

        private uint getIntValue(BinaryReader binaryReader, string key)
        {
            string keyLoaded = binaryReader.ReadString();
            if (keyLoaded.Equals(key))
                return binaryReader.ReadUInt32();
            return 0;
        }

        public bool loadHeader()
        {
            FileStream stream = null;
            BinaryReader binaryReader = null;

            try
            {
                stream = new FileStream(filename, FileMode.Open, FileAccess.Read);
                binaryReader = new BinaryReader(stream);

                ModeloHardware = getIntValue(binaryReader, "H");
                Protocolo = getIntValue(binaryReader, "P");
                Version = getIntValue(binaryReader, "V");
                Tramas = getIntValue(binaryReader, "T");
                Size = getIntValue(binaryReader, "S");

                if (ModeloHardware == 0 || Protocolo == 0 || Version == 0 || Tramas == 0 || Size == 0)
                {
                    // Error de formato
                    return false;
                }

                // Comprobamos el Md5
                string firma = "Tecnausa #H." + ModeloHardware + "#P." + Protocolo + "#V." + Version + "#T." + Tramas + "#S." + Size + "#";
                byte[] md5c = GetMD5HashByteFromString(firma);
                byte[] md5r = binaryReader.ReadBytes(16);

                if (!ArraysEqual<byte>(md5c, md5r))
                {
                    // Error = error.ErrorMd5;
                    return false;
                }
            }
            catch { return false; }
            finally
            {
                if (binaryReader != null)
                    binaryReader.Dispose();
                if (stream != null)
                    stream.Dispose();
            }

            return true;
        }

        public bool loadTramas()
        {
            FileStream stream = null;
            BinaryReader binaryReader = null;

            try
            {
                stream = new FileStream(filename, FileMode.Open, FileAccess.Read);
                binaryReader = new BinaryReader(stream);

                // Saltamos el Header
                getIntValue(binaryReader, "H");
                getIntValue(binaryReader, "P");
                getIntValue(binaryReader, "V");
                getIntValue(binaryReader, "T");
                getIntValue(binaryReader, "S");

                // Saltamos el MD5
                binaryReader.ReadBytes(16);         // md5

                // Listo para leer TRamas
                int ntrama = 0;
                while (stream.Position < stream.Length)
                {
                    byte lenBuffer = binaryReader.ReadByte();
                    byte[] buffer = binaryReader.ReadBytes(lenBuffer);
                    BufferTramas.Add(ntrama++, buffer);
                }
            }
            catch { return false; }
            finally
            {
                binaryReader.Dispose();
                stream.Dispose();
            }

            return true;

        }

        public byte[] get(int id)
        {
            if (BufferTramas.ContainsKey(id))
                return BufferTramas[id];

            return null;
        }

        private static bool ArraysEqual<T>(T[] a1, T[] a2)
        {
            if (ReferenceEquals(a1, a2)) return true;
            if (a1 == null || a2 == null) return false;
            if (a1.Length != a2.Length) return false;

            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            for (int i = 0; i < a1.Length; i++)
            {
                if (!comparer.Equals(a1[i], a2[i])) return false;
            }
            return true;
        }

        private static byte[] GetMD5HashByteFromString(string cadena)
        {
            System.Text.ASCIIEncoding codificador = new System.Text.ASCIIEncoding();
            byte[] data = codificador.GetBytes(cadena);

            //MD5 md5 = new MD5CryptoServiceProvider();
            MD5 md5 = MD5.Create();
            byte[] retVal = md5.ComputeHash(data);
            return retVal;
        }


    }
}
