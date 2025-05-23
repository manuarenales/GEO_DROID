using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using GEO_DROID.Resources.Lib.Comunicacion;

namespace BLL.LeerInfoMaquina
{
    class ProtocoloTestHex : Protocolo
    {
        protected const byte COD_CABECERA = (byte)0x02;
        protected const byte COD_SO = (byte)0xFF;
        protected const byte COD_SD = (byte)0x00; //0xE0
        protected const byte COD_NM = (byte)0x00;
        protected const byte COD_ETX = (byte)0x03;

        protected const byte COD_ESC = (byte)0x1B;
        protected const byte COD_LF = (byte)0x0A;

        protected const char COD_ENQ = (char)0x05;
        protected const char COD_XON = (char)0x11;
        protected const char COD_XOFF = (char)0x13;
        protected const char COD_EOT = (char)0x04;
        protected const char COD_SYN = (char)0x16;
        protected const char COD_S = (char)'S';
        protected const char COD_C = (char)'C';

        public const byte CMD_SOLICITAR_DATOS = (byte)0x01;

        //protected const string PASSWORD = "01010101"; // Por defecto es : 01010101 10-20-30-40

        //string checksumTest = "";

        protected string _password;
        protected string _flags;
        int _timeoutDefault = 1000;

        public ProtocoloTestHex(IComunicacion com, IFiltroTrama filtroTrama) : base(com, filtroTrama)
        {
            _com = com;
        }

        //public ProtocoloTestHex(IComunicacion com, string password, int timeout)
        //{
        //    _password = password;
        //    _timeoutDefault = timeout;
        //    _com = com;
        //}

        //public ProtocoloTestHex(int baudrate, string puerto, string password, int timeout)
        //{
        //    if (password != null)
        //        _password = password;
        //    if (timeout > 0)
        //        _timeoutDefault = timeout;
        //    if (baudrate < 1200) baudrate = 1200;
        //    // 9600baud; Parity:None; Bits de datos:8; Bits de parada:1 
        //    _com = new ComunicacionSerie(puerto, baudrate, 0, Parity.None, 8, StopBits.One);
        //}

        //public ProtocoloTestHex(string mac, string pin, string password, int timeout)
        //{
        //    if (password != null)
        //        _password = password;
        //    if (timeout > 0)
        //        _timeoutDefault = timeout;

        //    _timeoutDefault += 1000;
        //    //_com = new ComunicacionBT(mac, pin);
        //    _com = ComunicacionBT.GetComunicacionBT(mac, pin);
        //}


        protected override char CrearChecksum(StringBuilder sb)
        {
            char c = (char)0x00;
            for (int i = 0; i < sb.Length; i++)
                c += sb[i];
            return (char)((c % 256) ^ 0x55);
        }

        protected override bool ComprobarChecksum(StringBuilder sb)
        {
            return true;
        }

        protected override StringBuilder MontarTrama(byte comando, byte[] datos) //throws CheckSumException
        {
            StringBuilder sbTemp = new StringBuilder();

            sbTemp.Append((char)COD_CABECERA); 	// Cabecera
            for (int i = 0; i < datos.Length; i++)
                sbTemp.Append((char)datos[i]);
            sbTemp.Append((char)CrearChecksum(sbTemp));      	// Checksum
            sbTemp.Append((char)COD_ETX);        // 

            return sbTemp;
        }

        public override InfoContadores LeerContadores(IProgressCallback callback)
        {
            // Leemos archivo de tramas
            List<byte[]> l = LeerFichero("");
            for (int i = 0; i < l.Count; i++)
            {
                EscribeLog("ENVIADO:" + ByteToHex(l[i]));
                if (EnviarTrama((byte)0, l[i]))
                {
                    StringBuilder sb = _com.RecibirDatos(_timeoutDefault, 0);
                    EscribeLog("RECIBIDO:" + ByteToHex(toByte(sb.ToString())));
                }
                else
                {
                    EscribeLog("ERROR EN EL ENVIO DE DATOS");
                    //Guardar error en el envio
                    break;
                }
            }

            InfoContadores info = new InfoContadores();
            //info.Buffer = ...;
            return info;
        }

        private List<byte[]> LeerFichero(string nombreFichero)
        {
            List<byte[]> l = new List<byte[]>();
            try
            {
                // string rutaDestino = BLL.AccesoBD.getAppPath() + "\\in.txt";
                // System.IO.FileStream fs = new System.IO.FileStream(rutaDestino, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                // System.IO.StreamReader m_streamWriter = new System.IO.StreamReader(fs);
                //m_streamWriter.BaseStream.Seek(0, System.IO.SeekOrigin.End);
                string lineaLeida;// = m_streamWriter.ReadLine();
                //while ((lineaLeida = m_streamWriter.ReadLine()) != null)
                //{
                //    l.Add(HexToByte(lineaLeida));
                //}

                //m_streamWriter.Close();
            }
            catch { }
            return l;
        }

        public static string ByteToHex(byte[] datos)
        {
            StringBuilder sb = new StringBuilder();
            if (datos != null)
            {
                byte[] b = datos;// Encoding.Unicode.GetBytes(cadena);
                for (int i = 0; i < b.Length; i++)
                {
                    if (b[i] < 16)
                        sb.Append("0");
                    sb.Append((b[i]).ToString("x"));
                }
            }
            return sb.ToString().ToUpper();
        }

        public static byte[] HexToByte(string cadena)
        {
            try
            {
                byte[] b = new byte[cadena.Length / 2];
                int j = 0;
                for (int i = 0; i < cadena.Length; i += 2)
                {
                    b[j++] = (byte.Parse(cadena.Substring(i, 2), System.Globalization.NumberStyles.HexNumber));
                }
                return b;// Encoding.Unicode.GetString(b);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;// "Error";
            }
        }

        public static byte[] toByte(string str)
        {
            byte[] caracteres = new byte[str.Length];
            for (int i = 0; i < str.Length; i++)
                caracteres[i] = (byte)str[i];

            return caracteres;
        }

        public static string toStr(byte[] b)
        {
            char[] caracteres = new char[b.Length];
            for (int i = 0; i < b.Length; i++)
            {
                caracteres[i] = (char)b[i];
            }

            return new string(caracteres);
        }


    }
}
