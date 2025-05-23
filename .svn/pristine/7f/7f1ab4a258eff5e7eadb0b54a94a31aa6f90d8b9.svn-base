using SQLite;

using System.Runtime.Serialization;

namespace GeoDroid.Data
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/GeoDroid.Models")]
    public class Carga
    {
        [PrimaryKey, Unique, DataMember, AutoIncrement]
        public int id { get; set; }
        [DataMember]
        public int idGeo { get; set; }
        [DataMember]
        public decimal cargaEmpresa { get; set; }
        [DataMember]
        public decimal cargaEstablecimiento { get; set; }
        [DataMember]
        public decimal recuperacionEmpresa { get; set; }
        [DataMember]
        public decimal recuperacionEstablecimiento { get; set; }
        [DataMember]
        public decimal ajusteEmpresa { get; set; }
        [DataMember]
        public decimal ajusteEstablecimiento { get; set; }
        [DataMember]
        public decimal saldoEmpresaChanged { get; set; }
        [DataMember]
        public decimal saldoEstablecimientoChanged { get; set; }

        [DataMember]
        public int? idIncidencia { get; set; }
        [Ignore]
        public virtual Incidencia? Incidencia { get; set; }
        [DataMember]
        public int? idAverias { get; set; }
        [Ignore]
        public virtual Averia? Averia { get; set; }

    }
}
