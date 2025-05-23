using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoDroid.Data.DTO
{
    public class CacheImportesConceptosRecaudacion
    {
        [Key]
        public int id { get; set; }
        public int idCacheImportes { get; set; }
        public int idConceptoRecaudacionGeo { get; set; }
        public decimal importe { get; set; }
    }
}
