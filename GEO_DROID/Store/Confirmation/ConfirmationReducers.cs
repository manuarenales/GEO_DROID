using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEO_DROID.Store.Confirmation
{
    public static class ConfirmationReducers
    {
        [ReducerMethod]
        public static ConfirmationState ReduceShow(ConfirmationState state, ShowConfirmationDialogAction action)
        {
            return state with
            {
                IsVisible = true,
                CurrentDialog = action.Dialog
            };
        }

        [ReducerMethod]
        public static ConfirmationState ReduceResponse(ConfirmationState state, ConfirmationResponseAction action)
        {
            if (state.CurrentDialog?.CompletionSource.Task.IsCompleted == true)
            {
                return state; // Si ya está completada, devolvemos el estado sin cambios
            }
            // Completar la tarea de confirmación 
            state.CurrentDialog?.CompletionSource.SetResult(action.Confirmed);

            return state with
            {
                IsVisible = false,
                CurrentDialog = null
            };
        }


    }

}
