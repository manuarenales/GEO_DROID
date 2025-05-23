using System;
using System.Collections.Generic;
using System.Text;
using GEO_DROID.Resources.Lib.Comunicacion;

namespace BLL.LeerInfoMaquina
{
    class ProtocoloFranco1 : Protocolo
    {
        public const byte CMD_PEDIR_FECHA_HORA = 0x01;//B - PEDIR FECHA Y HORA ................... "rd" + 0AH + CRC + 1AH
        public const byte CMD_PEDIR_IDENTIFICACION = 0x02;// E - PEDIR IDENTIFICATIVO DE LA MÁQUINA ... "ri" + 0AH + CRC + 1AH
        public const byte CMD_ENVIAR_PASSWORD = 0x03;//A - ENVIAR PASSWORD ...................  "ep " + " 8 BYTES " + 0AH + CRC + 1AH
        public const byte CMD_PEDIR_CONTADORES = 0x04;//- PEDIR CONTADORES ..................... "rc" + 0AH + CRC + 1AH
        public const byte CMD_PEDIR_REGISTROS = 0x05;//- PEDIR REGISROS ..................... "rf" + 0AH + CRC + 1AH

        public const char NACK = (char)0x15; //CRC mal, comando no existe.
        public const char ETX = (char)0x03; //Datos incorrectos, fuera de rango, etc.  
        public const char ENQ = (char)0x05; //Falta password.
        public const char ACK = (char)0x06; //Recepción correcta.

        private const string PASSWORD = "       1";
        private const char FIN_LINEA = (char)0x0A;
        private const char RELLENO_CRC = (char)0x1B;
        private const char FIN_TRAMA = (char)0x1A;

        string _password = PASSWORD;
        int _timeoutDefault = 500;

        public override byte ConfiguracionPuertoSerie
        {
            get { return (byte)0x54; }
        }

        public ProtocoloFranco1(IComunicacion com, IFiltroTrama filtroTrama, string password, int timeout)
            : base(com, filtroTrama)
        {
            if (password != null && password.Length > 0)
                _password = password;
            if (timeout > 0)
                _timeoutDefault = timeout;
        }

        protected override char CrearChecksum(StringBuilder sb)
        {
            char crc = (char)0x00;
            return crc;
        }

        protected override bool ComprobarChecksum(StringBuilder sb)
        {
            return false;
        }

        protected override StringBuilder MontarTrama(byte comando, byte[] datos) //throws CheckSumException
        {
            StringBuilder sbTemp = new StringBuilder();
            switch (comando)
            {
                case CMD_PEDIR_FECHA_HORA:
                    sbTemp.Append("rd");
                    break;
                case CMD_PEDIR_IDENTIFICACION:
                    sbTemp.Append("ri");
                    break;
                case CMD_ENVIAR_PASSWORD:
                    sbTemp.Append("ep " + _password);
                    break;
                case CMD_PEDIR_CONTADORES:
                    sbTemp.Append("rc");
                    break;
                case CMD_PEDIR_REGISTROS:
                    sbTemp.Append("rf");
                    break;
            }
            //if (datos != null)
            //{
            //    for (int i = 0; i < datos.Length; i++)
            //    {
            //        sbTemp.Append((char)datos[i]);
            //    }
            //}
            sbTemp.Append(FIN_LINEA);
            // Franco 1 va sin checksum
                //char crc = CrearChecksum(sbTemp);
                //sbTemp.Append(crc);
                //if (crc.Equals(FIN_TRAMA))
                //    sbTemp.Append(RELLENO_CRC);
            sbTemp.Append(FIN_TRAMA);
            return sbTemp;
        }

        private bool RecibidoIsError(string recibido)
        {
            if (recibido == null || recibido.Length == 0)
            {
                _error = ("Sin respuesta de la máquina");
            }
            else if (recibido[0].Equals(ProtocoloFranco2.NACK)) //CRC mal, comando no existe.
            {
                _error = ("CRC mal, comando no existe");
            }
            else if (recibido[0].Equals(ProtocoloFranco2.ETX)) //Datos incorrectos, fuera de rango, etc.
            {
                _error = ("Datos incorrectos, fuera de rango, etc.");
            }
            else if (recibido[0].Equals(ProtocoloFranco2.ENQ)) //Falta password.
            {
                _error = ("Falta password.");
            }
            else
            {
                return false;
            }
            return true;
        }

