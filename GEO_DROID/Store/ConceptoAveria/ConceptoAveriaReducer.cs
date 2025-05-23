using Fluxor;


namespace GEO_DROID.Store.Concepto
{


    public static class ConceptoAveriaReducer
    {

        [ReducerMethod]
        public static ConceptoAveriaState ConceptoAveriaListSelectedChangedAction(ConceptoAveriaState state, ChangeConceptoAveriasListForSelecter action)
        {
            return state with { ConceptoAveriaListSelecterSelected = action.ConceptoAveriaList };
        }

        [ReducerMethod]
        public static ConceptoAveriaState ChangeMaquinaSelectedAction(ConceptoAveriaState state, ChangeConceptoAveriaSelectedAction action)
        {
            return state with { ConceptoAveriaSelected = action.ConceptoAveria };
        }


    }
}
