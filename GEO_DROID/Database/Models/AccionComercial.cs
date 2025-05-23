using SQLite;

using System.Runtime.Serialization;

namespace GeoDroid.Data
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/GeoDroid.Models")]
    public class AccionComercial
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

        [MaxLength(2000), DataMember]
        public string comentarios { get; set; }
        [DataMember]
        public int? idEstablecimiento { get; set; }

        [Ignore]
        public virtual Establecimiento? Establecimiento { get; set; }
        [DataMember]
        public int? idTipoAccionComercial { get; set; }

        [Ignore]
        public virtual TipoAccionComercial? TipoAccionComercial { get; set; }
        [DataMember]
        public int? idMotivoAccionComercial { get; set; }

        [Ignore]
        public virtual MotivoAccionComercial? MotivoAccionComercial { get; set; }
    }
}
