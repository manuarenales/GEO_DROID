using GeoDroid.Data;
using GeoDroid.Data.DTO;
using GeoDroid.Data.SQL;

namespace GEO_DROID.Services.SincroService
{
    struct Config
    {
        //static string _dbName;
        public Config()
        {

        }

        //static public string getFilePath(string fileName)
        //{
        //    //string p = getAppDataDir();

        //    return Path.Combine(getAppDataDir(), fileName);
        //}
        static public void ResetDatabaseOnlyLocalizacion()
        {
            //SQLiteConnection db = new SQLiteConnection(Config.dbPath());
            //db.DropTable<Localizacion>();
            //CreateTableLocalizacion();
            //db.Close();
        }
        static public string getAppDataDir()
        {
            return System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        }

        static public bool CacheImportesTableExists()
        {
            bool exists = false;

            try
            {
                //SQLiteConnection db = new SQLiteConnection(Config.connectionString());

                //List<SQLiteMasterModel> list = db.Query<SQLiteMasterModel>("SELECT * FROM sqlite_master WHERE type='table' AND name='CacheImportes'");
                //if (list.Count > 0)
                //    exists = true;

                //db.Close();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

            return exists;
        }
        static public void DropDataBase(GeoDroidDatabase _dbContext)
        {
            try
            {

                _dbContext._database.Table<Establecimiento>().Where(e => true).DeleteAsync();

                _dbContext._database.Table<MaquinaConceptoRecaudacion>().Where(e => true).DeleteAsync();
                _dbContext._database.Table<Maquina>().Where(e => true).DeleteAsync();
                _dbContext._database.Table<Averia>().Where(e => true).DeleteAsync();
                _dbContext._database.Table<AveriaEstado>().Where(e => true).DeleteAsync();
                _dbContext._database.Table<Ruta>().Where(e => true).DeleteAsync();
                _dbContext._database.Table<TipoDistribucionConceptoRecaudacion>().Where(e => true).DeleteAsync();
                _dbContext._database.Table<PatContDetalle>().Where(e => true).DeleteAsync();
                _dbContext._database.Table<Incidencia>().Where(e => true).DeleteAsync();
                _dbContext._database.Table<LecturaContadorAuto>().Where(e => true).DeleteAsync();
                _dbContext._database.Table<LecturaContador>().Where(e => true).DeleteAsync();
                _dbContext._database.Table<LecturaDetalle>().Where(e => true).DeleteAsync();
                _dbContext._database.Table<LecturaDetalleAuto>().Where(e => true).DeleteAsync();
                _dbContext._database.Table<Recaudacion>().Where(e => true).DeleteAsync();
                _dbContext._database.Table<DesgloseBruto>().Where(e => true).DeleteAsync();
                _dbContext._database.Table<RecaudacionDetalles>().Where(e => true).DeleteAsync();
                _dbContext._database.Table<Cambio>().Where(e => true).DeleteAsync();
                _dbContext._database.Table<Carga>().Where(e => true).DeleteAsync();
                _dbContext._database.Table<ConceptoPrestamo>().Where(e => true).DeleteAsync();
                _dbContext._database.Table<Prestamo>().Where(e => true).DeleteAsync();
                _dbContext._database.Table<PrestamoRecuperacion>().Where(e => true).DeleteAsync();
                _dbContext._database.Table<Empresa>().Where(e => true).DeleteAsync();
                _dbContext._database.Table<UltimaLecturaRecaudacion>().Where(e => true).DeleteAsync();
                _dbContext._database.Table<ConfigurationGEO>().Where(e => true).DeleteAsync();
                _dbContext._database.Table<Localizacion>().Where(e => true).DeleteAsync();
                _dbContext._database.Table<ConceptoGastoEstablecimiento>().Where(e => true).DeleteAsync();
                _dbContext._database.Table<GastoEstablecimiento>().Where(e => true).DeleteAsync();
                _dbContext._database.Table<ConceptoMotivoMaquinaNoRecaudada>().Where(e => true).DeleteAsync();
                _dbContext._database.Table<MotivoMaquinaNoRecaudada>().Where(e => true).DeleteAsync();
                _dbContext._database.Table<Instalacion>().Where(e => true).DeleteAsync();
                _dbContext._database.Table<MotivoVisitaComercial>().Where(e => true).DeleteAsync();
                _dbContext._database.Table<VisitaComercial>().Where(e => true).DeleteAsync();
                _dbContext._database.Table<TipoAccionComercial>().Where(e => true).DeleteAsync();
                _dbContext._database.Table<MotivoAccionComercial>().Where(e => true).DeleteAsync();
                _dbContext._database.Table<AccionComercial>().Where(e => true).DeleteAsync();
                _dbContext._database.Table<TipoCombustible>().Where(e => true).DeleteAsync();
                _dbContext._database.Table<Vehiculo>().Where(e => true).DeleteAsync();
                _dbContext._database.Table<VehiculoCombustible>().Where(e => true).DeleteAsync();
                _dbContext._database.Table<VehiculoKm>().Where(e => true).DeleteAsync();
                _dbContext._database.Table<VehiculoRepostaje>().Where(e => true).DeleteAsync();
                _dbContext._database.Table<Poblacion>().Where(e => true).DeleteAsync();
                _dbContext._database.Table<ModeloMaquina>().Where(e => true).DeleteAsync();
                _dbContext._database.Table<EstablecimientoPotencial>().Where(e => true).DeleteAsync();
                _dbContext._database.Table<EstablecimientoPotencialMaquina>().Where(e => true).DeleteAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        static public void CreateCacheImportesTable()
        {
            try
            {
                //var dbPath = Config.dbPath();
                //SQLiteConnection db = new SQLiteConnection(Config.dbPath());
                //db.CreateTable<CacheImportes>();
                //db.Close();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }

        static public void BuildDataBase()
        {
            //try
            //{
            //    SQLiteConnection db = dbIni ?? new SQLiteConnection(Config.dbPath());

            //    //If database was previously initialized do not recreate configuration data
            //    if (!ConfigurationTableExists(db))
            //    {
            //        db.CreateTable<Configuration>();

            //        Configuration config = new Configuration();
            //        config.id = 1;
            //        config.ip = "172.16.1.23";
            //        config.port = 10008;
            //        config.unitNumber = 1;
            //        config.syncToken = null;
            //        config.password = "1111";
            //        config.printerMAC = "00:01:90:D8:E9:11";
            //        config.syncCompressionEnabled = true;
            //        db.Insert(config);
            //    }

            //    db.CreateTable<Establecimiento>();
            //    db.CreateTable<MaquinaConceptoRecaudacion>();
            //    db.CreateTable<Maquina>();
            //    db.CreateTable<Averia>();
            //    db.CreateTable<AveriaEstado>();
            //    db.CreateTable<ConceptoAveria>();
            //    db.CreateTable<Ruta>();
            //    db.CreateTable<TipoDistribucionConceptoRecaudacion>();
            //    db.CreateTable<PatContDetalle>();
            //    db.CreateTable<Incidencia>();
            //    db.CreateTable<LecturaContador>();
            //    db.CreateTable<LecturaContadorAuto>();
            //    db.CreateTable<LecturaDetalle>();
            //    db.CreateTable<LecturaDetalleAuto>();
            //    db.CreateTable<ModuloCaptura>();
            //    db.CreateTable<Recaudacion>();
            //    db.CreateTable<DesgloseBruto>();
            //    db.CreateTable<RecaudacionDetalles>();
            //    db.CreateTable<Cambio>();
            //    db.CreateTable<Carga>();
            //    db.CreateTable<ConceptoPrestamo>();
            //    db.CreateTable<Prestamo>();
            //    db.CreateTable<PrestamoRecuperacion>();
            //    db.CreateTable<Empresa>();
            //    db.CreateTable<UltimaLecturaRecaudacion>();
            //    db.CreateTable<ConfigurationGEO>();
            //    db.CreateTable<Localizacion>();
            //    db.CreateTable<ConceptoGastoEstablecimiento>();
            //    db.CreateTable<GastoEstablecimiento>();
            //    db.CreateTable<ConceptoMotivoMaquinaNoRecaudada>();
            //    db.CreateTable<MotivoMaquinaNoRecaudada>();
            //    db.CreateTable<Instalacion>();
            //    db.CreateTable<MotivoVisitaComercial>();
            //    db.CreateTable<VisitaComercial>();
            //    db.CreateTable<TipoAccionComercial>();
            //    db.CreateTable<MotivoAccionComercial>();
            //    db.CreateTable<AccionComercial>();
            //    db.CreateTable<TipoCombustible>();
            //    db.CreateTable<Vehiculo>();
            //    db.CreateTable<VehiculoCombustible>();
            //    db.CreateTable<VehiculoKm>();
            //    db.CreateTable<VehiculoRepostaje>();
            //    db.CreateTable<EmpresaCompetidora>();
            //    db.CreateTable<Poblacion>();
            //    db.CreateTable<ModeloMaquina>();
            //    db.CreateTable<EstablecimientoPotencial>();
            //    db.CreateTable<EstablecimientoPotencialMaquina>();

            //    if (db != dbIni)
            //        db.Close();
            //}
            //catch (Exception ex)
            //{
            //    string error = ex.Message;
            //}
        }

        static public void DeleteRowsInCacheImportesTable(DateTime fechaHasta)
        {
            try
            {
                //var dbPath = Config.dbPath();
                //string cad = "Delete from CacheImportes where fecha < '" + fechaHasta.ToString("yyyy-MM-ddTHH:mm:ss") + "'";
                //string cad2 = "Delete from CacheImportesConceptosRecaudacion where idCacheImportes IN (SELECT id FROM CacheImportes WHERE fecha < '" + fechaHasta.ToString("yyyy-MM-ddTHH:mm:ss") + "')";
                //SQLiteConnection db = new SQLiteConnection(Config.dbPath());
                //db.CreateCommand(cad2, null).ExecuteNonQuery();
                //db.CreateCommand(cad, null).ExecuteNonQuery();
                //db.Close();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }

        static public string getFilePath(string fileName)
        {
            //string p = getAppDataDir();

            return Path.Combine(getAppDataDir(), fileName);
        }
        //static public void setDBName(string dbName)
        //{
        //    _dbName = dbName;
        //}

        //static public string dbPath()
        //{

        //    return getFilePath(_dbName);
        //}

        //static public string connectionString()
        //{
        //    //return "Data Source=" + Config.dbPath() + ";DefaultTimeout=100";
        //    return Config.dbPath();
        //}



        //static public bool IsDataBaseFileInitialized()
        //{
        //    bool initialized = false;

        //    string dbPath = Config.dbPath();

        //    try
        //    {
        //        //Check first if file has been created
        //        if (File.Exists(dbPath))
        //        {
        //            initialized = true;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }

        //    return initialized;
        //}

        //public bool CheckTableExists(string tableName)
        //{
        //    bool exists = false;

        //    try
        //    {
        //        _dbContext.EnsureCreated();

        //        List<SQLiteMasterModel> list = db.Query<SQLiteMasterModel>("SELECT * FROM sqlite_master WHERE type='table' AND name='" + tableName + "'");
        //        if (list.Count > 0)
        //            exists = true;

        //        if (db != dbIni)
        //            db.Close();
        //    }
        //    catch { }

        //    return exists;
        //}

        //static public bool ConfigurationTableExists()
        //{
        //    bool exists = false;

        //    try
        //    {
        //        SQLiteConnection db = dbIni ?? new SQLiteConnection(Config.connectionString());
        //        //Check if we have configuration table. If it is not found then, 

        //        List<SQLiteMasterModel> list = db.Query<SQLiteMasterModel>("SELECT * FROM sqlite_master WHERE type='table' AND name='Configuration'");
        //        if (list.Count > 0)
        //            exists = true;

        //        //SQLiteCommand cmd = db.CreateCommand("SELECT * FROM sqlite_master WHERE type='table' AND name='Configuration'");

        //        //return (cmd.ExecuteQuery <SQLiteMasterModel>() != null);

        //        /*SQLiteConnection db = new SQLiteConnection(Config.connectionString());
        //        TableQuery<Configuration> tq = db.Table<Configuration>();
        //        if (tq != null)
        //            exists = true;*/

        //        if (db != dbIni)
        //            db.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        //User table does not exist
        //        //throw;
        //        string error = ex.Message;
        //    }

        //    return exists;
        //}

        //static public bool CacheImportesTableExists()
        //{
        //    bool exists = false;

        //    try
        //    {
        //        SQLiteConnection db = new SQLiteConnection(Config.connectionString());

        //        List<SQLiteMasterModel> list = db.Query<SQLiteMasterModel>("SELECT * FROM sqlite_master WHERE type='table' AND name='CacheImportes'");
        //        if (list.Count > 0)
        //            exists = true;

        //        db.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        string error = ex.Message;
        //    }

        //    return exists;
        //}

        //static public void CreateCacheImportesTable()
        //{
        //    try
        //    {
        //        var dbPath = Config.dbPath();

        //        SQLiteConnection db = new SQLiteConnection(Config.dbPath());

        //        db.CreateTable<CacheImportes>();
        //        db.Close();

        //    }
        //    catch (Exception ex)
        //    {
        //        string error = ex.Message;
        //    }

        //}

        //static public void DropCacheImportesTable()
        //{
        //    try
        //    {
        //        var dbPath = Config.dbPath();

        //        SQLiteConnection db = new SQLiteConnection(Config.dbPath());

        //        db.DropTable<CacheImportes>();
        //        db.Close();

        //    }
        //    catch (Exception ex)
        //    {
        //        string error = ex.Message;
        //    }

        //}



        //static public void DeleteRowsInCacheImportesTable(DateTime fechaHasta)
        //{
        //    try
        //    {
        //        var dbPath = Config.dbPath();

        //        string cad = "Delete from CacheImportes where fecha < '" + fechaHasta.ToString("yyyy-MM-ddTHH:mm:ss") + "'";
        //        string cad2 = "Delete from CacheImportesConceptosRecaudacion where idCacheImportes IN (SELECT id FROM CacheImportes WHERE fecha < '" + fechaHasta.ToString("yyyy-MM-ddTHH:mm:ss") + "')";

        //        SQLiteConnection db = new SQLiteConnection(Config.dbPath());


        //        db.CreateCommand(cad2, null).ExecuteNonQuery();
        //        db.CreateCommand(cad, null).ExecuteNonQuery();
        //        db.Close();

        //    }
        //    catch (Exception ex)
        //    {
        //        string error = ex.Message;
        //    }

        //}

        //static public void BuildDataBase()
        //{
        //    BuildDataBase(null);
        //}

        //static public void BuildDataBase()
        //{
        //    try
        //    {


        //        //If database was previously initialized do not recreate configuration data
        //        if (!ConfigurationTableExists(db))
        //        {
        //            db.CreateTable<Configuration>();

        //            Configuration config = new Configuration();
        //            config.id = 1;
        //            config.ip = "172.16.1.23";
        //            config.port = 10008;
        //            config.unitNumber = 1;
        //            config.syncToken = null;
        //            config.password = "1111";
        //            config.printerMAC = "00:01:90:D8:E9:11";
        //            config.syncCompressionEnabled = true;
        //            db.Insert(config);
        //        }

        //        db.CreateTable<Establecimiento>();
        //        db.CreateTable<MaquinaConceptoRecaudacion>();
        //        db.CreateTable<Maquina>();
        //        db.CreateTable<Averia>();
        //        db.CreateTable<AveriaEstado>();
        //        db.CreateTable<ConceptoAveria>();
        //        db.CreateTable<Ruta>();
        //        db.CreateTable<TipoDistribucionConceptoRecaudacion>();
        //        db.CreateTable<PatContDetalle>();
        //        db.CreateTable<Incidencia>();
        //        db.CreateTable<LecturaContador>();
        //        db.CreateTable<LecturaContadorAuto>();
        //        db.CreateTable<LecturaDetalle>();
        //        db.CreateTable<LecturaDetalleAuto>();
        //        db.CreateTable<ModuloCaptura>();
        //        db.CreateTable<Recaudacion>();
        //        db.CreateTable<DesgloseBruto>();
        //        db.CreateTable<RecaudacionDetalles>();
        //        db.CreateTable<Cambio>();
        //        db.CreateTable<Carga>();
        //        db.CreateTable<ConceptoPrestamo>();
        //        db.CreateTable<Prestamo>();
        //        db.CreateTable<PrestamoRecuperacion>();
        //        db.CreateTable<Empresa>();
        //        db.CreateTable<UltimaLecturaRecaudacion>();
        //        db.CreateTable<ConfigurationGEO>();
        //        db.CreateTable<Localizacion>();
        //        db.CreateTable<ConceptoGastoEstablecimiento>();
        //        db.CreateTable<GastoEstablecimiento>();
        //        db.CreateTable<ConceptoMotivoMaquinaNoRecaudada>();
        //        db.CreateTable<MotivoMaquinaNoRecaudada>();
        //        db.CreateTable<Instalacion>();
        //        db.CreateTable<MotivoVisitaComercial>();
        //        db.CreateTable<VisitaComercial>();
        //        db.CreateTable<TipoAccionComercial>();
        //        db.CreateTable<MotivoAccionComercial>();
        //        db.CreateTable<AccionComercial>();
        //        db.CreateTable<TipoCombustible>();
        //        db.CreateTable<Vehiculo>();
        //        db.CreateTable<VehiculoCombustible>();
        //        db.CreateTable<VehiculoKm>();
        //        db.CreateTable<VehiculoRepostaje>();
        //        db.CreateTable<EmpresaCompetidora>();
        //        db.CreateTable<Poblacion>();
        //        db.CreateTable<ModeloMaquina>();
        //        db.CreateTable<EstablecimientoPotencial>();
        //        db.CreateTable<EstablecimientoPotencialMaquina>();

        //        if (db != dbIni)
        //            db.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        string error = ex.Message;
        //    }
        //}

        //static public void DropDataBase()
        //{


        //    db.DropTable<Establecimiento>();
        //    db.DropTable<MaquinaConceptoRecaudacion>();
        //    db.DropTable<Maquina>();
        //    db.DropTable<Averia>();
        //    db.DropTable<AveriaEstado>();
        //    db.DropTable<ConceptoAveria>();
        //    db.DropTable<Ruta>();
        //    db.DropTable<TipoDistribucionConceptoRecaudacion>();
        //    db.DropTable<PatContDetalle>();
        //    db.DropTable<Incidencia>();
        //    db.DropTable<LecturaContadorAuto>();
        //    db.DropTable<LecturaContador>();
        //    db.DropTable<LecturaDetalle>();
        //    db.DropTable<LecturaDetalleAuto>();
        //    db.DropTable<ModuloCaptura>();
        //    db.DropTable<Recaudacion>();
        //    db.DropTable<DesgloseBruto>();
        //    db.DropTable<RecaudacionDetalles>();
        //    db.DropTable<Cambio>();
        //    db.DropTable<Carga>();
        //    db.DropTable<ConceptoPrestamo>();
        //    db.DropTable<Prestamo>();
        //    db.DropTable<PrestamoRecuperacion>();
        //    db.DropTable<Empresa>();
        //    db.DropTable<UltimaLecturaRecaudacion>();
        //    db.DropTable<ConfigurationGEO>();
        //    db.DropTable<Localizacion>();
        //    db.DropTable<ConceptoGastoEstablecimiento>();
        //    db.DropTable<GastoEstablecimiento>();
        //    db.DropTable<ConceptoMotivoMaquinaNoRecaudada>();
        //    db.DropTable<MotivoMaquinaNoRecaudada>();
        //    db.DropTable<Instalacion>();
        //    db.DropTable<MotivoVisitaComercial>();
        //    db.DropTable<VisitaComercial>();
        //    db.DropTable<TipoAccionComercial>();
        //    db.DropTable<MotivoAccionComercial>();
        //    db.DropTable<AccionComercial>();
        //    db.DropTable<TipoCombustible>();
        //    db.DropTable<Vehiculo>();
        //    db.DropTable<VehiculoCombustible>();
        //    db.DropTable<VehiculoKm>();
        //    db.DropTable<VehiculoRepostaje>();
        //    db.DropTable<EmpresaCompetidora>();
        //    db.DropTable<Poblacion>();
        //    db.DropTable<ModeloMaquina>();
        //    db.DropTable<EstablecimientoPotencial>();
        //    db.DropTable<EstablecimientoPotencialMaquina>();

        //    if (db != dbIni)
        //        db.Close();
        //}

        //static public void ResetDatabaseOnlyLocalizacion()
        //{

        //    CreateTableLocalizacion();

        //}

        //static public void CreateTableLocalizacion()
        //{
        //    SQLiteConnection db = new SQLiteConnection(Config.dbPath());
        //    db.CreateTable<Localizacion>();
        //    db.Close();
        //}
    }
}
