using System;
using System.Collections.Generic;
using System.Text;
using GEO_DROID.Resources.Lib.Comunicacion;

namespace BLL.LeerInfoMaquina
{
    class ProtocoloSAS : Protocolo
    {
        /* 
         * La implementación actual solicita contadores uno a uno. 
         * El protocolo SAS tiene comandos que devuelven conjuntos de contadores:
         * - private const byte CMD_METERS_11-15 = 0x19;
         * - private const byte CMD_TOTAL_BILL_METERS = 0x1E;
         * 
         * 
         * El comando 0x80 es necesario para iniciar las comnicaciones.
         * El comando 0x81 es necesario entre tramas iguales, para no recibir lo mismo que nos envió la máquina la última vez
         * 
         * 
         * ¿Cómo configurar la máquina para hacer la lectura?
         * Ajustes>>Maquina>>Sistemas online
         *   Configurar el com4 con la opción SAS.
         *   El com4 es el conector que en la máquina es macho y está situado junto a otro db9 hembea
         */

        //                                       CODE =  00 01 02 03  04  05  06   07   08    09    0A     0B  0C   0D   0E    0F    10     11     12     13      14      15      16      17 18 19  1A  1B    1C     1D    1E    1F 
        private readonly decimal[] DENOMINATION_TABLE = { 0, 1, 5, 10, 25, 50, 100, 500, 1000, 2000, 10000, 20, 200, 250, 2500, 5000, 20000, 25000, 50000, 100000, 200000, 250000, 500000, 2, 3, 15, 40, 0.5m, 0.25m, 0.2m, 0.1m, 0.05m};

        private const byte CMD_ID = 0x1F;
        private const byte CMD_SELECTED_METERS = 0x2F;
        private const byte CMD_EXTENDED_METERS = 0x6F;

        private const byte CMD_TOTAL_CANCELLED_CREDITS = 0x10;
        private const byte CMD_TOTAL_COIN_IN = 0x11;    
        private const byte CMD_TOTAL_COIN_OUT = 0x12;
        private const byte CMD_TOTAL_DROP = 0x13;
        private const byte CMD_TOTAL_JACKPOT = 0x14;
        private const byte CMD_GAMES_PLAYED = 0x15;
        private const byte CMD_GAMES_WON = 0x16;
        private const byte CMD_GAMES_LOST = 0x17;

        private const byte CMD_1_BILLS = 0x31;
        private const byte CMD_2_BILLS = 0x32;
        private const byte CMD_5_BILLS = 0x33;
        private const byte CMD_10_BILLS = 0x34;
        private const byte CMD_20_BILLS = 0x35;
        private const byte CMD_50_BILLS = 0x36;
        private const byte CMD_100_BILLS = 0x37;

        // GAMING MACHINE AFT TRANSFER STATUS CODES
        private const byte CMD_80 = 0x80;
        private const byte CMD_81 = 0x81;

