using Geodroid.Data;
using GeoDroid.Data.DTO;
using Microsoft.EntityFrameworkCore;

namespace GeoDroid.Data
{
    public class GeoDroidDbContext : DbContext
    {
        public DbSet<Averia> Averias { get; set; }
        public DbSet<AveriaEstado> AveriasEstados { get; set; }
        public DbSet<Carga> Cargas { get; set; }
        public DbSet<ConceptoAveria> ConceptoAverias { get; set; }
        public DbSet<Establecimiento> Establecimientos { get; set; }
        public DbSet<Incidencia> Incidencias { get; set; }
        public DbSet<Maquina> Maquinas { get; set; }
        public DbSet<Ruta> Rutas { get; set; }
        public DbSet<PatronContador> PatronesContador { get; set; }
        public DbSet<PatronContadorDetalle> PatronesContadoresDetalle { get; set; }
        public DbSet<LecturaContador> LecturaContadores { get; set; }
        public DbSet<LecturaDetalle> LecturasDetalle { get; set; }
        public DbSet<LecturaContadorAuto> LecturasContadoresAuto { get; set; }
        public DbSet<ModuloCaptura> ModulosCaptura { get; set; }
        public DbSet<Configuration> Configuration { get; set; }
        public DbSet<ConfigurationGEO> ConfigurationGEO { get; set; }
        public DbSet<CacheImportes> CacheImportes { get; set; }
        public DbSet<CacheImportesConceptosRecaudacion> CacheImportesConceptosRecaudacion { get; set; }
        public DbSet<Recaudacion> Recaudacion { get; set; }
        public DbSet<Incidencia> Incidencia { get; set; }
        public DbSet<LecturaContadorAuto> LecturaContadorAuto { get; set; }
        public DbSet<RecaudacionDetalles> RecaudacionDetalles { get; set; }
        public DbSet<Maquina> Maquina { get; set; }
        public DbSet<TipoDistribucionConceptoRecaudacion> TipoDistribucionConceptoRecaudacion { get; set; }
        public DbSet<Carga> Carga { get; set; }
        public DbSet<GastoEstablecimiento> GastoEstablecimiento { get; set; }
        public DbSet<PatContDetalle> PatContDetalle { get; set; }
        public DbSet<PrestamoRecuperacion> PrestamoRecuperacion { get; set; }
        public DbSet<Prestamo> Prestamo { get; set; }
        public DbSet<MaquinaConceptoRecaudacion> MaquinaConceptoRecaudacion { get; set; }
        public DbSet<Averia> Averia { get; set; }
        public DbSet<AveriaEstado> AveriaEstado { get; set; }
        public DbSet<ConceptoAveria> ConceptoAveria { get; set; }
        public DbSet<ConceptoGastoEstablecimiento> ConceptoGastoEstablecimiento { get; set; }
        public DbSet<ConceptoMotivoMaquinaNoRecaudada> ConceptoMotivoMaquinaNoRecaudada { get; set; }
        public DbSet<MotivoMaquinaNoRecaudada> MotivoMaquinaNoRecaudada { get; set; }
        public DbSet<Ruta> Ruta { get; set; }
        public DbSet<Cambio> Cambio { get; set; }
        public DbSet<ConceptoPrestamo> ConceptoPrestamo { get; set; }
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<UltimaLecturaRecaudacion> UltimaLecturaRecaudacion { get; set; }
        public DbSet<Localizacion> Localizacion { get; set; }
        public DbSet<LecturaContador> LecturaContador { get; set; }
        public DbSet<Instalacion> Instalacion { get; set; }
        public DbSet<MotivoVisitaComercial> MotivoVisitaComercial { get; set; }
        public DbSet<TipoAccionComercial> TipoAccionComercial { get; set; }
        public DbSet<VisitaComercial> VisitaComercial { get; set; }
        public DbSet<MotivoAccionComercial> MotivoAccionComercial { get; set; }
        public DbSet<Poblacion> Poblacion { get; set; }
        public DbSet<EmpresaCompetidora> EmpresaCompetidora { get; set; }
        public DbSet<VehiculoCombustible> VehiculoCombustible { get; set; }
        public DbSet<VehiculoKm> VehiculoKm { get; set; }
        public DbSet<VehiculoRepostaje> VehiculoRepostaje { get; set; }
        public DbSet<Vehiculo> Vehiculo { get; set; }
        public DbSet<TipoCombustible> TipoCombustible { get; set; }
        public DbSet<AccionComercial> AccionComercial { get; set; }
        public DbSet<LecturaDetalle> LecturaDetalle { get; set; }
        public DbSet<LecturaDetalleAuto> LecturaDetalleAuto { get; set; }
        public DbSet<ModuloCaptura> ModuloCaptura { get; set; }
        public DbSet<DesgloseBruto> DesgloseBruto { get; set; }
        public DbSet<ModeloMaquina> ModeloMaquina { get; set; }
        public DbSet<EstablecimientoPotencial> EstablecimientoPotencial { get; set; }
        public DbSet<EstablecimientoPotencialMaquina> EstablecimientoPotencialMaquina { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionDb = $"Filename={PathDB.GetPath("test.db")}";
            optionsBuilder.UseSqlite(connectionDb);
            optionsBuilder.EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

    }
}
