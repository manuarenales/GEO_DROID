using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace GeoDroid.Data
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/GeoDroid.Models")]
    public class Establecimiento
    {
        [Key, DataMember]
        public int id { get; set; }
        [DataMember]
        public string? codigo { get; set; }
        [DataMember]
        public string? nombre { get; set; }
        [DataMember]
        public int idGeo { get; set; }
        [DataMember]
        public int idEmpresa { get; set; }
        [DataMember]
        public string? direccion { get; set; }
        [DataMember]
        public string? poblacion { get; set; }
        [DataMember]
        public string? codigoPostal { get; set; }
        [DataMember]
        public string? telefono1 { get; set; }
        [DataMember]
        public string? telefono2 { get; set; }
        [DataMember]
        public string? fax { get; set; }
        [DataMember]
        public string? email { get; set; }
        [DataMember]
        public string? web { get; set; }
        [DataMember]
        public string? contacto { get; set; }
        [DataMember]
        public bool esAlmacen { get; set; }
        [DataMember]
        public string? permiso { get; set; }
        [DataMember]
        public string? comentarios { get; set; }
        [DataMember]
        public string? avisoRecaudacion { get; set; }
        [DataMember]
        public string? nombreTitular { get; set; }
        [DataMember]
        public string? nifTitular { get; set; }
        [DataMember]
        public bool envioFacturasEmail { get; set; }
        [DataMember]
        public DateTime fechaDesdeTitular { get; set; }
        [DataMember]
        public string? comercial { get; set; }
        [DataMember]
        public string? recaudador { get; set; }
        [DataMember]
        public string? mecanico { get; set; }
        [DataMember]
        public bool tieneLlave { get; set; }
        [DataMember]
        public DateTime horaApertura { get; set; }
        [DataMember]
        public DateTime horaCierre { get; set; }
        [DataMember]
        public string? comentariosHorario { get; set; }
        [DataMember]
        public string? comentariosContrato { get; set; }
        [DataMember]
        public string? diasDescanso { get; set; }
        [DataMember]
        public DateTime fechaProximaRecaudacion { get; set; }
        [DataMember]
        public int maxDiasSinArquear { get; set; }
        [DataMember]
        public string? tecnausaCountersServidor { get; set; }
        [DataMember]
        public string? tecnausaCountersUsuario { get; set; }
        [DataMember]
        public string? tecnausaCountersPassword { get; set; }
        [DataMember]
        public DateTime fechaPermiso { get; set; }
        [DataMember]
        public DateTime fechaCaducidadPermiso { get; set; }
        [DataMember]
        public DateTime fechaPresentacionPermiso { get; set; }
        [DataMember]
        public string? estadoPermiso { get; set; }
        [DataMember]
        public DateTime fechaContrato { get; set; }
        [DataMember]
        public DateTime fechaFinContrato { get; set; }
        [DataMember]
        public double recaudacion { get; set; }

        [JsonIgnore]
        public virtual ICollection<Maquina>? Maquinas { get; set; }
        [JsonIgnore]
        public virtual ICollection<AccionComercial>? AccionesComerciales { get; set; }
    }
}

