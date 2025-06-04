using Fluxor;


namespace GEO_DROID.Store.Application
{
    public static class AplicacionReducer
    {

        [ReducerMethod]
        public static AplicacionState MaquinaSelecterChangedAction(AplicacionState state, ChangeModalMaquinaSelecter action)
        {
            return state with { modalMaquinaSelecter = action.reference };
        }

        [ReducerMethod]
        public static AplicacionState ConceptoAveriaSelecterChangedAction(AplicacionState state, ChangeModalConceptoSelecter action)
        {
            return state with { modalConceptoAveriaSelecter = action.reference };
        }

        [ReducerMethod]
        public static AplicacionState EstadoSelecterChangedAction(AplicacionState state, ChangeModalEstadoSelecter action)
        {
            return state with { modalEstadoSelecter = action.reference };

        }

        [ReducerMethod]
        public static AplicacionState ChangeModalEstadoSelecter(AplicacionState state, ChangeModalEstadoSelecter action)
        {
            return state with { modalEstadoSelecter = action.reference };

        }


    }
}
