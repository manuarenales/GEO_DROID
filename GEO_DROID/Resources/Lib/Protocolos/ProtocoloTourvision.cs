using System;
using System.Collections.Generic;
using System.Text;
using GEO_DROID.Resources.Lib.Comunicacion;

namespace BLL.LeerInfoMaquina
{
    class ProtocoloTourvision : Protocolo
    {
        // Conexión serie por defecto TOURVISION: 9600, Parity.None, 8, StopBits.One

        protected const byte COD_CABECERA = (byte)0x02;
        protected const int INTENTOS_LECTURAS = 3;
        
        public const byte CMD_CHECK_PASS = 0x00;
        public const byte CMD_SEND_PASS = 0x01;
        public const byte CMD_CLEAR_PASSWORD = 0x02;
        public const byte CMD_PEDIR_CONTADORES = 0x11;

        public const char NACK = (char)0x15; //CRC mal, comando no existe.
        public const char ACK = (char)0x06; //Recepción correcta.

        private const string PASSWORD = "";//new string((char)0x00, 8);//((char)0x00 + (char)0x00 + (char)0x00 + (char)0x00 + (char)0x00 + (char)0x00 + (char)0x00 + (char)0x00);
        private const int TIMEOUT = 1000;

        string _password = PASSWORD;
        int _timeoutDefault = TIMEOUT;

        public ProtocoloTourvision(IComunicacion com, IFiltroTrama filtroTrama, string password, int timeout) : base(com, filtroTrama)
        {
            if (password != null && password.Length > 0)
                _password = password;
            if (timeout > 0)
                _timeoutDefault = timeout;
        }
        
        protected override char CrearChecksum(StringBuilder sb)
        {
            char crc = (char)0x00;
            for (int i = 0; i < sb.Length; i++)
            {
                crc += sb[i];
            }
            crc = (char)(crc % 256);
            return crc;
        }

        protected char[] CrearChecksum2(StringBuilder sb)
        {
            char crc = (char)0x00;
            char crcL = (char)0x00;
            char crcH = (char)0x00;

            //crc -= (char)COD_CABECERA;
            if (sb != null)
            {
                for (int i = 1; i < sb.Length; i++)
                {
                    crc += sb[i];
                }
            }
            crcL = (char)(crc % 256);
            crcH = (char)(crc / 256);
            return new char[2] { crcL, crcH };
        }

        protected override bool ComprobarChecksum(StringBuilder sb)
        {
            string datos = sb.ToString();
            if (datos.Length > 2)
            {
                int longitudDatos = datos.Length - 2; //En la longitud no contamos  el checksum

                byte[] b = new byte[2];
                b[1] = (byte)datos[longitudDatos+1];
                b[0] = (byte)datos[longitudDatos];
                
                int crc = (int)BitConverter.ToInt16(b, 0);

                int crcTmp = 0;
                for (int i = 1; i < longitudDatos; i++)
                {
                    crcTmp += (char)datos[i];
                }
                _error = "CRC=" + crc + " CALCULO:" + crcTmp;
                return crc == crcTmp;
            }
            return false;
        }

        protected override StringBuilder MontarTrama(byte comando, byte[] datos) //throws CheckSumException
        {
            StringBuilder sbTemp = new StringBuilder();
            StringBuilder sbPass = new StringBuilder();

            sbTemp.Append((char)COD_CABECERA);
            switch (comando)
            {
                case CMD_CHECK_PASS:
                    sbTemp.Append((char)CMD_CHECK_PASS);
                    break;
                case CMD_SEND_PASS:
                    sbTemp.Append((char)CMD_SEND_PASS);
                    for (int i = 0; i < 8; i++)
                    {
                        if (_password.Length > i)
                            sbPass.Append((char)_password[i]);
                        else
                            sbPass.Append((char)0x00);
                    }
                    break;
                case CMD_CLEAR_PASSWORD:
                    sbTemp.Append((char)CMD_CLEAR_PASSWORD);
                    break;
                case CMD_PEDIR_CONTADORES:
                    sbTemp.Append((char)CMD_PEDIR_CONTADORES);
                    sbPass.Append((char)0x01);
                    sbPass.Append((char)0x00);
                    break;
            }
            //if (datos != null)
            //{
            //    for (int i = 0; i < datos.Length; i++)
            //    {
            //        sbTemp.Append((char)datos[i]);
            //    }
            //}
            sbTemp.Append((char)sbPass.Length);
            sbTemp.Append((char)0x00);

            sbTemp.Append(sbPass);

            char[] crc = CrearChecksum2(sbTemp);
            sbTemp.Append(crc[0]);
            sbTemp.Append(crc[1]);
            return sbTemp;
        }

