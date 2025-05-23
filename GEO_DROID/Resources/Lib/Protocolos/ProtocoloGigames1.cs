using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using GEO_DROID.Resources.Lib.Comunicacion;

namespace BLL.LeerInfoMaquina
{
    class ProtocoloGigames1 : Protocolo
    {
        // Detalles del protocolo:
        // -----------------------
        // Comunicacioón con puerto serie: 9600 8N1
        // Trama delimitada por BYTE DE INICIO y BYTE DE FIN
        // En caso de encontrarnos con BYTE DE INICIO o BYTE DE FIN en los datos, se reemplaza por BYTE DE CONTROL + byte coincidencite negado
        // Los datos compuestos (formados por más de un byte) deberán enviarse como big-endian
        // Más detalles en la especificación del protocolo....

        // Comandos internos a la clase, para controlar la trama a montar
        private const int CMD_ACK = 0x01;
        private const int CMD_NACK = 0x02;
        private const int CMD_INICIO_SESION = 0x03;
        private const int CMD_CERRAR_SESION = 0x04;
        private const int CMD_CONTADORES = 0x05;
        private const int CMD_RESET = 0x06;

        // Constantes especificadas en el protocolo GIGAMES
        // Las constantes de tipo int tienen los bytes invertidos por especificación del protocolo
        private const byte BYTE_INICIO = (byte)0x10;
        private const byte BYTE_FIN = (byte)0x11;
        private const byte BYTE_CONTROL = (byte)0x12;
        
        private const int DBYTE_GAPID1 = 0xEA00;
        private const int DBYTE_GAPID2 = 0x5A00;
        private const int DBYTE_MSG_DATOS = 0x0300;
        private const int DBYTE_MSG_COMANDO = 0x0500;

        private const int DBYTE_CMD_ACK = 0x0100;
        private const int DBYTE_CMD_NACK = 0x0200;
        private const int DBYTE_CMD_INICIO_SESION = 0x0500;
        private const int DBYTE_CMD_CERRAR_SESION = 0x1000;
        private const int DBYTE_CMD_CONTADORES = 0x1600;
        private const int DBYTE_CMD_RESET = 0x9A02;
        private const int DBYTE_CMD_GETSTATUS = 0x0100;

        private const string PASSWORD = "PASSWORD";

        int numeroMensaje = 1; // no se usa... pero hay que enviarlo
        string _password;
        int _timeout1 = 500;
        int _timeout2 = 200;
            
        public ProtocoloGigames1(IComunicacion com, IFiltroTrama filtroTrama, string password, int timeout)
            : base(com, filtroTrama)
        {
            if (timeout > 0)
                _timeout1 = timeout;
            if (password != null && password.Length > 0)
                _password = password;
            else
                _password = PASSWORD;
        }

        private StringBuilder Swap(int numero)
        {
            StringBuilder ret = new StringBuilder();
            ret.Append((char)(numero % 256)); 
            ret.Append((char)(numero / 256));
            return ret;
        }

        private StringBuilder NoSwap(int numero)
        {
            StringBuilder ret = new StringBuilder();
            ret.Append((char)(numero / 256));
            ret.Append((char)(numero % 256));
            return ret;
        }

        protected override StringBuilder MontarTrama(byte comando, byte[] datos)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append((char)BYTE_INICIO);   //Byte de inicio

