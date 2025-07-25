﻿using Fluxor;

namespace GEO_DROID.Store.AveriaCase
{
    public static class AveriaReducer
    {

        [ReducerMethod]
        public static AveriaState AveriaSelectedChangedAction(AveriaState averiaState, ChangeAveriaSelectedAction action)
        {

            return averiaState with { AveriaSelected = action.averia };
        }

        [ReducerMethod]
        public static AveriaState AveriaListSelectedChangedAction(AveriaState averiaState, ChangeAveriasListSelectedByEstablecimiento action)
        {

            return averiaState with { AveriaListSelected = action.averiaList };
        }

        [ReducerMethod]
        public static AveriaState ChangeAveriasCount(AveriaState averiaState, ChangeAveriasCount action)
        {

            return averiaState with { AveriaCount = action.averiacount };
        }

    }
}