        // METER CODE VALUES
        private const byte COUNTER_TOTAL_COIN_IN_CREDITS = 0x00; //Total coin in credits
        private const byte COUNTER_TOTAL_COIN_OUT_CREDITS = 0x01; //Total coin out credits
        private const byte COUNTER_TOTAL_JACKPOT_CREDITS = 0x02; //Total jackpot credits
        private const byte COUNTER_TOTAL_HAND_PAID_CANCELLED_CREDITS = 0x03; //Total hand paid cancelled credits
        protected const byte COUNTER_TOTAL_CANCELLED_CREDITS = 0x04; //Total cancelled credits
        private const byte COUNTER_TOTAL_CREDITS_FROM_COIN_ACCEPTOR = 0x08; //Total credits from coins acceptor
        private const byte COUNTER_TOTAL_CREDITS_PAID_FROM_HOPPER = 0x09; //Total credits paid from hopper
        private const byte COUNTER_TOTAL_CREDITS_FROM_COINS_TO_DROP = 0x0A; //Total credits from coins to drop
        private const byte COUNTER_TOTAL_CREDITS_FROM_BILLS_ACCEPTED = 0x0B; //Total credits from bills accepted
        private const byte COUNTER_TOTAL_TICKET_IN = 0x15; //Total ticket in
        private const byte COUNTER_TOTAL_TICKET_OUT = 0x16; //Total ticket out
        protected const byte COUNTER_TOTAL_DROP = 0x24; //Total drop, including but not limited to coins to drop, bills to drop, tickets to drop, and electronic in (credits)
        private const byte COUNTER_TOTAL_NUMBER_OF_5_BILLS_ACCEPTED = 0x42; //Total number of $5.00 bills accepted
        private const byte COUNTER_TOTAL_NUMBER_OF_10_BILLS_ACCEPTED = 0x43; //Total number of $10.00 bills accepted
        private const byte COUNTER_TOTAL_NUMBER_OF_20_BILLS_ACCEPTED = 0x44; //Total number of $20.00 bills accepted
        private const byte COUNTER_TOTAL_NUMBER_OF_50_BILLS_ACCEPTED = 0x46; //Total number of $50.00 bills accepted
        private const byte COUNTER_TOTAL_CREDITS_FROM_BILLS_DISPENSED = 0x6E; //Total credits from bills dispensed from hopper
        private const byte COUNTER_TOTAL_NUMBER_OF_5_BILLS_DISPENSED = 0x71; //Total number of $5.00 bills dispensed from hopper
        private const byte COUNTER_TOTAL_NUMBER_OF_10_BILLS_DISPENSED = 0x72; //Total number of $10.00 bills dispensed from hopper
        private const byte COUNTER_TOTAL_NUMBER_OF_20_BILLS_DISPENSED = 0x73; //Total number of $20.00 bills dispensed from hopper
        private const byte COUNTER_TOTAL_NUMBER_OF_50_BILLS_DISPENSED = 0x74; //Total number of $50.00 bills dispensed from hopper

        protected byte COUNTER_ENTRADAS = COUNTER_TOTAL_COIN_IN_CREDITS;
        protected byte COUNTER_SALIDAS = COUNTER_TOTAL_COIN_OUT_CREDITS;

        const int MIN_NUMERO_BYTES_DATOS = 6; //Por defecto una trama tiene 6 bytes de datos y 2 de checksum
        const int NUMERO_BYTES_DATOS_1F = 22; //Longitud de datos de la trama del comando 1F
        const int NUMERO_BYTES_CHECKSUM = 2;

        const int TRAMA_NMAQUINA = 0;
        const int TRAMA_COMANDO = 1;
        const int TRAMA_LEN = 2;

        private const int TIMEOUT = 500;
        private const int TIMEOUT_2 = 100;

        //string _password = PASSWORD;
        int _timeoutDefault = TIMEOUT;
        InfoContadores _info;
        char _numeroMaquina = (char)0x01;
        int _denominationCode = 1; // Denomination 1 sería el factor neutro, ya que su valor asociado es 1 y al multiplicar nos quedaría el mismo valor leído

        public override byte ConfiguracionPuertoSerie
        {
            get { return (byte)0x55; } // Baudrate=19200, Parity=COM_PARITY_WAKEUP
        }

        public ProtocoloSAS(IComunicacion com, IFiltroTrama filtroTrama, string password, int timeout)
            : base(com, filtroTrama)
        {
            //if (password != null && password.Length > 0)
            //    _password = password;
            if (timeout > 0)
                _timeoutDefault = timeout;
        }

        protected override char CrearChecksum(StringBuilder sb)
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

