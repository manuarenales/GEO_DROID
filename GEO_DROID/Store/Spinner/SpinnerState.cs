using Fluxor;

namespace GEO_DROID.Store.Spinner
{

    [FeatureState]
    public record SpinnerState
    {
        public bool IsVisible { get; init; }
        public string Title { get; init; }
        public string Message { get; init; }
    }
}
