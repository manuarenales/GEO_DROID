using Fluxor;
using GeoDroid.Data;
using GeoDroid.Data.SQL;
using IDispatcher = Fluxor.IDispatcher;

namespace GEO_DROID.Store.Concepto
{


    public class ConceptoAveriaEffects
    {
        private readonly GeoDroidDatabase _database;

        public ConceptoAveriaEffects(GeoDroidDatabase database)
        {
            _database = database;
        }

        [EffectMethod]
        public async Task GetConceptoAveriasList(GetConceptoAveriaList action, IDispatcher dispatcher)
        {
            try
            {

                List<ConceptoAveria> ConceptoAveria = await _database._database.Table<ConceptoAveria>().ToListAsync();
                if (ConceptoAveria.Count == 0)
                {
                    await _database._database.InsertAsync(new ConceptoAveria { descripcion = "Cerrada" });
                }
                ConceptoAveria = await _database._database.Table<ConceptoAveria>().ToListAsync();
                dispatcher.Dispatch(new ChangeConceptoAveriasListForSelecter(ConceptoAveria));
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
