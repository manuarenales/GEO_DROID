
using Fluxor;

namespace GEO_DROID.Store.Configuration
{

    [FeatureState]
    public record ConfigurationState
    {
        public GeoDroid.Data.Configuration ConfigurationSelected { get; init; }
    }
}
