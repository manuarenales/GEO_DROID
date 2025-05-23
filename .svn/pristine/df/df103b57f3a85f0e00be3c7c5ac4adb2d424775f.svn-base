using System;
using System.Collections.Generic;
using System.Text;
using GEO_DROID.Resources.Lib.Comunicacion;

namespace BLL.LeerInfoMaquina
{
    class ProtocoloCostaCalida : ProtocoloVDAI
    {
        // Conexión serie por defecto SENTE: 9600, Parity.None, 8, StopBits.One

        private const string PASSWORD = "01010101"; // Por defecto es : 01010101
        private const string FLAGS = "IESLKC  ";

        enum TipoTicket { 
            Ticket_BW_0005      //Formato de ticket de máquinas LEJANO OESTE, RIO BRAVO,... 
            , Ticket_BW_ES08    //Formato de ticket de máquinas ACTION STAR,...
        }

        public ProtocoloCostaCalida(IComunicacion com, IFiltroTrama filtroTrama, string password)
            : base(com, filtroTrama, password)
        {
            if (_password == null)
                _password = PASSWORD;
            _flags = FLAGS;
        }

        private int GetBilletes(int valor, string seccion, string texto, string datos)
        {
            int billetes = 0;
            try
            {
                billetes = (int)GetDatoDecimal(seccion, texto, datos);
            }
            catch { }

            if (billetes > 0)
                billetes = billetes / valor;
            return billetes;
        }

        protected override InfoContadores ProcesarDatos(string datos)
        {
            InfoContadores info = new InfoContadores();
            info.Buffer = new StringBuilder(datos);
            if (datos != null)
            {
                // QUITAMOS LOS ESPACIOS PARA LOCALIZAR FACILMENTE LOS TEXTOS EN LA INFORMACIÓN LEIDA
                string datosProceso = datos.Replace(" ", "").Replace("EURO", "");

                // Por defecto, suponemos que el ticket es de las máquinas más modernas
                TipoTicket tipoTicket = TipoTicket.Ticket_BW_ES08; 
                
                // ENTRADA/SALIDA
                info.Entradas = GetDatoEntero("CONTADORESTOTALES:", "ENTRADAS:", datosProceso);
                if (info.Entradas < 0)
                {
                    info.Entradas = GetDatoEntero("ACTUAL:", "ENTRADAS:", datosProceso);
                    if (info.Entradas >= 0)
                        tipoTicket = TipoTicket.Ticket_BW_0005; // Si llegamos aquí es que el formato es el del ticket Ticket_BW_0005, por eso lo asignamos para tenerlo en cuenta
                }
                
                info.Salidas = GetDatoEntero("CONTADORESTOTALES:", "SALIDAS:", datosProceso);
                if (info.Salidas < 0)
                {
                    info.Salidas = GetDatoEntero("ACTUAL:", "SALIDAS:", datosProceso);
                    if (info.Salidas >= 0)
                        tipoTicket = TipoTicket.Ticket_BW_0005; // Si llegamos aquí es que el formato es el del ticket Ticket_BW_0005, por eso lo asignamos para tenerlo en cuenta
                }
                
                // BILLETES
                string textoSeccion = "ESTADISTICATOTAL:";
                if (tipoTicket == TipoTicket.Ticket_BW_0005)
                    textoSeccion = "INTRODUCIDAS:";
                else
                    datosProceso = datosProceso.Replace("EU", ""); //En lugar de EURO, la cadena en los tickets Ticket_BW_ES08 es EU

                info.Billetes5 = GetBilletes(5, textoSeccion, "5,00=", datosProceso);
                info.Billetes10 = GetBilletes(10, textoSeccion, "10,00=", datosProceso);
                info.Billetes20 = GetBilletes(20, textoSeccion, "20,00=", datosProceso);
                if (tipoTicket == TipoTicket.Ticket_BW_ES08)
                    info.Billetes50 = GetBilletes(50, textoSeccion, "50,00=", datosProceso);
                info.AutoSetBilletes();
            }
            return info;
        }

