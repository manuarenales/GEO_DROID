using SQLite;
using System.Runtime.Serialization;

namespace GeoDroid.Data
{
    public class VehiculoRepostaje
    {
        [PrimaryKey, Unique, DataMember, AutoIncrement]
        public int id { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public int idPersonal { get; set; }
        [DataMember]
        public decimal baseImponible { get { return Math.Round(total / (1 + (pctIva / 100)), 2, MidpointRounding.AwayFromZero); } set { } }
        [DataMember]
        public decimal pctIva { get; set; }
        [DataMember]
        public decimal iva { get { return total - baseImponible; } set { } }
        [DataMember]
        public decimal total { get; set; }
        [DataMember, MaxLength(2000)]
        public string comentarios { get; set; }
        [DataMember]
        public decimal precioUnidadCombustible { get; set; }
        [DataMember]
        public int km { get; set; }
        [DataMember]
        public byte[] foto { get; set; }
        [DataMember]
        public int? idTipoCombustible { get; set; }
        [Ignore]
        public virtual TipoCombustible? TipoCombustible { get; set; }
        [DataMember]
        public int? idEmpresas { get; set; }
        [Ignore]
        public virtual Empresa? Empresa { get; set; }
        [DataMember]
        public int? idVehiculos { get; set; }
        [Ignore]
        public virtual Vehiculo? Vehiculo { get; set; }
    }
}
