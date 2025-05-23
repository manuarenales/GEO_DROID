
#if ANDROID
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Android.App;
using GEO_DROID.Resources.Lib.Comunicacion;
#endif
namespace BLL.LeerInfoMaquina
{
#if ANDROID
    public class ModuloBluetooth : IDisposable, IComunicacion
    {
        //public enum ProtocoloMaquina { BARCREST, BELLFRUIT, CIRSA, COSTACALIDA, FRANCO, GIGAMES, SENTE, SLEIC, TECNAUSA, TOURVISION, WORTE, NONE };

        const bool LEER_HOPPERS = false; //LICENCIA 000018=true resto=false

        protected bool _disposed = false;

        const string ESTADO_CONEXION = "01";
        const string ESTADO_CONFIGURACION = "02";
        const string ESTADO_LECTURACONTADORES = "03";
        const string ESTADO_DESCONEXION = "04";

        public delegate void RefrescaEstadoDelegate(string texto);

        Comunicacion _com;
        ProtocoloTecnausa _protocoloTecnausa;
        ProtocoloAra _protocoloAra;
        ProtocoloMaquina _p; //protocolo que se ha seleccionado para inicializar
        string _passwordMaquina;
        int _timeout = 0;

        string _estado;
        string _error;

        // PROPIEDADES DE LA CLASE////////////
        public string Error
        {
            get { return _error; }
        }

        public string ErrorInfo
        {
            get { return ""; }
        }

        public string Estado
        {
            get { return _estado; }
        }

        public bool IsReady
        {
            get { return (_com != null) ? _com.IsReady() : false; }
        }

        public StringBuilder UltimoRecibido
        {
            get { return _com.RecibirDatos(0); }
        }

        decimal _valorPasoEntradas;
        public decimal ValorPasoEntradas
        {
            get { return _valorPasoEntradas; }
            set { _valorPasoEntradas = value; }
        }

        decimal _valorPasoSalidas;
        public decimal ValorPasoSalidas
        {
            get { return _valorPasoSalidas; }
            set { _valorPasoSalidas = value; }
        }

        /***************/
        /* CONSTRUCTOR */
        /***************/
        public ModuloBluetooth(ProtocoloMaquina p)
            : this(p, null)
        {
        }

        /*public ModuloBluetooth(ProtocoloMaquina p, string passwordMaquina)
            : this(p, passwordMaquina, 0)
        {
        }*/

        public ModuloBluetooth(ProtocoloMaquina p, string passwordMaquina)
        {
            _p = p;
            _passwordMaquina = passwordMaquina;
        }

        public ModuloBluetooth(ProtocoloMaquina p, string passwordMaquina, int timeout)
        {
            _p = p;
            _passwordMaquina = passwordMaquina;
            _timeout = timeout;
        }

        /*public static void InicializarLibrerias()
        {
            ComunicacionBTFranson.IsWidcommStack();
        }
        
        /// <summary>
        /// Método para linerar recursos (memoria,...)
        /// </summary>
        /// <param name="cerrandoAplicacion">true si se quieren liberar recursos para salir de la aplicación</param>
        public static void LiberarRecursos(bool cerrandoAplicacion)
        {
            if (cerrandoAplicacion)
            {
                ComunicacionBTFranson.LiberarRecursos();
            }
        }*/

        /************/
        /* CONECTAR */
        /************/
        /*public bool Conectar(string puerto, IProgressCallback callback)
        {
            return Conectar(puerto, 0, callback);
        }

        public bool Conectar(string puerto, int baudrate, IProgressCallback callback)
        {
            //if (_p != null)
            //{
                if (baudrate <= 0)
                {
                    if (_p == ProtocoloMaquina.BELLFRUIT)
                        baudrate = 4800;
                    else
                        baudrate = 9600;
                }

                _com = new ComunicacionSerie(puerto, baudrate, 0, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);

                if (_com != null && _com.Conectar(callback))
                    return true;
                else
                    _error = "Error en la conexión";
                _com.Dispose();
                _com = null;
            //}
            //else
            //{
            //    _error = "Protocolo de máquina no establecido";
            //}
            return false;
        }*/