        private bool RecibidoIsError(StringBuilder recibido)
        {
            if (recibido != null && recibido.Length > 0)
            {
                if (recibido[0].Equals(NACK)) //CRC mal, comando no existe.
                {
                    _error = ("Recibido NO OK");
                }
                else
                {
                    ProcesarACK(recibido);
                    return false;
                }
            }
            else
            {
                _error = "Trama no recibida";
            }
            return true;
        }

        private void ProcesarACK(StringBuilder sb)
        {
            //Quitamos los ACK
            while (sb.Length > 0 && sb[0] == ACK)
                sb.Remove(0, 1);
            return;
        }

        public override InfoContadores LeerContadores(IProgressCallback callback)
        {
            InfoContadores info = null;
            int tiempoBase = 0;
            StringBuilder sb;
            _errorEstado = "(S01 PASS)";
            if (EnviarTrama(CMD_SEND_PASS))
            {
                tiempoBase = Environment.TickCount;
                sb = RecibirDatos(_timeoutDefault);
                _errorEstado += "(" + (Environment.TickCount - tiempoBase) + "ms)";

                if (!RecibidoIsError(sb))
                {
                    bool contadoresLeidosCorrectamente = false; //Siempre entra una vez
                    for (int i = 0; i < INTENTOS_LECTURAS && !contadoresLeidosCorrectamente; i++)
                    {
                        _errorEstado = "(" + i + "S02 CONTA)";
                        if (EnviarTrama(CMD_PEDIR_CONTADORES))
                        {
                            tiempoBase = Environment.TickCount;
                            sb = RecibirDatos(_timeoutDefault);
                            _errorEstado += "(" + (Environment.TickCount - tiempoBase) + "ms)";

                            if (!RecibidoIsError(sb))
                            {
                                if (ComprobarChecksum(sb))
                                {
                                    InfoContadores info1 = ProcesarDatos(sb);
                                    _errorEstado = "(" + i + "S03 CONTA2)";
                                    if (EnviarTrama(CMD_PEDIR_CONTADORES))
                                    {
                                        tiempoBase = Environment.TickCount;
                                        sb = RecibirDatos(_timeoutDefault);
                                        _errorEstado += "(" + (Environment.TickCount - tiempoBase) + "ms)";

                                        if (!RecibidoIsError(sb))
                                        {
                                            if (ComprobarChecksum(sb))
                                            {
                                                _errorEstado = "(" + i + "S04 VALIDAR)";
                                                InfoContadores info2 = ProcesarDatos(sb);
                                                if (info1 != null && info2 != null
                                                    && info1.Entradas == info2.Entradas
                                                    && info1.Salidas == info2.Salidas
                                                    && info1.Billetes == info2.Billetes
                                                    && info1.Cajon == info2.Cajon)
                                                {
                                                    _error = null;
                                                    info = info1;
                                                    contadoresLeidosCorrectamente = true;
                                                    //return info1;
                                                }
                                                else
                                                {
                                                    _error = "Error en la lectura";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return info;
        }

        private int GetDato(StringBuilder datos, int indice)
        {
            int ret = 0;
            int pos = 4 + ((indice -1 ) * 4);
            if (datos.Length >= (pos + 4))
            {
                byte[] b = new byte[4];
                b[3] = (byte)datos[pos + 3];
                b[2] = (byte)datos[pos + 2];
                b[1] = (byte)datos[pos + 1];
                b[0] = (byte)datos[pos];
                ret = BitConverter.ToInt32(b, 0);
            }
            return ret;
        }

        private InfoContadores ProcesarDatos(StringBuilder datos)
        {
                //ACK (0x06) --> El ACK no debería llegar a este método

                //STX (0x02)
                //Comando (0x11)
                //Longitud (0x10,0x00)
                //Entradas (4 bytes)
                //Salidas (4 bytes)
                //Cajón (4 bytes)
                //Billetes (4 bytes)
                //Checksum (Low,High)
            int entradas = 0;
            int salidas = 0;
            int cajon = 0;
            int billetes = 0;

            entradas = GetDato(datos, 1);
            salidas = GetDato(datos, 2);
            cajon = GetDato(datos, 3);
            billetes = GetDato(datos, 4);

            InfoContadores info = new InfoContadores();
            info.Buffer = datos;
            info.Entradas = entradas;
            info.Salidas = salidas;
            info.Billetes = billetes;
            info.Cajon = cajon;
            return info;
        }
    }
}
