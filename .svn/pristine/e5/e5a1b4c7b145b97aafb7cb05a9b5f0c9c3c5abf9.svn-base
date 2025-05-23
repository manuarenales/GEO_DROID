using System;
using System.Text;

using BYTE = System.Int16;
using WORD = System.Int32;
using DWORD = System.Int64;
using GEO_DROID.Resources.Lib.Comunicacion;

namespace BLL.LeerInfoMaquina
{
    /* En el protocolo NOVAMATIC, la máquina es la que actua como master.
     * Cada 30 segundos envía una trama para indicar que está online, que se reenviará si no tiene respuesta en 500ms. Este reenvío se realiza dos veces y vuelve a callar hasta que pasan otros 30 segundos.
     * Definimos una constante TIMEOUT_INICIAL para representar los 30 segundos
     * 
     * 
     */
    class ProtocoloGSP : Protocolo
    {
        const WORD CREDIT_VALUE_COINS = 0x0301;
        const WORD CREDIT_VALUE_BILLS = 0x0302;
        const WORD ONLINE_CHECK    = 0xFF00;
        const WORD GD_ID           = 0x0100;
        const WORD TOTAL_CREDITS_BET = 0xA000; //Créditos
        const WORD TOTAL_CREDITS_WON = 0xA001; //Créditos
        const WORD TOTAL_BET       = 0xA100; //Fichas
        const WORD TOTAL_WIN       = 0xA101; //Fichas
        const WORD GAMES_PLAYED    = 0xA102;
        const WORD MONEDA_HOPPER1  = 0xA020;
        const WORD MONEDA_HOPPER2  = 0xA021;
        const WORD MONEDA_HOPPER3  = 0xA022;
        const WORD MONEDA_HOPPER4  = 0xA023;
        const WORD MONEDA_HOPPER5  = 0xA024;
        const WORD MONEDA_CAJON1   = 0xA030;
        const WORD MONEDA_CAJON2   = 0xA031;
        const WORD MONEDA_CAJON3   = 0xA032;
        const WORD MONEDA_CAJON4   = 0xA033;
        const WORD MONEDA_CAJON5   = 0xA034;
        const WORD BILLETE_CAJON1  = 0xA040;
        const WORD BILLETE_CAJON2  = 0xA041;
        const WORD BILLETE_CAJON3  = 0xA042;
        const WORD BILLETE_CAJON4  = 0xA043;
        const WORD BILLETE_CAJON5  = 0xA044;
        const WORD BILLETE_DISP1   = 0xA0A0;
        const WORD BILLETE_DISP2   = 0xA0A1;
        const WORD BILLETE_DISP3   = 0xA0A2;
        const WORD BILLETE_DISP4   = 0xA0A3;
        const WORD BILLETE_DISP5   = 0xA0A4;
        const WORD MONEDA_PAGADA1  = 0xA060;
        const WORD MONEDA_PAGADA2  = 0xA061;
        const WORD MONEDA_PAGADA3  = 0xA062;
        const WORD MONEDA_PAGADA4  = 0xA063;
        const WORD MONEDA_PAGADA5  = 0xA064;
        const WORD BILLETE_PAGADO1 = 0xA0C0;
        const WORD BILLETE_PAGADO2 = 0xA0C1;
        const WORD BILLETE_PAGADO3 = 0xA0C2;
        const WORD BILLETE_PAGADO4 = 0xA0C3;
        const WORD BILLETE_PAGADO5 = 0xA0C4;
        const WORD HAND_PAY        = 0xA012;
        const WORD HOPPER_REFILL   = 0xA015;
        const WORD START_UP_COMPLETED = 0xE02B;

        private const int TIMEOUT_INICIAL = 30000;
        private const int TIMEOUT = 500;
        private const int TIMEOUT_ENTRE_BYTES = 100;
        private const int MAX_ITERACIONES = 100;
        int _numeroTrama;
        private int _timeout = TIMEOUT;

        public override byte ConfiguracionPuertoSerie
        {
            get { return (byte)0x05; } // Conexión serie por defecto GSP: 19200, Parity.None, 8, StopBits.One
        }

        public ProtocoloGSP(IComunicacion com, IFiltroTrama filtroTrama, int timeout)
            : base(com, filtroTrama)
        {
            if (timeout > 0)
                _timeout = timeout;
        }

