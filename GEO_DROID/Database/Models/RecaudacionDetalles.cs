﻿using GEO_DROID.Services.SincroService;
using SQLite;
using System.Runtime.Serialization;

namespace GeoDroid.Data
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/GeoDroid.Models")]
    public class RecaudacionDetalles
    {

        private class TipoRecaudacionQuery
        {
            [DataMember]
            public TipoDistribucionConceptoRecaudacion tipoDisConRec { get; set; }
            [DataMember]
            public RecaudacionDetalles recaudacionDetalles { get; set; }
            [DataMember]
            public MaquinaConceptoRecaudacion maquinaConRec { get; set; }
            [DataMember]
            public Maquina maquina { get; set; }
        };

        [DataMember, PrimaryKey, AutoIncrement]
        public int id { get; set; }
        [DataMember, ForeingKeyIdGeo(typeof(Recaudacion), "id", "FK_RecaudacionDetalles_Recaudacion")]
        public int idRecaudacion { get; set; }

        [DataMember]
        public decimal importe { get; set; }
        [DataMember]
        public decimal importeDefecto { get; set; }




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

        //-----------------------------------------------------------------------------------------------------------------------//
        [Ignore]
        public virtual Recaudacion? Recaudacion { get; set; }
    }
}