        //private bool Conectar(string puerto, byte configuracion, IProgressCallback callback)
        //{
        //    _com = new ComunicacionSerie(puerto, baudrate, 0, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);

        //    if (_com != null && _com.Conectar(callback))
        //        return true;
        //    else
        //        _error = "Error en la conexión";
        //    return false;
        //}

        public bool Conectar(string mac, Activity activity, ModuloBluetooth.RefrescaEstadoDelegate dEstado, TipoDispositivoBTEnum tipoDispositivo, String pin = "")
        {
            try
            {

                _com = ComunicacionBT.GetComunicacionBT(mac, tipoDispositivo, pin);
                //_com = ComunicacionBT.GetComunicacionBT(mac, (TipoDispositivoBTEnum)BTUtils.BluetoothConnectionType.InsecureBTCon);
                _com.RefrescaEstadoDel = dEstado;
                (_com as ComunicacionBT).SetActivity(activity);
                if (ConectarModulo(null))
                    return true;
            }
            catch (Exception ex)
            {
                _error = "Error en la conexión: " + ex;
            }
            if (_com != null)
                _com.Dispose();
            _com = null;
            return false;
        }

        public bool ConectarTodos(string mac, string pin, IProgressCallback callback, ModuloBluetooth.RefrescaEstadoDelegate dEstado)
        {
            try
            {
                _com = ComunicacionBT.GetComunicacionBT(mac, (TipoDispositivoBTEnum)BTUtils.BluetoothConnectionType.OfficialBTCon);
                _com.RefrescaEstadoDel = dEstado;
                if (ConectarModulo(callback))
                    return true;

                _com = ComunicacionBT.GetComunicacionBT(mac, (TipoDispositivoBTEnum)BTUtils.BluetoothConnectionType.InsecureBTCon);
                _com.RefrescaEstadoDel = dEstado;
                if (ConectarModulo(callback))
                    return true;

                _com = ComunicacionBT.GetComunicacionBT(mac, (TipoDispositivoBTEnum)BTUtils.BluetoothConnectionType.ReflexedBTCon);
                _com.RefrescaEstadoDel = dEstado;
                if (ConectarModulo(callback))
                    return true;
            }
            catch (Exception ex)
            {
                _error = "Error en la conexión: " + ex;
            }
            if (_com != null)
                _com.Dispose();
            _com = null;
            return false;
        }

        private bool ConectarModulo(IProgressCallback callback)
        {
            _error = null;
            _estado = ESTADO_CONEXION;
            if (_com != null)
            {
                _com.LimpiarBuffer();
                if (_com.Conectar(callback))
                {
                    //// Recibir versión de firmware
                    StringBuilder sb = _com.RecibirDatos(1000, 200);
                    //if (_protocoloTecnausa == null) esto se comprobaría si no quisieramos reiniciar la variable, pero no le veo sentido a eso
                    _protocoloTecnausa = ProtocoloTecnausa.Get(this, sb);

                    /*byte[] buffer = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
                    string hex = BitConverter.ToString(buffer);
                    hex=hex.Replace("-", "");*/


                    if (_protocoloTecnausa != null)
                    {
                        // Enviamos respuesta a la conexión ya que el módulo v2, al conectar, nos envía la versión continuamente hasta que le respondemos
                        _protocoloTecnausa.EnviarRespuestaConexion();
                        return true;
                    }
                    else
                    {
                        _error = "Conexión establecida. Protocolo no identificado";
                    }
                    _com.Desconectar();
                }
                else
                {
                    _error = "No se ha podido conectar. Compruebe MAC y PIN";
                }
            }
            else
            {
                _error = "Conexión no inicializada";
            }

            return false;
        }