        //public void Test()//Ticket_BW_0005
        //{
        //    string ticket1 =
        //                    "----" + AccesoBD.SALTO_LINEA +
        //                    "------------------------" + AccesoBD.SALTO_LINEA +
        //                    "BALLY WULFF        00.05" + AccesoBD.SALTO_LINEA +
        //                    "LEJANO OESTE 452  P 3,0" + AccesoBD.SALTO_LINEA +
        //                    "OPERADOR:" + AccesoBD.SALTO_LINEA +
        //                    "LOCAL:        B" + AccesoBD.SALTO_LINEA +
        //                    "MAQUINA NUM.: 0000000000" + AccesoBD.SALTO_LINEA +
        //                    "FECHA:  22.03.10   13:22" + AccesoBD.SALTO_LINEA +
        //                    "------------------------" + AccesoBD.SALTO_LINEA +
        //                    "ULTIMA RECAUDACION:" + AccesoBD.SALTO_LINEA +
        //                    "FECHA:  00.00.00   00:00" + AccesoBD.SALTO_LINEA +
        //                    "ENTRADAS  :            0" + AccesoBD.SALTO_LINEA +
        //                    "SALIDAS   :            0" + AccesoBD.SALTO_LINEA +
        //                    "RECAUDACION ACTUAL:" + AccesoBD.SALTO_LINEA +
        //                    "ENTRADAS  :       871671" + AccesoBD.SALTO_LINEA +
        //                    "SALIDAS   :       612981" + AccesoBD.SALTO_LINEA +
        //                    "PORCENTAJE:      70,32 %" + AccesoBD.SALTO_LINEA +
        //                    "RECAUDACION:      258690" + AccesoBD.SALTO_LINEA +
        //                    "RIESGO 10C.:     2586,90" + AccesoBD.SALTO_LINEA +
        //                    "CONT. RELLENO       0,00" + AccesoBD.SALTO_LINEA +
        //                    "------------------------" + AccesoBD.SALTO_LINEA +
        //                    "MONEDAS INTRODUCIDAS:" + AccesoBD.SALTO_LINEA +
        //                    "20,00 =    10080,00 EURO" + AccesoBD.SALTO_LINEA +
        //                    "10,00 =    10800,00 EURO" + AccesoBD.SALTO_LINEA +
        //                    "5,00 =     5650,00 EURO" + AccesoBD.SALTO_LINEA +
        //                    "2,00 =     2126,00 EURO" + AccesoBD.SALTO_LINEA +
        //                    "1,00 =    14233,00 EURO" + AccesoBD.SALTO_LINEA +
        //                    "0,50 =     1379,00 EURO" + AccesoBD.SALTO_LINEA +
        //                    "0,20 =     1589,80 EURO" + AccesoBD.SALTO_LINEA +
        //                    "0,10 =      374,30 EURO" + AccesoBD.SALTO_LINEA +
        //                    "TOTAL:     46232,10 EURO" + AccesoBD.SALTO_LINEA +
        //                    "PAGO DEL HOPPER:" + AccesoBD.SALTO_LINEA +
        //                    "1,00 =    59226,00 EURO" + AccesoBD.SALTO_LINEA +
        //                    "1,00 =       85,00 EURO" + AccesoBD.SALTO_LINEA +
        //                    "0,20 =      716,00 EURO" + AccesoBD.SALTO_LINEA +
        //                    "TOTAL:     60027,00 EURO" + AccesoBD.SALTO_LINEA +
        //                    "------------------------" + AccesoBD.SALTO_LINEA +
        //                    "HORA: L 12 : M  9 : X  9";

        //    InfoContadores info = ProcesarDatos(ticket1);

        //    System.Diagnostics.Debug.WriteLine(info.Entradas);//871671
        //    System.Diagnostics.Debug.WriteLine(info.Salidas);//612981
        //    System.Diagnostics.Debug.WriteLine(info.Billetes);//2714
        //    System.Diagnostics.Debug.WriteLine(info.Billetes5);//1130
        //    System.Diagnostics.Debug.WriteLine(info.Billetes10);//1080
        //    System.Diagnostics.Debug.WriteLine(info.Billetes20);//504
        //}

