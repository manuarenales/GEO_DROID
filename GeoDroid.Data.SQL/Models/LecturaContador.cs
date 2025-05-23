
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;


namespace GeoDroid.Data
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/GeoDroid.Models")]
    public class LecturaContador
    {
        [Key, DataMember]
        public int id { get; set; }
        [DataMember]
        public int idGeo { get; set; }

        [ForeignKey("incidencia"), DataMember]
        public int? idIncidencias { get; set; }
        public virtual Incidencia? incidencia { get; set; }
    }
}
