using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;


namespace GeoDroid.Data
{
    public class PatronContador
    {
        [Key, DataMember]
        public int id { get; set; }
        [JsonIgnore]
        public virtual ICollection<PatronContadorDetalle> PatronesContadoresDetalle { get; set; }



    }
}
