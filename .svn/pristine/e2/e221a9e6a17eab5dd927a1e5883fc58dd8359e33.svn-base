using SQLite;
using System.Runtime.Serialization;


namespace GeoDroid.Data
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/GeoDroid.Models")]
    public class Ruta
    {
        [PrimaryKey, Unique, DataMember, AutoIncrement]
        public int id { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public bool bloqueada { get; set; }
        [DataMember]
        public int? idEstablecimiento { get; set; }
        [Ignore]
        public virtual Establecimiento? Establecimiento { get; set; }
    }

    [DataContract(Name = "Ruta")]
    class Ruta_V1
    {
        [PrimaryKey, Unique, DataMember]

        public int id { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public int idEstablecimiento { get; set; }

        [Ignore]
        public virtual Establecimiento Establecimiento { get; set; }
    }
}