        protected override bool ComprobarChecksum(StringBuilder sb)
        {
            
            if (sb != null && sb.Length > 0)
            {
                if (sb.Length >= (MIN_NUMERO_BYTES_DATOS + NUMERO_BYTES_CHECKSUM))
                {
                    int nbd = MIN_NUMERO_BYTES_DATOS;
                    if (sb[TRAMA_COMANDO] == CMD_ID)
                    {
                        nbd = NUMERO_BYTES_DATOS_1F;
                    }
                    else if (sb[TRAMA_COMANDO] == CMD_SELECTED_METERS || sb[TRAMA_COMANDO] == CMD_EXTENDED_METERS) //Este comando nos indica la longitud de datos en el tercer byte
                    {
                        int tmpNbd = (int)sb[TRAMA_LEN] + 3; //id, comando, longitud (3)
                        if (tmpNbd < sb.Length)
                            nbd = tmpNbd;
                    }
                    StringBuilder sbDatos = new StringBuilder();
                    sbDatos.Append(sb);
                    sbDatos.Remove(nbd, sbDatos.Length - nbd);
                    char checksum = CrearChecksum(sbDatos);

                    byte[] b = new byte[NUMERO_BYTES_CHECKSUM];
                    b[0] = (byte)sb[nbd];
                    b[1] = (byte)sb[nbd + 1];
                    char checksumRecibido = BitConverter.ToChar(b, 0);
                    return (checksum == checksumRecibido);
                }
            }
            return false;
        }

        private bool IsComandoControl(byte comando)
        {
            return (comando == CMD_80 || comando == CMD_81);
        }

        private bool SendCRC(byte comando)
        {
            // False para comandos 80, 81 e identificación
            return !(comando == CMD_80 || comando == CMD_81 || comando == CMD_ID);
        }
        
        private bool AplicarDenomination(byte comando)
        {
            return false;// (comando == CMD_TOTAL_COIN_IN || comando == CMD_TOTAL_COIN_OUT || comando == CMD_TOTAL_DROP || comando == CMD_TOTAL_JACKPOT);
        }

        protected override StringBuilder MontarTrama(byte comando, byte[] datos) //throws CheckSumException
        {
            const int LEN_GAME_NUMBER = 2; //2 bytes
            StringBuilder sbTemp = new StringBuilder();

            if (!IsComandoControl(comando))
                sbTemp.Append(_numeroMaquina); // Número de máquina 

            //if (comando == CMD_TOTAL_COIN_IN || comando == CMD_TOTAL_COIN_OUT)
            //{   //Multi-denom preamble
            //    //int numMeters = (datos.Length > MAX_METERS) ? MAX_METERS : datos.Length;
            //    //numMeters = numMeters + 3;

            //    sbTemp.Append((char)0xB0); // Comando
            //    sbTemp.Append((char)0x02); // Len
            //    sbTemp.Append((char)0x01); // Denom
            //}
            
            sbTemp.Append((char)comando); // Comando
            if (datos != null)
            {
                int numMeters = datos.Length;
                
                sbTemp.Append((char)(LEN_GAME_NUMBER + numMeters)); // Longitud de datos
                sbTemp.Append((char)0x00); // game number 2bytes bcd
                sbTemp.Append((char)0x00); // game number 2bytes bcd
                for (int i = 0; i < numMeters; i++)
                    sbTemp.Append((char)datos[i]); //meters
            }
            if (SendCRC(comando))
            {
                char crc = CrearChecksum(sbTemp);
                byte[] bytesCrc = BitConverter.GetBytes(crc);
                sbTemp.Append((char)bytesCrc[0]); //crc
                sbTemp.Append((char)bytesCrc[1]); //crc
            }
            return sbTemp;
        }

