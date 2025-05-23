using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GeoDroid.Data
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/GeoDroid.Models")]
    public class MotivoAccionComercial
    {
        [Key, DataMember]
        public int id { get; set; }
        [DataMember]
        public int idGeo { get; set; }
        [DataMember, MaxLength(10)]
        public string codigo { get; set; }
        [DataMember, MaxLength(50)]
        public string descripcion { get; set; }

        [JsonIgnore]
        public virtual ICollection<AccionComercial>? AccionesComerciales { get; set; }
    }
}
