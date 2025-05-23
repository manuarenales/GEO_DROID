
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace GeoDroid.Data
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/GeoDroid.Models")]
    public class AveriaEstado
    {
        [Key, DataMember]
        public int id { get; set; }
        [DataMember]
        public int idGeo { get; set; }
        [DataMember, MaxLength(20)]
        public string descripcion { get; set; }
        [DataMember]
        public int color { get; set; }
        [DataMember]
        public bool fin { get; set; }
    }
}
