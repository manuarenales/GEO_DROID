using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEO_DROID.Store.Recaudacion
{
    public static class RecaudacionReducer
    {

        [ReducerMethod]
        public static RecaudacionState ChangeRecaudacionSelectedAction(RecaudacionState state, ChangeRecaudacionSelectedAction action)
        {
            return state with { RecaudacionSelected = action.Recaudacion };
        }


        [ReducerMethod]
        public static RecaudacionState changeLecturaSelectedForRecaudacionForm(RecaudacionState state, changeLecturaSelectedForRecaudacionForm action)
        {
            return state with { LecturaContador = action.lectura };
        }

    }
}
