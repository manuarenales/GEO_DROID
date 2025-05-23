using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace GeoDroid.Data
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/GeoDroid.Models")]
    public class ConceptoAveria
    {
        [Key, DataMember]
        public int id { get; set; }
        [DataMember]
        public int idGeo { get; set; }
        [MaxLength(50), DataMember]
        public string descripcion { get; set; }
    }
}
