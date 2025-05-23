using Fluxor;
using GeoDroid.Data;


namespace GEO_DROID.Store.LecturaDetalles
{
    [FeatureState]
    public record LecturaDetalleState
    {

        public Dictionary<PatContDetalle, GeoDroid.Data.LecturaDetalle> LecturaDetallesSelected { get; init; }

    }
}
