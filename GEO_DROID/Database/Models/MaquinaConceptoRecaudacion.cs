using SQLite;
using System.Runtime.Serialization;

namespace GeoDroid.Data
{

    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/GeoDroid.Models")]
    public class MaquinaConceptoRecaudacion
    {
        [PrimaryKey, Unique, DataMember, AutoIncrement]

        public int id { get; set; }
        [DataMember]
        public int idConceptoRecaudacionGeo { get; set; }
        [DataMember]
        public decimal fijo { get; set; }
        [DataMember]
        public decimal diario { get; set; }
        [DataMember]
        public decimal semanal { get; set; }
        [DataMember]
        public int pct { get; set; }
        [DataMember]
        public bool acumular { get; set; }
        [DataMember]
        public decimal pendiente { get; set; }
        [DataMember]
        //[ForeignKey("Maquina"), DataMember]
        public int? idMaquina { get; set; }

        [Ignore]
        public virtual Maquina? Maquina { get; set; }

        public decimal pct_d
        {
            get
            {
                decimal d = pct;
                if (d > 100)
                    d = d / 10;
                return d;
            }
        }
    }
}
