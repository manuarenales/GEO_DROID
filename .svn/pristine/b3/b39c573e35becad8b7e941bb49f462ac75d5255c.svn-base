using Fluxor;
using GeoDroid.Data;
using GeoDroid.Data.SQL;
using IDispatcher = Fluxor.IDispatcher;

namespace GEO_DROID.Store
{
    public class EstablecimientoEffects
    {
        private readonly GeoDroidDatabase _database;

        public EstablecimientoEffects(GeoDroidDatabase database)
        {
            _database = database;
        }

        [EffectMethod]
        public async Task GetEstablecimientosListByDate(GetEstablecimientosListByDate action, IDispatcher dispatcher)
        {
            try
            {
                List<Establecimiento> establecimientos = await _database._database.Table<Establecimiento>().ToListAsync();

                dispatcher.Dispatch(new ChangeEstablecimientosListSelected(establecimientos));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [EffectMethod]
        public async Task GetEstablecimientosList(GetEstablecimientosList action, IDispatcher dispatcher)
        {
            try
            {
                List<Establecimiento> establecimientos = await _database._database.Table<Establecimiento>().ToListAsync();
                dispatcher.Dispatch(new ChangeEstablecimientosListForSelecter(establecimientos));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
