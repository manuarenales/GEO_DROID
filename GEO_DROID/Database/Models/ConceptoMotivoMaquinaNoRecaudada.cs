using SQLite;
using System.Runtime.Serialization;

namespace GeoDroid.Data
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/GeoDroid.Models")]
    public class ConceptoMotivoMaquinaNoRecaudada
    {
        [PrimaryKey, Unique, DataMember, AutoIncrement]
        public int id { get; set; }
        [DataMember]
        public int idGeo { get; set; }
        [MaxLength(10), DataMember]
        public string codigo { get; set; }
        [MaxLength(50), DataMember]
        public string descripcion { get; set; }

    }
}
