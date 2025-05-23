using GeoDroid.Data;
using GeoDroid.Data.SQL;

namespace GEO_DROID.Services
{
    public class AveriasService : IDisposable
    {
        private readonly GeoDroidDatabase _database;
        public AveriasService(GeoDroidDatabase database)
        {
            _database = database;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetAveriasCount(Establecimiento Establecimiento)
        {
            // Obtener datos de la base de datos 
            try
            {
                // Obtener establecimiento
                Establecimiento establecimiento = await _database._database.Table<Establecimiento>()
                    .Where(e => e.id == Establecimiento.id).FirstOrDefaultAsync();

                if (establecimiento == null)
                {
                    return 0;
                }
                //obtenemos todas las maquinas del establecimineto 
                List<Maquina> maquinas = await _database._database.Table<Maquina>()
                  .Where(m => m.idEstablecimiento == Establecimiento.id).ToListAsync();

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
                    return 0;
                }

                //buscamos las averias que tengan estas incidencias 
                int averiaNumber = 0;
                foreach (Incidencia incidencia in incidencias)
                {
                    List<Averia> i = await _database._database.Table<Averia>().Where(a => a.idIncidencias == incidencia.id).ToListAsync();
                    averiaNumber += i.Count;
                }
                return averiaNumber;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