        public static char CrearChecksumS(StringBuilder sb)
        {
            const ushort MAGIC_NUMBER = 4225; //010201; //The magic number 010201 octal is derived from the CRC polynomial x^16+x^12+x^5+1
            ushort c, q, crcval = 0;
            int len = sb.Length;
            int indice = 0;

            for (int i = 0; i < len; i++)
            {
                c = sb[indice++];
                System.Diagnostics.Debug.WriteLine("CS (" + (int)c + ")");
                q = (ushort)((crcval ^ c) & 15); //15 is 017 octal
                crcval = (ushort)((crcval >> 4) ^ (q * MAGIC_NUMBER));
                q = (ushort)((crcval ^ (c >> 4)) & 15);
                crcval = (ushort)((crcval >> 4) ^ (q * MAGIC_NUMBER));
            }
            return (char)crcval;
        }

        protected override char CrearChecksum(StringBuilder sb)
        {
            return ProtocoloGSP.CrearChecksumS(sb);
        }

        public static bool ComprobarChecksumS(StringBuilder sb)
        {
            const int MIN_NUMERO_BYTES_DATOS = 4;
            const int NUMERO_BYTES_CHECKSUM = 2;
            if (sb != null && sb.Length > 0)
            {
                if (sb.Length >= (MIN_NUMERO_BYTES_DATOS + NUMERO_BYTES_CHECKSUM))
                {
                    StringBuilder sbDatos = new StringBuilder();
                    sbDatos.Append(sb);
                    sbDatos.Remove(sbDatos.Length - NUMERO_BYTES_CHECKSUM, NUMERO_BYTES_CHECKSUM); // Quitamos CRC
                    char checksum = CrearChecksumS(sbDatos);

                    byte[] b = new byte[NUMERO_BYTES_CHECKSUM];
                    b[0] = (byte)sb[sb.Length - 2];
                    b[1] = (byte)sb[sb.Length - 1];
                    char checksumRecibido = BitConverter.ToChar(b, 0);
                    System.Diagnostics.Debug.WriteLine("Checksum Calculado:" + (int)checksum + " Recibido:" + (int)checksumRecibido);

                    return (checksum == checksumRecibido);
                }
            }
            return false;
        }

        protected override bool ComprobarChecksum(StringBuilder sb)
        {
            return ProtocoloGSP.ComprobarChecksumS(sb);
        }

        protected override StringBuilder MontarTrama(byte comando, byte[] datos) //throws CheckSumException
        {
            StringBuilder sbTemp = new StringBuilder();

            sbTemp.Append((char)0xFF); //Comando
            sbTemp.Append((char)0x06); //Subcomando
            sbTemp.Append((char)_numeroTrama); //Número de trama
            if (datos == null || datos.Length == 0)
            {
                sbTemp.Append((char)0);//Longitud de datos
            }
            else
            {
                sbTemp.Append((char)datos.Length);//Longitud de datos
                for (int i = 0; i < datos.Length; i++)
                    sbTemp.Append((char)datos[i]); //Datos
            }
            char crc = CrearChecksum(sbTemp);
            sbTemp.Append((char)(crc % 256)); //CRC LOW
            sbTemp.Append((char)(crc / 256)); //CRC HIGH

            return sbTemp;
        }