            switch (comando)
            {
                case CMD_ACK:
                    // Trama ACK: 10 00 01 00 01 00 EA 00 5A 11
                    sb.Append(NoSwap(numeroMensaje));   //Nº de mensaje
                    sb.Append(Swap(DBYTE_CMD_ACK)); //Tipo commando: Commando cierre sesión, valor: 0x0010
                    break;
                case CMD_NACK:
                    // Trama NACK: 10 00 01 00 02 00 EA 00 5A 11
                    sb.Append(NoSwap(numeroMensaje));   //Nº de mensaje
                    sb.Append(Swap(DBYTE_CMD_NACK)); //Tipo commando: Commando cierre sesión, valor: 0x0010
                    break;
                case CMD_INICIO_SESION:
                    // Trama CMD_INICIO_SESION: 10 P A S S W O R D 00 01 00 05 00 05 00 EA 00 5A 11
                    sb.Append(new StringBuilder(_password));    //Clave: Inicialmente es "PASSWORD", como máximo son 8 bytes. Se puede cambiar con el comando CAMBIAR PASSWORD.
                    sb.Append(NoSwap(numeroMensaje));   //Nº de mensaje
                    sb.Append(Swap(DBYTE_CMD_INICIO_SESION)); //Tipo commando: Commando inicio sesión, valor: 0x0005
                    break;
                case CMD_CERRAR_SESION:
                    // Trama CMD_CERRAR_SESION: 10 00 01 00 (12) EF 00 EA 00 5A 11
                    sb.Append(NoSwap(numeroMensaje));   //Nº de mensaje
                    sb.Append(Swap(DBYTE_CMD_CERRAR_SESION)); //Tipo commando: Commando cierre sesión, valor: 0x0010
                    break;
                case CMD_CONTADORES:
                    // La trama de contadores debe enviarse asi: 
                    // 10 
                    // 00 
                    // 00 01 
                    // 00 01 
                    // 00 01 
                    // 00 00 
                    // 00 01 
                    // 00 16 00 05
                    // 00 ea 00 5a
                    // 11
                    sb.Append((char)0x00);   //byte adicional (reservado), no importa el valor.
                    for (int i = 0; i < datos.Length; i++) // int16: contadores desde
                        sb.Append((char)datos[i]);
                    for (int i = 0; i < datos.Length; i++) // int16: contadores hasta
                        sb.Append((char)datos[i]);
                    sb.Append(Swap(0x0100));   //Constante 1: entero de 16 bits sin signo.
                    sb.Append(Swap(0x0000));   //Entero de 16 bits con valor total=0
                    sb.Append(NoSwap(numeroMensaje));   //Nº de mensaje
                    sb.Append(Swap(DBYTE_CMD_CONTADORES)); //Tipo de comando: Recuperar contadores, valor: 0x0016
                    break;
                case CMD_RESET:
                    // Trama CMD_RESET: 10 00 01 02 9A 00 EA 00 5A 11
                    sb.Append(NoSwap(numeroMensaje));   //Nº de mensaje
                    sb.Append(Swap(DBYTE_CMD_RESET)); //Tipo commando: Commando cierre sesión, valor: 0x0010
                    break;
            }

            sb.Append(Swap(DBYTE_MSG_COMANDO));  //Tipo de mensaje: Mesaje comando, valor: 0x0005
            sb.Append(Swap(DBYTE_GAPID1));   //GAPID1
            sb.Append(Swap(DBYTE_GAPID2));   //GAPID2

            sb.Append((char)BYTE_FIN); //Byte de fin

            //Si encontramos un byte de INICIO O FIN, insertamos un CONTROL + NEGADO DEL BYTE
            for (int i = 1; i < sb.Length - 1; i++)
            {
                if (sb[i] == BYTE_INICIO || sb[i] == BYTE_FIN)
                {
                    sb[i] = (char)(~sb[i]);
                    sb.Insert(i, new char[] { (char)BYTE_CONTROL});
                }
            }

            return sb;
        }

        protected override char CrearChecksum(StringBuilder sb)
        {
            char checksum = (char)0x00;
            return checksum;
        }

        protected override bool ComprobarChecksum(StringBuilder sb)
        {
            return true;
        }

        private bool RecibidoIsError(StringBuilder recibido)
        {
            if (recibido == null || recibido.Length == 0)
            {
                _error = ("Sin respuesta de la máquina");
            }
            else
            {
                string s = StringToHEX(recibido).ToString();
                TramaGigames tg = TramaGigames.GetTrama(recibido);
                if (tg != null && tg.IsACK())
                    return false;
                _error = ("No recibo ACK de la máquina:" + s);
            }
            return true;
        }

