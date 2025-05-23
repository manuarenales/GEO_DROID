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
    public class VehiculoKm
    {
        public enum Tipo { SALIDA = 0, LLEGADA = 1, OTROS = 2 };
        [Key, DataMember]
        public int id { get; set; }
        [DataMember]
        public int idGeo { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public int km { get; set; }
        [DataMember]
        public int tipo { get; set; }

        [ForeignKey("Vehiculos"), DataMember]
        public int? idVehiculos { get; set; }
        public virtual Vehiculo? Vehiculos { get; set; }
    }
}
