using SQLite;
using System.Runtime.Serialization;



namespace GeoDroid.Data
{
    public class PatronContador
    {
        [PrimaryKey, Unique, DataMember, AutoIncrement]
        public int id { get; set; }
        [Ignore]
        public virtual ICollection<PatronContadorDetalle> PatronesContadoresDetalle { get; set; }
    }
}
