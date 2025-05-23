using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace GeoDroid.Data
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/GeoDroid.Models")]
    public class Carga
    {
        [Key, DataMember]
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


        [ForeignKey("Incidencia"), DataMember]
        public int? idIncidencia { get; set; }
        public virtual Incidencia? Incidencia { get; set; }

        [ForeignKey("Averia"), DataMember]
        public int? idAverias { get; set; }
        public virtual Averia? Averia { get; set; }

    }
}
