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
    public class Instalacion
    {
        [Key, DataMember]
        public int id { get; set; }
        [DataMember]
        public int idGeo { get; set; }
        [DataMember]
        public DateTime fechaInstalacion { get; set; }
        [DataMember]
        public DateTime fechaInstalacionPrevista { get; set; }
        [DataMember]
        public DateTime fechaTransporteGet { get; set; }
        [DataMember]
        public DateTime fechaTransportePut { get; set; }
        [DataMember]
        public DateTime fechaModificacion { get; set; }
        [DataMember]
        public Decimal arqueo { get; set; }
        [DataMember]
        public Decimal arqueoEstablecido { get; set; }
        [MaxLength(2000), DataMember]
        public string comentarios { get; set; }

        [ForeignKey("Incidencia"), DataMember]
        public int idIncidencia { get; set; }
        public virtual Incidencia Incidencia { get; set; }

        [ForeignKey("Maquina"), DataMember]
        public int idMaquinas { get; set; }
        public virtual Maquina Maquina { get; set; }

        [ForeignKey("EstablecimientoOrigen"), DataMember]
        public int? idEstablecimientoOrigen { get; set; }
        public virtual Establecimiento? EstablecimientoOrigen { get; set; }

        [ForeignKey("EstablecimientoDestino"), DataMember]
        public int? idEstablecimientoDestino { get; set; }
        public virtual Establecimiento? EstablecimientoDestino { get; set; }

    }
}
