using SQLite;
using System.Runtime.Serialization;

namespace GeoDroid.Data
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/GeoDroid.Models")]
    public class TipoCombustible
    {
        [PrimaryKey, Unique, DataMember, AutoIncrement]

        public int id { get; set; }
        [DataMember]
        public int idGeo { get; set; }
        [DataMember, MaxLength(10)]
        public string codigo { get; set; }
        [DataMember, MaxLength(50)]
        public string descripcion { get; set; }
    }
}
