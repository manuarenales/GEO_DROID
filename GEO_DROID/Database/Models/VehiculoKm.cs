using SQLite;
using System.Runtime.Serialization;

namespace GeoDroid.Data
{
    public class VehiculoKm
    {
        public enum Tipo { SALIDA = 0, LLEGADA = 1, OTROS = 2 };

        [PrimaryKey, Unique, DataMember, AutoIncrement]
        public int id { get; set; }
        [DataMember]
        public int idGeo { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public int km { get; set; }
        [DataMember]
        public int tipo { get; set; }
        [DataMember]
        public int? idVehiculos { get; set; }

        [Ignore]
        public virtual Vehiculo? Vehiculos { get; set; }
    }
}