        public override InfoContadores LeerContadores(IProgressCallback callback)
        {
            StringBuilder sbRecibido;
            InfoContadores info = null;
            int iteraciones = 0;
            _numeroTrama = 0;

            /*StringBuilder sbEntradas = new StringBuilder();
            sbEntradas.Append((char)0xA1);
            sbEntradas.Append((char)0x00);
            sbEntradas.Append((char)0x15);
            sbEntradas.Append((char)0x04);
            sbEntradas.Append((char)0x00);
            sbEntradas.Append((char)0x00);
            sbEntradas.Append((char)0xD9);
            sbEntradas.Append((char)0x1C);
            sbEntradas.Append((char)0x44);
            sbEntradas.Append((char)0xD5);
            StringBuilder sbSalidas = new StringBuilder();
            sbSalidas.Append((char)0xA1);
            sbSalidas.Append((char)0x01);
            sbSalidas.Append((char)0x16);
            sbSalidas.Append((char)0x04);
            sbSalidas.Append((char)0x00);
            sbSalidas.Append((char)0x01);
            sbSalidas.Append((char)0xD1);
            sbSalidas.Append((char)0xA4);
            sbSalidas.Append((char)0x33);
            sbSalidas.Append((char)0xEB);
             */
            /*
            StringBuilder sbTest = new StringBuilder();
            sbTest.Append((char)0x01);
            sbTest.Append((char)0x00);
            sbTest.Append((char)0x01);
            sbTest.Append((char)0x08);
            sbTest.Append((char)0x00);
            sbTest.Append((char)0x00);
            sbTest.Append((char)0x00);
            sbTest.Append((char)0x00);
            sbTest.Append((char)0xB3);
            sbTest.Append((char)0xB4);
            sbTest.Append((char)0x89);
            sbTest.Append((char)0x29);
            sbTest.Append((char)0xDD);
            sbTest.Append((char)0x9C);*/


            _com.LimpiarBuffer();
            int reintentos = 1;
            while (iteraciones++ < MAX_ITERACIONES && _numeroTrama < 0xA0)
            {
                //if (EnviarTrama((byte)0))
                //{
                sbRecibido = RecibirDatos(_numeroTrama == 0 ? TIMEOUT_INICIAL : _timeout, TIMEOUT_ENTRE_BYTES);


                    //EnviarTrama((byte)0);
                    //_numeroTrama++;

                    /*if (_numeroTrama == 0x01)
                        sbRecibido = sbTest;
                    else if (_numeroTrama < 0x03)
                        sbRecibido = MontarTrama(0, null);*/
                        /*
                    else if ((_numeroTrama == 0x16))
                        sbRecibido = sbSalidas;
                    else if (_numeroTrama < 0x16)
                        sbRecibido = MontarTrama(0, null);*/
                
                    if (!RecibidoIsError(sbRecibido))
                    {
                        if (info == null)
                            info = new InfoContadores();
                        info.Buffer = sbRecibido;
                        Trama trama;
                        //do
                        //{
                            trama = Trama.GetTrama(sbRecibido);
                            System.Diagnostics.Debug.WriteLine("ESPERADO ntrama: " + _numeroTrama + " RECIBIDO nTrama:" + (trama == null ? (byte)0 : trama._nTrama));
                        //}
                        //while (trama != null && trama._nTrama < _numeroTrama);
                        if (!trama.tramaOk)// trama == null || trama.CheckComando(0))
                        {
                            //_error = sbRecibido.Length + "Trama no válida - CMD:" + (trama == null ? "null" : trama.error) + (trama == null ? "00" : trama._nTrama.ToString("x"));
                            _error += sbRecibido.Length + "Trama no válida - " + " CMD:" + trama._comando.ToString("x") + " SUBCMD:" + trama._subcomando.ToString("x") + " NTrama:" + trama._nTrama.ToString("x") + "LENDATA:" + trama._datos.Length.ToString("x"); 
                            if (reintentos <= 0)
                                _numeroTrama = 0xA1;
                            reintentos--;
                        }
                        else
                        {
                            reintentos = 1;
                            if (trama._nTrama == _numeroTrama)
                            {
                                switch (trama.ComandoPlus)
                                {
                                    case TOTAL_CREDITS_BET:
                                        info.Entradas = trama.GetContador(1);
                                        break;
                                    case TOTAL_CREDITS_WON:
                                        info.Salidas = trama.GetContador(1);
                                        break;
                                    case MONEDA_CAJON2:
                                        info.CajonMonedas020 = trama.GetContador(1);
                                        break;
                                    case MONEDA_CAJON3:
                                        info.CajonMonedas050 = trama.GetContador(1);
                                        break;
                                    case MONEDA_CAJON4:
                                        info.CajonMonedas100 = trama.GetContador(1);
                                        break;
                                    case MONEDA_CAJON5:
                                        info.CajonMonedas200 = trama.GetContador(1);
                                        break;
                                    case BILLETE_CAJON1:
                                        info.Billetes5 = trama.GetContador(1);
                                        break;
                                    case BILLETE_CAJON2:
                                        info.Billetes10 = trama.GetContador(1);
                                        break;
                                    case BILLETE_CAJON3:
                                        info.Billetes20 = trama.GetContador(1);
                                        break;
                                    case BILLETE_CAJON4:
                                        info.Billetes50 = trama.GetContador(1);
                                        break;
                                    case START_UP_COMPLETED:
                                        _numeroTrama = 0xA02;
                                        break;
                                }
                                //if (info.Entradas >= 0 && info.Salidas >= 0)
                                //    _numeroTrama = 0xA2;
                            }
                            else if (trama._nTrama < _numeroTrama)
                            {
                                _numeroTrama--;// = trama._nTrama;
                            }
                            else
                            {
                                _error = "Desajuste sincronización";
                                _numeroTrama = 0xA3;
                            }
                            EnviarTrama((byte)0);
                            _numeroTrama++;
                        }
                        //EnviarTrama((byte)0);
                        //_numeroTrama++;
                    }
                    else
                    {
                        _numeroTrama = 0xA4;
                    }
                //}
            }
            return info;
        }

