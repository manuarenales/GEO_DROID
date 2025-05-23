using System;
using System.Collections.Generic;
using System.Text;
using GEO_DROID.Resources.Lib.Comunicacion;

namespace BLL.LeerInfoMaquina
{
    class ProtocoloSleic : Protocolo
    {
        // Conexión serie por defecto SLEIC: 9600, Parity.None, 8, StopBits.One

        #region Constantes
        private const int TIMEOUT = 500;

        private const byte COD_STX = (byte)0x02;
        private const byte COD_ETX = (byte)0x03;

        #region Codigos
        private const byte COD_CREDITOS_ENTRADA_TOTAL   = 1;    // 01 .- Créditos de Entrada Totales 
        private const byte COD_CREDITOS_SALIDA_TOTAL    = 2;    // 02.-  Créditos de Salida Totales 
        private const byte COD_CREDITOS_JUGADOS_TOTAL   = 3;    // 03.-  Créditos Jugados Totales 
        private const byte COD_CREDITOS_GANADOS_TOTAL   = 4;    // 04.-  Créditos Ganados Totales 
        private const byte COD_CREDITOS_ENTRADA_PARCIAL = 5;    // 05.-  Créditos de Entrada Parciales 
        private const byte COD_CREDITOS_SALIDA_PARCIAL  = 6;    // 06.-  Créditos de Salida Parciales 
        private const byte COD_CREDITOS_JUGADOS_PARCIAL = 7;    // 07.-  Créditos Jugados Parciales  
        private const byte COD_CREDITOS_GANADOS_PARCIAL = 8;    // 08.-  Créditos Ganados Parciales 

        private const byte COD_MONEDAS_ENTRADA_TIPO1    = 10;   // 10.-  Monedas de Entrada de tipo I   (25ptas/0,10€)
        private const byte COD_MONEDAS_ENTRADA_TIPO2    = 11;   // 11.-  Monedas de Entrada de tipo II  (50ptas/0,20€) 
        private const byte COD_MONEDAS_ENTRADA_TIPO3    = 12;   // 12.-  Monedas de Entrada de tipo III (100ptas/0,50€) 
        private const byte COD_MONEDAS_ENTRADA_TIPO4    = 13;   // 13.-  Monedas de Entrada de tipo IV (200ptas/1€) 
        private const byte COD_MONEDAS_ENTRADA_TIPO5    = 14;   // 14.-  Monedas de Entrada de tipo V  (500ptas/2€) 
        private const byte COD_BILLETES_ENTRADA_TIPO1   = 15;   // 15.-  Billetes de Entrada de tipo I      (1000ptas/5€) 
        private const byte COD_BILLETES_ENTRADA_TIPO2   = 16;   // 16.-  Billetes de Entrada de tipo II     (2000ptas/10€) 
        private const byte COD_BILLETES_ENTRADA_TIPO3   = 17;   // 17.-  Billetes de Entrada de tipo III    (----/20€) 

        private const byte COD_MONEDAS_SALIDA_TIPO1     = 20;   // 20.-  Monedas de Salida de tipo I      (25ptas/0,10€) 
        private const byte COD_MONEDAS_SALIDA_TIPO2     = 21;   // 21.-  Monedas de Salida de tipo II     (----/0,20€) 
        private const byte COD_MONEDAS_SALIDA_TIPO3     = 22;   // 22.-  Monedas de Salida de tipo III    (100ptas/0,50€) 
        private const byte COD_MONEDAS_SALIDA_TIPO4     = 23;   // 23.-  Monedas de Salida de tipo IV    (----/1€) 

        private const byte COD_MONEDAS_CAJON_TIPO1 = 30;   // 30.-  Monedas en Cajón de tipo I    (25ptas/0,10€) 
        private const byte COD_MONEDAS_CAJON_TIPO2 = 31;   // 31.-  Monedas en Cajón de tipo II   (50ptas/0,20€) 
        private const byte COD_MONEDAS_CAJON_TIPO3 = 32;   // 32.-  Monedas en Cajón de tipo III  (100ptas/0,50€) 
        private const byte COD_MONEDAS_CAJON_TIPO4 = 33;   // 33.-  Monedas en Cajón de tipo IV  (200ptas/1€) 
        private const byte COD_MONEDAS_CAJON_TIPO5 = 34;   // 34.-  Monedas en Cajón de tipo V   (500ptas/2€) 

        private const byte COD_HORAS_FUNCIONAMIENTO_TOTAL   = 40; // 40.-  Horas de funcionamiento Totales   (HHHHHHH:MM) 
        private const byte COD_HORAS_FUNCIONAMIENTO_PARCIAL = 41; // 41.-  Horas de funcionamiento Parciales (HHHHHHH:MM) 

        private const byte COD_PARTIDAS_JUGADAS_TOTAL_DN    = 50; // 50.-  Partidas totales  jugadas a     0,10€ a DOBLE/NADA  
        private const byte COD_PARTIDAS_GANADAS_TOTAL_DN    = 51; // 51.-  Partidas totales ganadas a      0,10€ a DOBLE/NADA 
        private const byte COD_PARTIDAS_JUGADAS_PARCIAL_DN  = 52; // 52.-  Partidas parciales  jugadas a  0,10€ a DOBLE/NADA  
        private const byte COD_PARTIDAS_GANADAS_PARCIAL_DN  = 53; // 53.-  Partidas parciales  ganadas a 0,10€ a DOBLE/NADA
        #endregion
        #endregion

