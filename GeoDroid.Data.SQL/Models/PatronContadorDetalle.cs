using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;


namespace GeoDroid.Data
{
    public class PatronContadorDetalle
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
        public int? idGeo { get; set; }
        [DataMember]
        public string tipoContadores { get; set; }
        [DataMember]
        public decimal valorPaso { get; set; }
        [DataMember]
        public int tipoBasico { get; set; }
        [DataMember]
        public int tipoProtocolos { get; set; }
        [DataMember]
        public int orden { get; set; }

        [ForeignKey("patronContador"), DataMember]
        public int? idPatronContador { get; set; }
        public virtual PatronContador? patronContador { get; set; }
    }
}
