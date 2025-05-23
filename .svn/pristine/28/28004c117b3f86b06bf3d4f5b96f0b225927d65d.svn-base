using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEO_DROID.Resources.Lib.Extra
{
    public class CmdReceiver : IDisposable
    {

        private object lockBufferInput = new object();
        private byte[] bufferInput = new byte[1024 * 4];    // 4kb de buffer
        private int bufferInputPointer = 0;


        public void Dispose()
        {
            bufferInput = null;
            CryptEngine = null;
        }

        #region Control de Buffer de entrada


        private int timeMsDescartarCommand = 500;

        public void addBuffer(byte[] bufferReceived, int len)
        {
            lock (lockBufferInput)
            {
                // Si nos vamos a pasar con la trama que escribimos entonces es mejor resetear
                if (len + bufferInputPointer > bufferInput.Length)
                {
                    bufferInputPointer = 0;
                }

                Array.Copy(bufferReceived, 0, bufferInput, bufferInputPointer, len);
                bufferInputPointer += len;
            }
        }

        protected void splitBuffer(int position)
        {
            lock (lockBufferInput)
            {
                Array.Copy(bufferInput, position, bufferInput, 0, bufferInputPointer - position);
                bufferInputPointer -= position;
            }
        }

        public void reset()
        {
            bufferInputPointer = 0;
            sizeAnalizado = 0;
            clock.Reset();
        }

        #endregion


        #region Procesamiento de Buffer
        private Stopwatch clock = new Stopwatch();
        private int sizeAnalizado = 0;


        enum ProccesResult
        {
            CommandOk,
            CommandError,
            CommandIncomplete,
            BufferEmpty
        }


        public Cmd Procesa()
        {
            if (bufferInputPointer == 0)
                return null;

            //
            // Este codigo evita que las tramas vengan rotas, si estamos analizando una trama no puede llegar con separacion de 500ms
            //
            if (sizeAnalizado > 0 && clock.ElapsedMilliseconds > timeMsDescartarCommand)
            {
                splitBuffer(sizeAnalizado);
                sizeAnalizado = 0;
            }

            // El metodo ProcesaBuffer debia devolver 2 valores:
            //    - Result: OK, Incomplete, Error, etc  -> todos los posibles errores
            //    - CMD   : Comando resultado en caso de ser OK
            //
            // Este metodo se llama cada vez que hay trafico en los buffers, por eso en muchos casos habrán comandos a media
            // y no me interesa estar generando new cmd() para cada intento de parsear un comando.
            //
            // Por eso el metodo devuelve un CMD unicamente cuando hay comando, y mediante el 'ProcesoResult' sabemos si hay comando o no
            //
            //
            //
            // Console.WriteLine("CmdRec - Analiza Buffer");

            ProccesResult ProcesoResult = ProccesResult.CommandError;
            Cmd comando = ProcesaBuffer(out ProcesoResult);

            // Console.WriteLine("CmdRec - Result > " + ProcesoResult.ToString());


            clock.Reset();

            // Aqui podemos analizar el ProcesoResult si nos interesa analizar los errores

            return comando;
        }


        #region Parser Buffer

        byte flagHead = 0xF0;
        byte flagNormal = 0xF8; // 11111000 F8 // 11111001 F9 // 11111010 FA // 11111011 FB
        byte flagCifrado = 0x04; // 11111100 FC // 11111101 FD // 11111110 FE // 11111111 FF

        private string _CryptKey;
        private CryptoTn4 CryptEngine = null;

        public string CryptKey
        {
            get { return _CryptKey; }
            set
            {
                _CryptKey = value;
                CryptEngine = new CryptoTn4();
                CryptEngine.Init(CryptKey);
            }
        }


        private Cmd ProcesaBuffer(out ProccesResult result)
        {
            byte byteFlag = 0;
            bool comandoCifrado = false;
            bool esperoSemillaCifrado = false;

            byte crc1 = 0, crc2 = 0;

            byte byteDataSize = 0;
            bool checkEndCommand = false;

            if (bufferInputPointer == 0)
            {
                result = ProccesResult.CommandError;
                return null;
            }

            //
            byte[] buffer = new byte[512];
            byte bufferPointer = 0;

            // Vamos recorriendo todo el buffer desde inicio hasta lo que tengamos
            for (int i = 0; i < bufferInputPointer; i++)
            {
                sizeAnalizado = i + 1;

                byte b = bufferInput[i];

                // Si está cifrado, vamos descifrando cada byte
                if (comandoCifrado)
                    b = CryptEngine.Decrypt(b);

                // Byte Cabecera
                if (byteFlag == 0)
                {
                    if ((b & flagHead) == flagHead)              // Es cabecera? 
                    {
                        if ((b & flagNormal) == flagNormal) comandoCifrado = false;
                        if ((b & flagCifrado) == flagCifrado) comandoCifrado = true;

                        if (comandoCifrado)
                        {
                            if (CryptEngine == null)            // Error <- Nos llegan comandos cifrados sin tener KEY de cifrado
                                comandoCifrado = false;         // ponemos el FLAG a false para que los comandos se ignores
                            else
                                CryptEngine.Reset();

                            esperoSemillaCifrado = true;

                        }

                        // Empezamos a meter el comando en un buffer[] // asi será mas comodo luego calcular el CRC y pasarselo a una clase CMD si es valido
                        bufferPointer = 0;

                        if (comandoCifrado)
                        {
                            buffer[bufferPointer++] = (byte)(b - flagCifrado);
                            // Console.WriteLine("\t Cabecera : " + b.ToString("X2") + " > Cifrado activo > " + buffer[0].ToString("X2"));
                        }
                        else
                        {
                            buffer[bufferPointer++] = b;
                            // Console.WriteLine("\t Cabecera : " + b.ToString("X2"));
                        }

                        // incrementamos flag
                        byteFlag++;
                    }
                }


                // Semilla de cifrado (byte de relleno simplemente para generar semillas aleatorias)
                else if (byteFlag == 1 && esperoSemillaCifrado)
                {
                    // Console.WriteLine("\t Semilla cifrado : " + b);
                    esperoSemillaCifrado = false;
                    continue;
                }

                // CRC
                else if (byteFlag == 1)
                {
                    crc1 = b;
                    buffer[bufferPointer++] = 0;
                    byteFlag++;
                }

                else if (byteFlag == 2)
                {
                    crc2 = b;
                    // Console.WriteLine("\t Crc : byte0 " + crc1.ToString("x2") + " byte1 " + crc2.ToString("x2") + " > Word 0x" + ((crc2 * 256 + crc1).ToString("x4")));
                    buffer[bufferPointer++] = 0;
                    byteFlag++;
                }

                // Id Paquete
                else if (byteFlag == 3)
                {
                    // Console.WriteLine("\t IdPaquete : " + b);
                    buffer[bufferPointer++] = b;
                    byteFlag++;
                }

                // 1 Byte Comando
                else if (byteFlag == 4)
                {
                    // Console.WriteLine("\t Comando : " + b.ToString("x2") + " > " + b.getValue(4, 4) + "." + b.getValue(0, 4));
                    buffer[bufferPointer++] = b;
                    byteFlag++;
                }

                // Data Len
                else if (byteFlag == 5)
                {
                    // Console.WriteLine("\t Len : " + b);
                    buffer[bufferPointer++] = b;
                    byteDataSize = b;
                    byteFlag++;

                    // Si no hay datos que leer ya estamos listos para calcular el crc
                    if (byteDataSize == 0)
                        checkEndCommand = true;

                }
                else
                {
                    if (byteDataSize > 0)
                    {
                        buffer[bufferPointer++] = b;
                        byteDataSize--;
                    }

                    if (byteDataSize == 0)
                        checkEndCommand = true;
                }

                if (checkEndCommand)
                {


                    // Cuando datasize llega a 0 significa que ya hemos leido los bytes de datos y los tenemos en el buffer[]
                    // Debemos calcular el CRC para ver si es correcto o ha fallado    
                    int byteEntrada = crc2 * 256 + crc1;
                    int byteMicroCrc = MicroChipCrc16.Compute(buffer, bufferPointer);



                    // Si el CRC coincide es que el comando que ha entrado es correcto
                    if (byteEntrada == byteMicroCrc)
                    {
                        // Console.WriteLine("\t Comprobando Checksum Trama(" + byteEntrada.ToString("x4") + ") Calculado(" + byteMicroCrc.ToString("x4") + ") <--- CRC OK");
                        // ponemos el CRC en su sitio 
                        buffer[1] = crc1;
                        buffer[2] = crc2;

                        // Construimos el comando a partir del Buffer que ya tenemos
                        Cmd comando = new Cmd(buffer, bufferPointer);
                        // Console.WriteLine("\t Comando = " + comando);

                        // Limpiamos la trama Input
                        splitBuffer(sizeAnalizado);

                        result = ProccesResult.CommandOk;
                        return comando;
                    }
                    else
                    {
                        // Console.WriteLine("\t Comprobando Checksum Trama(" + byteEntrada.ToString("x4") + ") Calculado(" + byteMicroCrc.ToString("x4") + ") <--- CRC Error");

                        // Limpiamos la trama Input
                        splitBuffer(sizeAnalizado);

                        result = ProccesResult.CommandError;
                        return null;
                    }
                }


            }

            result = ProccesResult.CommandIncomplete;
            return null;
        }

        #endregion

        #endregion

    }
}
