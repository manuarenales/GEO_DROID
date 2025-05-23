using System;
using System.Collections.Generic;
using System.Text;
using GEO_DROID.Resources.Lib.Comunicacion;

namespace BLL.LeerInfoMaquina
{
    class ProtocoloUnidesa : Protocolo
    {
        // Conexión serie por defecto CIRSA: 9600, Parity.None, 8, StopBits.One

        protected const byte COD_CABECERA = (byte)0x02;
        protected const byte COD_SO = (byte)0xFF;
        protected const byte COD_SD = (byte)0x00; //0xE0
        protected const byte COD_NM = (byte)0x00;
        protected const byte COD_ETX = (byte)0x03;

        public const byte CMD_PRESENCIA = (byte)0;
        public const byte CMD_ESTADO = (byte)1;
        public const byte CMD_RESPUESTA_ESTADO = (byte)2;
        public const byte CMD_ENVIAR_PASSWORD = (byte)11;
        public const byte CMD_PEDIR_CONTADORES = (byte)22;
        public const byte CMD_RESPUESTA_CONTADORES = (byte)23;

        public const char NACK = (char)0x15;
        public const char ACK = (char)0x06;

        //int _baudios = 1200; //600,1200,2400,4800,9600,19200
        //Parity _paridad = Parity.None;
        //int _bitsDeDatos = 8;
        //StopBits _bitsDeParada = StopBits.One;

        string _password = "";
        int _timeoutDefault = 1000;
        int _numeroTrama = 0;
        int _slotDestino = COD_SD;

        public ProtocoloUnidesa(IComunicacion com, IFiltroTrama filtroTrama, string pass, int timeout)
            : base(com, filtroTrama)
        {
            if (pass != null && pass.Length > 0)
                _password = pass;
            if (timeout > 0)
                _timeoutDefault = timeout;
        }

        protected override char CrearChecksum(StringBuilder sb)
        {
            char c = (char)0x00;
            for (int i = 0; i < sb.Length; i++)
                c += sb[i];
            return (char)((c % 256) ^ 0x55);
        }

        protected override bool ComprobarChecksum(StringBuilder sb)
        {   
            char checksum = (char) 0x00;
            if (sb.Length > 2)
                checksum = sb[sb.Length - 2];
            char c = (char)0x00;
            for (int i = 0; i < sb.Length-2; i++)
                c += sb[i];
            return ((c % 256) ^ 0x55) == checksum;
        }

        protected override StringBuilder MontarTrama(byte comando, byte[] datos) //throws CheckSumException
        {
            // Formato de trama Cirsa/Unidesa
            // STX | SD | SO | NM | CMD | ND | D1 | ... | Dn | CHK | ETX
            StringBuilder sbTemp = new StringBuilder();

            sbTemp.Append((char)COD_CABECERA); 	// Cabecera
            sbTemp.Append((char)_slotDestino);  // Destino
            sbTemp.Append((char)COD_SO);        // Origen
            sbTemp.Append((char)_numeroTrama);  // Número de trama
            sbTemp.Append((char)comando); 		// Comando
            if (datos != null)
            {
                sbTemp.Append((char)datos.Length); 		    // Número de datos
                for (int i = 0; i < datos.Length; i++)
                {
                    sbTemp.Append((char)datos[i]);
                }
            }
            else
            {
                sbTemp.Append((char)0x00); 		 // Número de datos
            }
            sbTemp.Append((char)CrearChecksum(sbTemp));      	// Checksum
            sbTemp.Append((char)COD_ETX);        // 
            return sbTemp;
        }

        private bool RecibidoIsError(StringBuilder recibido, bool comprobarChecksum, bool comprobarSesion, byte comandoEsperado)
        {
            if (recibido == null || recibido.Length == 0)
            {
                _error = ("Recibido vacio");
            }
            else if (recibido[0].Equals(NACK)) 
            {
                _error = ("NACK");
            }
            else
            {
                //Quitamos los ACK
                while (recibido.Length > 0 && recibido[0] == ACK)
                    recibido.Remove(0, 1);
                
                // Compruebo checksum si procede
                if (comprobarChecksum && !ComprobarChecksumYResponder(recibido))
                {
                    _error = "CHECKSUM Error";
                    return true;//=
                }

                if (comprobarSesion && !ComprobarSesion(recibido))
                {
                    _error = "SESION Error";
                    return true;//=
                }

                if (comandoEsperado > 0 && !ComprobarComando(recibido, comandoEsperado))
                {
                    _error = "CMD ESPERADO (" + comandoEsperado + ") Error";
                    return true;//=
                }

                return false;
            }
            return true;//=
        }

