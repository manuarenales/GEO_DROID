using Fluxor;


namespace GEO_DROID.Store.Configuration
{
    public static class ConfigurationReducer
    {

        [ReducerMethod]
        public static ConfigurationState ConfigurationSelectedChangedAction(ConfigurationState state, ChangeConfigurationSelected action)
        {
            return state with { ConfigurationSelected = action.Configuration };
        }

    }
}
