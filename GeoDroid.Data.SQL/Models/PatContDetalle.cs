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
    public class PatContDetalle
    {
        public enum TipoBasico
        {
            Jugadas = 1,
            Premios = 2,
            Auxiliar = 3,
            PagosManuales = 4,
            Partidas = 5,
            TicketIn = 6,
            TicketOut = 7,
        }

        [Key, DataMember]
        public int id { get; set; }
        [DataMember]
        public int idGeo { get; set; }
        /// <summary> Campo asociado con Maquinas.idPatronContadores, ya que la tabla PatronContadores no existe porque no aporta información adicional </summary>
        [DataMember]
        public int idPatronContadores { get; set; }
        [DataMember, MaxLength(50)]
        public string tipoContadores { get; set; }
        [DataMember]
        public decimal valorPaso { get; set; }
        /// <summary> Valores: PatContDetalles.TipoBasico </summary>
        [DataMember]
        public int tipoBasico { get; set; }
        /// <summary> Valores: InfoContadores.Contador </summary>
        [DataMember]
        public int tipoProtocolos { get; set; }
        [DataMember]
        public int orden { get; set; }
    }
}