        public override InfoContadores LeerContadores(IProgressCallback callback)
        {
            ProtocoloTecnausaV2 pt = new ProtocoloTecnausaV2(_com);

            _errorEstado = "(S0.0.GETID)";
            _numeroMaquina = GetNumeroMaquina();
            if (_numeroMaquina > 0)
            {
                _errorEstado = "(S0.1.INI)";
                EnviarYRecibirComando(pt, CMD_80, null, 1, callback); // Si no iniciamos las comunicaciones con este comando no conseguimos obtener respuesta de la máquina
                if (string.IsNullOrEmpty(_error))
                {
                    _errorEstado = "(S0.2.START)";
                    LeerIdentificacion(pt, 2, callback);
                    if (string.IsNullOrEmpty(_error))
                    {
                        _info = new InfoContadores();
                        _info.Buffer = new StringBuilder();
                        _info.Auxiliar1 = _denominationCode;

                        _errorEstado = "(S0.3.COUNTERS)";

                        EnviarYRecibirComando(pt, CMD_81, null, 3, callback);
                        LeerContadoresExtendidos(pt, 4, callback);

                        EnviarYRecibirComando(pt, CMD_81, null, 5, callback);
                        LeerContadoresMulti(pt, 6, callback);

                        // Si no hemos recibido entradas y salidas, reintentamos con los comandos básicos.
                        // Esto se hace pensando en que el comando CMD_EXTENDED_METERS no esté implementado en la máquina
                        if (_info.Entradas < 0 && _info.Salidas < 0)
                        {
                            _info.Auxiliar1 += 1000; //Sumamos 1000, para identificar que la lectura ha sido por comandos básicos

                            
                            //TODO: Establecer referencias contador-comando para evitar modificación de condiciones
                            if (COUNTER_SALIDAS == COUNTER_TOTAL_COIN_OUT_CREDITS)
                                LeerContadores(pt, CMD_TOTAL_COIN_OUT, InfoContadores.Contador.Salidas, 7, callback);
                            else if (COUNTER_SALIDAS == COUNTER_TOTAL_CANCELLED_CREDITS)
                                LeerContadores(pt, CMD_TOTAL_CANCELLED_CREDITS, InfoContadores.Contador.Salidas, 7, callback);
                            
                            if (COUNTER_ENTRADAS == COUNTER_TOTAL_COIN_IN_CREDITS)
                                LeerContadores(pt, CMD_TOTAL_COIN_IN, InfoContadores.Contador.Entradas, 8, callback);
                            else if (COUNTER_ENTRADAS == COUNTER_TOTAL_DROP)
                                LeerContadores(pt, CMD_TOTAL_DROP, InfoContadores.Contador.Entradas, 8, callback);
                        }
                    }
                }
            }
            else
            {
                _error = "Identificador de máquina no recibido";
            }
            return _info;
        }

        private char GetNumeroMaquina()
        {
            /* 
             * La máquina está enviando su identificador de forma continua, aunque puede haber pausas de 5 segundos
             * después de realizar alguna comuniación. 
             * Esto lo tendremos en cuenta al obtener su identificador.
             */
            // Obtener identificador de la máquina
            char numMaq = (char)0x00;
            const int MAX_INTENTOS = 3;

            _com.LimpiarBuffer();
            for (int i = 1; (i <= MAX_INTENTOS) && (numMaq == 0); i++)
            {
                //Esperaremos que en el buffer se acumule un segundo de datos y luego hacemos una lectura sin espera
                System.Threading.Thread.Sleep(1000 * i); // Incrementamos el tiempo de espera en cada reintento
                StringBuilder sb = RecibirDatos(0, 0);
                for (int j = 0; j < sb.Length - 2; j++)
                {
                    if ((sb[j] == sb[j + 1] && sb[j] == sb[j + 2])) // Si encontramos 3 coincidencias damos por identificada la máquina
                        numMaq = sb[j];
                }
            }
            return numMaq;
        }

        private StringBuilder EnviarYRecibirComando(ProtocoloTecnausaV2 pt, byte comando, byte[] datos, int estado, IProgressCallback callback)
        {
            const int MAX_INTENTOS = 1;
            StringBuilder sbRespuesta = null;
            StringBuilder sb;
            int numeroIntentos = MAX_INTENTOS;
            
            if (IsComandoControl(comando))
                numeroIntentos = 1;

            if (pt != null)
            {
                for (int i = 0; i < numeroIntentos; i++)
                {
                    _errorEstado = "(S" + estado + "." + (int)comando + "." + i + " WAKEUP)";
                    if (pt.EnviarTrama(ProtocoloTecnausaV2.CMD_SAS_WAKEUP))
                    {
                        sb = RecibirDatos(_timeoutDefault);
                        _errorEstado = "(S" + estado + "." + (int)comando + "." + i + " SEND COMMAND)";
                        if (EnviarTrama(comando, datos) && !IsComandoControl(comando)) //Los comandos de control no obtienen respuesta
                        {
                            _errorEstado = "(S" + estado + "." + (int)comando + "." + i + " RECV COMMAND)";
                            sb = RecibirDatos(_timeoutDefault, TIMEOUT_2);
                            if (!RecibidoIsError(sb, comando))
                            {
                                _errorEstado = "";
                                sbRespuesta = sb;
                                break;
                            }
                        }
                    }
                }
            }
            return sbRespuesta;
        }

