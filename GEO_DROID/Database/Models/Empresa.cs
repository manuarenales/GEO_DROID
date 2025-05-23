using SQLite;
using System.Runtime.Serialization;

namespace GeoDroid.Data
{

    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/GeoDroid.Models")]
    public class Empresa
    {
        [PrimaryKey, Unique, DataMember, AutoIncrement]
        public int id { get; set; }
        [DataMember]
        public int idGeo { get; set; }
        [DataMember]
        public string codigo { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string poblacion { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public string cif { get; set; }
    }
}
