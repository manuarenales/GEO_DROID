using Fluxor;
using GEO_DROID.Store.Concepto;


namespace GEO_DROID.Store.AveriaEstado
{

    public static class AveriaEstadoReducer
    {

        [ReducerMethod]
        public static AveriaEstadoState ConceptoAveriaEstadoListSelectedChangedAction(AveriaEstadoState state, ChangeAveriaEstadoListForSelecter action)
        {
            return state with { AveriaEstadoListSelecterSelected = action.AveriaEstadoList };
        }

        [ReducerMethod]
        public static AveriaEstadoState ChangeAveriaEstadoSelectedAction(AveriaEstadoState state, ChangeAveriaEstadoSelectedAction action)
        {
            return state with { AveriaEstadoSelected = action.AveriaEstado };
        }

    }
}
