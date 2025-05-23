using System;
using System.Collections.Generic;
using System.Text;
using GEO_DROID.Resources.Lib.Comunicacion;

namespace BLL.LeerInfoMaquina
{
    class ProtocoloFranco3 : Protocolo
    {
        public const byte CMD_FECHA = 0x01;
        public const byte CMD_ID = 0x02;
        public const byte CMD_SEND_CLAVE = 0x03;
        public const byte CMD_CONTADORES = 0x04;
        public const byte CMD_CLAVE = 0x05;
        public const byte CMD_PEDIR_REGISTROS = 0x06;//- PEDIR REGISROS ..................... "rf" + 0AH + CRC + 1AH
        public const byte CMD_GENCLAVE = 0x07; //Restaurar la clave genérica. Es necesario habilitar físicamente el interruptor de la placa de contadores

        private const string PASSWORD = "RF";
        private const char FIN_LINEA = (char)0x0D;
        private const char RELLENO_CRC = (char)0x1B;
        private const char FIN_TRAMA = (char)0x0A;
        private const char CSUM_INICIO = (char)'(';
        private const char CSUM_FIN = (char)')';

        string _password = PASSWORD;
        int _timeoutDefault = 2000;
        const int TIMEOUT2 = 1000;

        public ProtocoloFranco3(IComunicacion com, IFiltroTrama filtroTrama, string password, int timeout)
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
            
            // Opción de CHECKSUM:
            // Cualquier comando puede ir precedido de un guión ('-')
            // En este caso el resultado irá seguido de un CHECKSUM
            switch (comando)
            {
                case CMD_CLAVE:
                    sbTemp.Append("CLAVE");
                    break;
                case CMD_SEND_CLAVE:
                    sbTemp.Append(_password);
                    break;
                case CMD_CONTADORES:
                    sbTemp.Append("-CONTADORES");
                    break;
                case CMD_PEDIR_REGISTROS:
                    sbTemp.Append("-REGISTROS");
                    break;
                case CMD_FECHA:
                    sbTemp.Append("-f");
                    break;
                case CMD_ID:
                    sbTemp.Append("-ID");
                    break;
                case CMD_GENCLAVE:
                    sbTemp.Append("GENCLAVE");
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
            //char crc = CrearChecksum(sbTemp);
            //sbTemp.Append(crc);
            //if (crc.Equals(FIN_TRAMA))
            //    sbTemp.Append(RELLENO_CRC);
            sbTemp.Append(FIN_TRAMA);
            return sbTemp;
        }

        private bool RecibidoIsError(string recibido, string respuestaEsperada)
        {
            if (recibido == null)
                _error = "Recibido null";
            else if (recibido.Length == 0)
                _error = "Recibido vacío";
            else if (recibido.IndexOf(respuestaEsperada) >= 0 || recibido.IndexOf(">") >= 0)
                _error = null; // Esto es bueno... no hay error
            else
                _error = "Recibido:(" + recibido + ")";
            
            return (_error != null);
        }

        private StringBuilder EnviaComando(byte comando, string comandoEsperado, int timeout2 = TIMEOUT2)
        {
            StringBuilder sb;
            if (EnviarTrama(comando))
            {
                sb = RecibirDatos(_timeoutDefault, timeout2);
                if (!RecibidoIsError(sb == null ? null : sb.ToString(), comandoEsperado))
                    return sb;
            }
            return null;
        }

        public override InfoContadores LeerContadores(IProgressCallback callback)
        {
            StringBuilder sb;
            _protocoloOK = false;

            // Paso 1 - Comando que nos servirá para limpiar la entrada de datos
            _errorEstado = "(S01 INI)";
            EnviaComando(CMD_ID, ""); //OJO!! En alguna ocasión se ha puesto '?' como texto esperado como respuesta, pero algunas máquinas devuelven basura y esa comprobación detiene el proceso de lectura
            // Paso 2 - Iniciamos login
            if (string.IsNullOrEmpty(_error))
            {
                _errorEstado = "(S01 PASS1)";
                EnviaComando(CMD_CLAVE, ""); //OJO!! En alguna ocasión se ha puesto ':' como texto esperado como respuesta, pero algunas máquinas devuelven basura y esa comprobación detiene el proceso de lectura
            }
            // Paso 3 - Enviamos la clave
            if (string.IsNullOrEmpty(_error))
            {
                _errorEstado = "(S01 PASS2)";
                EnviaComando(CMD_SEND_CLAVE, ">");
            }
            // Paso 4 - Pedimos contadores
            if (string.IsNullOrEmpty(_error))
            {
                _protocoloOK = true; // Si llegamos aquí, el protocolo es este, aunque después pueda fallar algo
                _errorEstado = "(S01 CONTA)";
                sb = EnviaComando(CMD_PEDIR_REGISTROS, ">", _timeoutDefault);
                string recibido = sb.ToString();

                Dictionary<string, LineaDatos> lineasProcesadas = PreprocesarDatos(recibido);
                return ProcesarDatos(recibido, lineasProcesadas);
            }

            // Paso especial: Reset password
            if (_error != null && _error.ToLower().IndexOf("clave incorrecta") >= 0)
            {
                string errorPrevio = _error;
                EnviaComando(CMD_GENCLAVE, "#");
                _error = errorPrevio + _error;
            }
            return null;
        }

