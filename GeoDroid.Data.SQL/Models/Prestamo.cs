using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GeoDroid.Data
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/GeoDroid.Models")]
    public class Prestamo
    {
        [Key]
        public int id { get; set; }
        [DataMember]
        public int idGeo { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public int idPersonalEntrega { get; set; }
        [DataMember]
        public decimal importe { get; set; }
        [DataMember]
        public decimal saldo { get; set; }
        [DataMember]
        public decimal importePorRecuperacion { get; set; }
        [DataMember]
        public int pctPorRecuperacion { get; set; }
        [DataMember]
        public DateTime fechaUltimaRecuperacion { get; set; }

        [ForeignKey("Empresa"), DataMember]
        public int? idEmpresa { get; set; }
        public virtual Empresa? Empresa { get; set; }

        [ForeignKey("Establecimiento"), DataMember]
        public int? idEstablecimiento { get; set; }
        public virtual Establecimiento? Establecimiento { get; set; }

        [ForeignKey("ConceptoPrestamo"), DataMember]
        public int? idConceptoPrestamo { get; set; }
        public virtual ConceptoPrestamo? ConceptoPrestamo { get; set; }

        [ForeignKey("Maquina"), DataMember]
        public int? idMaquina { get; set; }
        public virtual Maquina? Maquina { get; set; }
    }
}