        /***************/
        /* DESCONECTAR */
        /***************/
        public bool Desconectar()
        {
            return Desconectar(null);
        }

        public bool Desconectar(IProgressCallback callback)
        {
            _estado = ESTADO_DESCONEXION;
            if (_com != null && _com.Desconectar(callback))
            {
                //if (_com is ComunicacionBT)
                //    ((ComunicacionBT)_com).Desvincular();
                return true;
            }
            else if (_com != null)
            {
                _error = _com.Estado() + "," + _com.Error;
            }
            else
            {
                _error = "Conexión no inicializada";
            }
            return false;
        }

        /*******************/
        /* Programar */
        /*******************/
        public bool Programar(String[] content, IProgressCallback progreso)
        {
            String lectura = "";

            if (!IsReady)
            {
                _error = "Comunicación no inicializada";
                return false;
            }

            if (_protocoloTecnausa != null)
            {
                lectura = null;

                // OJO!! Hay que comprobar primero las clases heredadas, ya que también se identificarán como la padre
                if (_protocoloTecnausa is ProtocoloTecnausaV56)
                    lectura = ""; //Distinto de null, aunque sin valor porque la clase ProtocoloTecnausaV56 sabe que archivo utilizar
                else if (_protocoloTecnausa is ProtocoloTecnausaV3)
                    lectura = content[2]; //rutaArchivo = rutaArchivo.Replace("PRI.BIN", "PRIV3.BIN");
                else if (_protocoloTecnausa is ProtocoloTecnausaV2)
                    lectura = content[1]; //rutaArchivo = rutaArchivo.Replace("PRI.BIN", "PRIv2.BIN");

                if (lectura != null)
                {
                    string ret = _protocoloTecnausa.Programar(lectura, progreso);
                    if (ret != null && ret.Length > 0)
                        _error = ret;
                    else
                        return true;
                }
                else
                {
                    _error = "Protocolo del módulo no reconocido";
                }
            }
            else
            {
                _error = "Protocolo del módulo no establecido";
            }
            return false;
        }

        public string GetAPNConfig(IProgressCallback progreso)
        {
            if (!IsReady)
            {
                _error = "Comunicación no inicializada";
                return null;
            }

            if (_protocoloTecnausa != null)
            {
                // OJO!! Hay que comprobar primero las clases heredadas, ya que también se identificarán como la padre
                if (_protocoloTecnausa is ProtocoloTecnausaV56)
                    return ((ProtocoloTecnausaV56)_protocoloTecnausa).GetAPNConfig();
                else
                    _error = "Protocolo del módulo no reconocido";
            }
            else
            {
                _error = "Protocolo del módulo no establecido";
            }
            return null;
        }

        public bool SetAPNConfig(string configFileName, IProgressCallback progreso)
        {
            if (!IsReady)
            {
                _error = "Comunicación no inicializada";
                return false;
            }

            if (_protocoloTecnausa != null)
            {
                // OJO!! Hay que comprobar primero las clases heredadas, ya que también se identificarán como la padre
                if (_protocoloTecnausa is ProtocoloTecnausaV56)
                    return ((ProtocoloTecnausaV56)_protocoloTecnausa).SetAPNConfig(configFileName);
                else
                    _error = "Protocolo del módulo no reconocido";
            }
            else
            {
                _error = "Protocolo del módulo no establecido";
            }
            return false;
        }

        /*******************/
        /* SET CONTADORES */
        /*******************/
        public bool SetContadores(int mec1, int mec2, int mec3, int mec4)
        {
            if (!IsReady)
            {
                _error = "Comunicación no inicializada";
                return false;
            }

            if (_protocoloTecnausa != null)
            {
                if (_protocoloTecnausa.SetContadoresMecanicos(1, mec1))
                    if (_protocoloTecnausa.SetContadoresMecanicos(2, mec2))
                        if (_protocoloTecnausa.SetContadoresMecanicos(3, mec3))
                            return _protocoloTecnausa.SetContadoresMecanicos(4, mec4);
            }
            else
            {
                _error = "Protocolo del módulo no establecido";
            }
            return false;
        }