        private int _timeout = TIMEOUT;

        public ProtocoloSleic(IComunicacion com, IFiltroTrama filtroTrama, int timeout)
            : base(com, filtroTrama)
        {
            if (timeout > 0)
                _timeout = timeout;
        }

        /// <summary>
        /// Devuelve el código como dos caracteres ascii que representan ese número: 1 -> "01", 40 -> "40"
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        private string COD2String(byte codigo)
        {
            return codigo.ToString().PadLeft(2, '0');
        }
        
        private byte String2COD(string codigo)
        {
            if (codigo != null)
                return (byte)int.Parse(codigo);
            else return 0x00;
        }

        protected override StringBuilder MontarTrama(byte comando, byte[] datos)
        {
            string datosStr = "#SL" + COD2String(comando);
            char checksum = CrearChecksum(new StringBuilder(datosStr));

            StringBuilder sb = new StringBuilder();
            sb.Append((char)COD_STX);
            sb.Append(datosStr + checksum);
            sb.Append((char)COD_ETX);
            return sb;
        }

        protected override char CrearChecksum(StringBuilder sb)
        {
            char checksum = (char)0x00;
            if (sb != null)
            {
                string cadena = sb.ToString();
                for (int i = 0; i < cadena.Length; i++)
                    checksum += cadena[i];
                checksum = (char)(checksum % 256);
            }
            return checksum;
        }

        protected override bool ComprobarChecksum(StringBuilder sb)
        {
            if (sb == null || sb.Length < 12) return false;
            string cadena = sb.ToString();

            string datos = cadena.Substring(1, 10);
            char crc = cadena[11];//cadena.Substring(11, 1)[0];
            char crcCalculado = (char)0x00;

            for (int i = 0; i < 10; i++)
                crcCalculado += datos[i];
            crcCalculado = (char)(crcCalculado % 256);

            return crcCalculado == crc;
        }

        public override InfoContadores LeerContadores(IProgressCallback callback)
        {
            InfoContadores info = null;
            info = new InfoContadores();

            info.Entradas   = ObtenerContador(COD_CREDITOS_JUGADOS_TOTAL, info);
            if (!string.IsNullOrEmpty(_error)) return null;

            info.Salidas = ObtenerContador(COD_CREDITOS_GANADOS_TOTAL, info);
            if (!string.IsNullOrEmpty(_error)) return null;

            info.CajonMonedas020 = ObtenerContador(COD_MONEDAS_CAJON_TIPO2, info);
            if (!string.IsNullOrEmpty(_error)) return null;

            info.CajonMonedas050 = ObtenerContador(COD_MONEDAS_CAJON_TIPO3, info);
            if (!string.IsNullOrEmpty(_error)) return null;

            info.CajonMonedas100 = ObtenerContador(COD_MONEDAS_CAJON_TIPO4, info);
            if (!string.IsNullOrEmpty(_error)) return null;

            info.CajonMonedas200 = ObtenerContador(COD_MONEDAS_CAJON_TIPO5, info);
            if (!string.IsNullOrEmpty(_error)) return null;

            info.Billetes5 = ObtenerContador(COD_BILLETES_ENTRADA_TIPO1, info);
            if (!string.IsNullOrEmpty(_error)) return null;

            info.Billetes10 = ObtenerContador(COD_BILLETES_ENTRADA_TIPO2, info);
            if (!string.IsNullOrEmpty(_error)) return null;

            info.Billetes20 = ObtenerContador(COD_BILLETES_ENTRADA_TIPO3, info);
            if (!string.IsNullOrEmpty(_error)) return null;

            return info;
        }

        private int ObtenerContador(byte codigo, InfoContadores info)
        {
            int valor = -1;
            int intentos = 2;
            do
            {
                if (EnviarTrama(codigo))
                {
                    StringBuilder sb = RecibirDatos(_timeout);
                    if (!RecibidoIsError(sb))
                    {
                        if (info != null)
                        {
                            if (info.Buffer == null)
                                info.Buffer = new StringBuilder();
                            info.Buffer.Append("[" + codigo + "]");
                            info.Buffer.Append(sb);
                        }
                        string cadena = sb.ToString();
                        string datos = cadena.Substring(1, 10);

                        try { 
                            valor = int.Parse(datos);
                            _error = "";
                        }
                        catch { }
                    }
                    intentos--;
                }
            } while (valor == -1 && intentos > 0);
            return valor;
        }

        private bool RecibidoIsError(StringBuilder recibido)
        {
            // Comprobamos el tamaño del comando
            if (recibido == null || recibido.Length == 0)
            {
                _error = "Recibido vacío";
                return true;
            }
            else if (recibido.Length < 12)  // Nunca se recibe fin de cadena -> '03'
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
            }
            
            // El resto es válido
            return false;
        }
    }
}
