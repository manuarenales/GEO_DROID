using SQLite;
using System.Runtime.Serialization;


namespace GeoDroid.Data
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/GeoDroid.Models")]
    public class ModeloMaquina
    {
        [PrimaryKey, Unique, DataMember, AutoIncrement]

        public int id { get; set; }
        [DataMember]
        public int idGeo { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string TextoModeloMaquina
        {
            get
            {
                return nombre;
            }
        }
    }
}