        /// <summary>
        /// Método que nos devuelve una estructura de lineas a partir de los datos pasados por parámetro
        /// </summary>
        /// <param name="datos">Datos a procesar</param>
        /// <returns>Diccionario con las líneas encontradas en los datos. La clave utilizada es LineaDatos.Nombre</returns>
        private Dictionary<string, LineaDatos> PreprocesarDatos(string datos)
        {
            Dictionary<string, LineaDatos> d = new Dictionary<string,LineaDatos>();
            if (datos != null)
            {
                string[] lineas = datos.Split(new char[] { FIN_LINEA });
                for (int i = 0; i < lineas.Length; i++)
                {
                    string linea = lineas[i].Trim();
                    LineaDatos ld = new LineaDatos(linea);
                    if (ld.Nombre != null && !d.ContainsKey(ld.Nombre))
                        d.Add(ld.Nombre.ToLower(), ld);
                }
            }
            return d;
        }

        /// <summary>
        /// Método para obtener un dato a partir de una estructura de lineas. Se comprueba el checksum y en caso de no ser correcto no se devuelve el dato.
        /// </summary>
        /// <param name="texto">Cadena a buscar entre los datos</param>
        /// <param name="lineasProcesadas">Estructura de lineas</param>
        /// <returns>El dato si se encuentra y además tiene el checksum correcto</returns>
        private int GetDato(string texto, Dictionary<string, LineaDatos> lineasProcesadas)
        {
            int valor = -1;
            string clave = texto.ToLower();
            if (lineasProcesadas != null && lineasProcesadas.ContainsKey(clave))
            {
                LineaDatos ld = lineasProcesadas[clave];
                if (ld.IsChecksumOK())
                    valor = ld.Dato;
                else
                    _error = "Checksum error " + texto + "(" + ld.Checksum + "<>" + ld.ChecksumCalculado + " Y " + ld.Checksum + "<>" + ld.ChecksumCalculado2 + ")";
            }
            return valor;
        }

        private InfoContadores ProcesarDatos(string datos, Dictionary<string, LineaDatos> lineasProcesadas)
        {
            // CADENAS QUE ENCONTRAREMOS EN LA TRAMA DE DATOS RECIBIDA
            // -------------------------------------------------------
            //"total..ent:", "total..sal:", "total.test:", "mo.ent.0,05", "mo.sal.0,05"
            //"mo.ent.0,10", "mo.sal.0,10", "mo.ent.0,20", "mo.sal.0,20",
            //"mo.ent.0,50", "mo.sal.0,50", "mo.ent.1,00", "mo.sal.1,00",
            //"mo.ent.2,00", "mo.sal.2,00"
            
            InfoContadores info = new InfoContadores();
            info.Buffer = new StringBuilder(datos);

            // ENTRADAS
            info.Entradas = GetDato("total..ent:", lineasProcesadas);
            if (info.Entradas < 0)
                info.Entradas = GetDato("total...ent", lineasProcesadas);
            
            // SALIDAS
            info.Salidas = GetDato("total..sal:", lineasProcesadas);
            if (info.Salidas < 0)
                info.Salidas = GetDato("total...sal", lineasProcesadas);
            
            // TEST
            info.Test = GetDato("total.test:", lineasProcesadas);
            if (info.Test < 0)
                info.Test = GetDato("total..test", lineasProcesadas);
            
            // BILLETES
            info.Billetes5 = GetDato("bille..5.EU", lineasProcesadas);
            info.Billetes10 = GetDato("bille.10.EU", lineasProcesadas);
            info.Billetes20 = GetDato("bille.20.EU", lineasProcesadas);
            info.Billetes50 = GetDato("bille.50.EU", lineasProcesadas);
            info.AutoSetBilletes();

            info.PagosManuales = GetDato("tot.p.manu:", lineasProcesadas);

            //Cambios en la identificación de partidas
			//“partid.a.1C”  pasa a ser   “Partid.a.1C”
            //“partid.a.2C”  pasa a ser   “Partid.a.2C”
            //“partid.a.3C”  pasa a ser   “Partid.a>2C”
            info.Partidas1 = GetDato("partid.a.1A", lineasProcesadas);
            if (info.Partidas1 < 0)
                info.Partidas1 = GetDato("Partid.a.1C", lineasProcesadas);

            info.Partidas2 = GetDato("partid.a.2A", lineasProcesadas);
            if (info.Partidas2 < 0)
                info.Partidas2 = GetDato("Partid.a.2C", lineasProcesadas);
            
            info.Partidas3 = GetDato("partid.a.3A", lineasProcesadas);
            if (info.Partidas3 < 0)
                info.Partidas3 = GetDato("Partid.a>2C", lineasProcesadas);
            if (info.Partidas3 < 0)
                info.Partidas3 = GetDato("partid.a.3C", lineasProcesadas);

            info.Auxiliar1 = 30; //Valor que nos identifica protocolo Franco 3
            
            return info;
        }