        private StringBuilder StringToHEX(StringBuilder sb)
        {
            StringBuilder sbHex = new StringBuilder();

            int len = sb.Length;
            byte[] caracteres = new byte[len];
            for (int i = 0; i < len; i++)
            {
                caracteres[i] = (byte)sb[i];
                if (caracteres[i] < 16)
                    sbHex.Append("0");
                sbHex.Append(caracteres[i].ToString("x").ToUpper());
            }
            return sbHex;
        }
        
        private int GetContador(byte[] datos)
        {
            StringBuilder sbRecibido = null;
            if (EnviarTrama(CMD_CONTADORES, datos)) // Pedir contadores
            {
                sbRecibido = RecibirDatos(_timeout1, _timeout2);
                if (sbRecibido != null && sbRecibido.Length > 0)
                {
                    TramaGigames tg = TramaGigames.GetTrama(sbRecibido);
                    if (tg != null)
                    {
                        if (tg.IsContadores())
                        {
                            // Enviamos ACK y leemos ACK para continuar con otro contador posterior
                            if (EnviarTrama(CMD_ACK, null))
                            {
                                StringBuilder sbTmp = RecibirDatos(_timeout1, _timeout2);
                                TramaGigames tgTmp = TramaGigames.GetTrama(sbTmp);
                                if (tgTmp != null && tgTmp.IsACK())
                                    return tg.ValorContador;
                                else
                                    _error = "Recibido contador(" + tg.ValorContador + "), pero no recibido ACK";
                            }
                        }
                        else if (tg.IsACK())
                        {
                            _error = "Recibido ACK. Se esperaba trama de contadores";
                        }
                        else if (tg.IsNACK())
                        {
                            _error = "Recibido NACK. Se esperaba trama de contadores";
                        }
                        else
                        {
                            _error = "Trama no válida. Se esperaba trama de contadores";
                        }
                    }
                    else
                    {
                        _error = "Trama incorrecta";
                    }
                }
                else
                {
                    _error = "Recibido vacío";
                }
            }
            return -1;
        }

        public override InfoContadores LeerContadores(IProgressCallback callback)
        {
            StringBuilder sbRecibido = null;
            _errorEstado = "(S00 RESET)";
            if (EnviarTrama(CMD_RESET)) // Hacer reset
            {
                // El comando de RESET no tiene respuesta
                Thread.Sleep(1000); // espero para dejar tiempo a que la máquina resetee
                
                _errorEstado = "(S01 INI)";
                if (EnviarTrama(CMD_INICIO_SESION)) // Iniciar sesion
                {
                    sbRecibido = RecibirDatos(_timeout1, _timeout2);
                    if (!RecibidoIsError(sbRecibido))
                    {
                        // *** LEEMOS CONTADORES ***
                        _errorEstado = "(S02 CONTA)";
                        InfoContadores info = new InfoContadores();
                        info.Buffer = sbRecibido;
                        info.Entradas = GetContador(new byte[] { 0x00, 0x01 }); //Jugadas -> 1
                        if (info.Entradas >= 0)
                        {
                            info.Salidas = GetContador(new byte[] { 0x00, 0x02 }); // Premios -> 2
                            if (info.Salidas >= 0)
                            {
                                //info.Salidas = GetContador(new byte[] { 0x00, 0x03 }); // Pagos manuales -> 3
                                info.Cajon = GetContador(new byte[] { 0x00, 0x04 }); // cajon -> 4

                                //info.CajonMonedas010 = GetContador(new byte[] { 0x00, 0x1E });
                                info.CajonMonedas020 = GetContador(new byte[] { 0x00, 0x1F });
                                info.CajonMonedas050 = GetContador(new byte[] { 0x00, 0x20 });
                                info.CajonMonedas100 = GetContador(new byte[] { 0x00, 0x21 });
                                info.CajonMonedas200 = GetContador(new byte[] { 0x00, 0x22 });

                                info.Billetes5  = GetContador(new byte[] { 0x00, 0xAA });
                                info.Billetes10 = GetContador(new byte[] { 0x00, 0xAB });
                                info.Billetes20 = GetContador(new byte[] { 0x00, 0xAC });
                                info.Billetes50 = GetContador(new byte[] { 0x00, 0xAD });
                            }
                        }
                        // *** CERRAMOS SESIÓN ***
                        if (EnviarTrama(CMD_CERRAR_SESION)) // Cerrar sesión
                            sbRecibido = RecibirDatos(_timeout1, _timeout2);
                        return info;
                    }
                }
            }
            return null;
        }

