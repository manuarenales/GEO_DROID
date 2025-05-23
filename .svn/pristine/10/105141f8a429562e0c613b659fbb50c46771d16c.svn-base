using Fluxor;
 

namespace GEO_DROID.Store.Navegation
{
    [FeatureState]
    public record NavigationState
    {
        public string CurrentRoute { get; init; } = "/";
        public string? PendingRoute { get; init; }
        public Stack<string> History { get; init; } = new();
        public bool AllowNavigation { get; init; } = false;

        public bool NormalNavegation { get; init; } = false;

    }

}