        /// <summary>
        /// Clase que representa una linea de datos de las que hemos recibido de la máquina.
        /// Tendremos en cuanta que todas las lineas siguen el formato:
        /// -11 caracteres con el texto para identificar el dato
        /// -1 espacio
        /// -8 caracteres con el dato
        /// -1 espacio
        /// -checksum entre paréntesis
        /// </summary>
        class LineaDatos
        {
            const int NUMERO_BYTES_CABECERA_DATO = 11;

            string _nombre;
            public string Nombre
            {
                get { return _nombre; }
            }

            int _dato;
            public int Dato
            {
                get { return _dato; }
            }

            int _checksum;
            public int Checksum
            {
                get { return _checksum; }
            }

            int _checksumCalculado;
            public int ChecksumCalculado
            {
                get { return _checksumCalculado; }
            }

            int _checksumCalculado2;
            public int ChecksumCalculado2
            {
                get { return _checksumCalculado2; }
            }

            public LineaDatos(string lineaOrigen)
            {
                _nombre = GetNombre(lineaOrigen);
                _dato = GetDato(lineaOrigen);
                _checksum = GetChecksum(lineaOrigen);
                _checksumCalculado = CalculaChecksum(lineaOrigen);

                // Hay alguna máquina que calcula el checksum a partir de los datos en lugar de hacerlo desde inicio de línea, por ejemplo MULTIJUEGO DE CRONOS
                if (lineaOrigen != null && lineaOrigen.Length > (NUMERO_BYTES_CABECERA_DATO + 1))
                    _checksumCalculado2 = CalculaChecksum(lineaOrigen.Substring(NUMERO_BYTES_CABECERA_DATO + 1));
                else
                    _checksumCalculado2 = _checksumCalculado;
            }

            public bool IsChecksumOK()
            {
                return (_checksum == _checksumCalculado || _checksum == _checksumCalculado2);
            }

            public static string GetNombre(string lineaOrigen)
            {
                if (lineaOrigen != null && lineaOrigen.Length >= NUMERO_BYTES_CABECERA_DATO)
                    return lineaOrigen.Substring(0, 11);
                return null;
            }

            private static int GetDato(string lineaOrigen)
            {
                int dato = -1;
                if (lineaOrigen != null && lineaOrigen.Length >= NUMERO_BYTES_CABECERA_DATO)
                {
                    int indiceFin = lineaOrigen.IndexOf(CSUM_INICIO);
                    if (indiceFin < 0)
                        indiceFin = lineaOrigen.Length - 1;
                    else
                        indiceFin--;

                    string textoDato = lineaOrigen.Substring(NUMERO_BYTES_CABECERA_DATO, indiceFin - NUMERO_BYTES_CABECERA_DATO);
                    try
                    {
                        dato = int.Parse(textoDato);
                    }
                    catch { }
                }
                return dato;
            }

            private static int GetChecksum(string lineaOrigen)
            {
                int cs = 0;
                if (lineaOrigen != null)
                {
                    int indiceChecksum = lineaOrigen.IndexOf(CSUM_INICIO);
                    int indiceChecksumFin = lineaOrigen.IndexOf(CSUM_FIN);
                    if (indiceChecksum > 0 && indiceChecksum < indiceChecksumFin)
                    {
                        string checksum = lineaOrigen.Substring(indiceChecksum + 1, indiceChecksumFin - indiceChecksum - 1);
                        try
                        {
                            cs = int.Parse(checksum.Trim(), System.Globalization.NumberStyles.HexNumber);
                        }
                        catch { }
                    }
                }
                return cs;
            }

            private static int CalculaChecksum(string lineaOrigen)
            {
                int cs = 0;
                if (lineaOrigen != null)
                {
                    int indiceChecksum = lineaOrigen.IndexOf(CSUM_INICIO);
                    if (indiceChecksum > 0)
                    {
                        string linea = lineaOrigen.Substring(0, indiceChecksum - 1);

                        for (int j = 0; j < linea.Length; j++)
                            cs += (char)linea[j];
                    }
                }
                return cs;
            }
        }
    }
}
