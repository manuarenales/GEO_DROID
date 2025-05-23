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
    public class VehiculoCombustible
    {
        [Key, DataMember]
        public int id { get; set; }

        [ForeignKey("Vehiculos"), DataMember]
        public int? idVehiculos { get; set; }
        public virtual Vehiculo? Vehiculos { get; set; }

        [ForeignKey("TipoCombustible"), DataMember]
        public int? idTipoCombustible { get; set; }
        public virtual TipoCombustible? TipoCombustible { get; set; }
    }
}
