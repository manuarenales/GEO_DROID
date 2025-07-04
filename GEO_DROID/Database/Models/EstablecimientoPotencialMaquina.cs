﻿using SQLite;
using System.Runtime.Serialization;

namespace GeoDroid.Data
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/GeoDroid.Models")]
    public class EstablecimientoPotencialMaquina
    {
        [PrimaryKey, Unique, DataMember, AutoIncrement]
        public int id { get; set; }
        [DataMember]
        public int idGeo { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public DateTime fechaPermiso { get; set; }
        [DataMember]
        public DateTime fechaRenovacion { get; set; }
        //[ForeignKey("EmpresaCompetidora"), DataMember]
        public int? idEmpresaCompetidora { get; set; }
        //[ForeignKey("EstablecimientoPotencial"), DataMember]
        public int? idEstablecimientoPotencial { get; set; }

        //[ForeignKey("Modelo"), DataMember]
        public int? idModelo { get; set; }



        //-----------------------------------------------------------------------------------------------------------------------//
        [Ignore]
        public virtual EstablecimientoPotencial? EstablecimientoPotencial { get; set; }
        [Ignore]
        public EmpresaCompetidora? EmpresaCompetidora { get; set; }
        [Ignore]
        public virtual ModeloMaquina? Modelo { get; set; }

    }
}
