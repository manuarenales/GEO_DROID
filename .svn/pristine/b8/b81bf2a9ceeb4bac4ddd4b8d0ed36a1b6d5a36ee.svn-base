
using GeoDroid.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;


namespace GeoDroid.Data
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/GeoDroid.Models")]
    public class Maquina
    {
        [Key, DataMember]
        public int id { get; set; }
        [DataMember]
        public int idGeo { get; set; }
        [DataMember]
        public string? codigo { get; set; }
        [DataMember]
        public int? pctEstablecimiento { get; set; }
        [DataMember]
        public DateTime? fechaUltimoArqueo { get; set; }
        [DataMember]
        public decimal? ultimoArqueo { get; set; }
        [DataMember]
        public decimal? arqueoEstablecido { get; set; }
        [DataMember]
        public decimal? diferenciaArqueo { get; set; }
        [DataMember]
        public decimal? redondeoValor { get; set; }
        [DataMember]
        public bool? redondeoParaEmpresa { get; set; }
        [DataMember]
        public DateTime? fechaUltimaRecaudacion { get; set; }
        [DataMember]
        public decimal? empresaSaldo { get; set; }
        [DataMember]
        public decimal? establecimientoSaldo { get; set; }
        [DataMember]
        public int? protocolosMaquinas { get; set; }
        [DataMember]
        public int? idInstalaciones { get; set; }
        [DataMember]
        public int? idPatronContadores { get; set; }
        [DataMember]
        public PatronContador? patronContador { get; set; }
        [DataMember]
        public DateTime? fechaInstalacion { get; set; }
        [DataMember]
        public int? idTipoDistribucion { get; set; }
        [MaxLength(50), DataMember]
        public string? descripcionDistribucion { get; set; }
        [MaxLength(10), DataMember]
        public string? modeloMaquina { get; set; }
        [MaxLength(50), DataMember]
        public string? password { get; set; }
        [MaxLength(20), DataMember]
        public string? permiso { get; set; }

        [ForeignKey("Empresa"), DataMember]
        public int? Empresaid { get; set; }
        public virtual Empresa? Empresa { get; set; }

        [ForeignKey("establecimiento"), DataMember]
        public int? idEstablecimiento { get; set; }
        public virtual Establecimiento? establecimiento { get; set; }

        [JsonIgnore, NotMapped]
        public virtual ICollection<Averia>? Averias { get; set; }

    }
}
