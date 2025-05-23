using Fluxor;
using GeoDroid.Data.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IDispatcher = Fluxor.IDispatcher; // Explicit alias

namespace GEO_DROID.Store.Establecimiento
{
    public class EstablecimientoEffects
    {
        private readonly GeoDroidDatabase _database;

        public EstablecimientoEffects(GeoDroidDatabase database)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
        }

        [EffectMethod]
        public async Task HandleLoadEstablecimientosAction(LoadEstablecimientosAction action, IDispatcher dispatcher)
        {
            try
            {
                // Assuming _database._database is the SQLiteAsyncConnection or similar
                List<GeoDroid.Data.Establecimiento> establecimientos = await _database._database.Table<GeoDroid.Data.Establecimiento>().ToListAsync();
                dispatcher.Dispatch(new LoadEstablecimientosSuccessAction(establecimientos));
            }
            catch (Exception ex)
            {
                // Log the exception ex.ToString()
                dispatcher.Dispatch(new LoadEstablecimientosFailureAction("Failed to load establecimientos."));
            }
        }

        [EffectMethod]
        public async Task HandleLoadEstablecimientosByDateAction(LoadEstablecimientosByDateAction action, IDispatcher dispatcher)
        {
            try
            {
                // Example: Filtering by a hypothetical 'CreationDate' property.
                // Adjust the property name and logic as per your Establecimiento model and requirements.
                // This is a placeholder for actual date filtering logic.
                List<GeoDroid.Data.Establecimiento> establecimientos = await _database._database.Table<GeoDroid.Data.Establecimiento>()
                    // .Where(e => e.SomeDateProperty.Date == action.Date.Date) // Example filter
                    .ToListAsync(); // Current implementation fetches all; needs filter logic

                // If no specific filtering is applied, this behaves like LoadEstablecimientosAction.
                // Consider if a separate action/effect is truly needed if filtering isn't implemented.
                dispatcher.Dispatch(new LoadEstablecimientosByDateSuccessAction(establecimientos, action.Date));
            }
            catch (Exception ex)
            {
                // Log the exception ex.ToString()
                dispatcher.Dispatch(new LoadEstablecimientosByDateFailureAction($"Failed to load establecimientos for date {action.Date.ToShortDateString()}.", action.Date));
            }
        }

        [EffectMethod]
        public async Task HandleLoadEstablecimientosForSelecterAction(LoadEstablecimientosForSelecterAction action, IDispatcher dispatcher)
        {
            try
            {
                List<GeoDroid.Data.Establecimiento> establecimientos = await _database._database.Table<GeoDroid.Data.Establecimiento>().ToListAsync();
                dispatcher.Dispatch(new LoadEstablecimientosForSelecterSuccessAction(establecimientos));
            }
            catch (Exception ex)
            {
                // Log the exception ex.ToString()
                dispatcher.Dispatch(new LoadEstablecimientosForSelecterFailureAction("Failed to load establecimientos for selecter."));
            }
        }
    }
}
