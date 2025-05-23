
using SQLite;
using System.Runtime.Serialization;

namespace GeoDroid.Data
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/GeoDroid.Models")]
    public class AveriaEstado
    {
        [PrimaryKey, Unique, DataMember, AutoIncrement]
        public int id { get; set; }
        [DataMember]
        public int idGeo { get; set; }
        [MaxLength(20), DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public int color { get; set; }
        [DataMember]
        public bool fin { get; set; }
    }
}
