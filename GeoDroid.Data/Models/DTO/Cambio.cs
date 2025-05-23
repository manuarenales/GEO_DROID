using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GeoDroid.Data.DTO
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/GeoDroid.Models")]
    public class Cambio
    {
        [Key]
        public int id { get; set; }
        public int idGeo { get; set; }
        public int idPersonalEntrega { get; set; }
        public int idPersonalRecuperacion { get; set; }
        public int idEmpresa { get; set; }
        public DateTime fechaRecuperacion { get; set; }
        public DateTime fechaEntrega { get; set; }
        public decimal importe { get; set; }
        public string comentarios { get; set; }
        public bool bloqueado { get; set; }
        public DateTime fechaModificacion { get; set; }

        //////////////////////////////////////////////////////////
        [ForeignKey("idEstablecimientos")]
        public int? idEstablecimientos { get; set; }
        public virtual Establecimiento? establecimiento { get; set; }
    }
}
