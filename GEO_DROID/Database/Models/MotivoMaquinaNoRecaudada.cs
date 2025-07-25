﻿using GEO_DROID.Services.SincroService;
using SQLite;
using System.Runtime.Serialization;

namespace GeoDroid.Data
{

    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/GeoDroid.Models")]
    public class MotivoMaquinaNoRecaudada
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        /// <summary>
        /// 0 when new insertion in mobile device
        /// </summary>
        public int idGeo { get; set; }
        [ForeingKeyIdGeo(typeof(Incidencia))]
        public int idIncidencia { get; set; }
        [DataMember, ForeingKeyIdGeo(typeof(ConceptoMotivoMaquinaNoRecaudada))]
        public int idConceptoMotivoMaquinaNoRecaudada { get; set; }
        [DataMember, MaxLength(2000)]
        public string comentarios { get; set; }

        //-----------------------------------------------------------------------------------------------------------------------//
        [Ignore]
        public virtual ConceptoMotivoMaquinaNoRecaudada? ConceptoMotivoMaquinaNoRecaudada { get; set; }
    }
}
