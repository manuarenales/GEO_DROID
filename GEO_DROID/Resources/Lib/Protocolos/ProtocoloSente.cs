using System;
using System.Collections.Generic;
using System.Text;
using GEO_DROID.Resources.Lib.Comunicacion;

namespace BLL.LeerInfoMaquina
{
    class ProtocoloSente : ProtocoloVDAI
    {
        // Conexión serie por defecto SENTE: 9600, Parity.None, 8, StopBits.One
        
        private const string PASSWORD = "01010101"; // Por defecto es : 01010101
        private const string FLAGS = "IESLKC  ";

        enum TipoTicket
        {
            Ticket_Base      //Formato de ticket de máquinas CASH LINE PLATINO, VOLCANO MAGIC,... 
            , Ticket_MG3     //Formato de ticket de máquinas MASTER GAMES 3
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

        public ProtocoloSente(IComunicacion com, IFiltroTrama filtroTrama, string password)
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
                string datosProceso = datos.Replace(" ", "");

                // Por defecto, suponemos que el ticket es el base, ya que el único modelo con un ticket distinto que hemos detectado es con MASTER GAMES 3
                TipoTicket tipoTicket = TipoTicket.Ticket_Base;

                // ENRADAS
                info.Entradas = GetDato("TOTALAPOSTADO:", "<EUR1>", datosProceso, _valorPasoEntradas);
                if (info.Entradas < 0)
                {
                    info.Entradas = GetDato("MONEDASJUGADAS:", "APOSTADO:", datosProceso, _valorPasoEntradas);
                    if (info.Entradas >= 0)
                        tipoTicket = TipoTicket.Ticket_MG3; // Si llegamos aquí es que el formato es el del ticket Ticket_MG3, por eso lo asignamos para tenerlo en cuenta
                }

                // SALIDAS
                info.Salidas = GetDato("TOTALPREMIOS:", "<EUR1>", datosProceso, _valorPasoSalidas);
                if (info.Salidas < 0)
                {
                    info.Salidas = GetDato("MONEDASJUGADAS:", "PREMIOS:", datosProceso, _valorPasoSalidas);
                    if (info.Salidas >= 0)
                        tipoTicket = TipoTicket.Ticket_MG3; // Si llegamos aquí es que el formato es el del ticket Ticket_MG3, por eso lo asignamos para tenerlo en cuenta
                }

                // BILLETES
                if (tipoTicket == TipoTicket.Ticket_Base) 
                {
                    datosProceso = datosProceso.Replace("MONED", ""); // Quitamos el texto MONED, para eliminarlo del final de la línea con el importe que queremos procesar para obtener los billetes
                    info.Billetes5 = GetDatoEntero("TOTALBILLETES:", "EU5=", datosProceso);
                    info.Billetes10 = GetDatoEntero("TOTALBILLETES:", "EU10=", datosProceso);
                    info.Billetes20 = GetDatoEntero("TOTALBILLETES:", "EU20=", datosProceso);
                    info.Billetes50 = GetDatoEntero("TOTALBILLETES:", "EU50=", datosProceso);
                }
                else
                {
                    datosProceso = datosProceso.Replace("BILLE", ""); // Quitamos el texto BILLE, para eliminarlo del final de la línea con el importe que queremos procesar para obtener los billetes
                    info.Billetes5 = GetDatoEntero("DESGLOSEDECAJA", "EU5.0=", datosProceso);
                    info.Billetes10 = GetDatoEntero("DESGLOSEDECAJA", "EU10.0=", datosProceso);
                    info.Billetes20 = GetDatoEntero("DESGLOSEDECAJA", "EU20.0=", datosProceso);
                    info.Billetes50 = GetDatoEntero("DESGLOSEDECAJA", "EU50.0=", datosProceso);
                }
                info.AutoSetBilletes();
            }
            return info;
        }

