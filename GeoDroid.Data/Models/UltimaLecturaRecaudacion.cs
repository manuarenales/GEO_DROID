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
    public class UltimaLecturaRecaudacion
    {
        [Key, DataMember]
        public int id { get; set; }
        [DataMember]
        public int idGeo { get; set; }
        [DataMember]
        public long valor { get; set; }
        [DataMember]
        public long ajustePostLectura { get; set; }

        [ForeignKey("Maquina"), DataMember]
        public int? idMaquina { get; set; }
        public virtual Maquina? Maquina { get; set; }

        [ForeignKey("PatContDetalles")]
        [DataMember]
        public int? idPatContDetalles { get; set; }
        public PatContDetalle? PatContDetalles { get; set; }

    }
}
