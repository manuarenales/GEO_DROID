using Fluxor;
using GeoDroid.Data;


namespace GEO_DROID.Store.Maquinas
{

    [FeatureState]
    public record MaquinasState
    {
        private MaquinasState() { }

        public Maquina MaquinaSelected { get; init; }

        public List<Maquina> MaquinaListSelected { get; init; }

        public List<Maquina> MaquinaListSelecterSelected { get; init; }

    }
}
