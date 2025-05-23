using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;


namespace GeoDroid.Data
{
    public class LecturaContadorAuto
    {
        public enum Tipo
        {
            Manual = 0,
            Cable = 1,
            Bluetooth = 2
        };
        [Key, DataMember]
        public int id { get; set; }

        [DataMember]
        public int tipoLecturaContadores { get; set; }
        [DataMember]
        public int valor { get; set; }

        [ForeignKey("Incidencia"), DataMember]
        public int? idIncidencias { get; set; }
        public virtual Incidencia? Incidencia { get; set; }
    }
}
