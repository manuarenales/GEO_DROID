using SQLite;
using System.Runtime.Serialization;


namespace GeoDroid.Data
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/GeoDroid.Models")]
    public class VisitaComercial
    {
        [PrimaryKey, Unique, DataMember, AutoIncrement]
        public int id { get; set; }
        [DataMember]
        public int idGeo { get; set; }
        [DataMember]
        public int idPersonal { get; set; }
        [DataMember]
        public string personaContacto { get; set; }
        [DataMember]
        public DateTime fechaInicio { get; set; }
        [DataMember]
        public DateTime fechaFin { get; set; }
        [DataMember]
        public int valoracion { get; set; }
        [DataMember, MaxLength(2000)]
        public string comentarios { get; set; }
        [DataMember]
        //[ForeignKey("Establecimiento"), DataMember]
        public int? idEstablecimiento { get; set; }
        [Ignore]
        public virtual Establecimiento? Establecimiento { get; set; }
        [DataMember]
        //[ForeignKey("MotivoVisitaComercial"), DataMember]
        public int? idMotivoVisitaComercial { get; set; }
        [Ignore]
        public virtual MotivoVisitaComercial? MotivoVisitaComercial { get; set; }
    }
}
