using System;
using System.Collections.Generic;
using System.Text;
using GEO_DROID.Resources.Lib.Comunicacion;

namespace BLL.LeerInfoMaquina
{
    class ProtocoloMerkur : ProtocoloVDAI
    {
        // Conexión serie por defecto MERKUR: 9600, Parity.None, 8, StopBits.One
        
        private const string PASSWORD = "01010101"; // Por defecto es : 01010101
        private const string FLAGS = "     C  ";

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

        public ProtocoloMerkur(IComunicacion com, IFiltroTrama filtroTrama, string password)
            : base(com, filtroTrama, password)
        {
            if (_password == null)
                _password = PASSWORD;
            _flags = FLAGS;
            _separadorDecimales = ".";
        }

        private int GetDato(string seccion, string texto, string datos, decimal valorPaso)
        {
            int valor = -1;
            try
            {
                decimal valorAux = GetDatoDecimal(seccion, texto, datos);
                if (valorAux >= 0)
                {
                    if (valorPaso != 0)
                        valor = (int)(valorAux / valorPaso);
                    else
                        valor = (int)(valorAux / (decimal)0.20);
                }
            }
            catch { }
            return valor;
        }

        protected override InfoContadores ProcesarDatos(string datos)
        {
            InfoContadores info = new InfoContadores();
            info.Buffer = new StringBuilder(datos);
            if (datos != null)
            {
                // Quitamos los espacios para procesar las cadenas de forma más simple
                // Reemplazamos <EUR> por <EU> para simplificar la búsqueda de los datos
                string datosProceso = datos.Replace(" ", "").Replace("<EUR>", "<EU>");//.Replace("MONED", "");

                // ENRADAS/SALIDAS
                info.Entradas = GetDato("TOTALAPOSTADO:", "<EU>", datosProceso, _valorPasoEntradas);
                info.Salidas = GetDato("TOTALPREMIOS:", "<EU>", datosProceso, _valorPasoSalidas);
                //MONEDAS INTRODUCIDAS - ENTRADAS DINERO
                info.Auxiliar3 = GetDato("TOTALINTRODUCIDO:", "<EU>", datosProceso, _valorPasoEntradas);
                if (info.Auxiliar3 < 0)
                    info.Auxiliar3 = GetDato("MONEDASINTRODUCIDAS:", "<EU>", datosProceso, _valorPasoEntradas);
                //MONEDAS PAGADAS - SALIDAS DINERO
                info.Auxiliar4 = GetDato("TOTALPAGADO:", "<EU>", datosProceso, _valorPasoSalidas);
                if (info.Auxiliar4 < 0)
                    info.Auxiliar4 = GetDato("MONEDASPAGADAS:", "<EU>", datosProceso, _valorPasoSalidas);
                //// BILLETES
                //info.Billetes5 = GetDatoEntero("TOTALBILLETES:", "EU5=", datosProceso);
                //info.Billetes10 = GetDatoEntero("TOTALBILLETES:", "EU10=", datosProceso);
                //info.Billetes20 = GetDatoEntero("TOTALBILLETES:", "EU20=", datosProceso);
                //info.Billetes50 = GetDatoEntero("TOTALBILLETES:", "EU50=", datosProceso);
                //info.AutoSetBilletes();
                // PAGOS MANUALES
                info.PagosManuales = GetDato("TOTALCANCELCREDIT:", "<EU>", datosProceso, _valorPasoSalidas);
                // TITO
                info.TicketIN = GetDato("TOTALTICKETSIN:", "<EU>", datosProceso, _valorPasoEntradas);
                info.TicketOUT = GetDato("TOTALTICKETSOUT:", "<EU>", datosProceso, _valorPasoEntradas);

            }
            return info;
        }

