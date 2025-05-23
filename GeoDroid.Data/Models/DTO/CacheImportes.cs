using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace GeoDroid.Data.DTO
{
    public class CacheImportes
    {
        [Key]
        public int id { get; set; }
        public DateTime fecha { get; set; }
        public int numeroRecaudaciones { get; set; }
        public decimal neto { get; set; }
        public decimal tasas { get; set; }
        public decimal cargas { get; set; }
        public decimal recuperacionesCargas { get; set; }
        public decimal prestamos { get; set; }
        public decimal recuperacionesPrestamos { get; set; }
        public decimal gastos { get; set; }
        [JsonIgnore]
        public List<CacheImportesConceptosRecaudacion> cacheImportesConceptosRecaudacion { get; set; }
        public decimal Total
        {
            get
            {
                return neto - cargas + recuperacionesCargas - prestamos + recuperacionesPrestamos - gastos + cacheImportesConceptosRecaudacion.Sum(x => x.importe);
            }
        }
        public bool IsCero
        {
            get
            {
                return numeroRecaudaciones == 0
                 && neto == 0 && cargas == 0 && recuperacionesCargas == 0
                 && prestamos == 0 && recuperacionesPrestamos == 0
                 && gastos == 0
                 && cacheImportesConceptosRecaudacion.Sum(x => x.importe) == 0;
            }
        }
    }
}