        /* TEST
        static class AccesoBD
        {
            public static string SALTO_LINEA = Environment.NewLine;
        }

        public void Test()
        {
            string ticket1 =    "----" + AccesoBD.SALTO_LINEA + 
                                "------------------------" + AccesoBD.SALTO_LINEA + 
                                "K!" + AccesoBD.SALTO_LINEA + 
                                "STELLA             11.00" + AccesoBD.SALTO_LINEA + 
                                "K\"" + AccesoBD.SALTO_LINEA + 
                                "VOLCANO MAGIC     F5   E" + AccesoBD.SALTO_LINEA + 
                                "K#" + AccesoBD.SALTO_LINEA + 
                                "SALA NO.:       00000000" + AccesoBD.SALTO_LINEA + 
                                "K$" + AccesoBD.SALTO_LINEA + 
                                "MAQUINA NO.:    00000000" + AccesoBD.SALTO_LINEA + 
                                "KC" + AccesoBD.SALTO_LINEA + 
                                "SERIE   :     6011570008" + AccesoBD.SALTO_LINEA + 
                                "------------------------" + AccesoBD.SALTO_LINEA + 
                                "K%" + AccesoBD.SALTO_LINEA + 
                                "RECAUDACION NO.:     014" + AccesoBD.SALTO_LINEA + 
                                "RECAUDACION DEL:" + AccesoBD.SALTO_LINEA +         
                                "K&" + AccesoBD.SALTO_LINEA + 
                                       "28.01.10    15:13" + AccesoBD.SALTO_LINEA + 
                                "ULTIMA RECAUDACION:" + AccesoBD.SALTO_LINEA +      
                                "K'" + AccesoBD.SALTO_LINEA + 
                                       "27.01.10    10:56" + AccesoBD.SALTO_LINEA + 
                                  "." + AccesoBD.SALTO_LINEA +                      
                                "------------------------" + AccesoBD.SALTO_LINEA + 
                                "TOTAL EN CAJA:" + AccesoBD.SALTO_LINEA +           
                                "KB" + AccesoBD.SALTO_LINEA + 
                                 "<EUR 1>           33.30" + AccesoBD.SALTO_LINEA + 
                                "TOTAL BILLETES:" + AccesoBD.SALTO_LINEA +          
                                "Kf" + AccesoBD.SALTO_LINEA + 
                                 "<EUR 1>           30.00" + AccesoBD.SALTO_LINEA + 
                                "MONEDAS INTRODUCIDAS:" + AccesoBD.SALTO_LINEA +    
                                "K?" + AccesoBD.SALTO_LINEA + 
                                 "<EUR 1>          102.10" + AccesoBD.SALTO_LINEA + 
                                "MONEDAS PAGADAS:" + AccesoBD.SALTO_LINEA +         
                                "K?" + AccesoBD.SALTO_LINEA + 
                                 "<EUR 1>           41.00" + AccesoBD.SALTO_LINEA + 
                                "------------------------" + AccesoBD.SALTO_LINEA + 
                                "TOTAL EN CAJA:" + AccesoBD.SALTO_LINEA +           
                                 "EU  0,10 =      8 MONED" + AccesoBD.SALTO_LINEA + 
                                 "EU  0,50 =      5 MONED" + AccesoBD.SALTO_LINEA + 
                                "------------------------" + AccesoBD.SALTO_LINEA + 
                                "TOTAL BILLETES:" + AccesoBD.SALTO_LINEA +          
                                 "EU     5 =      2 MONED" + AccesoBD.SALTO_LINEA + 
                                 "EU    20 =      1 MONED" + AccesoBD.SALTO_LINEA + 
                                "------------------------" + AccesoBD.SALTO_LINEA + 
                                "CONTENIDO HOPPER 1:" + AccesoBD.SALTO_LINEA +      
                                 "<INI  200 MONE>" + AccesoBD.SALTO_LINEA +         
                                "K{" + AccesoBD.SALTO_LINEA + 
                                "EU   0.20 =    204 MONED" + AccesoBD.SALTO_LINEA + 
                                "CONTENIDO HOPPER 2:" + AccesoBD.SALTO_LINEA +      
                                 "<INI  400 MONE>" + AccesoBD.SALTO_LINEA +         
                                "Ks" + AccesoBD.SALTO_LINEA + 
                                "EU   1.00 =    427 MONED" + AccesoBD.SALTO_LINEA + 
                                "------------------------" + AccesoBD.SALTO_LINEA + 
                                "PARTIDAS JUGADAS:" + AccesoBD.SALTO_LINEA +        
                                "Kg" + AccesoBD.SALTO_LINEA + 
                                                     "425" + AccesoBD.SALTO_LINEA + 
                                "MONEDAS JUGADAS:" + AccesoBD.SALTO_LINEA +         
                                "Kg" + AccesoBD.SALTO_LINEA + 
                                                     "493" + AccesoBD.SALTO_LINEA + 
                                "TOTAL APOSTADO:" + AccesoBD.SALTO_LINEA +          
                                 "<EUR 1>           98.70" + AccesoBD.SALTO_LINEA + 
                                "TOTAL PREMIOS:" + AccesoBD.SALTO_LINEA +           
                                 "<EUR 1>           37.60" + AccesoBD.SALTO_LINEA + 
                                "------------------------" + AccesoBD.SALTO_LINEA + 
                                "TOTAL RELLENADO:" + AccesoBD.SALTO_LINEA +         
                                "K?" + AccesoBD.SALTO_LINEA + 
                                 "<EUR 1>            0.00" + AccesoBD.SALTO_LINEA + 
                                "RELLENAR DEL:" + AccesoBD.SALTO_LINEA +            
                                "TIEM  DI.ME.AN NO EUR.CT" + AccesoBD.SALTO_LINEA + 
                                "------------------------" + AccesoBD.SALTO_LINEA + 
                                "CAJA ABIERTA DEL:" + AccesoBD.SALTO_LINEA +        
                                 "TIEM  DI.ME.AN NO" + AccesoBD.SALTO_LINEA +       
                                 "15:02 28.01.10  1" + AccesoBD.SALTO_LINEA +       
                                 "15:07 28.01.10  2" + AccesoBD.SALTO_LINEA +       
                                "------------------------" + AccesoBD.SALTO_LINEA + 
                                "K}" + AccesoBD.SALTO_LINEA + 
                                "DURA. SERVI.:         6H" + AccesoBD.SALTO_LINEA + 
                                "------------------------" + AccesoBD.SALTO_LINEA + 
                                "MAQUINA ABIERTA DEL" + AccesoBD.SALTO_LINEA +      
                                 "TIEM  DI.ME.AN NO" + AccesoBD.SALTO_LINEA +       
                                 "14:33 27.01.10  1" + AccesoBD.SALTO_LINEA +       
                                 "14:53 28.01.10  2" + AccesoBD.SALTO_LINEA +       
                                 "14:54 28.01.10  3" + AccesoBD.SALTO_LINEA +       
                                 "15:02 28.01.10  4" + AccesoBD.SALTO_LINEA +       
                                 "15:08 28.01.10  5" + AccesoBD.SALTO_LINEA +       
                                "------------------------" + AccesoBD.SALTO_LINEA + 
                                "" + AccesoBD.SALTO_LINEA + 
                                                       "" + AccesoBD.SALTO_LINEA + 
                                "DATO NO CANCELAR" + AccesoBD.SALTO_LINEA + 
                                       "" + AccesoBD.SALTO_LINEA + 
                                "ENDE    NL" + AccesoBD.SALTO_LINEA + 
                                "" + AccesoBD.SALTO_LINEA + 
                                "" + AccesoBD.SALTO_LINEA + 
                                           "" + AccesoBD.SALTO_LINEA + 
                                "" + AccesoBD.SALTO_LINEA + 
                                                       "" + AccesoBD.SALTO_LINEA + 
                                "C55A3" + AccesoBD.SALTO_LINEA + 
                                "" + AccesoBD.SALTO_LINEA + 
                                "----" + AccesoBD.SALTO_LINEA;

            InfoContadores info = ProcesarDatos(ticket1);
            System.Diagnostics.Debug.WriteLine(info.Entradas);//493
            System.Diagnostics.Debug.WriteLine(info.Salidas);//188
            System.Diagnostics.Debug.WriteLine(info.Billetes);//3
            System.Diagnostics.Debug.WriteLine(info.Billetes5);//2
            System.Diagnostics.Debug.WriteLine(info.Billetes10);//-1
            System.Diagnostics.Debug.WriteLine(info.Billetes20);//1
            System.Diagnostics.Debug.WriteLine(info.Billetes50);//-1
        }

        public void TestMG3()
        {
            string ticket1 =
                "13/05/15 12:35:12" + AccesoBD.SALTO_LINEA +
                "------------------------" + AccesoBD.SALTO_LINEA +
                "K!" + AccesoBD.SALTO_LINEA +
                "     SENTE - MERKUR" + AccesoBD.SALTO_LINEA +
                "K\"" + AccesoBD.SALTO_LINEA +
                "  MASTER GAMES 3 EURO" + AccesoBD.SALTO_LINEA +
                "------------------------" + AccesoBD.SALTO_LINEA +
                "K\"" + AccesoBD.SALTO_LINEA +
                "F1-101020040C00-FF-BE" + AccesoBD.SALTO_LINEA +
                "K\"" + AccesoBD.SALTO_LINEA +
                "8A-171020000C00-99999999" + AccesoBD.SALTO_LINEA +
                "------------------------" + AccesoBD.SALTO_LINEA +
                "K\"" + AccesoBD.SALTO_LINEA +
                "BAR: NOMBRE DE SU LOCAL" + AccesoBD.SALTO_LINEA +
                "K#" + AccesoBD.SALTO_LINEA +
                "SALA NO.   :    00000001" + AccesoBD.SALTO_LINEA +
                "K$" + AccesoBD.SALTO_LINEA +
                "MAQUINA NO.:    00000002" + AccesoBD.SALTO_LINEA +
                "KC" + AccesoBD.SALTO_LINEA +
                "SERIE      :       30045" + AccesoBD.SALTO_LINEA +
                "COMUNIDAD  :    VALENCIA" + AccesoBD.SALTO_LINEA +
                "------------------------" + AccesoBD.SALTO_LINEA +
                " TOTAL DATOS HISTORICOS" + AccesoBD.SALTO_LINEA +
                "K'" + AccesoBD.SALTO_LINEA +
                "  DEL 02/10/2014   11:01" + AccesoBD.SALTO_LINEA +
                "K&" + AccesoBD.SALTO_LINEA +
                "  AL  27/05/2015   12:25" + AccesoBD.SALTO_LINEA +
                "------------------------" + AccesoBD.SALTO_LINEA +
                "TOTAL EN CAJA" + AccesoBD.SALTO_LINEA +
                "KB" + AccesoBD.SALTO_LINEA +
                " TOTAL EUROS:   51082.00" + AccesoBD.SALTO_LINEA +
                " MONEDAS:        3797.00" + AccesoBD.SALTO_LINEA +
                "Kf" + AccesoBD.SALTO_LINEA +
                " BILLETES:      47285.00" + AccesoBD.SALTO_LINEA +
                "DINERO INTRODUCIDO" + AccesoBD.SALTO_LINEA +
                "K?" + AccesoBD.SALTO_LINEA +
                " TOTAL EUROS:   57770.40" + AccesoBD.SALTO_LINEA +
                " MONEDAS:       10485.40" + AccesoBD.SALTO_LINEA +
                " BILLETES:      47285.00" + AccesoBD.SALTO_LINEA +
                "DINERO PAGADO" + AccesoBD.SALTO_LINEA +
                "K?" + AccesoBD.SALTO_LINEA +
                " TOTAL EUROS:   11010.40" + AccesoBD.SALTO_LINEA +
                " MONEDAS:       11010.40" + AccesoBD.SALTO_LINEA +
                " BILLETES:          0.00" + AccesoBD.SALTO_LINEA +
                "DINERO CAMBIO" + AccesoBD.SALTO_LINEA +
                " TOTAL EUROS:     182.40" + AccesoBD.SALTO_LINEA +
                "------------------------" + AccesoBD.SALTO_LINEA +
                "    DESGLOSE DE CAJA" + AccesoBD.SALTO_LINEA +
                "MONEDAS:         3797.00" + AccesoBD.SALTO_LINEA +
                "EU   0.1 =       0 MONED" + AccesoBD.SALTO_LINEA +
                "EU   0.2 =     385 MONED" + AccesoBD.SALTO_LINEA +
                "EU   0.5 =       0 MONED" + AccesoBD.SALTO_LINEA +
                "EU   1.0 =    3514 MONED" + AccesoBD.SALTO_LINEA +
                "EU   2.0 =     103 MONED" + AccesoBD.SALTO_LINEA +
                "BILLETES:       47285.00" + AccesoBD.SALTO_LINEA +
                "EU   5.0 =     117 BILLE" + AccesoBD.SALTO_LINEA +
                "EU  10.0 =     232 BILLE" + AccesoBD.SALTO_LINEA +
                "EU  20.0 =     614 BILLE" + AccesoBD.SALTO_LINEA +
                "EU  50.0 =     642 BILLE" + AccesoBD.SALTO_LINEA +
                "------------------------" + AccesoBD.SALTO_LINEA +
                " DESGLOSE  INTRODUCIDAS" + AccesoBD.SALTO_LINEA +
                "EU   0.1 =       0 MONED" + AccesoBD.SALTO_LINEA +
                "EU   0.2 =     422 MONED" + AccesoBD.SALTO_LINEA +
                "EU   0.5 =       0 MONED" + AccesoBD.SALTO_LINEA +
                "EU   1.0 =   10205 MONED" + AccesoBD.SALTO_LINEA +
                "EU   2.0 =     103 MONED" + AccesoBD.SALTO_LINEA +
                "EU   5.0 =       0 BILLE" + AccesoBD.SALTO_LINEA +
                "EU  10.0 =       0 BILLE" + AccesoBD.SALTO_LINEA +
                "EU  20.0 =       0 BILLE" + AccesoBD.SALTO_LINEA +
                "EU  50.0 =       0 BILLE" + AccesoBD.SALTO_LINEA +
                "JUG.BANCO=  41286.00 EUR" + AccesoBD.SALTO_LINEA +
                "    DESGLOSE PAGADAS" + AccesoBD.SALTO_LINEA +
                "EU   0.1 =       0 MONED" + AccesoBD.SALTO_LINEA +
                "EU   0.2 =      12 MONED" + AccesoBD.SALTO_LINEA +
                "EU   0.5 =       0 MONED" + AccesoBD.SALTO_LINEA +
                "EU   1.0 =   11008 MONED" + AccesoBD.SALTO_LINEA +
                "EU   2.0 =       0 MONED" + AccesoBD.SALTO_LINEA +
                "EU   5.0 =       0 BILLE" + AccesoBD.SALTO_LINEA +
                "EU  10.0 =       0 BILLE" + AccesoBD.SALTO_LINEA +
                "EU  20.0 =       0 BILLE" + AccesoBD.SALTO_LINEA +
                "EU  50.0 =       0 BILLE" + AccesoBD.SALTO_LINEA +
                "JUG.BANCO=  41286.00 EUR" + AccesoBD.SALTO_LINEA +
                "MED.REC.TD=    32.12 EUR" + AccesoBD.SALTO_LINEA +
                "MED.ENT.TD=      797 MON" + AccesoBD.SALTO_LINEA +
                "MED.REC.MD=    39.10 EUR" + AccesoBD.SALTO_LINEA +
                "MED.ENT.MD=      617 MON" + AccesoBD.SALTO_LINEA +
                "MED.ENT.P=     49.64 EUR" + AccesoBD.SALTO_LINEA +
                "MED.APUES=      0.93 EUR" + AccesoBD.SALTO_LINEA +
                "VEC.ENTRA=      4.20 VEC" + AccesoBD.SALTO_LINEA +
                "MED.RECAU=     54.11 EUR" + AccesoBD.SALTO_LINEA +
                "JUGADAS 0,10=          0" + AccesoBD.SALTO_LINEA +
                "GANADAS 0,10=          0" + AccesoBD.SALTO_LINEA +
                "------------------------" + AccesoBD.SALTO_LINEA +
                "PAGO MANUAL     30767.00" + AccesoBD.SALTO_LINEA +
                "------------------------" + AccesoBD.SALTO_LINEA +
                "Kg" + AccesoBD.SALTO_LINEA +
                "APORTA JP BONOS:  459072" + AccesoBD.SALTO_LINEA +
                "------------------------" + AccesoBD.SALTO_LINEA +
                "Kg" + AccesoBD.SALTO_LINEA +
                "MONEDAS JUGADAS:  494370" + AccesoBD.SALTO_LINEA +
                "PARTIDAS:         102462" + AccesoBD.SALTO_LINEA +
                "APOSTADO:       98874.00" + AccesoBD.SALTO_LINEA +
                "PREMIOS :       82881.00" + AccesoBD.SALTO_LINEA +
                "RECAUDACION:    15993.00" + AccesoBD.SALTO_LINEA +
                "PORC.ACTUAL:      83.82%" + AccesoBD.SALTO_LINEA +
                "PORC.PROGR:       AUTO-2" + AccesoBD.SALTO_LINEA +
                "CTRL.PROGR:AU-2,MUY ALTA" + AccesoBD.SALTO_LINEA +
                "------------------------" + AccesoBD.SALTO_LINEA +
                "Kj" + AccesoBD.SALTO_LINEA +
                "PARTIDAS 1C:        4485" + AccesoBD.SALTO_LINEA +
                "Kk" + AccesoBD.SALTO_LINEA +
                "PARTIDAS 2C:           0" + AccesoBD.SALTO_LINEA +
                "Kl" + AccesoBD.SALTO_LINEA +
                "PARTIDAS 3C:           0" + AccesoBD.SALTO_LINEA +
                "Km" + AccesoBD.SALTO_LINEA +
                "PARTIDAS 4C:           0" + AccesoBD.SALTO_LINEA +
                "Kn" + AccesoBD.SALTO_LINEA +
                "PARTIDAS 5C:       97977" + AccesoBD.SALTO_LINEA +
                "------------------------" + AccesoBD.SALTO_LINEA +
                "    RECARGAS-VACIADOS" + AccesoBD.SALTO_LINEA +
                "RELLENADO:          0.00" + AccesoBD.SALTO_LINEA +
                "VACIADO:         1536.20" + AccesoBD.SALTO_LINEA +
                "DIFERENCIA: -    1536.20" + AccesoBD.SALTO_LINEA +
                "------------------------" + AccesoBD.SALTO_LINEA +
                "HOPPER1 VACIO:     0 VEC" + AccesoBD.SALTO_LINEA +
                "HOPPER2 VACIO:     0 VEC" + AccesoBD.SALTO_LINEA +
                "------------------------" + AccesoBD.SALTO_LINEA +
                "" + AccesoBD.SALTO_LINEA +
                "DATOS NO BORRADOS" + AccesoBD.SALTO_LINEA +
                "ENDE    NL" + AccesoBD.SALTO_LINEA +
                "" + AccesoBD.SALTO_LINEA +
                "" + AccesoBD.SALTO_LINEA +
                "          " + AccesoBD.SALTO_LINEA +
                "" + AccesoBD.SALTO_LINEA +
                "C20BA" + AccesoBD.SALTO_LINEA +
                "C003B" + AccesoBD.SALTO_LINEA +
                "";

            InfoContadores info = ProcesarDatos(ticket1);
            System.Diagnostics.Debug.WriteLine(info.Entradas);//494370
            System.Diagnostics.Debug.WriteLine(info.Salidas);//414405
            System.Diagnostics.Debug.WriteLine(info.Billetes);//1605
            System.Diagnostics.Debug.WriteLine(info.Billetes5);//117
            System.Diagnostics.Debug.WriteLine(info.Billetes10);//232
            System.Diagnostics.Debug.WriteLine(info.Billetes20);//614
            System.Diagnostics.Debug.WriteLine(info.Billetes50);//642
        }
        */
    }
}