        //public void Test()
        //{
        //    string ticket1 =    "----" + AccesoBD.SALTO_LINEA + 
        //                        "------------------------" + AccesoBD.SALTO_LINEA + 
        //                        "K!" + AccesoBD.SALTO_LINEA + 
        //                        "STELLA             11.00" + AccesoBD.SALTO_LINEA + 
        //                        "K\"" + AccesoBD.SALTO_LINEA + 
        //                        "VOLCANO MAGIC     F5   E" + AccesoBD.SALTO_LINEA + 
        //                        "K#" + AccesoBD.SALTO_LINEA + 
        //                        "SALA NO.:       00000000" + AccesoBD.SALTO_LINEA + 
        //                        "K$" + AccesoBD.SALTO_LINEA + 
        //                        "MAQUINA NO.:    00000000" + AccesoBD.SALTO_LINEA + 
        //                        "KC" + AccesoBD.SALTO_LINEA + 
        //                        "SERIE   :     6011570008" + AccesoBD.SALTO_LINEA + 
        //                        "------------------------" + AccesoBD.SALTO_LINEA + 
        //                        "K%" + AccesoBD.SALTO_LINEA + 
        //                        "RECAUDACION NO.:     014" + AccesoBD.SALTO_LINEA + 
        //                        "RECAUDACION DEL:" + AccesoBD.SALTO_LINEA +         
        //                        "K&" + AccesoBD.SALTO_LINEA + 
        //                               "28.01.10    15:13" + AccesoBD.SALTO_LINEA + 
        //                        "ULTIMA RECAUDACION:" + AccesoBD.SALTO_LINEA +      
        //                        "K'" + AccesoBD.SALTO_LINEA + 
        //                               "27.01.10    10:56" + AccesoBD.SALTO_LINEA + 
        //                          "." + AccesoBD.SALTO_LINEA +                      
        //                        "------------------------" + AccesoBD.SALTO_LINEA + 
        //                        "TOTAL EN CAJA:" + AccesoBD.SALTO_LINEA +           
        //                        "KB" + AccesoBD.SALTO_LINEA + 
        //                         "<EUR 1>           33.30" + AccesoBD.SALTO_LINEA + 
        //                        "TOTAL BILLETES:" + AccesoBD.SALTO_LINEA +          
        //                        "Kf" + AccesoBD.SALTO_LINEA + 
        //                         "<EUR 1>           30.00" + AccesoBD.SALTO_LINEA + 
        //                        "MONEDAS INTRODUCIDAS:" + AccesoBD.SALTO_LINEA +    
        //                        "K?" + AccesoBD.SALTO_LINEA + 
        //                         "<EUR 1>          102.10" + AccesoBD.SALTO_LINEA + 
        //                        "MONEDAS PAGADAS:" + AccesoBD.SALTO_LINEA +         
        //                        "K?" + AccesoBD.SALTO_LINEA + 
        //                         "<EUR 1>           41.00" + AccesoBD.SALTO_LINEA + 
        //                        "------------------------" + AccesoBD.SALTO_LINEA + 
        //                        "TOTAL EN CAJA:" + AccesoBD.SALTO_LINEA +           
        //                         "EU  0,10 =      8 MONED" + AccesoBD.SALTO_LINEA + 
        //                         "EU  0,50 =      5 MONED" + AccesoBD.SALTO_LINEA + 
        //                        "------------------------" + AccesoBD.SALTO_LINEA + 
        //                        "TOTAL BILLETES:" + AccesoBD.SALTO_LINEA +          
        //                         "EU     5 =      2 MONED" + AccesoBD.SALTO_LINEA + 
        //                         "EU    20 =      1 MONED" + AccesoBD.SALTO_LINEA + 
        //                        "------------------------" + AccesoBD.SALTO_LINEA + 
        //                        "CONTENIDO HOPPER 1:" + AccesoBD.SALTO_LINEA +      
        //                         "<INI  200 MONE>" + AccesoBD.SALTO_LINEA +         
        //                        "K{" + AccesoBD.SALTO_LINEA + 
        //                        "EU   0.20 =    204 MONED" + AccesoBD.SALTO_LINEA + 
        //                        "CONTENIDO HOPPER 2:" + AccesoBD.SALTO_LINEA +      
        //                         "<INI  400 MONE>" + AccesoBD.SALTO_LINEA +         
        //                        "Ks" + AccesoBD.SALTO_LINEA + 
        //                        "EU   1.00 =    427 MONED" + AccesoBD.SALTO_LINEA + 
        //                        "------------------------" + AccesoBD.SALTO_LINEA + 
        //                        "PARTIDAS JUGADAS:" + AccesoBD.SALTO_LINEA +        
        //                        "Kg" + AccesoBD.SALTO_LINEA + 
        //                                             "425" + AccesoBD.SALTO_LINEA + 
        //                        "MONEDAS JUGADAS:" + AccesoBD.SALTO_LINEA +         
        //                        "Kg" + AccesoBD.SALTO_LINEA + 
        //                                             "493" + AccesoBD.SALTO_LINEA + 
        //                        "TOTAL APOSTADO:" + AccesoBD.SALTO_LINEA +          
        //                         "<EUR 1>           98.70" + AccesoBD.SALTO_LINEA + 
        //                        "TOTAL PREMIOS:" + AccesoBD.SALTO_LINEA +           
        //                         "<EUR 1>           37.60" + AccesoBD.SALTO_LINEA + 
        //                        "------------------------" + AccesoBD.SALTO_LINEA + 
        //                        "TOTAL RELLENADO:" + AccesoBD.SALTO_LINEA +         
        //                        "K?" + AccesoBD.SALTO_LINEA + 
        //                         "<EUR 1>            0.00" + AccesoBD.SALTO_LINEA + 
        //                        "RELLENAR DEL:" + AccesoBD.SALTO_LINEA +            
        //                        "TIEM  DI.ME.AN NO EUR.CT" + AccesoBD.SALTO_LINEA + 
        //                        "------------------------" + AccesoBD.SALTO_LINEA + 
        //                        "CAJA ABIERTA DEL:" + AccesoBD.SALTO_LINEA +        
        //                         "TIEM  DI.ME.AN NO" + AccesoBD.SALTO_LINEA +       
        //                         "15:02 28.01.10  1" + AccesoBD.SALTO_LINEA +       
        //                         "15:07 28.01.10  2" + AccesoBD.SALTO_LINEA +       
        //                        "------------------------" + AccesoBD.SALTO_LINEA + 
        //                        "K}" + AccesoBD.SALTO_LINEA + 
        //                        "DURA. SERVI.:         6H" + AccesoBD.SALTO_LINEA + 
        //                        "------------------------" + AccesoBD.SALTO_LINEA + 
        //                        "MAQUINA ABIERTA DEL" + AccesoBD.SALTO_LINEA +      
        //                         "TIEM  DI.ME.AN NO" + AccesoBD.SALTO_LINEA +       
        //                         "14:33 27.01.10  1" + AccesoBD.SALTO_LINEA +       
        //                         "14:53 28.01.10  2" + AccesoBD.SALTO_LINEA +       
        //                         "14:54 28.01.10  3" + AccesoBD.SALTO_LINEA +       
        //                         "15:02 28.01.10  4" + AccesoBD.SALTO_LINEA +       
        //                         "15:08 28.01.10  5" + AccesoBD.SALTO_LINEA +       
        //                        "------------------------" + AccesoBD.SALTO_LINEA + 
        //                        "" + AccesoBD.SALTO_LINEA + 
        //                                               "" + AccesoBD.SALTO_LINEA + 
        //                        "DATO NO CANCELAR" + AccesoBD.SALTO_LINEA + 
        //                               "" + AccesoBD.SALTO_LINEA + 
        //                        "ENDE    NL" + AccesoBD.SALTO_LINEA + 
        //                        "" + AccesoBD.SALTO_LINEA + 
        //                        "" + AccesoBD.SALTO_LINEA + 
        //                                   "" + AccesoBD.SALTO_LINEA + 
        //                        "" + AccesoBD.SALTO_LINEA + 
        //                                               "" + AccesoBD.SALTO_LINEA + 
        //                        "C55A3" + AccesoBD.SALTO_LINEA + 
        //                        "" + AccesoBD.SALTO_LINEA + 
        //                        "----" + AccesoBD.SALTO_LINEA;

        //    InfoContadores info = ProcesarDatos(ticket1);
        //    System.Diagnostics.Debug.WriteLine(info.Entradas);//493
        //    System.Diagnostics.Debug.WriteLine(info.Salidas);//188
        //    System.Diagnostics.Debug.WriteLine(info.Billetes);//3
        //    System.Diagnostics.Debug.WriteLine(info.Billetes5);//2
        //    System.Diagnostics.Debug.WriteLine(info.Billetes10);//-1
        //    System.Diagnostics.Debug.WriteLine(info.Billetes20);//1
        //    System.Diagnostics.Debug.WriteLine(info.Billetes50);//-1

        //}
    }
}
