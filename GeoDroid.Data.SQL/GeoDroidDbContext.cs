using GeoDroid.Data.DTO;

namespace GeoDroid.Data.SQL
{
    public class GeoDroidDbContext
    {

        public IEnumerable<Averia> Averia { get; set; }
        public IEnumerable<AveriaEstado> AveriaEstado { get; set; }
        public IEnumerable<Carga> Carga { get; set; }
        public IEnumerable<ConceptoAveria> ConceptoAveria { get; set; }
        public IEnumerable<Establecimiento> Establecimiento { get; set; }
        public IEnumerable<Maquina> Maquina { get; set; }
        public IEnumerable<Ruta> Ruta { get; set; }
        public IEnumerable<PatronContador> PatronContador { get; set; }
        public IEnumerable<PatronContadorDetalle> PatronContadorDetalle { get; set; }
        public IEnumerable<LecturaContador> LecturaContador { get; set; }
        public IEnumerable<LecturaDetalle> LecturaDetalle { get; set; }
        public IEnumerable<LecturaContadorAuto> LecturaContadorAuto { get; set; }
        public IEnumerable<ModuloCaptura> ModulosCaptura { get; set; }
        public IEnumerable<Configuration> Configuration { get; set; }
        public IEnumerable<ConfigurationGEO> ConfigurationGEO { get; set; }
        public IEnumerable<CacheImportes> CacheImportes { get; set; }
        public IEnumerable<CacheImportesConceptosRecaudacion> CacheImportesConceptosRecaudacion { get; set; }
        public IEnumerable<Recaudacion> Recaudaciones { get; set; }
        public IEnumerable<Incidencia> Incidencias { get; set; }

        public GeoDroidDbContext()
        {

        }


    }
}
