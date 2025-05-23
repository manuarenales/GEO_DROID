using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace GeoDroid.Data
{

    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/GeoDroid.Models")]
    public class ModuloCaptura
    {
        [Key, DataMember]
        public int id { get; set; }
        [DataMember]
        public int idGeo { get; set; }
        [DataMember]
        public string mac { get; set; }
        [DataMember]
        public string imei { get; set; }
        [DataMember]
        public string password { get; set; }
        [ForeignKey("maquina"), DataMember]
        public int? idMaquinas { get; set; }
        public virtual Maquina? maquina { get; set; }
    }
}
