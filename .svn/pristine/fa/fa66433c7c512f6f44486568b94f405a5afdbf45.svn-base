using Fluxor;


namespace GEO_DROID.Store.Maquinas
{
    public static class MaquinasReducer
    {

        [ReducerMethod]
        public static MaquinasState EstablecimientoListSelectedChangedAction(MaquinasState state, ChangeMaquinasListForSelecter action)
        {
            return state with { MaquinaListSelecterSelected = action.MaquinaList };
        }

        [ReducerMethod]
        public static MaquinasState ChangeMaquinaSelectedAction(MaquinasState state, ChangeMaquinaSelectedAction action)
        {
            return state with { MaquinaSelected = action.maquina };
        }


    }
}