        private bool Configurar(byte configuracion)
        {
            return Configurar(ProtocoloTecnausa.PuertoSerie.Serie0, configuracion);
        }

        private bool Configurar(ProtocoloTecnausa.PuertoSerie puertoSerie, byte configuracion)
        {
            if (_protocoloTecnausa != null)
            {
                if (_protocoloTecnausa.EnviarConfiguracion(puertoSerie, configuracion))
                    return true;
                else
                    _error = "Error al configurar el módulo";
                return false;
            }
            //else
            //{
            //    return true;
            //    //_error = "Protocolo de máquina no identificado";
            //}
            return true;
        }

        /*******************/
        /* LEER CONTADORES */
        /*******************/
        public InfoContadores LeerContadores()
        {
            return LeerContadores(null);
        }

        private static Protocolo GetProtocolo(ProtocoloMaquina p, IComunicacion com, IFiltroTrama filtroTrama, string passwordMaquina, int timeout)
        {
            Protocolo ret = null;

            switch (p)
            {
                case ProtocoloMaquina.BARCREST:
                    ret = new ProtocoloGigames2(com, filtroTrama, passwordMaquina, timeout);
                    break;
                case ProtocoloMaquina.BELLFRUIT:
                    ret = new ProtocoloBellFruit(com, filtroTrama);
                    break;
                case ProtocoloMaquina.CIRSA:
                    ret = new ProtocoloUnidesa(com, filtroTrama, passwordMaquina, timeout);
                    break;
                case ProtocoloMaquina.COSTACALIDA:
                    ret = new ProtocoloCostaCalida(com, filtroTrama, passwordMaquina);
                    break;
                case ProtocoloMaquina.FRANCO:
                    ret = new ProtocoloFranco(com, filtroTrama, passwordMaquina, timeout);
                    break;
                case ProtocoloMaquina.GIGAMES:
                    ret = new ProtocoloGigames(com, filtroTrama, passwordMaquina, timeout);
                    break;
                case ProtocoloMaquina.SENTE:
                    ret = new ProtocoloSente(com, filtroTrama, passwordMaquina);
                    break;
                case ProtocoloMaquina.SLEIC:
                    ret = new ProtocoloSleic(com, filtroTrama, timeout);
                    break;
                case ProtocoloMaquina.TOURVISION:
                    ret = new ProtocoloTourvision(com, filtroTrama, passwordMaquina, timeout);
                    break;
                case ProtocoloMaquina.TECNAUSA:
                    ret = new ProtocoloTecnausa(com);
                    break;
                case ProtocoloMaquina.WORTE:
                    ret = new ProtocoloGigames2(com, filtroTrama, passwordMaquina, timeout);
                    break;
                case ProtocoloMaquina.GEMINI:
                    ret = new ProtocoloGemini(com, filtroTrama);
                    break;
                case ProtocoloMaquina.AMATIC:
                case ProtocoloMaquina.NOVOMATIC_SAS:
                    ret = new ProtocoloSAS(com, filtroTrama, passwordMaquina, timeout);
                    break;
                case ProtocoloMaquina.MERKUR:
                    ret = new ProtocoloMerkur(com, filtroTrama, passwordMaquina);
                    break;
                case ProtocoloMaquina.OLYMPIC:
                    ret = new ProtocoloOlympic(com, filtroTrama);
                    break;
                case ProtocoloMaquina.NOVOMATIC:
                    ret = new ProtocoloGSP(com, filtroTrama, timeout);
                    break;
                case ProtocoloMaquina.ZITRO:
                    ret = new ProtocoloZitro(com, filtroTrama, passwordMaquina, timeout);
                    break;
                case ProtocoloMaquina.GISTRA:
                    ret = new ProtocoloGistra(com, filtroTrama, timeout);
                    break;

            }
            return ret;
        }

