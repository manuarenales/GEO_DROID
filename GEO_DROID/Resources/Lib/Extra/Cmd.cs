using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEO_DROID.Resources.Lib.Extra
{
    public class Cmd
    {
        public static class Configuration
        {
            public static int sizePacketCtor = 16;      // Tamaño en bytes con el que paquete empieza
        }

        public static DateTime date1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local);

        private byte[] buffer;              // Buffer de datos (completo)
        private int bufferPointer = 0;      // Puntero de escritura


        #region Constructores

        public static Cmd F8(byte idComando, byte idSubComando, int idPaqueteToResponse = 0)
        {
            return new Cmd(0xf8, idComando, idSubComando, idPaqueteToResponse);
        }
        public static Cmd F9(byte idComando, byte idSubComando, int idPaqueteToResponse = 0)
        {
            return new Cmd(0xf9, idComando, idSubComando, idPaqueteToResponse);
        }
        public static Cmd FA(byte idComando, byte idSubComando, int idPaqueteToResponse = 0)
        {
            return new Cmd(0xfA, idComando, idSubComando, idPaqueteToResponse);
        }
        public static Cmd FA(byte idComando, byte idSubComando, Cmd comandoToResponse)
        {
            return new Cmd(0xfA, idComando, idSubComando, comandoToResponse.idPaquete);
        }


        #endregion

        public Cmd(byte byteHeader, byte idComando, byte idSubComando, int idPaqueteToResponse = 0)
        {
            // 1 byte   Header 
            // 2 bytes  Crc16       | Se llena al final
            // 1 byte   Paquete     | Se llena al final
            // 1 byte   Comando
            // 1 byte   DataSize
            // [DATA]
            SizeHeader = 6;

            // Empezamos con un buffer de 16 bytes
            BufferResizeIn(Configuration.sizePacketCtor);

            this.Header = byteHeader;             // Header
            this.F9CommandRetry = 15000;                  // 15 segundos
            this.F9CommandTimeout = 300000;                 // 5 minutos

            this.Crc = 0;                           // CRC
            this.idPaquete = (byte)idPaqueteToResponse;   // Id paquete
            this.ComandoByte = 0;
            this.Comando = idComando;                   // Comando 
            this.SubComando = idSubComando;                // SubComando
            this.SizeData = 0;                           // Tamaño de datos

            // Posicion de escritura en el buffer
            seek(0);

        }

        public Cmd(byte[] inputBuffer, byte inputSize)
        {
            SizeHeader = 6;

            // preparamos un buffer tan largo como el inputsize
            BufferResizeIn(inputSize);

            // copiamos los datos
            Array.Copy(inputBuffer, buffer, inputSize);

            bufferPointer = inputSize;

            seek(0);
        }

        #region IS -> Para comparar rapidamente el tipo de comando

        public bool IS(byte header, int comando, int subcomando)
        {
            return this.Header == header && IS(comando, subcomando);
        }

        public bool IS(int comando, int subcomando)
        {
            return (this.Comando == comando && this.SubComando == subcomando);
        }

        public bool ISResponseTo(int comando, int subcomando)
        {
            Cmd CmdSrc = response;
            if (CmdSrc != null && CmdSrc.IS(comando, subcomando))
                return true;
            return false;
        }

        //
        // Este metodo se usa para detectar un FA de un comando concreto (ver la capa HardwareConnect)
        //
        public bool IS(byte header, int comando, int subcomando, int comandoSrc, int subcomandoSrc)
        {
            return IS(header, comando, subcomando) && ISResponseTo(comandoSrc, subcomandoSrc);
        }

        public bool IS(int comando, int subcomando, int comandoSrc, int subcomandoSrc)
        {
            return IS(comando, subcomando) && ISResponseTo(comandoSrc, subcomandoSrc);
        }

        #endregion

        public long tickCreacion, tickResponse, tickEnvio;
        public int F9CommandRetry;
        public int F9CommandTimeout;
        public int F9CommandTimeBack = -1;       // Este time sirve para añadir un tiempo de retardo a los comandos (lo metemos en la pila, pero se debe lanzar dentro de x segundos)

        public bool readyForSend = true;
        public bool deleteFromOutput = false;


        // Cuando un comando es FA, aqui se mete el comando al que se ha respondido
        // es decir EL PADRE, no la respuesta
        public Cmd response;



        #region Header Read/Writers

        public byte SizeHeader { get; private set; }
        public int Size
        {
            get { return (int)bufferPointer; }
        }

        public byte Header
        {
            get { return buffer[0]; }
            private set { buffer[0] = value; }
        }

        public int Crc
        {
            get { return buffer[2] * 256 + buffer[1]; }
            private set
            {
                buffer[1] = (byte)(value & 0x000000FF);
                buffer[2] = (byte)((value & 0x0000FF00) >> 8);
            }
        }

        public byte idPaquete
        {
            get { return buffer[3]; }
            set { buffer[3] = (byte)value; }
        }

        public byte ComandoByte
        {
            get { return buffer[4]; }
            private set { buffer[4] = value; }
        }

        public int Comando
        {
            get
            {
                return ComandoByte.getValue(4, 4);  //0000xxxx
            }
            private set
            {
                ComandoByte = ByteExtension.setByte(SubComando, 4, value, 4);
            }
        }

        public int SubComando
        {
            get
            {
                return ComandoByte.getValue(0, 4);  // xxxx0000
            }
            private set
            {
                ComandoByte = ByteExtension.setByte(value, 4, Comando, 4);
            }
        }

        public int SizeData
        {
            get { return buffer[5]; }
            set { buffer[5] = (byte)value; }
        }

        /*
        public DateTime TimeStamp
        {
            get {
                if (Configuration.useTimeStamp)
                {
                    uint totalSeconds = BitConverter.ToUInt32(buffer, 6);
                    // TODO comvertir esto a fecha normal
                    return DateTime.Now;
                }
                else
                {
                    return DateTime.Now;
                }
            }
            set {
                if(Configuration.useTimeStamp)
                {
                    uint current = (uint)value.Subtract(date1970).TotalSeconds;
                    byte[] tmp = BitConverter.GetBytes(current);
                    buffer[6] = tmp[0];
                    buffer[7] = tmp[1];
                    buffer[8] = tmp[2];
                    buffer[9] = tmp[3];
                }
            }
        }
        public string TimeStampX
        {
            get {
                if (Configuration.useTimeStamp)
                {
                    string s = string.Empty;
                    for (int i = 0; i < 4; i++)
                        s += string.Format("{0:X2}", buffer[9 - i]);
                    return s;
                }
                else
                {
                    return "0x00";
                }
            }
        }
        */

        #region Pack
        bool comandoPacked = false;
        public void pack()
        {
            this.SizeData = bufferPointer - SizeHeader;        // Ponemos el parametro SIZE en su posicion
            this.Crc = 0;                                       // Ponemos el CRC a 0

            // Calculamos el CRC y lo ponemos en su sitio
            this.Crc = MicroChipCrc16.Compute(buffer, bufferPointer);

            comandoPacked = true;
        }


        #endregion

        #endregion

        #region Gestion del tamaño buffer

        //
        // Cuando creamos un comando iniciamos con un buffer de 16bytes
        // cada vez que hacemos una escritura de datos comprobamos si caben los datos y si nos falta espacio añadimos 8 bytes al tamaño del buffer
        // la unica excepcion es cuando añadimos un String o un byte[] que añadimos al buffer mas bytes en funcion de lo que quepa (pero siempre en mod8)
        //
        private void BufferResizeIn(int size)
        {
            // para comodidad de memoria hacemos el size modulo de 8
            if (size % 8 > 0)
                size += (8 - (size % 8));

            // Redimensionamos el array dando el nuevo tamaño
            Array.Resize(ref buffer, BufferSize + size);
        }

        private int BufferSize
        {
            get { return (buffer != null) ? buffer.Length : 0; }
        }

        private int BufferFreeSpace
        {
            get { return BufferSize - bufferPointer; }
        }

        private void BufferCheckSize(int size)
        {
            if (BufferFreeSpace < size)         // Si el espacio libre es menor que los bytes que queremos meter
                BufferResizeIn(size);           // entomces hacemos que crezca en esos bytes
        }


        // Devuelve una COPIA del buffer
        public byte[] getBuffer()
        {
            byte[] copyBuffer = new byte[SizeHeader + SizeData];
            Array.Copy(buffer, copyBuffer, copyBuffer.Length);
            return copyBuffer;
        }

        public byte[] getBufferCifrado()
        {
            byte[] copyBuffer = new byte[bufferCifrado.Length];
            Array.Copy(bufferCifrado, copyBuffer, copyBuffer.Length);
            return copyBuffer;
        }


        #endregion

        #region Writers

        public void addByte(byte value)
        {
            BufferCheckSize(1);     // Cabe el Byte ?
            buffer[bufferPointer] = value;
            bufferPointer++;
        }
        public void addByte(int value)
        {
            addByte((byte)value);
        }

        public void addByte(string value)
        {
            byte[] bytesCadena = Encoding.ASCII.GetBytes(value);
            addByte(bytesCadena);
        }

        public void addByte(byte[] value)
        {
            BufferCheckSize(value.Length);     // Cabe el Byte[]
            Array.Copy(value, 0, buffer, bufferPointer, value.Length);
            bufferPointer += value.Length;
        }

        public void addBool(bool value)
        {
            addByte((value) ? 1 : 0);
        }



        public void addInt16(int value) { addByte(BitConverter.GetBytes((short)value)); }
        public void addInt32(int value) { addByte(BitConverter.GetBytes(value)); }
        public void addUInt32(uint value) { addByte(BitConverter.GetBytes(value)); }
        public void addFloat(float v) { addByte(BitConverter.GetBytes(v)); }
        public void addDouble(double v) { addByte(BitConverter.GetBytes(v)); }

        public void addString(string cadena)
        {
            byte[] bytesCadena = Encoding.ASCII.GetBytes(cadena);

            // Si el string cabe en 0xff se pone el tamaño con 1 byte
            if (bytesCadena.Length < 0xff)
            {
                addByte(bytesCadena.Length);    // 1 byte para indicar el tamaño del string
                addByte(bytesCadena);           // bytes
            }

            // si el tamapo no cabe en 1 byte entonces van x paquetes de 256
            if (bytesCadena.Length >= 0xff)
            {
                throw new Exception("No se ha implemetado la escritura de STRINGS > 256Chars");
            }
        }

        public void addKeyValue(object key, object value)
        {
            addString(key == null ? string.Empty : key.ToString());
            addString(value == null ? string.Empty : value.ToString());
        }

        /*
        public void addHashtable(Hashtable hash)
        {
            // 1 byte indicando el tamaño de hash
            addByte(hash.Count);

            // los pares de Key-Value
            foreach (DictionaryEntry entry in hash)
                addKeyValue(entry.Key, entry.Value);
        }
        */

        #endregion

        #region Readers

        public void seek(int position)
        {
            bufferPointer = SizeHeader + position;
        }

        public byte get(int position)
        {
            return buffer[SizeHeader + position];

        }

        public void set(int position, int value)
        {
            buffer[SizeHeader + position] = (byte)value;
        }


        public byte getByte()
        {
            if (bufferPointer < BufferSize)
                return buffer[bufferPointer++];
            else return 0;
        }

        public byte[] getByte(int len)
        {
            byte[] v = new byte[len];

            // Si queremos leer mas bytes de los que hay, modificamos el len al size disponible (el resto será 0)
            if (bufferPointer + len >= BufferSize)
                len = BufferSize - bufferPointer;

            Array.Copy(buffer, bufferPointer, v, 0, len);
            bufferPointer += len;

            return v;
        }

        public byte BufferByte(int position)
        {
            return buffer[SizeHeader + position];
        }


        public bool getBool()
        {
            return (getByte() != 0);
        }

        public int getInt16()
        {
            if (bufferPointer + 2 < BufferSize)
            {
                int v = BitConverter.ToInt16(buffer, bufferPointer);
                bufferPointer += 2;
                return v;
            }
            return 0;
        }
        public int getUInt16()
        {
            if (bufferPointer + 2 < BufferSize)
            {
                int v = BitConverter.ToUInt16(buffer, bufferPointer);
                bufferPointer += 2;
                return v;
            }
            return 0;
        }
        public int getInt32()
        {
            if (bufferPointer + 4 < BufferSize)
            {
                int v = BitConverter.ToInt32(buffer, bufferPointer);
                bufferPointer += 4;
                return v;
            }
            return 0;
        }
        public uint getUInt32()
        {
            if (bufferPointer + 4 < BufferSize)
            {
                uint v = BitConverter.ToUInt32(buffer, bufferPointer);
                bufferPointer += 4;
                return v;
            }
            return 0;
        }

        public Int64 getInt64()
        {
            if (bufferPointer + 8 < BufferSize)
            {
                Int64 v = BitConverter.ToInt64(buffer, bufferPointer);
                bufferPointer += 8;
                return v;
            }
            return 0;
        }

        public UInt64 getUInt64()
        {
            if (bufferPointer + 8 < BufferSize)
            {
                UInt64 v = BitConverter.ToUInt64(buffer, bufferPointer);
                bufferPointer += 8;
                return v;
            }
            return 0;
        }

        public float getFloat()
        {
            if (bufferPointer + 4 < BufferSize)
            {
                float f = BitConverter.ToSingle(buffer, bufferPointer);
                bufferPointer += 4;
                return f;
            }
            return 0;
        }
        public double getDouble()
        {
            if (bufferPointer + 8 < BufferSize)
            {
                double f = BitConverter.ToDouble(buffer, bufferPointer);
                bufferPointer += 8;
                return f;
            }
            return 0;
        }

        public string getString()
        {
            StringBuilder sb = new StringBuilder();

            int size = getByte();
            for (int i = 0; i < size; i++)
                sb.Append((char)getByte());

            return sb.ToString();
        }



        #endregion

        #region Cifrado


        public bool CryptEnabled = true;
        public bool Cifrado = false;

        byte[] bufferCifrado;

        public void Crypt(CryptoTn4 engine)
        {
            if (Cifrado) return;

            // Console.WriteLine("# Cifrado");
            // Console.WriteLine("# Input  ["+(SizeHeader + SizeData).ToString("00") +"] " + buffer.getHexString(SizeHeader + SizeData));

            // Creamos el buffer cifrado (+1 byte porque lleva un byte random para generar un cifrado unico a partir de esa semilla)
            bufferCifrado = new byte[SizeHeader + SizeData + 1];

            // CryptoTn4 implementa un semaforo interno (ya que mientras ciframos este bloque no puede entrar otro o nos rompe la semilla)
            engine.WaitOne();

            // Ciframos el buffer a partir de primer byte
            engine.Reset();

            // Ponemos la cabecera, y un byte aleatorio (que ya se cifra para ramblear el resto)
            bufferCifrado[0] = (byte)(buffer[0] + 0x04);
            bufferCifrado[1] = engine.Crypt(engine.randomByte);

            for (int i = 2; i < bufferCifrado.Length; i++)
                bufferCifrado[i] = engine.Crypt(buffer[i - 1]);

            // Fin de cifrado
            engine.Release();

            // Console.WriteLine("# Output [" + bufferCifrado.Length.ToString("00") + "] " + bufferCifrado.getHexString(bufferCifrado.Length));
            Cifrado = true;

        }

        #endregion

        #region tostring

        public override string ToString()
        {
            return ToString(true);
        }

        public string ToString(bool complete)
        {
            return CmdToString(complete);
        }

        public string CmdToString(bool complete)
        {
            StringBuilder sb = new StringBuilder();

            // Head
            sb.Append(Header.ToString("X") + " ");
            sb.Append(Comando.ToString() + "." + SubComando.ToString());
            sb.Append(" id(" + this.idPaquete + ") Size(" + this.SizeData + ")");

            // Data
            if (complete)
            {
                sb.Append(buffer.getHexString(SizeHeader + SizeData));
            }

            return sb.ToString();
        }


        #endregion

    }
}