        //private static void EscribeFichero(string msg)
        //{
        //    try
        //    {
        //        //try
        //        //{
        //            string rutaDestino = "salida1.txt";
        //            System.IO.FileStream fs = new System.IO.FileStream(rutaDestino, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
        //            System.IO.StreamWriter m_streamWriter = new System.IO.StreamWriter(fs);
        //            m_streamWriter.BaseStream.Seek(0, System.IO.SeekOrigin.End);
        //            m_streamWriter.WriteLine(msg);
        //            m_streamWriter.Flush();
        //            m_streamWriter.Close();
        //        //}
        //        //catch { } 

        //        //System.IO.StreamWriter sw = new System.IO.StreamWriter("salida.txt", true);
        //        //sw.WriteLine(msg);
        //        //sw.Flush();
        //        //sw.Close();
        //    }
        //    catch { }
        //}

        //public static bool TestTramaGigames()
        //{
        //    bool ret = true;

        //    string cadenaACK1 = new string(new char[] { (char)0x00, (char)0x10, (char)0x00, (char)0x01, (char)0x00, (char)0x01, (char)0x00, (char)0xEA, (char)0x00, (char)0x5A, (char)0x11 });

        //    string cadenaNACK1 = new string(new char[] { (char)0x00, (char)0x10, (char)0x00, (char)0x01, (char)0x00, (char)0x02, (char)0x00, (char)0xEA, (char)0x00, (char)0x5A, (char)0x11 });

        //    string cadenaContador1 = new string(new char[] { (char)0x00, (char)0x10, (char)0x01, (char)0xE8, (char)0x93, (char)0x01, (char)0x00, (char)0x00, (char)0x01, (char)0x00, (char)0x00, (char)0x00, (char)0x01, (char)0x00, (char)0x03, (char)0x00, (char)0xEA, (char)0x00, (char)0x5A, (char)0x11 });
        //    string cadenaContador2 = new string(new char[] { (char)0x00, (char)0x10, (char)0x01, (char)0xE8, (char)0x93, (char)0x12, (char)0xFE, (char)0x00, (char)0x00, (char)0x01, (char)0x00, (char)0x00, (char)0x00, (char)0x01, (char)0x00, (char)0x03, (char)0x00, (char)0xEA, (char)0x00, (char)0x5A, (char)0x11 });
        //    //string cadenaContador2 = new string(new char[] { 0x00, 0x00, 0x00, 0x00});
        //    //string cadenaContador3 = new string(new char[] { 0x00, 0x00, 0x00, 0x00});
            
        //    TramaGigames tgACK = TramaGigames.GetTrama(new StringBuilder(cadenaACK1));
        //    ret = ret && tgACK.IsACK();

        //    TramaGigames tgNACK = TramaGigames.GetTrama(new StringBuilder(cadenaNACK1));
        //    ret = ret && tgNACK.IsNACK();

        //    TramaGigames tgConta = TramaGigames.GetTrama(new StringBuilder(cadenaContador1));
        //    ret = ret && tgConta.IsContadores();

        //    tgConta = TramaGigames.GetTrama(new StringBuilder(cadenaContador2));
        //    ret = ret && tgConta.IsContadores();

        //    return ret;
        //}

        /****
         * 
         *  CLASE TRAMAGIGAMES.... **************** 
         * 
         */
        protected class TramaGigames
        {
            StringBuilder _datos = new StringBuilder();
            int _comando = 0;
            int i_valor = -1;
            string s_valor = null;

