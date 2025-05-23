using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GeoDroid.Data
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/GeoDroid.Models")]
    public class Poblacion
    {
        [Key, DataMember]
        public int id { get; set; }
        [DataMember]
        public int idGeo { get; set; }
        [DataMember]
        public string nombre { get; set; }
        public string TextoPoblacion
        {
            get
            {
                return nombre;
            }
        }
    }
}
