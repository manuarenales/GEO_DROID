using Fluxor;
using GeoDroid.Data;
using GeoDroid.Data.SQL;
using IDispatcher = Fluxor.IDispatcher;

namespace GEO_DROID.Store.AveriaCase
{
    public class AveriaEffects
    {
        private readonly GeoDroidDatabase _database;

        public AveriaEffects(GeoDroidDatabase database)
        {
            _database = database;
        }

        [EffectMethod]
        public async Task GetAveriasByEstablecimientoIdAsync(GetAveriasByEstablecimiento action, IDispatcher dispatcher)
        {
            try
            {

                if (action == null)
                    throw new ArgumentNullException(nameof(action));
                if (_database == null || _database._database == null)
                    throw new InvalidOperationException("Base de datos no inicializada");

                List<Averia> result = new();

                // Obtener establecimiento
                var establecimiento = await _database._database.Table<GeoDroid.Data.Establecimiento>()
                    .Where(e => e.id == action.establecimientoId).FirstOrDefaultAsync();

                if (establecimiento == null)
                {
                    dispatcher.Dispatch(new ChangeAveriasListSelectedByEstablecimiento(result));
                    return;
                }

                // Obtener máquina del establecimiento
                var maquina = await _database._database.Table<Maquina>()
                    .Where(m => m.idEstablecimiento == action.establecimientoId).FirstOrDefaultAsync();

                if (maquina == null)
                {
                    dispatcher.Dispatch(new ChangeAveriasListSelectedByEstablecimiento(result));
                    return;
                }

                // Obtener incidencia de la máquina 22 cada
                List<Incidencia> incidencias = await _database._database.Table<Incidencia>().Where(i => i.idMaquinas == maquina.id).ToListAsync();

                foreach (Incidencia Incidencia in incidencias)
                {
                    if (Incidencia != null)
                    {
                        var i = await _database._database.Table<Averia>().Where(a => a.idIncidencias == Incidencia.id).FirstOrDefaultAsync();
                        if (i is not null)
                        {
                            result.Add(i);
                        }
                    }
                }

                // Si no hay incidencias, devolvemos lista vacía 
                if (result == null || result.Count == 0)
                {
                    dispatcher.Dispatch(new ChangeAveriasListSelectedByEstablecimiento(new List<Averia>()));
                    return;
                }

                // Cargar información relacionada por cada avería
                foreach (var averia in result)
                {
                    if (averia.idAveriaEstados != null)
                    {
                        averia.AveriaEstado = await _database._database.Table<GeoDroid.Data.AveriaEstado>()
                            .Where(a => a.id == averia.idAveriaEstados).FirstOrDefaultAsync();
                    }

                    if (averia.idIncidencias != 0)
                    {
                        averia.Incidencia = await _database._database.Table<Incidencia>()
                            .Where(a => a.id == averia.idIncidencias).FirstOrDefaultAsync();

                        if (averia.Incidencia?.idMaquinas != 0)
                        {
                            averia.Incidencia.maquina = await _database._database.Table<Maquina>()
                                .Where(a => a.id == averia.Incidencia.idMaquinas).FirstOrDefaultAsync();
                        }

                        if (averia.Incidencia?.idMaquinas != 0)
                        {
                            averia.Incidencia.maquina = await _database._database.Table<Maquina>()
                                .Where(a => a.id == averia.Incidencia.idMaquinas).FirstOrDefaultAsync();

                            averia.Incidencia.maquina.Empresa = await _database._database.Table<Empresa>().Where(a => a.id == averia.Incidencia.maquina.Empresaid).FirstOrDefaultAsync();
                            averia.Incidencia.maquina.establecimiento = await _database._database.Table<GeoDroid.Data.Establecimiento>().Where(a => a.id == averia.Incidencia.maquina.idEstablecimiento).FirstOrDefaultAsync();

                            averia.Incidencia.maquina.patronContador = await _database._database.Table<PatContDetalle>().Where(a => a.id == averia.Incidencia.maquina.idPatronContadores).ToListAsync();
                            //List<PatContDetalle> list = await _database._database.Table<PatContDetalle>().ToListAsync();
                        }
                    }

                    if (averia.idConceptos != null)
                    {
                        averia.ConceptoAveria = await _database._database.Table<ConceptoAveria>()
                            .Where(a => a.id == averia.idConceptos).FirstOrDefaultAsync();
                    }
                }
                dispatcher.Dispatch(new ChangeAveriasListSelectedByEstablecimiento(result));
            }
            catch (Exception ex)
            {
                // Aquí podrías loguear el error o lanzar uno personalizado
                Console.WriteLine($"Error en GetAveriasByEstablecimientoIdAsync: {ex}");
                throw;
            }

        }

        [EffectMethod]
        public async Task AddNewAveriaAsync(AddAveria action, IDispatcher dispatcher)
        {
            try
            {
                await _database._database.InsertAsync(action.averia);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [EffectMethod]
        public async Task GetAveriasCount(GetAveriasCount action, IDispatcher dispatcher)
        {
            // Obtener datos de la base de datos 
            try
            {
                // Obtener establecimiento
                GeoDroid.Data.Establecimiento establecimiento = await _database._database.Table<GeoDroid.Data.Establecimiento>()
                    .Where(e => e.id == action.establecimiento.id).FirstOrDefaultAsync();

                if (establecimiento == null)
                {
                    dispatcher.Dispatch(new ChangeAveriasCount(0));
                    return;
                }
                //obtenemos todas las maquinas del establecimineto 
                List<Maquina> maquinas = await _database._database.Table<Maquina>()
                  .Where(m => m.idEstablecimiento == action.establecimiento.id).ToListAsync();

                //por cada maquina obtenemos las incidencias de las maquinas 
                List<Incidencia> incidencias = new List<Incidencia>();
                foreach (Maquina maquina in maquinas)
                {
                    List<Incidencia> inci = await _database._database.Table<Incidencia>().Where(i => i.idMaquinas == maquina.id).ToListAsync();

                    foreach (Incidencia i in inci)
                    {
                        incidencias.Add(i);
                    }
                }
                if (incidencias == null || incidencias.Count == 0)
                {
                    dispatcher.Dispatch(new ChangeAveriasCount(0));
                    return;
                }

                //buscamos las averias que tengan estas incidencias 
                int averiaNumber = 0;
                foreach (Incidencia incidencia in incidencias)
                {
                    List<Averia> i = await _database._database.Table<Averia>().Where(a => a.idIncidencias == incidencia.id).ToListAsync();
                    averiaNumber += i.Count;
                }
                dispatcher.Dispatch(new ChangeAveriasCount(averiaNumber));
                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

}
