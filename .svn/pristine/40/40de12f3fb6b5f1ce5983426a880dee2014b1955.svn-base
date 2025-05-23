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
    public class VehiculoRepostaje
    {
        [Key, DataMember]
        public int id { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
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
        [DataMember]
        public decimal precioUnidadCombustible { get; set; }
        [DataMember]
        public int km { get; set; }
        [DataMember]
        public byte[] foto { get; set; }

        [ForeignKey("TipoCombustible"), DataMember]
        public int? idTipoCombustible { get; set; }
        public virtual TipoCombustible? TipoCombustible { get; set; }

        [ForeignKey("Empresa"), DataMember]
        public int? idEmpresas { get; set; }
        public virtual Empresa? Empresa { get; set; }

        [ForeignKey("Vehiculo"), DataMember]
        public int? idVehiculos { get; set; }
        public virtual Vehiculo? Vehiculo { get; set; }
    }
}