        private void QuitarACK(StringBuilder recibido)
        {
            //Quitamos los ACK
            while (recibido.Length > 0 && recibido[0] == ACK)
                recibido.Remove(0, 1);
        }

        private byte[] DatosPassword()
        {
            byte[] datos = new byte[9];
            datos[0] = 3;
            for (int i = 0; i < 8; i++)
            {
                if (_password != null && _password.Length > i)
                    datos[i + 1] = (byte)_password[i];
                else
                    datos[i + 1] = 0;
            }
            return datos;
        }

        private byte[] DatosPedirContadores(int indiceContadorInicial, int numeroContadores)
        {
            byte[] datosConta = new byte[3];
            datosConta[0] = 0;
            datosConta[1] = (byte)(indiceContadorInicial - 1);
            datosConta[2] = (byte)numeroContadores;
            return datosConta;
        }

        private void EnviarACK()
        {
            if (_com != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append((char)ACK);
                _com.EnviarDatos(sb);
            }
        }

        private void EnviarNACK()
        {
            if (_com != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append((char)NACK);
                _com.EnviarDatos(sb);
            }
        }

        private int GetSlotDestino(StringBuilder sbRecibido)
        {
            int n = 0;
            if (sbRecibido != null && sbRecibido.Length > 2)
            {
                n = (char)sbRecibido[2]; // Leemos el slot origen de la trama
            }
            return n;
        }

        private bool ComprobarACK(StringBuilder sb)
        {
            if (sb != null && sb.Length > 0)
            {
                if (ACK == sb[0])
                {
                    QuitarACK(sb);
                    return true;
                }
            }
            return false;
        }

        private bool ComprobarChecksumYResponder(StringBuilder sb)
        {
            if (ComprobarChecksum(sb))
            {
                EnviarACK();
                return true;
            }
            else
            {
                EnviarNACK();
                return false;
            }
        }

        private bool ComprobarComando(StringBuilder sb, byte comando)
        {
            if (sb != null && sb.Length > 4)
            {
                return ((char)comando == (char)sb[4]);
            }
            return false;
        }

        private bool ComprobarSesion(StringBuilder sb)
        {
            if (sb != null && sb.Length > 6)
            {
                return ((2 == (char)sb[4]) && (3 == (char)sb[6]));
            }
            return false;
        }

        private StringBuilder ComunicacionContadores(int intentos, int indiceContadorInicial, int numeroContadores)
        {
            while (intentos > 0)
            {
                if (EnviarTrama(CMD_PEDIR_CONTADORES, DatosPedirContadores(indiceContadorInicial, numeroContadores)))
                {
                    StringBuilder sb = RecibirDatos(_timeoutDefault, 300);
                    if (!RecibidoIsError(sb, true, false, CMD_RESPUESTA_CONTADORES))
                    //if (ComprobarACK(sb) && ComprobarChecksumYResponder(sb) && ComprobarComando(sb, CMD_RESPUESTA_CONTADORES))
                    {
                        _error = "";
                        _errorEstado = "";
                        return sb;
                    }
                }
                intentos--;
            }
            return null;
        }