        private void LeerIdentificacion(ProtocoloTecnausaV2 pt, int estado, IProgressCallback callback)
        {
            StringBuilder sb;
            sb = EnviarYRecibirComando(pt, CMD_ID, null, estado, callback);
            if (sb != null)
                _denominationCode = GetDenominationCode(sb);
        }

        private decimal GetFactor(byte comando)
        {
            decimal factor = 1; //factor de unidad (ver DENOMINATION TABLE)
            if (AplicarDenomination(comando))
                factor = DENOMINATION_TABLE[_denominationCode];
            return factor;
        }

        private void LeerContadores(ProtocoloTecnausaV2 pt, byte comando, InfoContadores.Contador contador, int estado, IProgressCallback callback)
        {
            StringBuilder sb;
            sb = EnviarYRecibirComando(pt, comando, null, estado, callback);
            if (sb != null)
            {
                decimal factor = GetFactor(comando); 
                _info[contador] = (int)(GetDato(sb) * factor);
            }
        }

        private void LeerContadoresMulti(ProtocoloTecnausaV2 pt, int estado, IProgressCallback callback)
        {
            StringBuilder sb;
            // CMD_SELECTED_METERS - MAX METERS = 10
            byte[] selectedMeters = new byte[] { COUNTER_ENTRADAS, COUNTER_SALIDAS, 0x24, 0x04, 0x42, 0x43, 0x44, 0x46, 0x0A, 0x0B   /*0x08, 0x09, 0x0B, 0x6E, 0x15, 0x16*/
                //COUNTER_TOTAL_COIN_IN_CREDITS 
                //, COUNTER_TOTAL_NUMBER_OF_5_BILLS_ACCEPTED 
                //, COUNTER_TOTAL_NUMBER_OF_10_BILLS_ACCEPTED 
                //, COUNTER_TOTAL_NUMBER_OF_20_BILLS_ACCEPTED 
                //, COUNTER_TOTAL_NUMBER_OF_50_BILLS_ACCEPTED 
            };
            sb = EnviarYRecibirComando(pt, CMD_SELECTED_METERS, selectedMeters, estado, callback);
            if (sb != null)
            {
                decimal factorEntradas = GetFactor(CMD_TOTAL_COIN_IN);
                decimal factorSalidas = GetFactor(CMD_TOTAL_COIN_OUT);
                //_info[InfoContadores.Contador.Entradas] = (int)((decimal)GetDato(sb, 0) * factorEntradas);
                //_info[InfoContadores.Contador.Salidas] = (int)((decimal)GetDato(sb, 1) * factorSalidas);
                _info[InfoContadores.Contador.Billetes5] = (int)((decimal)GetDato(sb, 4));
                _info[InfoContadores.Contador.Billetes10] = (int)((decimal)GetDato(sb, 5));
                _info[InfoContadores.Contador.Billetes20] = (int)((decimal)GetDato(sb, 6));
                _info[InfoContadores.Contador.Billetes50] = (int)((decimal)GetDato(sb, 7));
            }
        }

