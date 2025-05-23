using System;
using System.Collections.Generic;
using System.Text;
using GEO_DROID.Resources.Lib.Comunicacion;

namespace BLL.LeerInfoMaquina
{
    abstract class Protocolo : IDisposable
    {
        protected bool _disposed = false;

        protected IComunicacion _com;
        protected IFiltroTrama _filtroTrama;

        protected string _error = "";
        protected string _errorEstado = "";
        protected string _errorInfo = "";
        protected bool _protocoloOK = true;

        public string Error
        {
            get
            {
                string s = "";
                if (_error != null && _error.Length > 0)
                    s += "Error:" + _error;
                if (_errorEstado != null && _errorEstado.Length > 0)
                    s += _errorEstado;
                //if (_com != null && _com.Error != null && _com.Error.Length > 0)
                //{
                //    if (s.Length > 0)
                //        s += " - ";
                //    s += "Error COM:" + _com.Error;
                //}
                return s;
            }
        }

        public string ErrorInfo
        {
            get
            {
                string s = "";
                if (_errorInfo != null && _errorInfo.Length > 0)
                    s += _errorInfo;
                return s;
            }
        }

        public bool IsProtocoloOK
        {
            get { return _protocoloOK; }
        }

        /// <summary>
        /// ---Configuración del puerto de comunicación con la máquina para los módulos de recaudación M2M---
        ///typedef union
        ///{
        ///    BYTE val;
        ///    struct
        ///    {
        ///        unsigned char baudios:3;
        ///        unsigned char RTS:1;
        ///        unsigned char parity_mask:3;
        ///        unsigned char DTR:1;
        ///    };
        ///}CFGSerie;
        ///
        ///#define B600            0
        ///#define B1200           1
        ///#define B2400           2
        ///#define B4800           3
        ///#define B9600           4
        ///#define B19200          5
        ///#define B38400          6
        ///#define B57600          7
        ///
        ///#define COM_PARITY_NONE   0
        ///#define COM_PARITY_ODD    1
        ///#define COM_PARITY_EVEN   2
        ///#define COM_PARITY_MARK   3
        ///#define COM_PARITY_SPACE  4
        ///#define COM_PARITY_WAKEUP 5
        ///
        /// Ejemplos: (1:DTR, 3:parity, 1:RTS, 3:baudrate)
        ///     9600, none -> 0000 0100
        ///     19200, even -> 0010 0101
        /// 
        /// </summary>
        public virtual byte ConfiguracionPuertoSerie
        {
            get { return (byte)0x04; }
        }

        public Protocolo(IComunicacion com, IFiltroTrama filtroTrama)
        {
            _com = com;
            _filtroTrama = filtroTrama;
        }

        protected abstract StringBuilder MontarTrama(byte comando, byte[] datos);
        protected abstract char CrearChecksum(StringBuilder sb);
        protected abstract bool ComprobarChecksum(StringBuilder sb);

        public abstract InfoContadores LeerContadores(IProgressCallback callback);
        public virtual InfoContadores LeerContadores()
        {
            return LeerContadores(null);
        }
        //public abstract bool LeerEventos();

        public StringBuilder RecibirDatos(int timeout)
        {
            return RecibirDatos(timeout, 0);
        }

        public StringBuilder RecibirDatos(int timeout, int timeout2)
        {
            if (_com != null)
                return _com.RecibirDatos(timeout, timeout2);
            return new StringBuilder();
        }

        public bool EnviarTrama(byte comando)
        {
            return EnviarTrama(comando, null);
        }

        public bool EnviarTrama(byte comando, byte[] datos)
        {
            if (_com != null)// && _com.IsReady())
            {
                StringBuilder trama = MontarTrama(comando, datos);
                if (_filtroTrama != null)
                    trama = _filtroTrama.FiltrarTrama(trama);
                return _com.EnviarDatos(trama);
            }
            _error = "Comunicacion no establecida";
            if (_com != null)
                _error = "Comunicacion no establecida";// (" + _com.IsReady() + ")"; 
            return false;
        }

        ~Protocolo()
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
                    // ...
                }

                _disposed = true;
            }
        }

        public static void EscribeLog(string msg)
        {
#if LibreriaRecaudacionBT
#elif PocketPC
            try
            {
                string rutaDestino = BLL.AccesoBD.getAppPath() + "\\logComunicacion.txt";
                System.IO.FileStream fs = new System.IO.FileStream(rutaDestino, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
                System.IO.StreamWriter m_streamWriter = new System.IO.StreamWriter(fs);
                m_streamWriter.BaseStream.Seek(0, System.IO.SeekOrigin.End);
                m_streamWriter.WriteLine(System.DateTime.Now + " : " + msg + AccesoBD.SALTO_LINEA);
                m_streamWriter.Flush();
                m_streamWriter.Close();
            }
            catch { }
#endif
        }

    }
}