        private static int GetBitsDeDatos(byte b)
        {
            return ((b & 0x08) == 0) ? 8 : 7;
        }

        private static int GetBaudrate(byte b)
        {
            int ret = 9600; // por defecto ponemos 9600
            int baudrate = b & 0x07;

            switch (baudrate)
            {
                case 0:
                    ret = 600;
                    break;
                case 1:
                    ret = 1200;
                    break;
                case 2:
                    ret = 2400;
                    break;
                case 3:
                    ret = 4800;
                    break;
                case 4:
                    ret = 9600;
                    break;
                case 5:
                    ret = 19200;
                    break;
            }

            return ret;
        }

        public InfoContadores LeerContadores(IProgressCallback callback)
        {
            InfoContadores info = null;
            InfoContadores infoMecanicos = null;

            // Comprobamos si estamos conectados
            if (!IsReady)
            {
                _error = "Comunicación no inicializada";
                return null;
            }

            _estado = ESTADO_LECTURACONTADORES;

#if LibreriaRecaudacionBT
            // Llamamos al método que verifica que el módulo de captura es 'made in TECNAUSA'
            if (_protocoloTecnausa is ProtocoloTecnausaV2)
            {
                if (!_protocoloTecnausa.Verificar(_com.GetHashCode()))
                {
                    _error = "Protocolo incorrecto";
                    return null;
                }
            }
#endif

            InfoContadores infoHoppers = null;
            // Leemos los contadores de hoppers
            if (LEER_HOPPERS)
            {
                _protocoloAra = new ProtocoloAra(this, _protocoloTecnausa);
                if (Configurar(ProtocoloTecnausa.PuertoSerie.Serie1, _protocoloAra.ConfiguracionPuertoSerie))
                    infoHoppers = _protocoloAra.LeerContadores();
            }

            // Leemos los contadores mecanicos
            if (_protocoloTecnausa is ProtocoloTecnausaV2)
                infoMecanicos = _protocoloTecnausa.LeerContadores(callback);

            // Leemos los contadores electrónicos
            if (_p == ProtocoloMaquina.TECNAUSA)
            {
                if (_protocoloTecnausa is ProtocoloTecnausa)
                    info = _protocoloTecnausa.LeerContadores(callback);
            }
            else
            {
                Protocolo p = GetProtocolo(_p, this, _protocoloTecnausa, _passwordMaquina, _timeout);
                if (p != null)
                {
                    // Asignamos los valores de paso para contadores que vamos a recibir en importes
                    // Calcularemos los pasos con esa información
                    if (p is ProtocoloBellFruit)
                    {
                        ((ProtocoloBellFruit)p).ValorPasoSalidas = _valorPasoSalidas;
                    }
                    else if (p is ProtocoloSente)
                    {
                        ((ProtocoloSente)p).ValorPasoEntradas = _valorPasoEntradas;
                        ((ProtocoloSente)p).ValorPasoSalidas = _valorPasoSalidas;
                    }

                    if (Configurar(p.ConfiguracionPuertoSerie))
                    {
                        info = p.LeerContadores(callback);
                        if (info == null)
                        {
                            _error = p.Error;
                        }
                        /*else
                        {
                            UtilsBLL.EscribeFichero(BLL.AccesoBD.getAppPath() + "\\lecturaElectronica.txt", info.Buffer);
                            UtilsBLL.EscribeFichero(BLL.AccesoBD.getAppPath() + "\\lecturaElectronicaHEX.txt", info.BufferHex);
                        }*/
                    }
                    else
                    {
                        _error = "Error configurando módulo";
                    }
                }
                else
                {
                    if (infoMecanicos == null && p == null)
                        _error = "Protocolo de máquina no implementado";
                }
            }
            // Preparamos los contadores a devolver en función de los que hemos obtenido
            if (info == null)
                info = infoMecanicos;
            else
                info.MergeMecanicos(infoMecanicos);

            // Asignamos el contador de hoppers
            if (info != null && infoHoppers != null)
                info.Auxiliar1 = infoHoppers.Auxiliar1;
            if (_protocoloAra != null && !string.IsNullOrEmpty(_protocoloAra.Error))
                _error += " Error lectura hoppers:" + _protocoloAra.Error;

            return info;
        }

