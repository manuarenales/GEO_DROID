using System;
using System.Collections.Generic;
using System.Text;
using GEO_DROID.Resources.Lib.Comunicacion;

namespace BLL.LeerInfoMaquina
{
    class ProtocoloFranco : Protocolo
    {
        //string _puerto;
        //string _mac;
        //string _pin;
        string _password;
        int _timeoutDefault;

        public ProtocoloFranco(IComunicacion com, IFiltroTrama filtroTrama, string password, int timeout)
            : base(com, filtroTrama)
        {
            _password = password;
            _timeoutDefault = timeout;
        }

        protected override char CrearChecksum(StringBuilder sb)
        {
            return (char)0x00;
        }

        protected override bool ComprobarChecksum(StringBuilder sb)
        {
            return false;
        }

        protected override StringBuilder MontarTrama(byte comando, byte[] datos) //throws CheckSumException
        {
            return null;
        }

        public override InfoContadores LeerContadores(IProgressCallback callback)
        {
            InfoContadores info = null;
            _error = "";
            
            // Primero probamos con Franco 3
            EscribeLog("F3:INTENTO COMUNICAR");
            Protocolo p = null;
            
            p = new ProtocoloFranco3(_com, _filtroTrama, _password, _timeoutDefault);
            info = p.LeerContadores();
            
            if (info == null && !p.IsProtocoloOK)
            {
                // Si parece que no sea el protocolo correcto probamos con Franco 2
                _error += "(F3)" + p.Error;
                EscribeLog("F2:INTENTO COMUNICAR");
                p = new ProtocoloFranco2(_com, _filtroTrama, _password, _timeoutDefault);
                // Aqui necesitamos cambiar la configuración del puerto
                _com.ConfigurarComunicacion(p.ConfiguracionPuertoSerie);
                ((ProtocoloFranco2)p).EnviarComandoTonto();
                info = p.LeerContadores();

                if (info == null && !p.IsProtocoloOK)
                {
                    // Si parece que no sea el protocolo correcto probamos con Franco 1
                    _error += "(F2)" + p.Error;
                    EscribeLog("F1:INTENTO COMUNICAR");
                    p = new ProtocoloFranco1(_com, _filtroTrama, _password, _timeoutDefault);
                    info = p.LeerContadores();

                    if (info == null && !p.IsProtocoloOK)
                    {
                        _error += "(F1)" + p.Error;
                    }
                    else if (info == null)
                    {
                        _error += "(F1)" + p.Error;
                    }
                    else
                    {
                        _error = "";
                    }
                }
                else if (info == null)
                {
                    _error += "(F2)" + p.Error;
                }
                else
                {
                    _error = "";
                    
                }
            }
            else if (info == null)
            {
                _error += "(F3)" + p.Error;
            }
            else
            {
                _error = "";
            }
            return info;
        }
    }
}
