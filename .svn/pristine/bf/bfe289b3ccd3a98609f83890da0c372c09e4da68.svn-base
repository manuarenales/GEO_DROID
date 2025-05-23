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
    public class PrestamoRecuperacion
    {
        [Key, DataMember]
        public int id { get; set; }
        [DataMember]
        public int idGeo { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public int idPersonalRecuperacion { get; set; }
        [DataMember]
        public decimal importe { get; set; }
        [DataMember]
        public string comentarios { get; set; }
        [DataMember]
        public int idRuta { get; set; }
        [DataMember]
        public int idCarga { get; set; }

        [ForeignKey("Prestamo"), DataMember]
        public int? idPrestamos { get; set; }
        public virtual Prestamo? Prestamo { get; set; }
    }
}
