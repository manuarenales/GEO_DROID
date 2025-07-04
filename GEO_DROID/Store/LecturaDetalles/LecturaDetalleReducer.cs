﻿using Fluxor;
using GEO_DROID.Store.Forms;
using GEO_DROID.Store.Forms.Averias;


namespace GEO_DROID.Store.LecturaDetalle
{
    public static class LecturaDetalleReducer
    {

        [ReducerMethod]
        public static AveriaFormState changeLecturasDetalleSelectedForAveriasForm(AveriaFormState state, changeLecturasDetalleSelected action)
        {
            return state with { PatronLecturaContador = action.patronLecturas, };
        }
    }
}