        private void LeerContadoresExtendidos(ProtocoloTecnausaV2 pt, int estado, IProgressCallback callback)
        {
            StringBuilder sb;
            // CMD_EXTENDED_METERS - MAX METERS = 12 (24 bytes)
            byte[] selectedMeters = new byte[] { 
                COUNTER_ENTRADAS, 0x00
                , COUNTER_SALIDAS, 0x00
                , COUNTER_TOTAL_HAND_PAID_CANCELLED_CREDITS, 0x00
                , COUNTER_TOTAL_CANCELLED_CREDITS, 0x00
                // Monedas
                , COUNTER_TOTAL_CREDITS_FROM_COIN_ACCEPTOR, 0x00
                , COUNTER_TOTAL_CREDITS_PAID_FROM_HOPPER, 0x00
                // Billetes
                , COUNTER_TOTAL_CREDITS_FROM_BILLS_ACCEPTED, 0x00
                , COUNTER_TOTAL_CREDITS_FROM_BILLS_DISPENSED, 0x00
                // Tickets
                , COUNTER_TOTAL_TICKET_IN, 0x00
                , COUNTER_TOTAL_TICKET_OUT, 0x00
            };
            sb = EnviarYRecibirComando(pt, CMD_EXTENDED_METERS, selectedMeters, estado, callback);
            if (sb != null)
            {
                Dictionary<byte, long> d = GetDatosExtendidos(sb);
                if (d.ContainsKey(COUNTER_ENTRADAS))
                    _info[InfoContadores.Contador.Entradas] = d[COUNTER_ENTRADAS];
                if (d.ContainsKey(COUNTER_SALIDAS))
                    _info[InfoContadores.Contador.Salidas] = d[COUNTER_SALIDAS];
                if (d.ContainsKey(COUNTER_TOTAL_HAND_PAID_CANCELLED_CREDITS))
                    _info[InfoContadores.Contador.PagosManuales] = d[COUNTER_TOTAL_HAND_PAID_CANCELLED_CREDITS];
                if (d.ContainsKey(COUNTER_TOTAL_CANCELLED_CREDITS))
                    _info[InfoContadores.Contador.Auxiliar2] = d[COUNTER_TOTAL_CANCELLED_CREDITS];

                long monedasBilletesIN = 0;
                long monedasBilletesOUT = 0;
                if (d.ContainsKey(COUNTER_TOTAL_CREDITS_FROM_COIN_ACCEPTOR))
                    monedasBilletesIN += d[COUNTER_TOTAL_CREDITS_FROM_COIN_ACCEPTOR];
                if (d.ContainsKey(COUNTER_TOTAL_CREDITS_PAID_FROM_HOPPER))
                    monedasBilletesOUT += d[COUNTER_TOTAL_CREDITS_PAID_FROM_HOPPER];

                if (d.ContainsKey(COUNTER_TOTAL_CREDITS_FROM_BILLS_ACCEPTED))
                    monedasBilletesIN += d[COUNTER_TOTAL_CREDITS_FROM_BILLS_ACCEPTED];
                if (d.ContainsKey(COUNTER_TOTAL_CREDITS_FROM_BILLS_DISPENSED))
                    monedasBilletesOUT += d[COUNTER_TOTAL_CREDITS_FROM_BILLS_DISPENSED];

                if (d.ContainsKey(COUNTER_TOTAL_TICKET_IN))
                    monedasBilletesIN += d[COUNTER_TOTAL_TICKET_IN];
                if (d.ContainsKey(COUNTER_TOTAL_TICKET_OUT))
                    monedasBilletesOUT += d[COUNTER_TOTAL_TICKET_OUT];

                if (d.ContainsKey(COUNTER_TOTAL_HAND_PAID_CANCELLED_CREDITS))
                    monedasBilletesOUT += d[COUNTER_TOTAL_HAND_PAID_CANCELLED_CREDITS];

                _info[InfoContadores.Contador.Auxiliar3] = monedasBilletesIN;
                _info[InfoContadores.Contador.Auxiliar4] = monedasBilletesOUT;
            }
        }

        private Dictionary<byte, long> GetDatosExtendidos(StringBuilder sb)
        {
            Dictionary<byte, long> d = new Dictionary<byte, long>();

            const int OFFSET_METERS = 5; //Sería 5, Pongo 6 para obtener byte menos significativo

            int index = OFFSET_METERS;
            byte meterCode;
            byte meterSize;
            long meterValue;
            while (index < (sb.Length - 3)) // Code 2 bytes; Size 1 byte
            {
                meterCode = (byte)sb[index++]; //byte de menor peso del MeterCode
                index++; //byte de mayor peso del MeterCode. Lo ignoramos
                meterSize = (byte)sb[index++];
                meterValue = 0;
                // Comprobamos si la longitud de los datos es mayor que la longitud que queremos leer, para no provocar excepción
                if ((index + meterSize) <= sb.Length)
                {
                    for (int i = 0; i < meterSize; i++)
                    {
                        char bcd = sb[index + i];
                        meterValue *= 100;
                        meterValue += (10 * (bcd >> 4)); //high
                        meterValue += (bcd & 0x0F); //low
                    }
                    index += meterSize;
                    d[meterCode] = meterValue;
                }
            }

            return d;
        }

