using Fluxor;
using GeoDroid.Data;


namespace GEO_DROID.Store.Rutas
{
    [FeatureState]
    public class RutasReducer
    {
        [ReducerMethod]
        public static RutasState RutasListSelectedChangedAction(RutasState state, ChangeRutasListSelected action)
        {
            return state with { RutasListSelected = action.RutaList };
        }

        [ReducerMethod]
        public static RutasState RutaSelectedChangedAction(RutasState state, ChangeRutaSelectedAction action)
        {
            return state with { RutaSelected = action.ruta };
        }

        [ReducerMethod]
        public static RutasState AddRutaTorutasSelected(RutasState state, AddRutaTorutasSelected action)
        {
            var rute = new List<Ruta>(state.RutasListSelected);

            if (!rute.Any(r => r.idEstablecimiento == action.NewRute.idEstablecimiento))
            {
                rute.Add(action.NewRute);
            }
            return state with { RutasListSelected = rute };
        }
    }

}
