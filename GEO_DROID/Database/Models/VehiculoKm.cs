﻿using GEO_DROID.Services.SincroService;
using SQLite;
using System.Runtime.Serialization;

namespace GeoDroid.Data
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/GeoDroid.Models")]

    public class VehiculoKm
    {
        public enum Tipo { SALIDA = 0, LLEGADA = 1, OTROS = 2 };

        [DataMember, PrimaryKey, AutoIncrement]
        public int id { get; set; }

        /// <summary>
        /// 0 when new insertion in mobile device
        /// </summary>
        [DataMember]
        public int idGeo { get; set; }
        [DataMember, ForeingKeyIdGeo(typeof(Vehiculo))]
        public int idVehiculos { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public int km { get; set; }
        [DataMember]
        public int tipo { get; set; }


        //-----------------------------------------------------------------------------------------------------------------------//
        [Ignore]
        public virtual Vehiculo? Vehiculos { get; set; }
    }
}
