using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;


namespace GeoDroid.Data
{
    public class GastoEstablecimiento
    {
        [Key, DataMember]
        public int id { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember, MaxLength(2000)]
        public string descripcion { get; set; }
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

        [ForeignKey("ConceptoGastoEstablecimiento"), DataMember]
        public int? idConceptoGastoEstablecimiento { get; set; }
        public virtual ConceptoGastoEstablecimiento? ConceptoGastoEstablecimiento { get; set; }

        [ForeignKey("Establecimiento"), DataMember]
        public int? idEstablecimiento { get; set; }
        public virtual Establecimiento? Establecimiento { get; set; }

        [ForeignKey("Empresa"), DataMember]
        public int? idEmpresas { get; set; }
        public virtual Empresa? Empresa { get; set; }
    }
}
