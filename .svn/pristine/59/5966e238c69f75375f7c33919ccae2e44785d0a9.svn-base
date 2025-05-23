using Fluxor;
using System.Globalization;

namespace GEO_DROID.Store.CultureCase
{
    public record CultureState(CultureInfo CurrentCulture);

    public class CultureFeature : Feature<CultureState>
    {
        public override string GetName() => "Culture";
        protected override CultureState GetInitialState() =>
            new CultureState(CultureInfo.CurrentUICulture);
    }

}
