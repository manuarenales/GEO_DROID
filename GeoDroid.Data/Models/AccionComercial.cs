using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace GeoDroid.Data
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/GeoDroid.Models")]
    public class AccionComercial
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

        [MaxLength(2000), DataMember]
        public string comentarios { get; set; }

        [ForeignKey("Establecimiento"), DataMember]
        public int? idEstablecimiento { get; set; }
        public virtual Establecimiento? Establecimiento { get; set; }

        [ForeignKey("TipoAccionComercial"), DataMember]
        public int? idTipoAccionComercial { get; set; }
        public virtual TipoAccionComercial? TipoAccionComercial { get; set; }

        [ForeignKey("MotivoAccionComercial"), DataMember]
        public int? idMotivoAccionComercial { get; set; }
        public virtual MotivoAccionComercial? MotivoAccionComercial { get; set; }
    }
}
