﻿using SQLite;
using System.Runtime.Serialization;

namespace GeoDroid.Data
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/GeoDroid.Models")]
    public class VehiculoCombustible
    {
        [PrimaryKey, Unique, DataMember, AutoIncrement]

        public int id { get; set; }

        [DataMember]
        public int? idVehiculos { get; set; }
        [Ignore]
        public virtual Vehiculo? Vehiculos { get; set; }
        [DataMember]

        public int? idTipoCombustible { get; set; }


        //-----------------------------------------------------------------------------------------------------------------------//
        [Ignore]
        public virtual TipoCombustible? TipoCombustible { get; set; }
    }
}
