using GeoDroid.Data;
using GeoDroid.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Geodroid.Data
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

        [Key, DataMember]
        public int id { get; set; }

        [DataMember]
        public int idConceptoRecaudacion { get; set; }
        [DataMember]
        public Decimal importe { get; set; }
        [DataMember]
        public Decimal importeDefecto { get; set; }

        [ForeignKey("Recaudacion"), DataMember]
        public int? idRecaudacion { get; set; }
        public virtual Recaudacion? Recaudacion { get; set; }

        public static decimal CalculateDefaultAmmount(DateTime fechaRecaudacionAnterior, MaquinaConceptoRecaudacion maquinaConRec, decimal gross)
        {
            decimal importePorDefecto = 0;

            DateTime today = DateTime.Today;
            DateTime fu = fechaRecaudacionAnterior;
            DateTime prevToday = new DateTime(fu.Year, fu.Month, fu.Day);
            int days = (today - prevToday).Days;
            int weeks = GEO_DROID.Services.DateTimeUtils.GetWeeksBetween(prevToday, today, DayOfWeek.Monday);

            if (maquinaConRec != null)
                importePorDefecto = maquinaConRec.fijo + (maquinaConRec.diario * days) + (maquinaConRec.semanal * weeks) + (gross * (maquinaConRec.pct_d / 100.0m));

            return importePorDefecto;
        }
    }
}