        private int GetIntFromBCD(StringBuilder sb, int posIni)
        {
            const int LEN_DATA = 4; // 4 bytes = int32
            int ret = -1;
            if (sb != null && sb.Length >= (posIni + LEN_DATA))
            {
                ret = 0;
                for (int i = 0; i < 4; i++)
                {
                    char bcd = sb[posIni + i];
                    ret *= 100;
                    ret += (10 * (bcd >> 4)); //high
                    ret += (bcd & 0x0F); //low
                }
            }
            return ret;
        }

        private int GetDato(StringBuilder sb)
        {
            const int LEN_TRAMA = 8;
            int ret = -1;
            if (sb != null && sb.Length > (LEN_TRAMA-1))
            {
                ret = GetIntFromBCD(sb, 2);
            }
            return ret;
        }
        
        private int GetDato(StringBuilder sb, int indexMeter)
        {
            const int OFFSET_METERS = 5;
            const int LEN_CODE_METERS = 1;
            const int LEN_VALUE_METERS = 4;
            const int LEN_METERS = LEN_CODE_METERS + LEN_VALUE_METERS;
            return GetIntFromBCD(sb, OFFSET_METERS + (indexMeter * LEN_METERS) + 1);
        }

        private int GetDenominationCode(StringBuilder sb)
        {
            const int POS_DENOMINATION = 7;
            int ret = -1;
            if (sb != null && sb.Length > (POS_DENOMINATION - 1))
                ret = sb[POS_DENOMINATION];
            
            // Comprobamos que el índice esté dentro del rango de valores y además, si denomination == 0 == None, dejamos el factor neutro para no afectar a la toma de datos
            if (ret < 1 || ret >= DENOMINATION_TABLE.Length)
                ret = 1;
            
            return ret;
        }

        /// <summary>
        /// Busca el inicio de trama esperado: [número de máquina][comando]
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="comandoEsperado"></param>
        /// <returns></returns>
        private bool EsInicioTrama(StringBuilder sb, byte comandoEsperado)
        {
            if (sb.Length > 1)
            {
                if (sb[TRAMA_NMAQUINA] == _numeroMaquina && sb[TRAMA_COMANDO] == (char)comandoEsperado)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Comprueba si la trama recibida esta completa
        /// </summary>
        /// <param name="sb"></param>
        /// <returns></returns>
        private bool TramaCompleta(StringBuilder sb)
        {
            bool completa = false;
            if (sb.Length > MIN_NUMERO_BYTES_DATOS )
            {
                completa = true;

                // Casos especiales
                if (sb[TRAMA_COMANDO] == CMD_ID)
                {
                    if (NUMERO_BYTES_DATOS_1F + NUMERO_BYTES_CHECKSUM  <= sb.Length)
                        completa = true;
                }
                else if (sb[TRAMA_COMANDO] == CMD_SELECTED_METERS || sb[TRAMA_COMANDO] == CMD_EXTENDED_METERS)
                {
                    if ((int)sb[TRAMA_LEN] + 3 + NUMERO_BYTES_CHECKSUM <= sb.Length)
                        completa = true;
                }
            }
            return completa;
        }

        private bool RecibidoIsError(StringBuilder recibido, byte comandoEsperado)
        {
            if (recibido != null && recibido.Length > 0)
            {
                // Localizamos el inicio de trama
                for (int i = 0; i < recibido.Length; i++)
                {
                    if (EsInicioTrama(recibido, comandoEsperado))
                    {
                        break;
                    }
                    else
                    {
                        recibido.Remove(i, 1);
                        i--;
                    }
                }
                if (recibido.Length > 0)
                {
                    if (TramaCompleta(recibido))
                    {
                        if (ComprobarChecksum(recibido))
                        {
                            return false;
                        }
                        else
                        {
                            _error = "Checksum error";
                        }
                    }
                    else
                    {
                        _error = "Trama recibida incompleta";
                    }
                }
                else
                {
                    _error = "Trama no válida";
                }
            }
            else
            {
                _error = "Recibido vacío";
            }
            return true;
        }

        #region *********** TEST **************
        private StringBuilder GetTrama1F_Test()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append((char)0x01);
            sb.Append((char)0x1F);
            sb.Append((char)0x55);
            sb.Append((char)0x44);
            sb.Append((char)0x00);
            sb.Append((char)0x00);
            sb.Append((char)0x00);
            sb.Append((char)0x02);
            sb.Append((char)0x1E);
            sb.Append((char)0x00);
            sb.Append((char)0xFF);
            sb.Append((char)0xFF);
            sb.Append((char)0x20);
            sb.Append((char)0x54);
            sb.Append((char)0x42);
            sb.Append((char)0x44);
            sb.Append((char)0x20);
            sb.Append((char)0x20);
            sb.Append((char)0x38);
            sb.Append((char)0x39);
            sb.Append((char)0x35);
            sb.Append((char)0x39);
            sb.Append((char)0x68);
            sb.Append((char)0x52);
            return sb;
        }

        private StringBuilder GetTrama1F_Test2()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append((char)0x01);
            sb.Append((char)0x1F);
            sb.Append((char)0x41);
            sb.Append((char)0x4D);
            sb.Append((char)0x30);
            sb.Append((char)0x30);
            sb.Append((char)0x30);
            sb.Append((char)0x01);
            sb.Append((char)0xFF);
            sb.Append((char)0x00);
            sb.Append((char)0x00);
            sb.Append((char)0x00);
            sb.Append((char)0x30);
            sb.Append((char)0x30);
            sb.Append((char)0x30);
            sb.Append((char)0x30);
            sb.Append((char)0x30);
            sb.Append((char)0x30);
            sb.Append((char)0x39);
            sb.Append((char)0x36);
            sb.Append((char)0x38);
            sb.Append((char)0x30);
            sb.Append((char)0x9E);
            sb.Append((char)0xB6);
            return sb;
        }

