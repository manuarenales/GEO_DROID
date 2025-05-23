
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;


namespace GeoDroid.Data
{

    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/GeoDroid.Models")]
    public class Localizacion
    {
        [Key, DataMember]
        public int id { get; set; }
        [DataMember]
        public int idTerminal { get; set; }
        [DataMember, MaxLength(50)]
        public string fechaHora { get; set; }
        [DataMember]
        public double latitud { get; set; }
        [DataMember]
        public double longitud { get; set; }
        [DataMember]
        public int estado { get; set; }  // AB, A = 1 Servicio, 2 Geodroid  B = 0 Vacio, 1 Wifi y 3G (Network), 2 GPS
    }
}
