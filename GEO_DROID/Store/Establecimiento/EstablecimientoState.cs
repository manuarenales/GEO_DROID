using Fluxor;
using System.Collections.Generic; // Required for List

namespace GEO_DROID.Store.Establecimiento // Changed namespace to be more specific
{
    [FeatureState(Name = "Establecimiento")] // Explicitly naming the feature state can be good practice
    public record EstablecimientoState
    {
        public bool IsLoading { get; init; }
        public string CurrentError { get; init; }
        public GeoDroid.Data.Establecimiento EstablecimientoSelected { get; init; }
        public List<GeoDroid.Data.Establecimiento> EstablecimientoList { get; init; } // Renamed for clarity
        public List<GeoDroid.Data.Establecimiento> EstablecimientoListForSelecter { get; init; } // Renamed for clarity

        // Parameterless constructor for Fluxor initialization
        public EstablecimientoState()
        {
            IsLoading = false;
            CurrentError = string.Empty;
            EstablecimientoSelected = null;
            EstablecimientoList = new List<GeoDroid.Data.Establecimiento>();
            EstablecimientoListForSelecter = new List<GeoDroid.Data.Establecimiento>();
        }

        // Optional: Parameterized constructor for creating specific states if needed
        public EstablecimientoState(
            bool isLoading,
            string currentError,
            GeoDroid.Data.Establecimiento establecimientoSelected,
            List<GeoDroid.Data.Establecimiento> establecimientoList,
            List<GeoDroid.Data.Establecimiento> establecimientoListForSelecter)
        {
            IsLoading = isLoading;
            CurrentError = currentError;
            EstablecimientoSelected = establecimientoSelected;
            EstablecimientoList = establecimientoList ?? new List<GeoDroid.Data.Establecimiento>();
            EstablecimientoListForSelecter = establecimientoListForSelecter ?? new List<GeoDroid.Data.Establecimiento>();
        }
    }
}
