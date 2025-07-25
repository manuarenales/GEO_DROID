﻿using GEO_DROID.Services.SincroService;
using SQLite;
using System.Runtime.Serialization;


namespace GeoDroid.Data
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/GeoDroid.Models")]
    public class LecturaDetalle
    {
        [PrimaryKey, Unique, DataMember, AutoIncrement]
        public int id { get; set; }
        [DataMember]
        public int? idGeo { get; set; }
        [DataMember]
        public Int64 valor { get; set; }
        [DataMember]
        public Int64 valorAntes { get; set; }
        [DataMember]
        public bool tieneAjuste { get; set; }
        [DataMember, ForeingKeyIdGeo(typeof(LecturaContador))]
        public int? idLecturaContadores { get; set; }
        [DataMember]
        public int? idPatContDetalles { get; set; }

    }
}