        public override InfoContadores LeerContadores(IProgressCallback callback)
        {
            _protocoloOK = true;
            _errorEstado = "(S01 PASS)";
            if (EnviarTrama(CMD_ENVIAR_PASSWORD))
            {
                StringBuilder sb = RecibirDatos(_timeoutDefault, 300);
                string recibido = sb.ToString();

                if (!RecibidoIsError(recibido))
                {
                    _errorEstado = "(S02 CONTA)";
                    if (EnviarTrama(CMD_PEDIR_REGISTROS)) //CMD_PEDIR_CONTADORES
                    {
                        sb = RecibirDatos(_timeoutDefault, _timeoutDefault);
                        recibido = sb.ToString();

                        if (!RecibidoIsError(recibido))
                        {
                            return ProcesarDatos(recibido);
                        }
                    }
                }
                else
                {
                    _protocoloOK = false;
                }
            }
            return null;
        }

        //public InfoContadores Identificacion()
        //{
        //    if (EnviarTrama(CMD_PEDIR_IDENTIFICACION))
        //    {
        //        System.Threading.Thread.Sleep(_timeoutDefault);
        //        StringBuilder sb = RecibirDatos(500);
        //        string recibido = sb.ToString();
        //        _error = recibido;
        //        //if (!RecibidoIsError(recibido))
        //        //{
        //        //    if (EnviarTrama(CMD_PEDIR_REGISTROS)) //CMD_PEDIR_CONTADORES
        //        //    {
        //        //        System.Threading.Thread.Sleep(_timeoutDefault);
        //        //        sb = RecibirDatos(500);
        //        //        recibido = sb.ToString();

        //        //        if (!RecibidoIsError(recibido))
        //        //        {
        //        //            return ProcesarDatos(recibido);
        //        //        }
        //        //    }
        //        //}
        //    }
        //    return null;
        //}

        private int GetDato(string texto, string datos)
        {
            int valor = -1;
            if (datos != null)
            {
                int indiceInicio = datos.IndexOf(texto);
                if (indiceInicio >= 0)
                {
                    indiceInicio = indiceInicio + texto.Length;
                    int indiceFin = datos.IndexOf(FIN_LINEA, indiceInicio);
                    if (indiceFin >= indiceInicio)
                    {
                        try
                        {
                            valor = int.Parse(datos.Substring(indiceInicio, indiceFin - indiceInicio).Trim());
                        }
                        catch { }
                    }
                }
            }
            return valor;
        }

        private InfoContadores ProcesarDatos(string datos)
        {
            // CADENAS QUE ENCONTRAREMOS EN LA TRAMA DE DATOS RECIBIDA
            // -------------------------------------------------------
            //"total...ent", "total...sal", "total..test", "mo.ent.0,05", "mo.sal.0,05"
            //"mo.ent.0,10", "mo.sal.0,10", "mo.ent.0,20", "mo.sal.0,20",
            //"mo.ent.0,50", "mo.sal.0,50", "mo.ent.1,00", "mo.sal.1,00",
            //"mo.ent.2,00", "mo.sal.2,00"
            
            InfoContadores info = new InfoContadores();
            info.Buffer = new StringBuilder(datos);
            
            // ENTRADAS/SALIDAS
            info.Entradas = GetDato("total...ent", datos);
            info.Salidas = GetDato("total...sal", datos);
            // TEST
            info.Test = GetDato("total..test", datos);
            // BILLETES
            info.Billetes5 = GetDato("bille..5.EU", datos); 
            info.Billetes10 = GetDato("bille.10.EU", datos);
            info.Billetes20 = GetDato("bille.20.EU", datos);
            info.Billetes50 = GetDato("bille.50.EU", datos);
            info.AutoSetBilletes();

            info.Auxiliar1 = 10; //Valor que nos identifica protocolo Franco 1

            return info;
        }
    }
}
