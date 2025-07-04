﻿using SQLite;
using System.Runtime.Serialization;


namespace GeoDroid.Data
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/GeoDroid.Models")]

    public class LecturaContadorAuto
    {
        public enum Tipo
        {
            Manual = 0,
            Cable = 1,
            Bluetooth = 2
        };

        [DataMember, PrimaryKey, AutoIncrement]
        public int id { get; set; }
        [DataMember]
        public int idIncidencias { get; set; }
        [DataMember]
        public int tipoLecturaContadores { get; set; }
        [DataMember]
        public int valor { get; set; }
    }
}
