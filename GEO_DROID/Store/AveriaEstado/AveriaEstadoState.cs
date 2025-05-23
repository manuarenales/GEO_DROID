using Fluxor;
using GeoDroid.Data;

namespace GEO_DROID.Store.AveriaEstado
{


    [FeatureState]
    public record AveriaEstadoState
    {
        private AveriaEstadoState() { }

        public GeoDroid.Data.AveriaEstado AveriaEstadoSelected { get; init; }

        public List<GeoDroid.Data.AveriaEstado> AveriaEstadoListSelected { get; init; }

        public List<GeoDroid.Data.AveriaEstado> AveriaEstadoListSelecterSelected { get; init; }

    }
}
