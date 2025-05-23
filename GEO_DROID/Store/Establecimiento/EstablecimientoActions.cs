using GeoDroid.Data;
using Microsoft.FluentUI.AspNetCore.Components;
using System.Collections.Generic;
using System; // Required for DateTime

namespace GEO_DROID.Store.Establecimiento
{
    // Actions to trigger loading data
    public record LoadEstablecimientosAction; // General purpose load
    public record LoadEstablecimientosByDateAction(DateTime Date); // If specific date filtering is needed
    public record LoadEstablecimientosForSelecterAction;

    // Actions for successful data loading
    public record LoadEstablecimientosSuccessAction(List<GeoDroid.Data.Establecimiento> Establecimientos);
    public record LoadEstablecimientosByDateSuccessAction(List<GeoDroid.Data.Establecimiento> Establecimientos, DateTime Date);
    public record LoadEstablecimientosForSelecterSuccessAction(List<GeoDroid.Data.Establecimiento> Establecimientos);

    // Actions for failed data loading
    public record LoadEstablecimientosFailureAction(string ErrorMessage);
    public record LoadEstablecimientosByDateFailureAction(string ErrorMessage, DateTime Date);
    public record LoadEstablecimientosForSelecterFailureAction(string ErrorMessage);

    // Action for changing the selected establecimiento
    public record SelectEstablecimientoAction(GeoDroid.Data.Establecimiento SelectedEstablecimiento);

    // Action for adding an establecimiento to a list (e.g., the main list)
    // Ensures immutability is handled correctly in the reducer.
    public record AddEstablecimientoToListAction(GeoDroid.Data.Establecimiento EstablecimientoToAdd);

    // Actions related to UI, like launching a selector dialog
    public record LaunchEstablecimientoSelecterAction;
    public record SetEstablecimientoSelecterDialogReferenceAction(IDialogReference DialogReference);

    // Action to clear the selected establecimiento
    public record ClearSelectedEstablecimientoAction;

    // Action to reset Establecimiento state to initial or default
    public record ResetEstablecimientoStateAction;
}