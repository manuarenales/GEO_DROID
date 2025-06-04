using Fluxor;
using System.Collections.Generic;
using System.Linq; // Required for .Any() and .ToList()

namespace GEO_DROID.Store.Establecimiento
{
    public static class EstablecimientoReducers // Renamed to plural for convention
    {
        // Reducer for when starting to load establecimientos
        [ReducerMethod]
        public static EstablecimientoState OnLoadEstablecimientosAction(EstablecimientoState state, LoadEstablecimientosAction action) =>
            state with { IsLoading = true, CurrentError = string.Empty };

        [ReducerMethod]
        public static EstablecimientoState OnLoadEstablecimientosByDateAction(EstablecimientoState state, LoadEstablecimientosByDateAction action) =>
            state with { IsLoading = true, CurrentError = string.Empty };

        [ReducerMethod]
        public static EstablecimientoState OnLoadEstablecimientosForSelecterAction(EstablecimientoState state, LoadEstablecimientosForSelecterAction action) =>
            state with { IsLoading = true, CurrentError = string.Empty };

        // Reducer for successfully loading establecimientos
        [ReducerMethod]
        public static EstablecimientoState OnLoadEstablecimientosSuccessAction(EstablecimientoState state, LoadEstablecimientosSuccessAction action) =>
            state with { IsLoading = false, EstablecimientoList = action.Establecimientos ?? new List<GeoDroid.Data.Establecimiento>() };

        // Reducer for successfully loading establecimientos by date
        [ReducerMethod]
        public static EstablecimientoState OnLoadEstablecimientosByDateSuccessAction(EstablecimientoState state, LoadEstablecimientosByDateSuccessAction action) =>
            state with { IsLoading = false, EstablecimientoList = action.Establecimientos ?? new List<GeoDroid.Data.Establecimiento>() }; // Assuming this updates the main list

        // Reducer for successfully loading establecimientos for a selecter
        [ReducerMethod]
        public static EstablecimientoState OnLoadEstablecimientosForSelecterSuccessAction(EstablecimientoState state, LoadEstablecimientosForSelecterSuccessAction action) =>
            state with { IsLoading = false, EstablecimientoListForSelecter = action.Establecimientos ?? new List<GeoDroid.Data.Establecimiento>() };

        // Reducer for failed loading of establecimientos
        [ReducerMethod]
        public static EstablecimientoState OnLoadEstablecimientosFailureAction(EstablecimientoState state, LoadEstablecimientosFailureAction action) =>
            state with { IsLoading = false, CurrentError = action.ErrorMessage };

        [ReducerMethod]
        public static EstablecimientoState OnLoadEstablecimientosByDateFailureAction(EstablecimientoState state, LoadEstablecimientosByDateFailureAction action) =>
            state with { IsLoading = false, CurrentError = action.ErrorMessage };

        [ReducerMethod]
        public static EstablecimientoState OnLoadEstablecimientosForSelecterFailureAction(EstablecimientoState state, LoadEstablecimientosForSelecterFailureAction action) =>
            state with { IsLoading = false, CurrentError = action.ErrorMessage };

        // Reducer for selecting an establecimiento
        [ReducerMethod]
        public static EstablecimientoState OnSelectEstablecimientoAction(EstablecimientoState state, SelectEstablecimientoAction action) =>
            state with { EstablecimientoSelected = action.SelectedEstablecimiento };

        // Reducer for clearing the selected establecimiento
        [ReducerMethod]
        public static EstablecimientoState OnClearSelectedEstablecimientoAction(EstablecimientoState state, ClearSelectedEstablecimientoAction action) =>
            state with { EstablecimientoSelected = null };

        // Reducer for adding an establecimiento to the list (immutable way)
        [ReducerMethod]
        public static EstablecimientoState OnAddEstablecimientoToListAction(EstablecimientoState state, AddEstablecimientoToListAction action)
        {
            if (action.EstablecimientoToAdd == null) return state; // Or throw an error

            var currentList = state.EstablecimientoList ?? new List<GeoDroid.Data.Establecimiento>();
            if (currentList.Any(e => e.id == action.EstablecimientoToAdd.id)) // Assuming 'id' is the unique identifier
            {
                return state; // Already exists, return current state
            }

            var newList = new List<GeoDroid.Data.Establecimiento>(currentList) { action.EstablecimientoToAdd };
            return state with { EstablecimientoList = newList, IsLoading = false }; // Ensure IsLoading is reset if this action implies an update
        }

        // Reducer for resetting the state
        [ReducerMethod]
        public static EstablecimientoState OnResetEstablecimientoStateAction(EstablecimientoState state, ResetEstablecimientoStateAction action) =>
            new EstablecimientoState(); // Returns a new default state instance
    }
}