        private bool RecibidoIsError(StringBuilder recibido)
        {
            // Comprobamos el tamaño del comando
            if (recibido == null || recibido.Length == 0)
            {
                _error = "Recibido vacío";
                return true;
            }
            /*else if (recibido.Length < 12)  // Nunca se recibe fin de cadena -> '03'
            {
                _error = "Trama incorrecta";
                return true;
            }

            if (!ComprobarChecksum(recibido))
            {
                _error = "Checksum incorrecto";
                return true;
            }

            // Comprobamos que no sea la trama de error
            string cadena = recibido.ToString();
            string datos = cadena.Substring(1, 10);
            if (datos.IndexOf("ERROR") != -1)
            {
                _error = "Se recibió un error";
                return true;
            }*/

            // El resto es válido
            return false;
        }
        protected class Trama
        {
            public StringBuilder _datos = new StringBuilder();
            public byte _comando = 0;
            public byte _subcomando = 0;
            public byte _nTrama;
            public string error = "";
            public bool tramaOk = false;

            public int ComandoPlus
            {
                get
                {
                    return (_comando * 256) + _subcomando;
                }
            }

            public static Trama GetTrama(StringBuilder datos)
            {
                Trama t = new Trama(datos);
                //if (t._comando != 0)
                    return t;
                //return null;
            }

            public Trama(StringBuilder sb)
            {
                int c;
                bool salir = false;
                int estado = 0;
                int lenDatos = 0;
                StringBuilder sbCrc = new StringBuilder();
                int nRemove = 0;
                error = "[(S00) LN " + sb.Length + "]";
                int posIni = 0;
                for (int i = 0; i < sb.Length && lenDatos <= sb.Length && !salir; i++)
                {
                    for (int x = i; x < sb.Length && lenDatos <= sb.Length && !salir; x++)
                    {
                        c = sb[x];		//Lee el caracter del buffer
                        if ((estado == 0 && c == 0))
                            break;

                        sbCrc.Append((char)c);
                        //nRemove++;
                        switch (estado)
                        {
                            case 0:
                                this._comando = (byte)c;
                                estado++;
                                break;
                            case 1:
                                this._subcomando = (byte)c;
                                estado++;
                                break;
                            case 2:
                                this._nTrama = (byte)c;
                                estado++;
                                break;
                            case 3:
                                lenDatos = (int)c;
                                lenDatos = lenDatos + 2;// CRC 2 bytes
                                estado++;
                                break;
                            case 4:
                                this._datos.Append((char)c);
                                if (--lenDatos <= 0)
                                    estado++;
                                break;
                        }

                        if (estado == 5)
                        {
                            if (ProtocoloGSP.ComprobarChecksumS(sbCrc))
                            {
                                salir = true;
                                error = "[NO ERROR]";
                                tramaOk = true;
                            }
                            else
                            {
                                _comando = 0;
                                _subcomando = 0;
                                _nTrama = 0;
                                lenDatos = 0;
                                _datos.Length = 0;
                                error = "(S05) Checksum error " + sbCrc.Length;
                            }
                        }
                    }
                }
                //sb.Remove(0, nRemove);
            }

            // CONTADORES
            public int GetContador(int indice)
            {
                //if (CheckComando(CMD_CONTADORES))
                //{   // Si la trama es un comando de contadores y tiene longitud 13 sacamos la informacion
                    int pos = ((indice - 1) * 4);
                    if (pos + 4 <= _datos.Length)
                    {
                        byte[] b = new byte[4];
                        b[3] = (byte)_datos[pos + 0];
                        b[2] = (byte)_datos[pos + 1];
                        b[1] = (byte)_datos[pos + 2];
                        b[0] = (byte)_datos[pos + 3];
                        return BitConverter.ToInt32(b, 0);
                    }
                //}
                return -1;
            }

            public bool CheckComando(byte comando)
            {
                return (_comando == comando);
            }
        }
    }
}
