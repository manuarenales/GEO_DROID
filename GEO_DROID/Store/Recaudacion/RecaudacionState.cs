using Fluxor;
using GeoDroid.Data;


namespace GEO_DROID.Store.Recaudacion
{
    [FeatureState]
    public record RecaudacionState
    {
        public List<GeoDroid.Data.Recaudacion> RecaudacionListSelected { get; init; }

        public GeoDroid.Data.Recaudacion RecaudacionSelected { get; init; }

        public Dictionary<PatContDetalle, GeoDroid.Data.LecturaDetalle> PatronLecturaContador { get; init; }

        public LecturaContador LecturaContador { get; init; }
    }
}
