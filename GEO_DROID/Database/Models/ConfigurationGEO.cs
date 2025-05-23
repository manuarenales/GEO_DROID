using SQLite;
using System.Runtime.Serialization;

namespace GeoDroid.Data
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/GeoDroid.Models")]
    public class ConfigurationGEO
    {
        //ejemplo constantes para el código
        public const string TP_TICKET_23 = "TP_TICKET_23";
        public const string TP_TICKET_20 = "TP_TICKET_20";
        public const string TP_TICKETS_000053_1 = "TP_TICKETS_000053_1";
        public const string TP_TICKET_01 = "TP_TICKET_01"; // Ticket Recaudación y préstamo (000032)
        public const string TP_TICKET_02 = "TP_TICKET_02"; // Ticket para instalaciones Recaudación liquidación (en teoría) + Carga (000035)
        public const string TP_TICKET_03 = "TP_TICKET_03"; // Ticket de recaudación detallado con desglose de monedas + cargas + préstamos (00068)
        public const string TP_TICKET_04 = "TP_TICKET_04"; // Ticket de préstamos y saldos. Además, establece una serie de tickets activos (00091)
        public const string TP_TICKET_05 = "TP_TICKET_05"; // Ticket de recaudacion con contadores y datos de empresa (00084)
        public const string TP_TICKET_06 = "TP_TICKET_06"; // Ticket de recaudacion sólo con importe entregado (00094)
        public const string TP_TICKET_07 = "TP_TICKET_07"; // Establece tickets personalizados (00041)
        public const string TP_TICKET_08 = "TP_TICKET_08"; // Ticket de recaudacion con desglose de bruto (00107)
        public const string TP_TICKET_09 = "TP_TICKET_09"; // Ticket de recaudacion reducido, con importes toales (00125)
        public const string TP_TICKET_10 = "TP_TICKET_10"; // Ticket de recaudacion detallado para empresa operadora (00096)
        public const string TP_TICKET_11 = "TP_TICKET_11"; // Ticket de recaudacion detallado para establecimiento (00096)
        public const string TP_TICKET_12 = "TP_TICKET_12"; // Ticket de recaudacion detallado con desglose monedas (00117)
        public const string TP_TICKET_13 = "TP_TICKET_13"; // Establece tickets personalizados (00103)
        public const string TP_TICKET_RECDET_PRINT_PRESTAMOS = "TP_TICKET_RECDET_PRINT_PRESTAMOS"; //Mostrar préstamos en el ticket de recaudación detallado
        public const string TP_TICKET_RECDET_PRINT_PRESTAMOS_POR_CONCEPTO = "TP_TICKET_RECDET_PRINT_PRESTAMOS_POR_CONCEPTO"; //Mostrar préstamos por concepto en el ticket de recaudación detallado
        public const string TP_TICKETS_ESPACIO_FIRMA_RECAUDADOR = "TP_TICKETS_ESPACIO_FIRMA_RECAUDADOR";
        public const string TP_TICKETS_FIRMA_LINEAS_VERTICALES = "TP_TICKETS_FIRMA_LINEAS_VERTICALES"; //True=Dibujar líneas verticales para formar un rectángulo para encuadrar la firma
        public const string TP_TICKET_UN_ELEMENTO_SIN_TOTAL = "TP_TICKET_UN_ELEMENTO_SIN_TOTAL";
        public const string TP_TICKET_PRINT_POBLACION = "TP_TICKET_PRINT_POBLACION"; // Mostrar población del establecimiento.
        public const string TP_TICKET_PRINT_COMENTARIOS_1 = "TP_TICKET_PRINT_COMENTARIOS_1"; //Mostrar comentarios de recaudación en el primer ticket. En la copia no se mostrará.
        public const string TP_TICKET_PRINT_DATOS_EMPRESA = "TP_TICKET_PRINT_DATOS_EMPRESA"; //Mostrar los datos de la empresa: nombre, ...
        public const string TP_TICKET_PRINT_IMPORTES_A_CERO = "TP_TICKET_PRINT_IMPORTES_A_CERO"; //Imprimir líneas aunque su importe sea igual a cero. Aplicable a cargas, recuperaciones y conceptos de recaudación)
        public const string TP_TICKET_PRINT_IMPORTE_FACTURABLE = "TP_TICKET_PRINT_IMPORTE_FACTURABLE"; // Mostrar importes facturables
        public const string TP_TICKET_PRINT_CONTADORES = "TP_TICKET_PRINT_CONTADORES"; // Mostrar contadores
        public const string TP_TICKET_PRINT_CONTADORES_TEORICO = "TP_TICKET_PRINT_CONTADORES_TEORICO"; // Mostrar teórico de contadores
        public const string TP_TICKET_PRINT_RECAUDADOR = "TP_TICKET_PRINT_RECAUDADOR"; // Mostrar recaudador
        public const string TP_TICKET_PRINT_BRUTO = "TP_TICKET_PRINT_BRUTO"; // Mostrar bruto
        public const string TP_TICKET_PRINT_TOTALES = "TP_TICKET_PRINT_TOTALES"; // Mostrar totales. En su defecto, aparecerá sólo el importe entregado al establecimiento
        public const string TP_TICKET_PRINT_ARQUEO = "TP_TICKET_PRINT_ARQUEO"; // Mostrar arqueo
        public const string TP_TICKET_PRINT_CONCEPTOS_RECAUDACION = "TP_TICKET_PRINT_CONCEPTOS_RECAUDACION"; // Mostrar detalle de los conceptos de recaudación
        public const string TP_TICKET_PRINT_RECUPERACION_CARGA = "TP_TICKET_PRINT_RECUPERACION_CARGA"; // Mostrar recuperación de carga
        public const string TP_TICKET_PRINT_FECHA = "TP_TICKET_PRINT_FECHA"; // Mostrar fecha de impresión del ticket
        public const string TP_TICKET_PRINT_AVERIA_DESCRIPCION = "TP_TICKET_PRINT_AVERIA_DESCRIPCION"; // Mostrar descripción de la averías
        public const string TP_TICKET_PRINT_PENDIENTE_TASAS_ESTABLECIMIENTO = "TP_TICKET_PRINT_PENDIENTE_TASAS_ESTABLECIMIENTO"; // Imprimir pendiente tasas total
        public const string TP_TICKET_CARGAS_PRINT_SALDOS = "TP_TICKET_CARGAS_PRINT_SALDOS"; // Imprimir saldos de cargas
        public const string TP_TICKET_PRINT_MENSAJE_ACEPTACION_FACTURA = "TP_TICKET_PRINT_MENSAJE_ACEPTACION_FACTURA";
        public const string TP_NETO_CALCULADO = "TP_NETO_CALCULADO";
        public const string TP_GEOLOCALIZACION = "TP_GEOLOCALIZACION";
        public const string TP_GEOLOCALIZACION_INTERVALO_CONSULTA = "TP_GEOLOCALIZACION_INTERVALO_CONSULTA"; // en segundos
        public const string TP_GEOLOCALIZACION_INTERVALO_SYNC = "TP_GEOLOCALIZACION_INTERVALO_SYNC"; // en segundos
        public const string TP_OCULTAR_LECTURA_ANTERIOR = "TP_OCULTAR_LECTURA_ANTERIOR";
        public const string TP_OCULTAR_LECTURA_ANTERIOR_Y_DIFERENCIA = "TP_OCULTAR_LECTURA_ANTERIOR_Y_DIFERENCIA";
        public const string TP_AVERIAS_SERVICE_INTERVALO_SYNC = "TP_AVERIAS_SERVICE_INTERVALO_SYNC";
        public const string TP_AVERIAS_SERVICE = "TP_AVERIAS_SERVICE";
        public const string TP_AVERIAS_FOTO = "TP_AVERIAS_FOTO"; //Habilitar opción de foto en averías (True/False)
        public const string TP_AVERIAS_FOTO_MAX_SIZE_PIXELS = "TP_AVERIAS_FOTO_MAX_SIZE_PIXELS";
        public const string TP_REPOSTAJES_FOTO = "TP_REPOSTAJES_FOTO"; //Habilitar opción de foto en repostajes (True/False)
        public const string TP_REPOSTAJES_FOTO_MAX_SIZE_PIXELS = "TP_REPOSTAJES_FOTO_MAX_SIZE_PIXELS";
        public const string TP_PERMISO_INSTALAR = "TP_PERMISO_INSTALAR";
        public const string TP_PERMISO_RUTA_ADD = "TP_PERMISO_RUTA_ADD";
        public const string TP_PERMISO_RECAUDAR = "TP_PERMISO_RECAUDAR";
        public const string TP_PERMISO_PRESTAMOS = "TP_PERMISO_PRESTAMOS";
        public const string TP_PERMISO_GASTOS = "TP_PERMISO_GASTOS";
        public const string TP_PERMISO_AJUSTE_CONTADORES = "TP_PERMISO_AJUSTE_CONTADORES";
        public const string TP_PERMISO_COMERCIAL = "TP_PERMISO_COMERCIAL";
        public const string TP_PERMISO_INFORMACIONGENERAL = "TP_PERMISO_INFORMACIONGENERAL";
        public const string TP_PERMISO_SELECCION_TIPO_LECTURA = "TP_PERMISO_SELECCION_TIPO_LECTURA";
        public const string TP_CACHE_IMPORTES = "TP_CACHE_IMPORTES";
        public const string TP_ESTABLECIMIENTOS_COMENTARIOS = "TP_ESTABLECIMIENTOS_COMENTARIOS";
        public const string TP_ALERTA_MAQUINA_EN_NEGATIVO = "TP_ALERTA_MAQUINA_EN_NEGATIVO";
        public const string TP_ALERTA_DEUDAS_PENDIENTES = "TP_ALERTA_DEUDAS_PENDIENTES"; //Alerta que nos indique que tenemos importes pendientes de recuperar (cargas, préstamos,...)
        public const string TP_DESGLOSEBRUTO_MINVALUE = "TP_DESGLOSEBRUTO_MINVALUE"; //Mínimo importe que se visualizará en el desglose de billetes/monedas: 0,10;0,20;...
        public const string TP_DESGLOSEBRUTO_NEXT_AUTO = "TP_DESGLOSEBRUTO_NEXT_AUTO"; //Solicitar siguiente importe de forma automática
        public const string TP_DESGLOSEBRUTO_ORDEN = "TP_DESGLOSEBRUTO_ORDEN"; //Ordenación de los importes en pantalla: ASC=0.01, 0.02, 0.05,..., 50; DESC=50, 20, 10,...,0.01
        public const string TP_DESGLOSEBRUTO_TIPO_1 = "TP_DESGLOSEBRUTO_TIPO_1"; //Desglose de billetes completo y monedas de 1, 0.2 y resto
        public const string TP_DESGLOSE_BRUTO = "TP_DESGLOSE_BRUTO"; //No permitir anotar importe bruto total, sólo por desglose de monedas
        public const string TP_DESGLOSE_ONLY = "TP_DESGLOSE_ONLY"; //No permitir anotar importes totales, sólo por desglose de monedas
        public const string TP_UPDATE_FECHA_INSTALACION = "TP_UPDATE_FECHA_INSTALACION";
        public const string TP_CULTURE = "TP_CULTURE";
        public const string TP_FORTUNE_URI = "TP_FORTUNE_URI"; //Localización del servidor de Fortune. Ejemplo:http://172.16.8.146:80/api
        public const string TP_FORTUNE_USER = "TP_FORTUNE_USER"; //Usuario para login en servidor Fortune
        public const string TP_FORTUNE_PASSWORD = "TP_FORTUNE_PASSWORD"; //Contraseña para login en servidor Fortune
        public const string TP_PRINTER_ZEBRA_MAC_PREFIJOS = "TP_PRINTER_ZEBRA_MAC_PREFIJOS"; //Prefijos MAC relativos a impresoras Zebra.Datos de ejemplo "00:A0:96" o "00:A0:96,00:A0:97"
        public const string TP_PRINTER_ESCPOS_MAC_PREFIJOS = "TP_PRINTER_ESCPOS_MAC_PREFIJOS"; //Prefijos MAC relativos a impresoras ESC/POS.Datos de ejemplo "00:A0:96" o "00:A0:96,00:A0:97"
        public const string TP_PRINTER_48_COLUMNAS_MAC_PREFIJOS = "TP_PRINTER_48_COLUMNAS_MAC_PREFIJOS"; //Prefijos MAC relativos a impresoras de 48 columnas. Datos de ejemplo "00:A0:96" o "00:A0:96,00:A0:97"
        public const string TP_PRINTER_SIMBOLO_EURO = "TP_PRINTER_SIMBOLO_EURO"; //Símbolo del euro en las impresiones de tickets
        public const string TP_PRINTER_SIMBOLO_EURO_MAC_PREFIJOS = "TP_PRINTER_SIMBOLO_EURO_MAC_PREFIJOS"; //Prefijos MAC relativos a impresoras que utilizarán el símbolo de euro de las impresiones (TP_PRINTER_SIMBOLO_EURO). Datos de ejemplo "00:A0:96" o "00:A0:96,00:A0:97"
        public const string TP_PRINTER_SIN_TILDES_MAC_PREFIJOS = "TP_PRINTER_SIN_TILDES_MAC_PREFIJOS"; //Prefijos MAC relativos a impresoras que no imprimen tildes. Datos de ejemplo "00:A0:96" o "00:A0:96,00:A0:97"
        public const string TP_VER_CONFIGURACION_CONCEPTOS_RECAUDACION = "TP_VER_CONFIGURACION_CONCEPTOS_RECAUDACION"; //Visualizar la configuración establecida para el cálculo de importes de conceptos de recaudación
        public const string TP_VER_PENDIENTE_CONCEPTOS_RECAUDACION = "TP_VER_PENDIENTE_CONCEPTOS_RECAUDACION"; //Visualizar el importe pendiente de conceptos de recaudación
        public const string TP_CARGAS_POR_ESTABLECIMIENTO = "TP_CARGAS_POR_ESTABLECIMIENTO"; //Compensar cargas
        public const string TP_TASAS_POR_ESTABLECIMIENTO = "TP_TASAS_POR_ESTABLECIMIENTO"; //Compensar tasas
        public const string TP_TASAS_POR_ESTABLECIMIENTO_IDS = "TP_TASAS_POR_ESTABLECIMIENTO_IDS"; //Compensar tasas para Establecimiento.idGeo específicos. Utilizar formato con separador coma ','. Ejemplo: 
        public const string TP_ARQUEO_MAX_DIAS_SHOW_ALERT = "TP_ARQUEO_MAX_DIAS_SHOW_ALERT"; //Mostrar diálogo de alerta
        public const string TP_CARGA_PENDIENTE_SHOW_ALERT = "TP_CARGA_PENDIENTE_SHOW_ALERT"; //Mostrar diálogo de alerta
        public const string TP_SEMILLA_BLOQUEO_RUTA = "TP_SEMILLA_BLOQUEO_RUTA"; //Semilla que se aplica en el código de bloqueo de ruta
        public const string TP_BLOQUEO_RUTA_TOTAL = "TP_BLOQUEO_RUTA_TOTAL"; //Si habilitado, se bloquean todas las opciones, en caso contrario se habilitan cambios y averías
        public const string TP_LECTURA_MANUAL_ENABLED = "TP_LECTURA_MANUAL_ENABLED"; //Se pueden anotar lectura manualmente
        public const string TP_LECTURA_MANUAL_CON_AUTO_ENABLED = "TP_LECTURA_MANUAL_CON_AUTO_ENABLED"; //Se puede anotar lectura manualmente si no hay opción de hacerlo de forma automática
        public const string TP_VER_SALDO_CARGA_LISTA_MAQUINAS_REC = "TP_VER_SALDO_CARGA_LISTA_MAQUINAS_REC"; //Ver el saldo de cargas en la lista de máquinas a recaudar
        public const string TP_VER_TOTAL_RECAUDACION_ESTABLECIMIENTO = "TP_VER_TOTAL_RECAUDACION_ESTABLECIMIENTO"; //Ver el total de recaudación, en lugar del importe pendiente de recuperar tasas
        public const string TP_VER_PENDIENTE_TASAS_ESTABLECIMIENTO = "TP_VER_PENDIENTE_TASAS_ESTABLECIMIENTO"; //Ver el importe pendiente de tasas, ignorando otros conceptos
        public const string TP_RUTA_SIMPLE = "TP_RUTA_SIMPLE"; //Ruta simple: Selección de establecimiento sin información adicional
        public const string TP_BRUTO_MAS_ARQUEO = "TP_BRUTO_MAS_ARQUEO"; //Recaudar con importe Bruto+Arqueo
        public const string TP_CARGAS_PARTE_ESTABLECIMIENTO = "TP_CARGAS_PARTE_ESTABLECIMIENTO"; //Cargas y recuperaciones habilitadas para la parte de establecimiento
        public const string TP_VER_AVISO_RECAUDACION_SIEMPRE = "TP_VER_AVISO_RECAUDACION_SIEMPRE"; //Ver el aviso de recaudación asignado al establecimiento, aunque no seamos recaudadores
        public const string TP_RUTA_REC_PENDIENTE_SI_MMNR = "TP_RUTA_REC_PENDIENTE_SI_MMNR"; //Ver icono de recaudar como pendiente si tenemos MMNR, ya que en ese caso no tenemos recaudación 
        public const string TP_VEHICULOS = "TP_VEHICULOS"; //Licencia para gestión de vehículos. True=Licencia habilitada
        public const string TP_VEHICULOS_MIN_KM_VALIDAR = "TP_VEHICULOS_MIN_KM_VALIDAR"; // True=Validar que los km introducidos son iguales o superiores a los anteriores
        public const string TP_VEHICULOS_MAX_KM_PARCIAL = "TP_VEHICULOS_MAX_KM_PARCIAL"; // Valor que representa los kilómetros de margen que se permiten anotar con respecto a los anteriores
        public const string TP_VEHICULOS_REPOSTAJE_MIN_PRECIO_UNIDAD = "TP_VEHICULOS_REPOSTAJE_MIN_PRECIO_UNIDAD"; // Valor que representa el precio unitario mínimo que se puede registrar
        public const string TP_VEHICULOS_REPOSTAJE_MAX_PRECIO_UNIDAD = "TP_VEHICULOS_REPOSTAJEhttp://schemas.datacontract.org/2004/07/GeoDroid.Data.Models_MAX_PRECIO_UNIDAD"; // Valor que representa el precio unitario máximo que se puede registrar
        public const string TP_ARQUEO_INICIALIZAR_A_VALOR_ESTABLECIDO = "TP_ARQUEO_INICIALIZAR_A_VALOR_ESTABLECIDO"; // Establecer para nuevas recaudaciones el importe de arqueo establecido
        public const string TP_ESTABLECIMIENTOS_TECNAUSACOUNTERS = "TP_ESTABLECIMIENTOS_TECNAUSACOUNTERS";
        public const string TP_IMPORTE_LIMITE_EFECTIVO = "TP_IMPORTE_LIMITE_EFECTIVO";
        public const string TP_ESTABLECIMIENTOS_VER_TOTAL_PAGOS_MANUALES = "TP_ESTABLECIMIENTOS_VER_TOTAL_PAGOS_MANUALES"; //Visualizar el importe total de pagos manuales registrados en el establecimiento
        public const string TP_CHECK_ARQUEO_REALIZADO_ENABLED = "TP_CHECK_ARQUEO_REALIZADO_ENABLED";
        public const string TP_CONFIGURACION_APN_ENABLED = "TP_CONFIGURACION_APN_ENABLED";
        public const string TP_COPIAR_BRUTO_DE_BRUTO_TEORICO = "TP_COPIAR_BRUTO_DE_BRUTO_TEORICO";
        public const string TP_PERMISO_EDITAR_CONCEPTOS_CALCULO_X_CONTA = "TP_PERMISO_EDITAR_CONCEPTOS_CALCULO_X_CONTA"; //Permitir editar conceptos calculados por contadores, como son los conceptos de TITO o los pagos manuales

        [PrimaryKey, Unique, DataMember, AutoIncrement]
        public int id { get; set; }
        [MaxLength(100), DataMember]
        public string codigo { get; set; }
        [MaxLength(100), DataMember]
        public string valor { get; set; }
    }
}