        /*******************/
        /* LEER CONTADORES */
        /*******************/
        public bool Reconectar(IProgressCallback progreso)
        {
            if (Desconectar())
            {
                //Esperamos un poco a que la placa se reinicialice...
                //for (int i = 0; i < 20; i++)
                //{
                //    System.Threading.Thread.Sleep(1000);
                //    if (progreso != null)
                //        progreso.SetText("Reconectando...("+i+")");
                //}
                //Volvemos a conectar
                if (progreso != null)
                    progreso.SetText("Reconectando...(-)");
                System.Threading.Thread.Sleep(2000);
                if (ConectarModulo(null))
                    return true;
                _error = "Problemas al reconectar";
                //ret = "Problemas al reconectar";
            }
            else
            {
                _error = "No hemos podido cerrar la conexión para reconectar";
            }
            _com.Dispose();
            _com = null;
            return false;
        }

        public bool ConfigurarComunicacion(byte parametros)
        {
            if (_protocoloTecnausa == null)
            {
                //HexDump
                //Cable
                /*if (_com is ComunicacionSerie)
                {
                    // Desconectar y volver a conectar
                    _com.Desconectar();
                    switch (parametros)
                    {
                        case 0x03: //4800 8N1
                            _com = new ComunicacionSerie(((ComunicacionSerie)_com).Puerto, 4800, 0, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
                            break;
                        case 0x54: //9600 8E2
                            _com = new ComunicacionSerie(((ComunicacionSerie)_com).Puerto, 9600, 0, System.IO.Ports.Parity.Even, 8, System.IO.Ports.StopBits.Two);
                            break;
                        default: //9600 8N1 
                            _com = new ComunicacionSerie(((ComunicacionSerie)_com).Puerto, 9600, 0, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
                            break;
                    }
                    return _com.Conectar();
                }*/
            }
            else if (_protocoloTecnausa is ProtocoloTecnausaV2)
            {
                return Configurar(parametros);// _protocoloTecnausa.EnviarConfiguracion(parametros);
            }
            return false;
            //if (_com is ComunicacionSerie)
            //{
            //    // Serie
            //    // Desconectar y volver a conectar
            //    // TODO:  ...
            //}
            //else
            //{
            //    // Bluetooth...
            //    return _protocoloTecnausa.EnviarConfiguracion(parametros);

            //}
            //return false;
        }

        public bool EnviarDatos(StringBuilder datos)
        {
            return _com.EnviarDatos(datos);
        }

        public StringBuilder RecibirDatos(int timeout, int timeout2)
        {
            return _com.RecibirDatos(timeout, timeout2);
        }

        public void LimpiarBuffer()
        {
            _com.LimpiarBuffer();
        }

        // TEST
        //public static bool TestProtocoloGigames()
        //{
        //    return ProtocoloGigames.TestTramaGigames();
        //}
        //        public enum ProtocoloMaquina { TECNAUSA, FRANCO, CIRSA, COSTACALIDA };

        //        //const string ESTADO_CONEXION = "01";
        //        //const string ESTADO_CONFIGURACION = "02";
        //        //const string ESTADO_LECTURACONTADORES = "03";
        //        //const string ESTADO_DESCONEXION = "04";

        //        Comunicacion _com;
        //        ProtocoloTecnausa _protocoloTecnausa;
        //        Protocolo _protocolo;

        //        ProtocoloMaquina _p;
        //        bool _configReconectar = false;

        //        string _estado;
        //        string _error;

        //        //string _version;
        //        //string _revision;

