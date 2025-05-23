using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;


namespace GeoDroid.Data
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/GeoDroid.Models")]
    public class Ruta
    {
        [Key, DataMember]
        public int id { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public bool bloqueada { get; set; }

        [ForeignKey("Establecimiento"), DataMember]
        public int? idEstablecimiento { get; set; }
        public virtual Establecimiento? Establecimiento { get; set; }
    }

    [DataContract(Name = "Ruta")]
    class Ruta_V1
    {
        [Key, DataMember]
        public int id { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }

        [ForeignKey("Establecimiento"), DataMember]
        public int idEstablecimiento { get; set; }
        public virtual Establecimiento Establecimiento { get; set; }
    }
}
