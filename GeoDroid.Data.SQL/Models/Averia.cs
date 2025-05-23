using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;


namespace GeoDroid.Data
{

    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/GeoDroid.Models")]
    public class Averia
    {
        public Averia()
        {
            observaciones = "";
        }

        [DataMember, Key]
        public int id { get; set; }
        [DataMember]
        public int? idGeo { get; set; }
        [DataMember]
        public int? idPersonal { get; set; }
        [DataMember]
        public string? observaciones { get; set; }
        [DataMember]
        public string? detalle { get; set; }
        [DataMember]
        public string? comentarios { get; set; }
        [DataMember]
        public Nullable<DateTime> fechaFin { get; set; }
        [DataMember]
        public Nullable<DateTime> fechaModificacion { get; set; }
        [DataMember]
        public byte[]? foto { get; set; }
        [DataMember]
        public string? estado { get; set; }

        [ForeignKey("ConceptoAveria"), DataMember]
        public int? idConceptos { get; set; }
        public virtual ConceptoAveria? ConceptoAveria { get; set; }

        [ForeignKey("Incidencia"), DataMember]
        public int idIncidencias { get; set; }
        public virtual Incidencia? Incidencia { get; set; }

        [ForeignKey("AveriaEstado"), DataMember]
        public int? idAveriaEstados { get; set; }
        public virtual AveriaEstado? AveriaEstado { get; set; }

    }
}

