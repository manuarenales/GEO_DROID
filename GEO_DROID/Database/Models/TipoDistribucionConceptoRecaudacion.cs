using SQLite;
using System.Runtime.Serialization;

namespace GeoDroid.Data
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/GeoDroid.Models")]

    public class TipoDistribucionConceptoRecaudacion
    {
        public enum TipoConceptoRecaudacion
        {
            Fallos = 1,
            Tasas = 2,
            Otros = 3,
            PagosManuales = 4,
            TicketIn = 5,
            TicketOut = 6
        }

        [PrimaryKey, Unique, DataMember, AutoIncrement]
        public int id { get; set; }
        [DataMember]
        public int idGeo { get; set; }
        [DataMember]
        public int idTipoDistribucion { get; set; }
        [DataMember]
        public int idConceptoRecaudacionGeo { get; set; }
        [DataMember, MaxLength(50)]
        public string concepto { get; set; }
        [DataMember]
        public int pctBruto { get; set; }
        [DataMember]
        public int pctEmpresa { get; set; }
        [DataMember]
        public int pctEstablecimiento { get; set; }
        [DataMember]
        public bool establecimiento { get; set; }
        [DataMember]
        public bool facturable { get; set; }
        [DataMember]
        public bool esIngreso { get; set; }
        [DataMember]
        public int tipo { get; set; }
        [DataMember]
        public int orden { get; set; }
        public struct TConceptData
        {
            public int pctBruto { get; set; }
            public int pctEmpresa { get; set; }
            public int pctEstablecimiento { get; set; }
            public decimal importe { get; set; }
            //This concept will be for establishment or firm?
            public bool establecimiento { get; set; }
            public int tipo { get; set; }
        }
        public static bool IsTipoTicket(int tipo)
        {
            return (tipo == (int)TipoConceptoRecaudacion.TicketIn || tipo == (int)TipoConceptoRecaudacion.TicketOut);
        }
        public static void CalcularDistribucionConceptos(
                List<TConceptData> conceptos,
                int pctEstablecimiento,
                decimal bruto,
                decimal iva,
                decimal rendondeoValor,
                bool redondeoParaEmpresa,
                out decimal netoEmpresa,
                out decimal netoEstablecimiento)
        {
            // Al calcular la distribución de los netos hay que tener en cuenta
            // que los conceptos a aplicar en el cálculo tengan repercusión sobre 
            // el resultado (o sea, que tengan algún porcentaje estblecido sobre el 
            // bruto, el establecimiento o la empresa)
            //

            decimal establecimiento = 0;
            decimal empresa = 0;

            decimal conceptosBruto = 0;
            decimal conceptosEmpresa = 0;
            decimal conceptosEstablecimiento = 0;

            decimal conceptosDestinoEmpresa = 0;
            decimal conceptosDestinoEstablecimiento = 0;

            foreach (TConceptData data in conceptos)
            {
                int pct = data.pctBruto + data.pctEmpresa + data.pctEstablecimiento;
                if (pct > 0)
                {
                    if (data.tipo == (int)TipoConceptoRecaudacion.TicketIn)
                        conceptosBruto -= data.importe * data.pctBruto / pct;
                    else
                        conceptosBruto += data.importe * data.pctBruto / pct;
                    conceptosEmpresa += data.importe * data.pctEmpresa / pct;
                    conceptosEstablecimiento += data.importe * data.pctEstablecimiento / pct;
                }
                // Debemos comprobar si afectará al establecimiento o a la empresa
                if (data.establecimiento)
                    conceptosDestinoEstablecimiento += data.importe;
                else
                    conceptosDestinoEmpresa += data.importe;
            }

            establecimiento = ((bruto - conceptosBruto) * pctEstablecimiento / 100) - conceptosEstablecimiento;
            empresa = (bruto - conceptosBruto) - ((bruto - conceptosBruto) * pctEstablecimiento / 100) - conceptosEmpresa;

            /// Si no tenemos redondeo configurado aplicamos redondeo al mínimo importe de la 
            /// moneda del sistema. No tiene sentido obtener un importe no redondeado, ya que
            /// no tendremos monedas con las que dar ese importe al establecimiento
            if (rendondeoValor <= 0)
            {
                try
                {
                    /// Obtenemos el número de decimales a usar
                    int decimales = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalDigits;

                    /// Generamos el valor en función de los decimales
                    decimal rv = 1M;
                    for (int i = 0; i < decimales; i++)
                        rv = rv / 10M;

                    // Asignamos los valores
                    redondeoParaEmpresa = true;
                    rendondeoValor = rv;
                }
                catch { } // Si hay problemas seguimos como si no hubiera pasado nada, ya que si falla lo que hacemos no podemos realizar el cálculo de otra forma
            }

            if (rendondeoValor > 0)
            {
                // Tenemos que sumar/restar al importe del establecimiento
                // la parte que nos permita redondear el valor con respecto 
                // al valor de redondeo.
                decimal factorDecimal = establecimiento / rendondeoValor;
                decimal factorEntero = 0;
                if (redondeoParaEmpresa)
                {
                    factorEntero = decimal.Floor(factorDecimal);
                }
                else
                {
#if PocketPC
                    factorEntero = (decimal)Math.Ceiling((double)factorDecimal);
#else
                    factorEntero = decimal.Ceiling(factorDecimal);
#endif
                }
                decimal factor = (factorDecimal - factorEntero) * rendondeoValor;

                establecimiento -= factor;
                empresa += factor;
            }

            // ---

            netoEstablecimiento = establecimiento;
            netoEmpresa = empresa;
        }

    }
}
