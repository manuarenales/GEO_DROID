using SQLite;
using System.Runtime.Serialization;

namespace GeoDroid.Data
{
    public class RecaudacionDetalles
    {
        private class TipoRecaudacionQuery
        {
            public TipoDistribucionConceptoRecaudacion tipoDisConRec { get; set; }
            public RecaudacionDetalles recaudacionDetalles { get; set; }
            public MaquinaConceptoRecaudacion maquinaConRec { get; set; }
            public Maquina maquina { get; set; }
        };

        [PrimaryKey, Unique, DataMember, AutoIncrement]

        public int id { get; set; }

        [DataMember]
        public int idConceptoRecaudacion { get; set; }
        [DataMember]
        public Decimal importe { get; set; }
        [DataMember]
        public Decimal importeDefecto { get; set; }
        [DataMember]
        //[ForeignKey("Recaudacion"), DataMember]
        public int? idRecaudacion { get; set; }

        [Ignore]
        public virtual Recaudacion? Recaudacion { get; set; }

        public static decimal CalculateDefaultAmmount(DateTime fechaRecaudacionAnterior, MaquinaConceptoRecaudacion maquinaConRec, decimal gross)
        {
            decimal importePorDefecto = 0;

            DateTime today = DateTime.Today;
            DateTime fu = fechaRecaudacionAnterior;
            DateTime prevToday = new DateTime(fu.Year, fu.Month, fu.Day);
            int days = (today - prevToday).Days;
            int weeks = 2; // GEO_DROID.Services.DateTimeUtils.GetWeeksBetween(prevToday, today, DayOfWeek.Monday);

            if (maquinaConRec != null)
                importePorDefecto = maquinaConRec.fijo + (maquinaConRec.diario * days) + (maquinaConRec.semanal * weeks) + (gross * (maquinaConRec.pct_d / 100.0m));

            return importePorDefecto;
        }
    }
}
