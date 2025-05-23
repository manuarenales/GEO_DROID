using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;


namespace GeoDroid.Data
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/GeoDroid.Models")]
    public class LecturaDetalle
    {
        [Key, DataMember]
        public int id { get; set; }
        [DataMember]
        public int? idGeo { get; set; }
        [DataMember]
        public Int64 valor { get; set; }
        [DataMember]
        public Int64 valorAntes { get; set; }
        [DataMember]
        public bool tieneAjuste { get; set; }

        [ForeignKey("LecturaContador"), DataMember]
        public int? idLecturaContadores { get; set; }
        public virtual LecturaContador? LecturaContador { get; set; }

        [ForeignKey("PatronContadorDetalle"), DataMember]
        public int? idPatContDetalles { get; set; }
        public virtual PatronContadorDetalle? PatronContadorDetalle { get; set; }
    }
}
