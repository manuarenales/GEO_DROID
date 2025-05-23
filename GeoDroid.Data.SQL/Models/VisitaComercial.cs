using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;


namespace GeoDroid.Data
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/GeoDroid.Models")]
    public class VisitaComercial
    {
        [Key, DataMember]
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

        [ForeignKey("Establecimiento"), DataMember]
        public int? idEstablecimiento { get; set; }
        public virtual Establecimiento? Establecimiento { get; set; }

        [ForeignKey("MotivoVisitaComercial"), DataMember]
        public int? idMotivoVisitaComercial { get; set; }
        public virtual MotivoVisitaComercial? MotivoVisitaComercial { get; set; }
    }
}
