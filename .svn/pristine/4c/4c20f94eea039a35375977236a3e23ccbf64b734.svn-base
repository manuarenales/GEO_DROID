using SQLite;


namespace GeoDroid.Data.SQL
{
    public class GeoDroidDatabase
    {
        public SQLiteAsyncConnection _database;

        public GeoDroidDatabase()
        {
            _database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);

            InitializeAsync();
        }

        private async void InitializeAsync()
        {
        
            await _database.CreateTableAsync<Establecimiento>();
            await _database.CreateTableAsync<ConfigurationGEO>();
            await _database.CreateTableAsync<Configuration>();
            await _database.CreateTableAsync<AccionComercial>();
            await _database.CreateTableAsync<Averia>();
            await _database.CreateTableAsync<AveriaEstado>();
            await _database.CreateTableAsync<Carga>();
            await _database.CreateTableAsync<ConceptoAveria>();
            await _database.CreateTableAsync<ConceptoGastoEstablecimiento>();
            await _database.CreateTableAsync<ConceptoMotivoMaquinaNoRecaudada>();
            await _database.CreateTableAsync<ConceptoPrestamo>();
            await _database.CreateTableAsync<Configuration>();
            await _database.CreateTableAsync<DesgloseBruto>();
            await _database.CreateTableAsync<Empresa>();
            await _database.CreateTableAsync<EmpresaCompetidora>();
            await _database.CreateTableAsync<EstablecimientoPotencial>();
            await _database.CreateTableAsync<EstablecimientoPotencialMaquina>();
            await _database.CreateTableAsync<GastoEstablecimiento>();
            await _database.CreateTableAsync<Incidencia>();
            await _database.CreateTableAsync<Instalacion>();
            await _database.CreateTableAsync<LecturaContador>();
            await _database.CreateTableAsync<LecturaContadorAuto>();
            await _database.CreateTableAsync<LecturaDetalle>();
            await _database.CreateTableAsync<LecturaDetalleAuto>();
            await _database.CreateTableAsync<Localizacion>();
            await _database.CreateTableAsync<Maquina>();
            await _database.CreateTableAsync<MaquinaConceptoRecaudacion>();
            await _database.CreateTableAsync<ModeloMaquina>();
            await _database.CreateTableAsync<ModuloCaptura>();
            await _database.CreateTableAsync<MotivoAccionComercial>();
            await _database.CreateTableAsync<MotivoMaquinaNoRecaudada>();
            await _database.CreateTableAsync<MotivoVisitaComercial>();
            await _database.CreateTableAsync<PatContDetalle>();
            await _database.CreateTableAsync<PatronContadorDetalle>();
            await _database.CreateTableAsync<Poblacion>();
            await _database.CreateTableAsync<Prestamo>();
            await _database.CreateTableAsync<PrestamoRecuperacion>();
            await _database.CreateTableAsync<Recaudacion>();
            await _database.CreateTableAsync<RecaudacionDetalles>();
            await _database.CreateTableAsync<Ruta>();
            await _database.CreateTableAsync<TipoAccionComercial>();
            await _database.CreateTableAsync<TipoCombustible>();
            await _database.CreateTableAsync<TipoDistribucionConceptoRecaudacion>();
            await _database.CreateTableAsync<UltimaLecturaRecaudacion>();
            await _database.CreateTableAsync<Vehiculo>();
            await _database.CreateTableAsync<VehiculoCombustible>();
            await _database.CreateTableAsync<VehiculoKm>();
            await _database.CreateTableAsync<VehiculoRepostaje>();
            await _database.CreateTableAsync<VisitaComercial>();
            await _database.CreateTableAsync<PatronContador>();
            await _database.CreateTableAsync<GeoDroid.Data.Configuration>();
            
        }

        public Task<int> InsertAsync<T>(T entity) where T : new()
        {
            return _database.InsertAsync(entity);
        }

        public Task<int> UpdateAsync<T>(T entity) where T : new()
        {
            return _database.UpdateAsync(entity);
        }

        public Task<int> DeleteAsync<T>(T entity) where T : new()
        {
            return _database.DeleteAsync(entity);
        }

        public Task<List<T>> GetAllAsync<T>() where T : new()
        {
            return _database.Table<T>().ToListAsync();
        }

        public Task<T> GetByIdAsync<T>(int id) where T : new()
        {
            return _database.FindAsync<T>(id);
        }

    }
}