        //public void Test2()Ticket_BW_ES08
        //{
        //    string ticket1 =
        //        "------------------------" + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //        "BW/ES              ES.08" + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //        "ES ActionStar TC   1.1.0" + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //        "LOCAL:        S" + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //        "N. MAQUINA:   0000000000" + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //        "HARDWARE ID:71D925DFEA3A" + AccesoBD.SALTO_LINEA +
        //        "KC" + AccesoBD.SALTO_LINEA +
        //        "IDENTIFICACION:600010309" + AccesoBD.SALTO_LINEA +
        //        "K)" + AccesoBD.SALTO_LINEA +
        //        "VERSION:BW6000      0920" + AccesoBD.SALTO_LINEA +
        //        "Km" + AccesoBD.SALTO_LINEA +
        //        "VALENCIA SALON" + AccesoBD.SALTO_LINEA +
        //        "------------------------" + AccesoBD.SALTO_LINEA +
        //        "K%" + AccesoBD.SALTO_LINEA +
        //        "N. IMPRESION: 0001 A 001" + AccesoBD.SALTO_LINEA +
        //        "RECAUDACION HASTA:" + AccesoBD.SALTO_LINEA +
        //        "K&" + AccesoBD.SALTO_LINEA +
        //               "23.10.14    10:38" + AccesoBD.SALTO_LINEA +
        //        "PUESTA EN MARCHA:" + AccesoBD.SALTO_LINEA +
        //        "K'" + AccesoBD.SALTO_LINEA +
        //               "21.10.14    13:25" + AccesoBD.SALTO_LINEA +
        //        "" + AccesoBD.SALTO_LINEA +
        //        "CONTADORES TOTALES:" + AccesoBD.SALTO_LINEA +
        //        "K~" + AccesoBD.SALTO_LINEA +
        //        "ENTRADAS     :        56" + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //        "SALIDAS      :         5" + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //        "PORCENTAJE   :      8,92" + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //        "PARTIDAS 1   :        41" + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //        "PARTIDAS 2   :         3" + AccesoBD.SALTO_LINEA +
        //        "" + AccesoBD.SALTO_LINEA +
        //        "DESDE PUESTA EN MARCHA:" + AccesoBD.SALTO_LINEA +
        //        "========================" + AccesoBD.SALTO_LINEA +
        //        "C.M. BORRADOS   :      0" + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //        "ENTRADAS P.     :     56" + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //        "SALIDAS P.      -      5" + AccesoBD.SALTO_LINEA +
        //                        "--------" + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //        "DIFERENCIA      :     51" + AccesoBD.SALTO_LINEA +
        //                        "========" + AccesoBD.SALTO_LINEA +
        //        "" + AccesoBD.SALTO_LINEA +
        //        "KQ" + AccesoBD.SALTO_LINEA +
        //        "ENTRADA DINERO :   11,00" + AccesoBD.SALTO_LINEA +
        //        "KR" + AccesoBD.SALTO_LINEA +
        //        "SALIDA DINERO  -    0,80" + AccesoBD.SALTO_LINEA +
        //        "Ku" + AccesoBD.SALTO_LINEA +
        //        "PAGOS MANUALES -    0,00" + AccesoBD.SALTO_LINEA +
        //                        "--------" + AccesoBD.SALTO_LINEA +
        //        "Kj" + AccesoBD.SALTO_LINEA +
        //        "DIFERENCIA     :   10,20" + AccesoBD.SALTO_LINEA +
        //                        "========" + AccesoBD.SALTO_LINEA +
        //        "" + AccesoBD.SALTO_LINEA +
        //        "KS" + AccesoBD.SALTO_LINEA +
        //        "DIF. DINERO    :   10,20" + AccesoBD.SALTO_LINEA +
        //        "RESERVA PAGOS" + AccesoBD.SALTO_LINEA +
        //        "KT" + AccesoBD.SALTO_LINEA +
        //        "INCREMENTO     -   10,20" + AccesoBD.SALTO_LINEA +
        //        "KU" + AccesoBD.SALTO_LINEA +
        //        "RELLENADOS     +    0,00" + AccesoBD.SALTO_LINEA +
        //        "K@" + AccesoBD.SALTO_LINEA +
        //        "RECAUDADO      -    0,00" + AccesoBD.SALTO_LINEA +
        //        "KV" + AccesoBD.SALTO_LINEA +
        //        "FALTANTE       -    0,00" + AccesoBD.SALTO_LINEA +
        //                        "--------" + AccesoBD.SALTO_LINEA +
        //        "KB" + AccesoBD.SALTO_LINEA +
        //        "CAJA" + AccesoBD.SALTO_LINEA +
        //        "ELECTRONICA    :    0,00" + AccesoBD.SALTO_LINEA +
        //                        "========" + AccesoBD.SALTO_LINEA +
        //        "" + AccesoBD.SALTO_LINEA +
        //        "KA" + AccesoBD.SALTO_LINEA +
        //        "RECAUDADO      +    0,00" + AccesoBD.SALTO_LINEA +
        //        "KW" + AccesoBD.SALTO_LINEA +
        //        "RELLENADOS     -    0,00" + AccesoBD.SALTO_LINEA +
        //                        "--------" + AccesoBD.SALTO_LINEA +
        //        "" + AccesoBD.SALTO_LINEA +
        //        "KX" + AccesoBD.SALTO_LINEA +
        //        "TOTAL          :    0,00" + AccesoBD.SALTO_LINEA +
        //                        "--------" + AccesoBD.SALTO_LINEA +
        //        "" + AccesoBD.SALTO_LINEA +
        //        "DATOS POR DIAS:" + AccesoBD.SALTO_LINEA +
        //        "FECHA T.ENC. T.JUE. DIF." + AccesoBD.SALTO_LINEA +
        //               "MIN. MIN.    EU" + AccesoBD.SALTO_LINEA +
        //        "KF" + AccesoBD.SALTO_LINEA +
        //        "21.10.  471    6    9,20" + AccesoBD.SALTO_LINEA +
        //        "22.10.  258   71    1,00" + AccesoBD.SALTO_LINEA +
        //        "23.10.   38   37    0,00" + AccesoBD.SALTO_LINEA +
        //        "" + AccesoBD.SALTO_LINEA +
        //        "------------------------" + AccesoBD.SALTO_LINEA +
        //            "VALORES ACTUALES" + AccesoBD.SALTO_LINEA +
        //        "------------------------" + AccesoBD.SALTO_LINEA +
        //        "TOLVAS HOPPERS:" + AccesoBD.SALTO_LINEA +
        //        "K[" + AccesoBD.SALTO_LINEA +
        //          "1,00 =    1,00 EU" + AccesoBD.SALTO_LINEA +
        //        "K]" + AccesoBD.SALTO_LINEA +
        //          "0,20 =    9,20 EU" + AccesoBD.SALTO_LINEA +
        //                 "==========" + AccesoBD.SALTO_LINEA +
        //        "K_" + AccesoBD.SALTO_LINEA +
        //        "ACTUAL:    10,20 EU" + AccesoBD.SALTO_LINEA +
        //        "KO" + AccesoBD.SALTO_LINEA +
        //        "ANTERIOR:   0,00 EU" + AccesoBD.SALTO_LINEA +
        //        "" + AccesoBD.SALTO_LINEA +
        //        "CARGA RECICLADOR:" + AccesoBD.SALTO_LINEA +
        //        "K=" + AccesoBD.SALTO_LINEA +
        //         "50,00 =    0,00 EU" + AccesoBD.SALTO_LINEA +
        //         "20,00 =    0,00 EU" + AccesoBD.SALTO_LINEA +
        //         "10,00 =    0,00 EU" + AccesoBD.SALTO_LINEA +
        //          "5,00 =    0,00 EU" + AccesoBD.SALTO_LINEA +
        //                 "==========" + AccesoBD.SALTO_LINEA +
        //        "K>" + AccesoBD.SALTO_LINEA +
        //        "ACTUAL:     0,00 EU" + AccesoBD.SALTO_LINEA +
        //        "K?" + AccesoBD.SALTO_LINEA +
        //        "ANTERIOR:   0,00 EU" + AccesoBD.SALTO_LINEA +
        //        "" + AccesoBD.SALTO_LINEA +
        //        "------------------------" + AccesoBD.SALTO_LINEA +
        //                 "CAJA" + AccesoBD.SALTO_LINEA +
        //        "------------------------" + AccesoBD.SALTO_LINEA +
        //        "MONEDAS:" + AccesoBD.SALTO_LINEA +
        //        "K4" + AccesoBD.SALTO_LINEA +
        //          "2,00 =    0,00 EU" + AccesoBD.SALTO_LINEA +
        //          "1,00 =    0,00 EU" + AccesoBD.SALTO_LINEA +
        //          "0,50 =    0,00 EU" + AccesoBD.SALTO_LINEA +
        //          "0,20 =    0,00 EU" + AccesoBD.SALTO_LINEA +
        //          "0,10 =    0,00 EU" + AccesoBD.SALTO_LINEA +
        //                 "==========" + AccesoBD.SALTO_LINEA +
        //        "K<" + AccesoBD.SALTO_LINEA +
        //                    "0,00 EU" + AccesoBD.SALTO_LINEA +
        //        "" + AccesoBD.SALTO_LINEA +
        //        "BILLETES:" + AccesoBD.SALTO_LINEA +
        //        "K5" + AccesoBD.SALTO_LINEA +
        //         "50,00 =    0,00 EU" + AccesoBD.SALTO_LINEA +
        //         "20,00 =    0,00 EU" + AccesoBD.SALTO_LINEA +
        //         "10,00 =    0,00 EU" + AccesoBD.SALTO_LINEA +
        //          "5,00 =    0,00 EU" + AccesoBD.SALTO_LINEA +
        //                 "==========" + AccesoBD.SALTO_LINEA +
        //        "KJ" + AccesoBD.SALTO_LINEA +
        //                    "0,00 EU" + AccesoBD.SALTO_LINEA +
        //        "========================" + AccesoBD.SALTO_LINEA +
        //        "" + AccesoBD.SALTO_LINEA +
        //                 "FIN" + AccesoBD.SALTO_LINEA +
        //        "" + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //        "CONTENIDO CAJA      0,00" + AccesoBD.SALTO_LINEA +
        //        "" + AccesoBD.SALTO_LINEA +
        //        "========================" + AccesoBD.SALTO_LINEA +
        //        "INICIO DATOS:      00.00" + AccesoBD.SALTO_LINEA +
        //        "FINAL DATOS:       00.00" + AccesoBD.SALTO_LINEA +
        //        "------------------------" + AccesoBD.SALTO_LINEA +
        //        "EST. GENERAL JUEGOS" + AccesoBD.SALTO_LINEA +
        //        "Ko" + AccesoBD.SALTO_LINEA +
        //        "------------------------" + AccesoBD.SALTO_LINEA +
        //        "TOTAL PARTIDAS:       61" + AccesoBD.SALTO_LINEA +
        //        "PUNTOS APOSTADOS    1500" + AccesoBD.SALTO_LINEA +
        //        "PUNTOS GANADOS:      579" + AccesoBD.SALTO_LINEA +
        //        "DIFFERENCIA:         921" + AccesoBD.SALTO_LINEA +
        //        "========================" + AccesoBD.SALTO_LINEA +
        //        "EST. POR JUEGOS" + AccesoBD.SALTO_LINEA +
        //        "------------------------" + AccesoBD.SALTO_LINEA +
        //        "JUEGO" + AccesoBD.SALTO_LINEA +
        //        "PART.    APOST.    GANA." + AccesoBD.SALTO_LINEA +
        //        "------------------------" + AccesoBD.SALTO_LINEA +
        //        "40 FRUITS:" + AccesoBD.SALTO_LINEA +
        //             "0        0        0" + AccesoBD.SALTO_LINEA +
        //        "ALICE:" + AccesoBD.SALTO_LINEA +
        //             "4       40        4" + AccesoBD.SALTO_LINEA +
        //        "BINGO BINGO:" + AccesoBD.SALTO_LINEA +
        //             "0        0        0" + AccesoBD.SALTO_LINEA +
        //        "BLACK PIRATE:" + AccesoBD.SALTO_LINEA +
        //             "0        0        0" + AccesoBD.SALTO_LINEA +
        //        "BRAZIL SAMBA:" + AccesoBD.SALTO_LINEA +
        //             "0        0        0" + AccesoBD.SALTO_LINEA +
        //        "CASH BOX:" + AccesoBD.SALTO_LINEA +
        //             "0        0        0" + AccesoBD.SALTO_LINEA +
        //        "EXPLODIAC:" + AccesoBD.SALTO_LINEA +
        //             "0        0        0" + AccesoBD.SALTO_LINEA +
        //        "FANCY FRUITS:" + AccesoBD.SALTO_LINEA +
        //             "0        0        0" + AccesoBD.SALTO_LINEA +
        //        "KING OF THE JUNG:" + AccesoBD.SALTO_LINEA +
        //             "0        0        0" + AccesoBD.SALTO_LINEA +
        //        "KING AND QUEEN:" + AccesoBD.SALTO_LINEA +
        //             "0        0        0" + AccesoBD.SALTO_LINEA +
        //        "LA DOLCE VITA:" + AccesoBD.SALTO_LINEA +
        //             "0        0        0" + AccesoBD.SALTO_LINEA +
        //        "MAGIC BOOK:" + AccesoBD.SALTO_LINEA +
        //             "1       10        0" + AccesoBD.SALTO_LINEA +
        //        "MAGIC STONE:" + AccesoBD.SALTO_LINEA +
        //             "0        0        0" + AccesoBD.SALTO_LINEA +
        //        "MIGHTY DRAGON:" + AccesoBD.SALTO_LINEA +
        //            "14      610       80" + AccesoBD.SALTO_LINEA +
        //        "NIGHT WOLVES:" + AccesoBD.SALTO_LINEA +
        //             "0        0        0" + AccesoBD.SALTO_LINEA +
        //        "RAMSES BOOK:" + AccesoBD.SALTO_LINEA +
        //             "0        0        0" + AccesoBD.SALTO_LINEA +
        //        "SALOON POKER:" + AccesoBD.SALTO_LINEA +
        //             "0        0        0" + AccesoBD.SALTO_LINEA +
        //        "SEVEN AND BARS:" + AccesoBD.SALTO_LINEA +
        //             "0        0        0" + AccesoBD.SALTO_LINEA +
        //        "SITTING BULL:" + AccesoBD.SALTO_LINEA +
        //             "0        0        0" + AccesoBD.SALTO_LINEA +
        //        "SNOW WHITE:" + AccesoBD.SALTO_LINEA +
        //             "0        0        0" + AccesoBD.SALTO_LINEA +
        //        "STACKED SEVENS D:" + AccesoBD.SALTO_LINEA +
        //             "0        0        0" + AccesoBD.SALTO_LINEA +
        //        "TAKE 5:" + AccesoBD.SALTO_LINEA +
        //             "0        0        0" + AccesoBD.SALTO_LINEA +
        //        "THORS HAMMER:" + AccesoBD.SALTO_LINEA +
        //            "42      840      495" + AccesoBD.SALTO_LINEA +
        //        "THUNDER SEVEN:" + AccesoBD.SALTO_LINEA +
        //             "0        0        0" + AccesoBD.SALTO_LINEA +
        //        "TRIPLE 5:" + AccesoBD.SALTO_LINEA +
        //             "0        0        0" + AccesoBD.SALTO_LINEA +
        //        "WESTERN JACK:" + AccesoBD.SALTO_LINEA +
        //             "0        0        0" + AccesoBD.SALTO_LINEA +
        //        "WIN SHOOTER:" + AccesoBD.SALTO_LINEA +
        //             "0        0        0" + AccesoBD.SALTO_LINEA +
        //        "WILD WARRIOR:" + AccesoBD.SALTO_LINEA +
        //             "0        0        0" + AccesoBD.SALTO_LINEA +
        //        "WILD RUBIES:" + AccesoBD.SALTO_LINEA +
        //             "0        0        0" + AccesoBD.SALTO_LINEA +
        //        "WIN BLASTER:" + AccesoBD.SALTO_LINEA +
        //             "0        0        0" + AccesoBD.SALTO_LINEA +
        //        "" + AccesoBD.SALTO_LINEA +
        //        "DIAS CONECTADA    12" + AccesoBD.SALTO_LINEA +
        //        "Ka" + AccesoBD.SALTO_LINEA +
        //        "M.HORAS/DIA      4,7 HR." + AccesoBD.SALTO_LINEA +
        //        "Kb" + AccesoBD.SALTO_LINEA +
        //        "M.TIEMPO JUEGO   0,1 HR." + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //        "M.RECAUDACION    0,85 EU" + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //        "UTILIZACION      3,32 %" + AccesoBD.SALTO_LINEA +
        //        "" + AccesoBD.SALTO_LINEA +
        //        "ESTADISTICA TOTAL:" + AccesoBD.SALTO_LINEA +
        //        "DIAS CONECTADA    12" + AccesoBD.SALTO_LINEA +
        //        "M.HORAS/DIA      4,7 HR." + AccesoBD.SALTO_LINEA +
        //        "M.TIEMPO JUEGO   0,1 HR." + AccesoBD.SALTO_LINEA +
        //        "M.RECAUDACION    0,85 EU" + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //        "UTILIZACION      3,32 %" + AccesoBD.SALTO_LINEA +
        //        "" + AccesoBD.SALTO_LINEA +
        //        "INSERTADO:" + AccesoBD.SALTO_LINEA +
        //        "Ks" + AccesoBD.SALTO_LINEA +
        //         "50,00 =  100,00 EU" + AccesoBD.SALTO_LINEA +
        //         "20,00 =   80,00 EU" + AccesoBD.SALTO_LINEA +
        //         "10,00 =   60,00 EU" + AccesoBD.SALTO_LINEA +
        //          "5,00 =   50,00 EU" + AccesoBD.SALTO_LINEA +
        //          "2,00 =    0,00 EU" + AccesoBD.SALTO_LINEA +
        //          "1,00 =    1,00 EU" + AccesoBD.SALTO_LINEA +
        //          "0,50 =    0,00 EU" + AccesoBD.SALTO_LINEA +
        //          "0,20 =   10,00 EU" + AccesoBD.SALTO_LINEA +
        //          "0,10 =    0,00 EU" + AccesoBD.SALTO_LINEA +
        //                 "==========" + AccesoBD.SALTO_LINEA +
        //        "Kt" + AccesoBD.SALTO_LINEA +
        //                   "11,00 EU" + AccesoBD.SALTO_LINEA +
        //        "" + AccesoBD.SALTO_LINEA +
        //        "HOPPER ANTERIOR:" + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //          "1,00 =    0,00 EU" + AccesoBD.SALTO_LINEA +
        //          "0,20 =    0,00 EU" + AccesoBD.SALTO_LINEA +
        //                 "==========" + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //                    "0,00 EU" + AccesoBD.SALTO_LINEA +
        //        "" + AccesoBD.SALTO_LINEA +
        //        "PAGOS POR HOPPER:" + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //          "1,00 =    0,00 EU" + AccesoBD.SALTO_LINEA +
        //          "0,20 =    0,80 EU" + AccesoBD.SALTO_LINEA +
        //                 "==========" + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //                    "0,80 EU" + AccesoBD.SALTO_LINEA +
        //        "" + AccesoBD.SALTO_LINEA +
        //        "RECICLADOR ANTERIOR:" + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //         "50,00 =    0,00 EU" + AccesoBD.SALTO_LINEA +
        //         "20,00 =    0,00 EU" + AccesoBD.SALTO_LINEA +
        //         "10,00 =    0,00 EU" + AccesoBD.SALTO_LINEA +
        //          "5,00 =    0,00 EU" + AccesoBD.SALTO_LINEA +
        //                 "==========" + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //                    "0,00 EU" + AccesoBD.SALTO_LINEA +
        //        "" + AccesoBD.SALTO_LINEA +
        //        "PAGOS RECICLADOR:" + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //         "50,00 =    0,00 EU" + AccesoBD.SALTO_LINEA +
        //         "20,00 =    0,00 EU" + AccesoBD.SALTO_LINEA +
        //         "10,00 =    0,00 EU" + AccesoBD.SALTO_LINEA +
        //          "5,00 =    0,00 EU" + AccesoBD.SALTO_LINEA +
        //                 "==========" + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //                    "0,00 EU" + AccesoBD.SALTO_LINEA +
        //        "" + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //        "BANCO:      0,00 EU" + AccesoBD.SALTO_LINEA +
        //        "" + AccesoBD.SALTO_LINEA +
        //        "Ky" + AccesoBD.SALTO_LINEA +
        //        "CODIGO        : 2412" + AccesoBD.SALTO_LINEA +
        //        "K{" + AccesoBD.SALTO_LINEA +
        //        "CODIGO PUERTA : 1492" + AccesoBD.SALTO_LINEA +
        //        "" + AccesoBD.SALTO_LINEA +
        //        "PUERTA ABIERTA:" + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //        "23.10. A10:33" + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //        "23.10. A10:04 Z10:05" + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //        "23.10. A10:03 Z10:03" + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //        "23.10. A10:02 Z10:02" + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //        "23.10. O09:53" + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //        "23.10. O09:53 S09:53" + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //        "23.10.        S09:53" + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //        "21.10. O19:45 S19:46" + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //        "21.10. A19:45 Z19:46" + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //        "21.10. O13:21 S13:25" + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //        "21.10. A13:21 Z13:25" + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //        "20.10. O19:26 S19:26" + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //        "20.10. O19:25 S19:26" + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //        "20.10.        S19:25" + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //        "20.10. A19:26 Z19:26" + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //        "20.10. A19:26 Z19:26" + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //        "20.10. A19:25 Z19:25" + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //        "20.10. A19:11 Z19:11" + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //        "20.10. A18:33 Z18:37" + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //        "20.10. A18:32 Z18:32" + AccesoBD.SALTO_LINEA +
        //        "" + AccesoBD.SALTO_LINEA +
        //        "TABLA SUCESOS:" + AccesoBD.SALTO_LINEA +
        //        "K|" + AccesoBD.SALTO_LINEA +
        //        "23.10. 10:35 C" + AccesoBD.SALTO_LINEA +
        //        "K|" + AccesoBD.SALTO_LINEA +
        //        "20.10. 18:33 C" + AccesoBD.SALTO_LINEA +
        //        "K|" + AccesoBD.SALTO_LINEA +
        //        "22.09. 17:03 C" + AccesoBD.SALTO_LINEA +
        //        "K|" + AccesoBD.SALTO_LINEA +
        //        "17.09. 11:33 C" + AccesoBD.SALTO_LINEA +
        //        "K|" + AccesoBD.SALTO_LINEA +
        //        "17.09. 11:32 A" + AccesoBD.SALTO_LINEA +
        //        "K|" + AccesoBD.SALTO_LINEA +
        //        "13.08. 12:21 P" + AccesoBD.SALTO_LINEA +
        //        "K|" + AccesoBD.SALTO_LINEA +
        //        "13.08. 12:21 C" + AccesoBD.SALTO_LINEA +
        //        "K|" + AccesoBD.SALTO_LINEA +
        //        "13.08. 12:20 A" + AccesoBD.SALTO_LINEA +
        //        "" + AccesoBD.SALTO_LINEA +
        //        "AJUSTES:" + AccesoBD.SALTO_LINEA +
        //        "Kp" + AccesoBD.SALTO_LINEA +
        //        "A:100; B:0,1,1,1,1,1;" + AccesoBD.SALTO_LINEA +
        //        "C:0; D:5; E:4,4;" + AccesoBD.SALTO_LINEA +
        //        "F:0,0;" + AccesoBD.SALTO_LINEA +
        //        "G:1,1,1,1; H:0;" + AccesoBD.SALTO_LINEA +
        //        "K:1500,1300;" + AccesoBD.SALTO_LINEA +
        //        "R:4;" + AccesoBD.SALTO_LINEA +
        //        "S:0,0,1,0,0,0,0,0;" + AccesoBD.SALTO_LINEA +
        //        "U:1; X:0.3;" + AccesoBD.SALTO_LINEA +
        //        "Y:544541B6,705,4;" + AccesoBD.SALTO_LINEA +
        //        "" + AccesoBD.SALTO_LINEA +
        //        "MODELO JUEGO:" + AccesoBD.SALTO_LINEA +
        //        "Kn" + AccesoBD.SALTO_LINEA +
        //           "APUESTA MIN. 5 PUNTOS" + AccesoBD.SALTO_LINEA +
        //        "Kn" + AccesoBD.SALTO_LINEA +
        //        "=> APUESTA MIN. 10 PUNTO" + AccesoBD.SALTO_LINEA +
        //        "" + AccesoBD.SALTO_LINEA +
        //        "Kc" + AccesoBD.SALTO_LINEA +
        //        "201014 APUESTA MIN. 10 P" + AccesoBD.SALTO_LINEA +
        //        "" + AccesoBD.SALTO_LINEA +
        //        "HARDWARE INFO:" + AccesoBD.SALTO_LINEA +
        //        "K" + AccesoBD.SALTO_LINEA +
        //        "MON:X6 V20.0 S02366095" + AccesoBD.SALTO_LINEA +
        //        "BIL:NV113552160B01+ENC7D" + AccesoBD.SALTO_LINEA +
        //        "H0 :U+ 7.2  4.3 S599589" + AccesoBD.SALTO_LINEA +
        //        "H1 :U+ 7.2  4.3 S599587" + AccesoBD.SALTO_LINEA +
        //        "" + AccesoBD.SALTO_LINEA +
        //        "========================" + AccesoBD.SALTO_LINEA +
        //        "NO DATOS ANTERIOR" + AccesoBD.SALTO_LINEA +
        //        "" + AccesoBD.SALTO_LINEA +
        //        "SIN ENTRADAS" + AccesoBD.SALTO_LINEA +
        //        "" + AccesoBD.SALTO_LINEA +
        //        "FIN NC NL" + AccesoBD.SALTO_LINEA +
        //        "C981B" + AccesoBD.SALTO_LINEA +
        //        "";
        //        InfoContadores info = ProcesarDatos(ticket1);

        //        System.Diagnostics.Debug.WriteLine(info.Entradas);//56
        //        System.Diagnostics.Debug.WriteLine(info.Salidas);//5
        //        System.Diagnostics.Debug.WriteLine(info.Billetes);//22
        //        System.Diagnostics.Debug.WriteLine(info.Billetes5);//10
        //        System.Diagnostics.Debug.WriteLine(info.Billetes10);//6
        //        System.Diagnostics.Debug.WriteLine(info.Billetes20);//4
        //        System.Diagnostics.Debug.WriteLine(info.Billetes50);//2
        //    }
    }
}
