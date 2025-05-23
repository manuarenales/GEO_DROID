
using Fluxor;
using Microsoft.AspNetCore.Components;
using System.Globalization;


namespace GEO_DROID.Store.CultureCase
{

    public static class CultureReducer
    {
        // [ReducerMethod(typeof(ChangeCultureAction))] --> para cuando no necesito Carga de datos 
        [ReducerMethod]
        public static CultureState CultureStateChangedAction(CultureState cultureState, ChangeCultureAction action)
        {
            Thread.CurrentThread.CurrentCulture = action.culture;
            Thread.CurrentThread.CurrentUICulture = action.culture;
            CultureInfo.DefaultThreadCurrentCulture = action.culture;
            CultureInfo.DefaultThreadCurrentUICulture = action.culture;

            return new CultureState(action.culture);

        }

    }
}
