﻿using SQLite;
using System.Runtime.Serialization;

namespace GeoDroid.Data
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/GeoDroid.Models")]
    public class Prestamo
    {
        [PrimaryKey, Unique, DataMember, AutoIncrement]

        public int id { get; set; }
        [DataMember]
        public int idGeo { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public int idPersonalEntrega { get; set; }
        [DataMember]
        public decimal importe { get; set; }
        [DataMember]
        public decimal saldo { get; set; }
        [DataMember]
        public decimal importePorRecuperacion { get; set; }
        [DataMember]
        public int pctPorRecuperacion { get; set; }
        [DataMember]
        public DateTime fechaUltimaRecuperacion { get; set; }
        [DataMember]
        //[ForeignKey("Empresa"), DataMember]
        public int? idEmpresa { get; set; }

        [DataMember]
        //[ForeignKey("Establecimiento"), DataMember]
        public int? idEstablecimiento { get; set; }

        [DataMember]
        //[ForeignKey("ConceptoPrestamo"), DataMember]
        public int? idConceptoPrestamo { get; set; }

        [DataMember]
        //[ForeignKey("Maquina"), DataMember]
        public int? idMaquina { get; set; }


        //-----------------------------------------------------------------------------------------------------------------------//
        [Ignore]
        public virtual Maquina? Maquina { get; set; }
        [Ignore]
        public virtual ConceptoPrestamo? ConceptoPrestamo { get; set; }
        [Ignore]
        public virtual Establecimiento? Establecimiento { get; set; }
        [Ignore]
        public virtual Empresa? Empresa { get; set; }
    }
}
