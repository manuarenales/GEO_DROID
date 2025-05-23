using SQLite;
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

        [PrimaryKey, Unique, DataMember, AutoIncrement]
        public int id { get; set; }

        [DataMember]
        public int tipoLecturaContadores { get; set; }
        [DataMember]
        public int valor { get; set; }
        [DataMember]
        //[ForeignKey("Incidencia"), DataMember]
        public int? idIncidencias { get; set; }
        //public virtual Incidencia? Incidencia { get; set; }
    }
}