        //        public string Version
        //        {
        //            get { return null; }
        ////            get { return (_protocoloTecnausa == null)?null:_protocoloTecnausa.Version; }
        //        }

        //        public string Error
        //        {
        //            get { return _error; }
        //        }

        //        public string Estado
        //        {
        //            get { return _estado; }
        //        }

        //        public bool IsReady
        //        {
        //            get { return (_com != null) ? _com.IsReady() : false; }
        //        }

        //        public StringBuilder UltimoRecibido
        //        {
        //            get { return _com.RecibirDatos(0); }
        //        }

        //        public Comunicacion.RefrescaEstadoDelegate DelegateEstado
        //        {
        //            get
        //            {
        //                return _com.RefrescaEstadoDel;
        //            }
        //            set
        //            {
        //                _com.RefrescaEstadoDel = value;
        //            }
        //        }

        //        // CONSTRUCTOR
        //        public ModuloBluetooth(ProtocoloMaquina p)
        //        {
        //            _p = p;
        //        }

        //        //public ModuloBluetooth(ProtocoloMaquina p, string password, int timeout, int timeout2)
        //        //{
        //        //    _protocolo = new ProtocoloCostaCalida(
        //        //}

        //        //public ModuloBluetooth(string mac, string pin)
        //        //{
        //        //    _com = new ComunicacionBT(mac, pin);
        //        //}

        //        //public ModuloBluetooth(string puerto) //"COM1, ..."
        //        //{
        //        //    _com = new ComunicacionSerie(mac, pin);
        //        //}

        //        /************/
        //        /* CONECTAR */
        //        /************/
        //        public bool Conectar(string mac, string pin)
        //        {
        //            _com = new ComunicacionBT(mac, pin);
        //            return Conectar(null);
        //        }

        //        public bool Conectar(string puerto, int baudrate)
        //        {
        //            _protocolo = new ProtocoloCostaCalida(puerto, null);
        //            return _protocolo.Abrir();
        //        }

        //        public bool Conectar()
        //        {
        //            return Conectar(null);
        //        }

        //        public bool Conectar(IProgressCallback callback)
        //        {
        //            _error = null;
        //            _estado = ESTADO_CONEXION;
        //            if (_com != null && _com.Conectar(callback))
        //            {
        //                //// Recibir versión de firmware
        //                _com.LimpiarBuffer();
        //                StringBuilder sb = _com.RecibirDatos(1000, 200);
        //                _protocoloTecnausa = ProtocoloTecnausa.Get(_com, sb);
        //                if (_protocoloTecnausa != null)
        //                {
        //                    _protocoloTecnausa.EnviarRespuestaConexion();
        //                    return true;
        //                }
        //                else
        //                {
        //                    _error = "Conexión establecida. Protocolo no identificado";
        //                }
        //            }
        //            else if (_com != null)
        //            {
        //                _error = "No se ha podido conectar. Compruebe MAC y PIN";
        //            }
        //            else
        //            {
        //                _error = "Conexión no inicializada.";
        //            }

        //            return false;
        //        }



        //        /***************/
        //        /* DESCONECTAR */
        //        /***************/
        //        public bool Desconectar()
        //        {
        //            return Desconectar(null);
        //        }

        //        public bool Desconectar(IProgressCallback callback)
        //        {
        //            _estado = ESTADO_DESCONEXION;
        //            if (_com != null && _com.Desconectar(callback))
        //            {
        //                return true;
        //            }
        //            else if (_com != null)
        //            {
        //                _error = _com.Estado() + "," + _com.Error;
        //            }
        //            else
        //            {
        //                _error = "Conexión no inicializada";
        //            }
        //            return false;
        //        }

