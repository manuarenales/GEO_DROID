﻿using GEO_DROID.Services.SincroService;
using GeoDroid.Data.DTO;
using SQLite;
using System.Runtime.Serialization;


namespace GeoDroid.Data
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/GeoDroid.Models")]
    public class Ruta
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember, ForeingKeyIdGeo(typeof(Establecimiento))]
        public int idEstablecimiento { get; set; }
        public bool bloqueada { get; set; }

        //-----------------------------------------------------------------------------------------------------------------------//
        [Ignore]
        public virtual Establecimiento? Establecimiento { get; set; }


        [Ignore]
        public virtual List<Averia> Averia { get; set; }
        [Ignore]
        public virtual List<Cambio> Cambios { get; set; }
        [Ignore]
        public virtual List<Prestamo> Prestamos { get; set; }


    }

    [DataContract(Name = "Ruta")]
    class Ruta_V1
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public DateTime fecha { get; set; }
        [ForeingKeyIdGeo(typeof(Establecimiento))]
        public int idEstablecimiento { get; set; }

        //-----------------------------------------------------------------------------------------------------------------------//
        [Ignore]
        public virtual Establecimiento Establecimiento { get; set; }
    }
}
