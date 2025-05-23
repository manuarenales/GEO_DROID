using SQLite;
using System.Runtime.Serialization;

namespace GeoDroid.Data
{
    public class MotivoMaquinaNoRecaudada
    {
        [PrimaryKey, Unique, DataMember, AutoIncrement]

        public int id { get; set; }
        [DataMember]
        public int idGeo { get; set; }
        [DataMember, MaxLength(2000)]
        public string comentarios { get; set; }
        [DataMember]
        //[ForeignKey("Incidencia"), DataMember]
        public int? idIncidencia { get; set; }
        [Ignore]
        public virtual Incidencia? Incidencia { get; set; }
        [DataMember]
        //[ForeignKey("ConceptoMotivoMaquinaNoRecaudada"), DataMember]
        public int? idConceptoMotivoMaquinaNoRecaudada { get; set; }
        [Ignore] 
        public virtual ConceptoMotivoMaquinaNoRecaudada? ConceptoMotivoMaquinaNoRecaudada { get; set; }
    }
}
