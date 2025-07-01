using Fluxor;


namespace GEO_DROID.Store.ModuloCaptura
{
    [FeatureState]
    public record ModuloCapturaState
    {
        private ModuloCapturaState() { }
        public GeoDroid.Data.ModuloCaptura ModuloCapturaSelected { get; init; }
    }

}
