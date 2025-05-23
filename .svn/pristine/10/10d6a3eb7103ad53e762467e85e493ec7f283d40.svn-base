using System;
using System.Collections.Generic;
using System.Text;
using GEO_DROID.Resources.Lib.Comunicacion;

namespace BLL.LeerInfoMaquina
{
    class ProtocoloOlympic : Protocolo
    {
        // Conexión serie por defecto OLYMPIC: 9600, Parity.None, 8, StopBits.One

        int _timeoutDefault = 30000;

        public override byte ConfiguracionPuertoSerie
        {
            get { return (byte)0x04; } 
        }

        public ProtocoloOlympic(IComunicacion com, IFiltroTrama filtroTrama) 
            : base(com, filtroTrama)
        {
        }

        protected override char CrearChecksum(StringBuilder sb)
        {
            return (char)0x00;
        }

        protected override bool ComprobarChecksum(StringBuilder sb)
        {
            return true;
        }

        protected override StringBuilder MontarTrama(byte comando, byte[] datos) //throws CheckSumException
        {
            StringBuilder sbTemp = new StringBuilder();

            // El protocolo Bell Fruit responde al recibir el comando 'DuMp01'
            sbTemp.Append("DuMp01");
            
            return sbTemp;
        }

        public override InfoContadores LeerContadores(IProgressCallback callback)
        {
            _com.LimpiarBuffer();

            // El tiempo de espera entre byte/bloque de bytes lo ponemos a 2 segundos, 
            // porque al tratarse de un ticket puede enviar datos de forma más lenta.
            // Así nos curamos en salud.
            StringBuilder sb = RecibirDatos(_timeoutDefault, 2000);
            //StringBuilder sb = new StringBuilder(
            //"Jugadas:91, Premios:120, Partidas:29, Porcentaje:131.87\r\n" //+ AccesoBD.SALTO_LINEA +                                                                                               
            //);

            if (sb != null && sb.Length > 0)
            {
                InfoContadores info = ProcesarDatos(sb);
                info.Buffer = sb;
                _error = "";
                return info;
            }
            else
            {
                _error = "Recibido vacio";
            }

            return null;
        }

        private bool IsNumber(char c)
        {
            return (c == '0' || c == '1' || c == '2' || c == '3' || c == '4'
                || c == '5' || c == '6' || c == '7' || c == '8' || c == '9');
        }

        private int GetDatoContador(string texto, string datos)
        {
            StringBuilder sb = new StringBuilder();
            int _indice = datos.IndexOf(texto);
            int valor = -1;
            if (_indice >= 0)
            {
                _indice += texto.Length;
                while (IsNumber(datos[_indice]))
                {
                    sb.Append(datos[_indice]);
                    _indice++;
                }
                if (sb.Length > 0)
                {
                    try
                    {
                        valor = int.Parse(sb.ToString());
                    }
                    catch { }
                }
            }
            return valor;
        }

        private InfoContadores ProcesarDatos(StringBuilder datos)
        {
            int entradas = 0;
            int salidas = 0;
            int partidas = 0;

            if (datos != null)
            {
                string datosProceso = datos.ToString().Replace(" ", "");
                entradas = GetDatoContador("Jugadas:", datosProceso);
                salidas = GetDatoContador("Premios:", datosProceso);
                partidas = GetDatoContador("Partidas:", datosProceso);
            }

            InfoContadores info = new InfoContadores();
            info.Entradas = entradas;
            info.Salidas = salidas;
            info.Auxiliar1 = partidas;
            return info;
        }
    }
}
