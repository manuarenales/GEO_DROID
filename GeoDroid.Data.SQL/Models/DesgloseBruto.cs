using Microsoft.EntityFrameworkCore;
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
    public class DesgloseBruto
    {
        [Key, DataMember]
        public int id { get; set; }
        public int idRecaudacion { get; set; }
        public int eur_50 { get; set; }
        public int eur_20 { get; set; }
        public int eur_10 { get; set; }
        public int eur_5 { get; set; }
        public int eur_2 { get; set; }
        public int eur_1 { get; set; }
        public int cent_50 { get; set; }
        public int cent_20 { get; set; }
        public int cent_10 { get; set; }
        public int cent_5 { get; set; }
        public int cent_2 { get; set; }
        public int cent_1 { get; set; }

        public static bool IsNullOrEmpty(DesgloseBruto desglose)
        {
            return desglose == null
                || (desglose.eur_50 == 0 && desglose.eur_20 == 0 && desglose.eur_10 == 0 && desglose.eur_5 == 0 && desglose.eur_2 == 0 && desglose.eur_1 == 0
                    && desglose.cent_50 == 0 && desglose.cent_20 == 0 && desglose.cent_10 == 0 && desglose.cent_5 == 0 && desglose.cent_2 == 0 && desglose.cent_1 == 0);
        }
    }
}
