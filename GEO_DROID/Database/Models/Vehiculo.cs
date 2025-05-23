using SQLite;
using System.Runtime.Serialization;

namespace GeoDroid.Data
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/GeoDroid.Models")]
    public class Vehiculo
    {
        [PrimaryKey, Unique, DataMember, AutoIncrement]
        public int id { get; set; }
        [DataMember]
        public int idGeo { get; set; }
        [DataMember]
        public string codigo { get; set; }
        [DataMember]
        public string matricula { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public int idEmpresa { get; set; }
        [DataMember]
        public int maxKm { get; set; }
        public string TextoVehiculo
        {
            get
            {
                return (string.IsNullOrEmpty(matricula) ? "" : matricula + " ") + descripcion;
            }
        }
    }
}
