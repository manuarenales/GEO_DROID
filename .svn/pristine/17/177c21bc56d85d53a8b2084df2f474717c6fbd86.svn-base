using System;
using System.Collections.Generic;
using System.Text;
using GEO_DROID.Resources.Lib.Comunicacion;

namespace BLL.LeerInfoMaquina
{
    class ProtocoloBellFruit : Protocolo
    {
        // Conexión serie por defecto BELL FRUIT: 4800, Parity.None, 8, StopBits.One

        int _timeoutDefault = 30000;

        public override byte ConfiguracionPuertoSerie
        {
            get { return (byte)0x03; } 
        }

        decimal _valorPasoSalidas;
        public decimal ValorPasoSalidas
        {
            get { return _valorPasoSalidas; }
            set { _valorPasoSalidas = value; }
        }

        public ProtocoloBellFruit(IComunicacion com, IFiltroTrama filtroTrama) 
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
            // Hay que enviarle el comando 'DuMp01'
            if (EnviarTrama(0))
            {
                // El tiempo de espera entre byte/bloque de bytes lo ponemos a 2 segundos, 
                // porque al tratarse de un ticket puede enviar datos de forma más lenta.
                // Así nos curamos en salud.
                StringBuilder sb = RecibirDatos(_timeoutDefault, 2000);
                //StringBuilder sb = new StringBuilder(
                //"Site  1                                     VTP=       0 WINS=       0" + AccesoBD.SALTO_LINEA +
                //"Site  2                                     VTP=       0 WINS=       0" + AccesoBD.SALTO_LINEA +
                //"Site  3                                     VTP=       0 WINS=       0" + AccesoBD.SALTO_LINEA +
                //"Site  4                                     VTP=       0 WINS=       0" + AccesoBD.SALTO_LINEA +
                //"Site  5                                     VTP=       0 WINS=       0" + AccesoBD.SALTO_LINEA +
                //"Site  6                                     VTP=       0 WINS=       0" + AccesoBD.SALTO_LINEA +
                //"Site  7                                     VTP=       0 WINS=       0" + AccesoBD.SALTO_LINEA +
                //"Site  8                                     VTP=       0 WINS=       0" + AccesoBD.SALTO_LINEA +
                //"Site  9                                     VTP=       0 WINS=       0" + AccesoBD.SALTO_LINEA +
                //"Site 10                                     VTP=       0 WINS=       0" + AccesoBD.SALTO_LINEA +
                //"Site 11                                     VTP=       0 WINS=       0" + AccesoBD.SALTO_LINEA +
                //"Site 12                                     VTP=       0 WINS=       0" + AccesoBD.SALTO_LINEA +
                //"Site 13                                     VTP=       0 WINS=       0" + AccesoBD.SALTO_LINEA +
                //"Site 14                                     VTP=       0 WINS=       0" + AccesoBD.SALTO_LINEA +
                //"Site 15                                     VTP=       0 WINS=       0" + AccesoBD.SALTO_LINEA +
                //"Site 16                                     VTP=       0 WINS=       0" + AccesoBD.SALTO_LINEA +
                //"Site 17                                     VTP=       0 WINS=       0" + AccesoBD.SALTO_LINEA +
                //"Site 18                                     VTP=       0 WINS=       0" + AccesoBD.SALTO_LINEA +
                //"Site 19                                     VTP=       0 WINS=       0" + AccesoBD.SALTO_LINEA +
                //"Site 20                                     VTP=       0 WINS=       0" + AccesoBD.SALTO_LINEA +
                //"Year  1                          21/12/2005 VTP=       0 WINS=       0" + AccesoBD.SALTO_LINEA +
                //"Year  2                                     VTP=       0 WINS=       0" + AccesoBD.SALTO_LINEA +
                //"Year  3                                     VTP=       0 WINS=       0" + AccesoBD.SALTO_LINEA +
                //"Year  4                                     VTP=       0 WINS=       0" + AccesoBD.SALTO_LINEA +
                //"Year  5                                     VTP=       0 WINS=       0" + AccesoBD.SALTO_LINEA +
                //"TOTAL VTP=    1234 TOTAL WINS=     894" + AccesoBD.SALTO_LINEA +
                //"PART CREDIT GAMBLE BEFORE=       0 AFTER=       0" + AccesoBD.SALTO_LINEA);

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
            }
            else
            {
                _error = "Error al enviar comando a la máquina";
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
            //TOTAL VTP=       0 TOTAL WINS=       0
            int entradas = 0;
            int salidas = 0;

            if (datos != null)
            {
                string datosProceso = datos.ToString().Replace(" ", "");
                entradas = GetDatoContador("TOTALVTP=", datosProceso);
                salidas = GetDatoContador("TOTALWINS=", datosProceso);

                // pasamos las salidas a pasos porque vienen en centimos de euro
                decimal vp = 0.2m; // por defecto 0.20€ que es el caso que antes estaba implementado
                if (_valorPasoSalidas != 0)
                    vp = _valorPasoSalidas;
                salidas = (int)(((decimal)salidas / 100m) / vp); // Dividimos por 100, porque el valor lo obtenemos en céntimos de euro y lo queremos tener en euros
            }

            InfoContadores info = new InfoContadores();
            info.Entradas = entradas;
            info.Salidas = salidas;
            return info;
        }
    }
}
