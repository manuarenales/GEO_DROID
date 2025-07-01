using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEO_DROID.Store.Recaudacion
{
    public static class RecaudacionFormReducer
    {

        [ReducerMethod]
        public static RecaudacionFormState ChangeRecaudacionSelectedAction(RecaudacionFormState state, ChangeRecaudacionSelectedAction action)
        {
            return state with { RecaudacionSelected = action.Recaudacion };
        }


        [ReducerMethod]
        public static RecaudacionFormState changeLecturaSelectedForRecaudacionForm(RecaudacionFormState state, changeLecturaSelectedForRecaudacionForm action)
        {
            return state with { LecturaContador = action.lectura };
        }

    }
}