        public override InfoContadores LeerContadores(IProgressCallback callback)
        {
            int maxIntentos = 3;
            StringBuilder sb;
            int intentos = maxIntentos;
            _slotDestino = 0;

            //ESTADO
            _errorEstado = "(S01 ESTADO)";
            _numeroTrama = 0;
            while (intentos > 0)
            {
                if (EnviarTrama(CMD_ESTADO))
                {
                    sb = RecibirDatos(_timeoutDefault, 300);
                    if (!RecibidoIsError(sb, true, false, CMD_RESPUESTA_ESTADO))
                    {
                        _slotDestino = GetSlotDestino(sb);
                        break;
                    }
                    /*
                    //if (ComprobarACK(sb)) // AQUÍ NO SE COMPRUEBA SI LLEGA ACK PORQUE NO SE MANDA SLOT DESTINO
                    //{
                        if (ComprobarChecksumYResponder(sb) && ComprobarComando(sb, CMD_RESPUESTA_ESTADO))
                            break;
                    //}
                     */
                }
                intentos--;
            }

            if (intentos > 0)
            {
                intentos = maxIntentos;

                // PASSWORD
                _errorEstado = "(S02 PASS)";
                _numeroTrama++;
                while (intentos > 0)
                {
                    _errorEstado = "(S02 PASS)";
                    if (EnviarTrama(CMD_ENVIAR_PASSWORD, DatosPassword()))
                    {
                        sb = RecibirDatos(_timeoutDefault, 300);
                        if (!RecibidoIsError(sb, false, false, 0))
                        //if (ComprobarACK(sb)) // Pero como no recibo mensaje de respuesto no compruebo CHECHSUM ni envío ACK/NACK
                        //{
                            _numeroTrama++;
                            _errorEstado = "(S03 PASS/SESION)";
                            if (EnviarTrama(CMD_ESTADO))
                            {
                                sb = RecibirDatos(_timeoutDefault, 300);
                                if (!RecibidoIsError(sb, true, true, CMD_RESPUESTA_ESTADO))
                                //if (ComprobarACK(sb) && ComprobarChecksumYResponder(sb) && ComprobarSesion(sb))
                                    break;
                            }
                            _numeroTrama--;
                        //}
                    }
                    intentos--;
                }

                if (intentos > 0)
                {
                    intentos = maxIntentos;

                    ////CONTADORES
                    //// Voy a obtener contadores de entrada, salida y cajón. si esta lectura falla
                    //// dará error, si no falla se intentará leer más contadores pero se considerará 
                    //// una lectura correcta. Los contadores de billetes y pagos manuales que fallen
                    //// se mostrarán con valor -1
                    _errorEstado = "(S04 CONTA)";
                    _numeroTrama++;

                    StringBuilder sbConta = null;
                    sbConta = ComunicacionContadores(maxIntentos, 1, 3);
                    if (sbConta != null)
                    {
                        InfoContadores info = new InfoContadores();

                        info.Entradas = GetDatoContador(sbConta, 1);
                        info.Salidas = GetDatoContador(sbConta, 2);
                        info.Cajon = GetDatoContador(sbConta, 3);

                        _errorEstado = "(S05 CONTA B5)";
                        _numeroTrama++;
                        info.Billetes5 = GetDatoContador(ComunicacionContadores(maxIntentos, 24, 1), 1);
                        
                        _errorEstado = "(S06 CONTA B10)";
                        _numeroTrama++;
                        info.Billetes10 = GetDatoContador(ComunicacionContadores(maxIntentos, 35, 1), 1);
                        
                        _errorEstado = "(S07 CONTA B20)";
                        _numeroTrama++;
                        info.Billetes20 = GetDatoContador(ComunicacionContadores(maxIntentos, 84, 1), 1);
                        
                        _errorEstado = "(S08 CONTA B50)";
                        _numeroTrama++;
                        info.Billetes50 = GetDatoContador(ComunicacionContadores(maxIntentos, 97, 1), 1);
                        
                        info.AutoSetBilletes();

                        _errorEstado = "(S09 CONTA PM)";
                        _numeroTrama++;
                        info.PagosManuales = GetDatoContador(ComunicacionContadores(maxIntentos, 71, 1), 1);

                        _errorEstado = "(S10 CONTA CS20)";
                        _numeroTrama++;
                        // 111 Créditos jugados (registrado en paralelo en E2p de BBOX o CPU, no en CS4)
                        // 112 Créditos premiados (registrado en paralelo en E2p de BBOX o CPU, no en CS4)
                        sbConta = ComunicacionContadores(maxIntentos, 111, 2);
                        info.Auxiliar3 = GetDatoContador(sbConta, 1);
                        info.Auxiliar4 = GetDatoContador(sbConta, 2);

                        return info;
                    }
                }
            }

            return null;
        }

        private int GetDatoContador(StringBuilder datos, int indice)
        {
            // Formato de trama Cirsa/Unidesa
            // STX | SD | SO | NM | CMD | ND | D1 | ... | Dn | CHK | ETX
            //
            // ND:número de datos (debe ser 3+5*num.conta, para 3 contadores sería 18)
            // El primer byte es el grupo al que pertenecen los contadores.
            // El segundo byte es el primer contador que envía la máquina.
            // El tercer byte es el número de contadores recibidos.
            // Del cuarto byte en adelante, están los contadores, en grupos de cinco bytes. El primer
            // byte del grupo contiene el formato del contador, y los otros cuatro bytes contienen elvalor del contador

            int n = -1;
            int pos = 9 + ((indice - 1) * 5);
            if (datos != null && datos.Length > (pos + 4))
            {
                byte formato = (byte)datos[pos];
                byte[] b = new byte[4];
                b[3] = (byte)datos[pos + 4];
                b[2] = (byte)datos[pos + 3];
                b[1] = (byte)datos[pos + 2];
                b[0] = (byte)datos[pos + 1];
                n = BitConverter.ToInt32(b, 0);
                if (formato == 0x40)
                    n = n / 100;
            }
            return n;
        }
    }
}
