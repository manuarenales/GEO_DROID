﻿
using GEO_DROID.Store.Confirmation;
using IDispatcher = Fluxor.IDispatcher;

namespace GEO_DROID.Store
{
    public static class FluxorConfirmationExtensions
    {

        public static Task<bool> ShowConfirmationAsync(this IDispatcher dispatcher, string title, string message)
        {
            var dialog = new ConfirmationDialogRequest(title, message);
            dispatcher.Dispatch(new ShowConfirmationDialogAction(dialog));
            return dialog.CompletionSource.Task;
        }


    }
}
