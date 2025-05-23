using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using GEO_DROID.Resources.Lib.Comunicacion;

namespace BLL.LeerInfoMaquina
{
    class ProtocoloGigames : Protocolo
    {
        string _password;
        int _timeoutDefault;
            
        public ProtocoloGigames(IComunicacion com, IFiltroTrama filtroTrama, string password, int timeout)
            : base(com, filtroTrama)
        {
            _password = password;
            _timeoutDefault = timeout;
        }

        protected override StringBuilder MontarTrama(byte comando, byte[] datos)
        {
            return null;
        }

        protected override char CrearChecksum(StringBuilder sb)
        {
            return (char)0x00;
        }

        protected override bool ComprobarChecksum(StringBuilder sb)
        {
            return true;
        }

        public override InfoContadores LeerContadores(IProgressCallback callback)
        {
            InfoContadores info = null;
            _error = "";

            /// Vamos a ir probando protocolos desde el último al primero de los que tenemos implementados
            EscribeLog("G2:INTENTO COMUNICAR");
            Protocolo p = null;
            p = new ProtocoloGigames2(_com, _filtroTrama, _password, _timeoutDefault);
            info = p.LeerContadores();
            if (info == null)// && !p.IsProtocoloOK)
            {
                // Si parece que no sea el protocolo correcto probamos con Gigames1
                _error += "(G2)" + p.Error;
                EscribeLog("G1:INTENTO COMUNICAR");
                p = new ProtocoloGigames1(_com, _filtroTrama, _password, _timeoutDefault);
                        ////// Aqui necesitamos cambiar la configuración del puerto
                        //////_com.ConfigurarComunicacion(p.ConfiguracionPuertoSerie);
                        //////((ProtocoloFranco2)p).EnviarComandoTonto();
                info = p.LeerContadores();
                if (info == null) // && !p.IsProtocoloOK)
                {
                    _error += "(G1)" + p.Error;
                }
                else
                {
                    _error = "";
                }
            }
            else
            {
                _error = "";
            }
            return info;
        }
    }
}
