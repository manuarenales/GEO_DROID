using SQLite;
using System.Runtime.Serialization;

namespace GeoDroid.Data
{

    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/GeoDroid.Models")]
    public class ModuloCaptura
    {
        [PrimaryKey, Unique, DataMember, AutoIncrement]

        public int id { get; set; }
        [DataMember]
        public int idGeo { get; set; }
        [DataMember]
        public string mac { get; set; }
        [DataMember]
        public string imei { get; set; }
        [DataMember]
        public string password { get; set; }
        [DataMember]
        //[ForeignKey("maquina"), DataMember]
        public int? idMaquinas { get; set; }

        [Ignore]
        public virtual Maquina? maquina { get; set; }
    }
}
