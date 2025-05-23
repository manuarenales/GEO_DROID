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
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/GeoDroid.Models")]
    public class EstablecimientoPotencialMaquina
    {
        [Key, DataMember]
        public int id { get; set; }
        [DataMember]
        public int idGeo { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public DateTime fechaPermiso { get; set; }
        [DataMember]
        public DateTime fechaRenovacion { get; set; }

        [ForeignKey("EstablecimientoPotencial"), DataMember]
        public int? idEstablecimientoPotencial { get; set; }
        public virtual EstablecimientoPotencial? EstablecimientoPotencial { get; set; }

        [ForeignKey("EmpresaCompetidora"), DataMember]
        public int? idEmpresaCompetidora { get; set; }
        public EmpresaCompetidora? EmpresaCompetidora { get; set; }

        [ForeignKey("Modelo"), DataMember]
        public int? idModelo { get; set; }
        public virtual ModeloMaquina? Modelo { get; set; }

    }
}
