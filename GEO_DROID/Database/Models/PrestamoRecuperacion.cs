using SQLite;
using System.Runtime.Serialization;

namespace GeoDroid.Data
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/GeoDroid.Models")]
    public class PrestamoRecuperacion
    {
        [PrimaryKey, Unique, DataMember, AutoIncrement]

        public int id { get; set; }
        [DataMember]
        public int idGeo { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public int idPersonalRecuperacion { get; set; }
        [DataMember]
        public decimal importe { get; set; }
        [DataMember]
        public string comentarios { get; set; }
        [DataMember]
        public int idRuta { get; set; }
        [DataMember]
        public int idCarga { get; set; }
        [DataMember]
        //[ForeignKey("Prestamo"), DataMember]
        public int? idPrestamos { get; set; }
        [Ignore]
        public virtual Prestamo? Prestamo { get; set; }
    }
}
