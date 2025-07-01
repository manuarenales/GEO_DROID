
using IDispatcher = Fluxor.IDispatcher;
using Fluxor;
using GeoDroid.Data.SQL;

namespace GEO_DROID.Store.ModuloCaptura
{
    public class ModuloCapturaEffects
    {

        private readonly GeoDroidDatabase _database;

        public ModuloCapturaEffects(GeoDroidDatabase database)
        {
            _database = database;
        }


        [EffectMethod]
        public async Task GetModuloCapturaByMaquinaID(GetModuloCapturaByMaquinaID action, IDispatcher dispatcher)
        {
            try
            {
                GeoDroid.Data.ModuloCaptura moduloCaptura = await _database._database.Table<GeoDroid.Data.ModuloCaptura>().Where(mc => mc.idMaquinas == action.MaquinaID).FirstOrDefaultAsync();

                dispatcher.Dispatch(new ChangeModuloCapturaSelectedAction(moduloCaptura));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
