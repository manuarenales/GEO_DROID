using SQLite;
using System.Runtime.Serialization;

namespace GeoDroid.Data
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/GeoDroid.Models")]
    public class DesgloseBruto
    {
        [PrimaryKey, Unique, DataMember, AutoIncrement]
        public int id { get; set; }
        [DataMember]
        public int idRecaudacion { get; set; }
        [DataMember]
        public int eur_50 { get; set; }
        [DataMember]
        public int eur_20 { get; set; }
        [DataMember]
        public int eur_10 { get; set; }
        [DataMember]
        public int eur_5 { get; set; }
        [DataMember]
        public int eur_2 { get; set; }
        [DataMember]
        public int eur_1 { get; set; }
        [DataMember]
        public int cent_50 { get; set; }
        [DataMember]
        public int cent_20 { get; set; }
        [DataMember]
        public int cent_10 { get; set; }
        [DataMember]
        public int cent_5 { get; set; }
        [DataMember]
        public int cent_2 { get; set; }
        [DataMember]
        public int cent_1 { get; set; }

        public static bool IsNullOrEmpty(DesgloseBruto desglose)
        {
            return desglose == null
                || (desglose.eur_50 == 0 && desglose.eur_20 == 0 && desglose.eur_10 == 0 && desglose.eur_5 == 0 && desglose.eur_2 == 0 && desglose.eur_1 == 0
                    && desglose.cent_50 == 0 && desglose.cent_20 == 0 && desglose.cent_10 == 0 && desglose.cent_5 == 0 && desglose.cent_2 == 0 && desglose.cent_1 == 0);
        }
    }
}