            public int ValorContador
            {
                get { return i_valor; }
            }

            public static TramaGigames GetTrama(StringBuilder datos)
            {
                TramaGigames t = new TramaGigames(datos);
                if (t._comando != 0)
                    return t;
                return null;
            }

            private int PopInt32(StringBuilder sb)
            {
                int ret = 0;
                if (sb != null && sb.Length > 4)
                {
                    byte[] b = new byte[4];
                    b[0] = (byte)sb[sb.Length - 4];
                    b[1] = (byte)sb[sb.Length - 3];
                    b[2] = (byte)sb[sb.Length - 2];
                    b[3] = (byte)sb[sb.Length - 1];
                    ret = BitConverter.ToInt32(b, 0)/100;
                    sb.Remove(sb.Length - 4, 4);
                }
                return ret;
            }

            private int PopUInt16(StringBuilder sb)
            {
                int ret = 0;
                if (sb != null && sb.Length > 1)
                {
                    byte[] b = new byte[2];
                    b[0] = (byte)sb[sb.Length-2];
                    b[1] = (byte)sb[sb.Length-1];
                    ret = BitConverter.ToUInt16(b, 0);
                    sb.Remove(sb.Length - 2, 2);
                }
                return ret;
            }

            private TramaGigames(StringBuilder sb)
            {
                bool leyendoDatos = false;
                char c = ' ';
                char last = ' ';
                
                for (int x = 0; x < sb.Length; x++)
                {
                    last = c;
                    c = sb[x];

                    if (leyendoDatos && (last == BYTE_CONTROL))
                    {   
                        //Procesamos después del byte de control
                        _datos.Append((char)~c);
                    }
                    else
                    {
                        if (c == BYTE_INICIO)
                        {
                            leyendoDatos = true;
                            _datos.Length = 0;
                        }
                        else if (c == BYTE_FIN)
                        {
                            // Comprobar si son datos válidos
                            int datoGapid2 = PopUInt16(_datos);
                            int datoGapid1 = PopUInt16(_datos);
                            
                            if (datoGapid1 == DBYTE_GAPID1 && datoGapid2 == DBYTE_GAPID2)
                            {
                                int tipoMensaje = PopUInt16(_datos);
                                int tipoComando = PopUInt16(_datos);
                                
                                if (tipoMensaje == DBYTE_CMD_ACK)
                                {
                                    _comando = DBYTE_CMD_ACK;
                                    _datos.Length = 0;
                                }
                                else if (tipoMensaje == DBYTE_CMD_NACK)
                                {
                                    _comando = DBYTE_CMD_NACK;
                                    _datos.Length = 0;
                                }
                                else if (tipoMensaje == DBYTE_MSG_DATOS && tipoComando == DBYTE_CMD_GETSTATUS)
                                {
                                    _comando = DBYTE_CMD_CONTADORES;
                                    int tipoDatos = PopUInt16(_datos);
                                    PopUInt16(_datos); // esto no hace nada, pero quita un entero que no se usa
                                    
                                    if (tipoDatos == 0)
                                    {   // Datos enteros
                                        i_valor = PopInt32(_datos);
                                    }
                                    else
                                    {   // Datos string
                                        int longitud = PopUInt16(_datos);
                                        s_valor = "";
                                        for (int y = 0; y < longitud; y++)
                                            s_valor += (char)(_datos[_datos.Length - 1 - y]);
                                    }
                                }
                            }
                            leyendoDatos = false;
                        }
                        else if (c == BYTE_CONTROL)
                        {
                            // No se hace nada, porque se hará en el próximo ciclo del bucle...
                        }
                        else if (leyendoDatos)
                        {
                            _datos.Append((char)c);
                        }
                    }
                }
            }

            public bool IsACK()
            {
                return (_comando == DBYTE_CMD_ACK);
            }

            public bool IsNACK()
            {
                return (_comando == DBYTE_CMD_NACK);
            }

            public bool IsContadores()
            {
                return (_comando == DBYTE_CMD_CONTADORES);
            }
        }
    }
}
