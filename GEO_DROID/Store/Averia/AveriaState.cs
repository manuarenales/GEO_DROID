using Fluxor;
using GeoDroid.Data;


namespace GEO_DROID.Store.AveriaCase
{
    [FeatureState]
    public record AveriaState
    {
        private AveriaState() { }

        public Averia AveriaSelected { get; init; }

        public List<Averia> AveriaListSelected { get; init; }

        public int AveriaCount { get; init; }


    }
}