        private StringBuilder GetTrama2F_Test()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append((char)0x01);
            sb.Append((char)0x2F);
            sb.Append((char)0x34);
            sb.Append((char)0x00);
            sb.Append((char)0x00);
            
            sb.Append((char)0x00);
            sb.Append((char)0x00);
            sb.Append((char)0x02);
            sb.Append((char)0x60);
            sb.Append((char)0x50);

            sb.Append((char)0x01);
            sb.Append((char)0x00);
            sb.Append((char)0x00);
            sb.Append((char)0x97);
            sb.Append((char)0x59);

            sb.Append((char)0x24);
            sb.Append((char)0x00);
            sb.Append((char)0x00);
            sb.Append((char)0x60);
            sb.Append((char)0x00);

            sb.Append((char)0x04);
            sb.Append((char)0x00);
            sb.Append((char)0x00);
            sb.Append((char)0x00);
            sb.Append((char)0x00);

            sb.Append((char)0x42);
            sb.Append((char)0x00);
            sb.Append((char)0x00);
            sb.Append((char)0x00);
            sb.Append((char)0x00);

            sb.Append((char)0x43);
            sb.Append((char)0x00);
            sb.Append((char)0x00);
            sb.Append((char)0x00);
            sb.Append((char)0x00);

            sb.Append((char)0x44);
            sb.Append((char)0x00);
            sb.Append((char)0x00);
            sb.Append((char)0x00);
            sb.Append((char)0x03);

            sb.Append((char)0x46);
            sb.Append((char)0x00);
            sb.Append((char)0x00);
            sb.Append((char)0x00);
            sb.Append((char)0x00);

            sb.Append((char)0x0A);
            sb.Append((char)0x00);
            sb.Append((char)0x00);
            sb.Append((char)0x00);
            sb.Append((char)0x00);

            sb.Append((char)0x0B);
            sb.Append((char)0x00);
            sb.Append((char)0x00);
            sb.Append((char)0x60);
            sb.Append((char)0x00);

            sb.Append((char)0xD4);
            sb.Append((char)0xAF);

            return sb;
        }
        #endregion
    }
}
