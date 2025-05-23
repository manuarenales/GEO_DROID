using SQLite;
using System.Runtime.Serialization;

namespace GeoDroid.Data
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/GeoDroid.Models")]
    public class UltimaLecturaRecaudacion
    {
        [PrimaryKey, Unique, DataMember, AutoIncrement]
        public int id { get; set; }
        [DataMember]
        public int idGeo { get; set; }
        [DataMember]
        public long valor { get; set; }
        [DataMember]
        public long ajustePostLectura { get; set; }
        [DataMember]
        public int? idMaquina { get; set; }
        [Ignore]
        public virtual Maquina? Maquina { get; set; }
        [DataMember]
        public int? idPatContDetalles { get; set; }
        [Ignore]
        public PatContDetalle? PatContDetalles { get; set; }

    }
}
