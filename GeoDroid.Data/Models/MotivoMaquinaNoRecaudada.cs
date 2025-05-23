using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GeoDroid.Data
{
    public class MotivoMaquinaNoRecaudada
    {
        [Key, DataMember]
        public int id { get; set; }
        [DataMember]
        public int idGeo { get; set; }
        [DataMember, MaxLength(2000)]
        public string comentarios { get; set; }

        [ForeignKey("Incidencia"), DataMember]
        public int? idIncidencia { get; set; }
        public virtual Incidencia? Incidencia { get; set; }

        [ForeignKey("ConceptoMotivoMaquinaNoRecaudada"), DataMember]
        public int? idConceptoMotivoMaquinaNoRecaudada { get; set; }
        public virtual ConceptoMotivoMaquinaNoRecaudada? ConceptoMotivoMaquinaNoRecaudada { get; set; }
    }
}
