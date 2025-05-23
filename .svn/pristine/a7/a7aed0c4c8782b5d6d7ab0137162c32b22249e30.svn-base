using System;
using System.Collections.Generic;
using System.Text;
using GEO_DROID.Resources.Lib.Comunicacion;

namespace BLL.LeerInfoMaquina
{
    class ProtocoloGemini : ProtocoloBellFruit
    {
        // Conexión serie por defecto GEMINI: 9600, Parity.None, 8, StopBits.One
        int _timeoutDefault = 30000;

        public override byte ConfiguracionPuertoSerie
        {
            get { return (byte)0x04; }
        }

        public ProtocoloGemini(IComunicacion com, IFiltroTrama filtroTrama) 
            : base(com, filtroTrama)
        {
        }

        public override InfoContadores LeerContadores(IProgressCallback callback)
        {
            // Limpiamos el buffer de entrada 
            if (_com != null) _com.LimpiarBuffer();
            
            // El tiempo de espera entre byte/bloque de bytes lo ponemos a 2 segundos, 
            // porque al tratarse de un ticket puede enviar datos de forma más lenta.
            // Así nos curamos en salud.
            StringBuilder sb = RecibirDatos(_timeoutDefault, 2000); // Para pruebas podemos usar: sb = GetEjemplo();

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

            if (datos != null)
            {
                string datosProceso = datos.ToString().Replace(" ", "");
                entradas = GetDatoContador("TOTALIN(x20cents)", datosProceso); //"TOTAL IN (x20 cents)"
                salidas = GetDatoContador("TOTALOUT(x20cents)", datosProceso); //"TOTAL OUT (x20 cents)"
            }

            InfoContadores info = new InfoContadores();
            info.Entradas = entradas;
            info.Salidas = salidas;
            return info;
        }
/*
        private StringBuilder GetEjemplo()
        {
            StringBuilder sb = new StringBuilder(
                "" + AccesoBD.SALTO_LINEA +
                "" + AccesoBD.SALTO_LINEA +
                "TOTAL IN (x20 cents) 655" + AccesoBD.SALTO_LINEA +
                "TOTAL OUT (x20 cents) 45" + AccesoBD.SALTO_LINEA +
                "ACTUAL PERCENTAGE 028.26" + AccesoBD.SALTO_LINEA +
                "TOTAL STAKED 13800" + AccesoBD.SALTO_LINEA +
                "TOTAL WON 3900" + AccesoBD.SALTO_LINEA +
                "" + AccesoBD.SALTO_LINEA +
                "YEAR      1" + AccesoBD.SALTO_LINEA +
                "TOTAL IN  655" + AccesoBD.SALTO_LINEA +
                "TOTAL OUT 45" + AccesoBD.SALTO_LINEA +
                "" + AccesoBD.SALTO_LINEA +
                "YEAR      2" + AccesoBD.SALTO_LINEA +
                "TOTAL IN  0" + AccesoBD.SALTO_LINEA +
                "TOTAL OUT 0" + AccesoBD.SALTO_LINEA +
                "" + AccesoBD.SALTO_LINEA +
                "YEAR      3" + AccesoBD.SALTO_LINEA +
                "TOTAL IN  0" + AccesoBD.SALTO_LINEA +
                "TOTAL OUT 0" + AccesoBD.SALTO_LINEA +
                "" + AccesoBD.SALTO_LINEA +
                "YEAR      4" + AccesoBD.SALTO_LINEA +
                "TOTAL IN  0" + AccesoBD.SALTO_LINEA +
                "TOTAL OUT 0" + AccesoBD.SALTO_LINEA +
                "" + AccesoBD.SALTO_LINEA +
                "YEAR      5" + AccesoBD.SALTO_LINEA +
                "TOTAL IN  0" + AccesoBD.SALTO_LINEA +
                "TOTAL OUT 0" + AccesoBD.SALTO_LINEA +
                "" + AccesoBD.SALTO_LINEA +
                "CYCLE 1" + AccesoBD.SALTO_LINEA +
                "END DATE 0 0 0" + AccesoBD.SALTO_LINEA +
                "PERCENTAGE 000.00" + AccesoBD.SALTO_LINEA +
                "STAKE 20 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 40 +0  9" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +0  175" + AccesoBD.SALTO_LINEA +
                "STAKE 20 +1  10" + AccesoBD.SALTO_LINEA +
                "STAKE 40 +2  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +3  38" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +5  6" + AccesoBD.SALTO_LINEA +
                "" + AccesoBD.SALTO_LINEA +
                "CYCLE 2" + AccesoBD.SALTO_LINEA +
                "END DATE 0 0 0" + AccesoBD.SALTO_LINEA +
                "PERCENTAGE 000.00" + AccesoBD.SALTO_LINEA +
                "STAKE 20 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 40 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 20 +1  0" + AccesoBD.SALTO_LINEA +
                "STAKE 40 +2  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +3  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +5  0" + AccesoBD.SALTO_LINEA +
                "" + AccesoBD.SALTO_LINEA +
                "CYCLE 3" + AccesoBD.SALTO_LINEA +
                "END DATE 0 0 0" + AccesoBD.SALTO_LINEA +
                "PERCENTAGE 000.00" + AccesoBD.SALTO_LINEA +
                "STAKE 20 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 40 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 20 +1  0" + AccesoBD.SALTO_LINEA +
                "STAKE 40 +2  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +3  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +5  0" + AccesoBD.SALTO_LINEA +
                "" + AccesoBD.SALTO_LINEA +
                "CYCLE 4" + AccesoBD.SALTO_LINEA +
                "END DATE 0 0 0" + AccesoBD.SALTO_LINEA +
                "PERCENTAGE 000.00" + AccesoBD.SALTO_LINEA +
                "STAKE 20 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 40 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 20 +1  0" + AccesoBD.SALTO_LINEA +
                "STAKE 40 +2  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +3  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +5  0" + AccesoBD.SALTO_LINEA +
                "" + AccesoBD.SALTO_LINEA +
                "CYCLE 5" + AccesoBD.SALTO_LINEA +
                "END DATE 0 0 0" + AccesoBD.SALTO_LINEA +
                "PERCENTAGE 000.00" + AccesoBD.SALTO_LINEA +
                "STAKE 20 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 40 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 20 +1  0" + AccesoBD.SALTO_LINEA +
                "STAKE 40 +2  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +3  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +5  0" + AccesoBD.SALTO_LINEA +
                "" + AccesoBD.SALTO_LINEA +
                "CYCLE 6" + AccesoBD.SALTO_LINEA +
                "END DATE 0 0 0" + AccesoBD.SALTO_LINEA +
                "PERCENTAGE 000.00" + AccesoBD.SALTO_LINEA +
                "STAKE 20 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 40 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 20 +1  0" + AccesoBD.SALTO_LINEA +
                "STAKE 40 +2  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +3  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +5  0" + AccesoBD.SALTO_LINEA +
                "" + AccesoBD.SALTO_LINEA +
                "CYCLE 7" + AccesoBD.SALTO_LINEA +
                "END DATE 0 0 0" + AccesoBD.SALTO_LINEA +
                "PERCENTAGE 000.00" + AccesoBD.SALTO_LINEA +
                "STAKE 20 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 40 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 20 +1  0" + AccesoBD.SALTO_LINEA +
                "STAKE 40 +2  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +3  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +5  0" + AccesoBD.SALTO_LINEA +
                "" + AccesoBD.SALTO_LINEA +
                "CYCLE 8" + AccesoBD.SALTO_LINEA +
                "END DATE 0 0 0" + AccesoBD.SALTO_LINEA +
                "PERCENTAGE 000.00" + AccesoBD.SALTO_LINEA +
                "STAKE 20 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 40 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 20 +1  0" + AccesoBD.SALTO_LINEA +
                "STAKE 40 +2  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +3  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +5  0" + AccesoBD.SALTO_LINEA +
                "" + AccesoBD.SALTO_LINEA +
                "CYCLE 9" + AccesoBD.SALTO_LINEA +
                "END DATE 0 0 0" + AccesoBD.SALTO_LINEA +
                "PERCENTAGE 000.00" + AccesoBD.SALTO_LINEA +
                "STAKE 20 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 40 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 20 +1  0" + AccesoBD.SALTO_LINEA +
                "STAKE 40 +2  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +3  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +5  0" + AccesoBD.SALTO_LINEA +
                "" + AccesoBD.SALTO_LINEA +
                "CYCLE 10" + AccesoBD.SALTO_LINEA +
                "END DATE 0 0 0" + AccesoBD.SALTO_LINEA +
                "PERCENTAGE 000.00" + AccesoBD.SALTO_LINEA +
                "STAKE 20 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 40 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 20 +1  0" + AccesoBD.SALTO_LINEA +
                "STAKE 40 +2  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +3  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +5  0" + AccesoBD.SALTO_LINEA +
                "" + AccesoBD.SALTO_LINEA +
                "CYCLE 11" + AccesoBD.SALTO_LINEA +
                "END DATE 0 0 0" + AccesoBD.SALTO_LINEA +
                "PERCENTAGE 000.00" + AccesoBD.SALTO_LINEA +
                "STAKE 20 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 40 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 20 +1  0" + AccesoBD.SALTO_LINEA +
                "STAKE 40 +2  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +3  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +5  0" + AccesoBD.SALTO_LINEA +
                "" + AccesoBD.SALTO_LINEA +
                "CYCLE 12" + AccesoBD.SALTO_LINEA +
                "END DATE 0 0 0" + AccesoBD.SALTO_LINEA +
                "PERCENTAGE 000.00" + AccesoBD.SALTO_LINEA +
                "STAKE 20 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 40 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 20 +1  0" + AccesoBD.SALTO_LINEA +
                "STAKE 40 +2  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +3  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +5  0" + AccesoBD.SALTO_LINEA +
                "" + AccesoBD.SALTO_LINEA +
                "CYCLE 13" + AccesoBD.SALTO_LINEA +
                "END DATE 0 0 0" + AccesoBD.SALTO_LINEA +
                "PERCENTAGE 000.00" + AccesoBD.SALTO_LINEA +
                "STAKE 20 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 40 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 20 +1  0" + AccesoBD.SALTO_LINEA +
                "STAKE 40 +2  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +3  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +5  0" + AccesoBD.SALTO_LINEA +
                "" + AccesoBD.SALTO_LINEA +
                "CYCLE 14" + AccesoBD.SALTO_LINEA +
                "END DATE 0 0 0" + AccesoBD.SALTO_LINEA +
                "PERCENTAGE 000.00" + AccesoBD.SALTO_LINEA +
                "STAKE 20 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 40 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 20 +1  0" + AccesoBD.SALTO_LINEA +
                "STAKE 40 +2  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +3  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +5  0" + AccesoBD.SALTO_LINEA +
                "" + AccesoBD.SALTO_LINEA +
                "CYCLE 15" + AccesoBD.SALTO_LINEA +
                "END DATE 0 0 0" + AccesoBD.SALTO_LINEA +
                "PERCENTAGE 000.00" + AccesoBD.SALTO_LINEA +
                "STAKE 20 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 40 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 20 +1  0" + AccesoBD.SALTO_LINEA +
                "STAKE 40 +2  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +3  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +5  0" + AccesoBD.SALTO_LINEA +
                "" + AccesoBD.SALTO_LINEA +
                "CYCLE 16" + AccesoBD.SALTO_LINEA +
                "END DATE 0 0 0" + AccesoBD.SALTO_LINEA +
                "PERCENTAGE 000.00" + AccesoBD.SALTO_LINEA +
                "STAKE 20 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 40 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 20 +1  0" + AccesoBD.SALTO_LINEA +
                "STAKE 40 +2  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +3  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +5  0" + AccesoBD.SALTO_LINEA +
                "" + AccesoBD.SALTO_LINEA +
                "CYCLE 17" + AccesoBD.SALTO_LINEA +
                "END DATE 0 0 0" + AccesoBD.SALTO_LINEA +
                "PERCENTAGE 000.00" + AccesoBD.SALTO_LINEA +
                "STAKE 20 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 40 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 20 +1  0" + AccesoBD.SALTO_LINEA +
                "STAKE 40 +2  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +3  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +5  0" + AccesoBD.SALTO_LINEA +
                "" + AccesoBD.SALTO_LINEA +
                "CYCLE 18" + AccesoBD.SALTO_LINEA +
                "END DATE 0 0 0" + AccesoBD.SALTO_LINEA +
                "PERCENTAGE 000.00" + AccesoBD.SALTO_LINEA +
                "STAKE 20 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 40 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 20 +1  0" + AccesoBD.SALTO_LINEA +
                "STAKE 40 +2  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +3  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +5  0" + AccesoBD.SALTO_LINEA +
                "" + AccesoBD.SALTO_LINEA +
                "CYCLE 19" + AccesoBD.SALTO_LINEA +
                "END DATE 0 0 0" + AccesoBD.SALTO_LINEA +
                "PERCENTAGE 000.00" + AccesoBD.SALTO_LINEA +
                "STAKE 20 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 40 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 20 +1  0" + AccesoBD.SALTO_LINEA +
                "STAKE 40 +2  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +3  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +5  0" + AccesoBD.SALTO_LINEA +
                "" + AccesoBD.SALTO_LINEA +
                "CYCLE 20" + AccesoBD.SALTO_LINEA +
                "END DATE 0 0 0" + AccesoBD.SALTO_LINEA +
                "PERCENTAGE 000.00" + AccesoBD.SALTO_LINEA +
                "STAKE 20 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 40 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +0  0" + AccesoBD.SALTO_LINEA +
                "STAKE 20 +1  0" + AccesoBD.SALTO_LINEA +
                "STAKE 40 +2  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +3  0" + AccesoBD.SALTO_LINEA +
                "STAKE 60 +5  0" + AccesoBD.SALTO_LINEA +
                "");
            return sb;
        }*/
    }
}