        //        /**************/
        //        /* CONFIGURAR */
        //        /**************/
        //        public bool Configurar(ProtocoloMaquina p, string password, int timeout)
        //        {
        //            _estado = ESTADO_CONFIGURACION;
        //            //Protocolo puerto pass timeout timeout2
        //            switch (p)
        //            {
        //                case ProtocoloMaquina.TECNAUSA:
        //                    _protocolo = new ProtocoloTecnausa(_com);
        //                    break;
        //                case ProtocoloMaquina.COSTACALIDA:
        //                    _protocolo = new ProtocoloCostaCalida(_com, _protocoloTecnausa, null);
        //                    break;
        //                case ProtocoloMaquina.FRANCO:
        //                    _protocolo = new ProtocoloFranco(_com, _protocoloTecnausa, this, null, 0);
        //                    break;
        //            }

        //            return ConfigurarComunicacion(_protocolo.ConfiguracionPuertoSerie);
        //        }

        //        public bool ConfigurarComunicacion(byte parametros)
        //        {
        //            if (_configReconectar)
        //            {
        //                // 
        //                Desconectar();
        //                Conectar();
        //            }
        //            else
        //            {
        //                if (_protocolo != null && _protocoloTecnausa != null)
        //                {
        //                    if (_protocoloTecnausa.EnviarConfiguracion(parametros))
        //                        return true;
        //                    else
        //                        _error = "Error al configurar el módulo";
        //                }
        //                else if (_protocolo != null)
        //                {
        //                    _error = "Protocolo del módulo no identificado";
        //                }
        //                else
        //                {
        //                    _error = "Protocolo de máquina no identificado";
        //                }
        //            }
        //            return false;
        //        }

        //        /*******************/
        //        /* SET CONTADORES */
        //        /*******************/
        //        // TODO:
        //        public bool SetContadores(int mec1, int mec2, int mec3, int mec4)
        //        {
        //            if (_protocoloTecnausa != null)
        //            {
        //                if (_protocoloTecnausa.SetContadoresMecanicos(1, mec1))
        //                    if (_protocoloTecnausa.SetContadoresMecanicos(2, mec2))
        //                        if (_protocoloTecnausa.SetContadoresMecanicos(3, mec3))
        //                            return _protocoloTecnausa.SetContadoresMecanicos(4, mec4);
        //            }
        //            else
        //            {
        //                _error = "Protocolo del módulo no establecido";
        //            }
        //            return false;
        //        }

        //        /*******************/
        //        /* LEER CONTADORES */
        //        /*******************/
        //        public InfoContadores LeerContadores()
        //        {
        //            InfoContadores info = null;
        //            InfoContadores infoMecanicos = null;

        //            _estado = ESTADO_LECTURACONTADORES;
        //            // Leemos los contadores mecanicos
        //            if (_protocoloTecnausa is ProtocoloTecnausaV2)
        //                infoMecanicos = _protocoloTecnausa.LeerContadores();

        //            // Leemos los contadores electrónicos
        //            if (_protocolo != null)
        //            {
        //                info = _protocolo.LeerContadores();
        //                if (info == null)
        //                    _error =  _protocolo.Error;
        //                else
        //                    info.MergeMecanicos(infoMecanicos);
        //            }
        //            else
        //            {
        //                return infoMecanicos;
        //                //_error = "Protocolo no iniciado";
        //            }

        //            return info;
        //        }

        //        /*******************/
        //        /* Programar */
        //        /*******************/
        //        public bool Programar(string rutaArchivo, IProgressCallback progreso)
        //        {
        //            if (_protocoloTecnausa != null)
        //            {
        //                string ret = _protocoloTecnausa.Programar(rutaArchivo, progreso);
        //                if (ret != null && ret.Length > 0)
        //                    _error = ret;
        //                else
        //                    return true;
        //            }
        //            else
        //            {
        //                _error = "Protocolo no inicializado";
        //            }
        //            return false;
        //        }

        /***************************************************************************/
        // Destructor y liberación de recursos
        /***************************************************************************/
        ~ModuloBluetooth()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    // Libero todo lo que he manejado
                    if (_com != null) _com.Dispose();
                }

                _disposed = true;
            }
        }


    }
#else 
public class ModuloBluetooth {}
#endif
}
