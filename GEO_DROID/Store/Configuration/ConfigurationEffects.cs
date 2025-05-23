using Fluxor;
using GeoDroid.Data.SQL;
using IDispatcher = Fluxor.IDispatcher;

namespace GEO_DROID.Store.Configuration
{
    public class ConfigurationEffects
    {
        private readonly GeoDroidDatabase _database;
        private readonly IState<ConfigurationState> _ConfigurationState;

        public ConfigurationEffects(GeoDroidDatabase database, IState<ConfigurationState> ConfigurationState)
        {
            _database = database;
            _ConfigurationState = ConfigurationState;
        }

        [EffectMethod]
        public async Task GetCongiguration(GetCongiguration action, IDispatcher dispatcher)
        {
            try
            {
                GeoDroid.Data.Configuration config = await _database._database.Table<GeoDroid.Data.Configuration>().FirstOrDefaultAsync();

                if (config == null)
                {
                    config = new GeoDroid.Data.Configuration()
                    {
                        password = "",
                        unitNumber = 0,
                        ip = "",
                        port = 0,
                        syncCompressionEnabled = false,
                        printerMAC = "",
                        printSignature = false,
                        printTwoCopies = false,
                        idPersonal = 0,
                        serverDate = DateTime.Now,
                        userName = ""
                    };
                }

                dispatcher.Dispatch(new ChangeConfigurationSelected(config));
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        [EffectMethod]

        public async Task SaveConfiguration(SaveConfiguration action, IDispatcher dispatcher)
        {
            try
            {
                GeoDroid.Data.Configuration config = await _database._database.Table<GeoDroid.Data.Configuration>().FirstOrDefaultAsync();

                if (config == null)
                {
                    await _database._database.InsertAsync(action.Configuration);
                }
                else
                {
                    await _database._database.UpdateAsync(action.Configuration);
                }

                dispatcher.Dispatch(new ChangeConfigurationSelected(action.Configuration));
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
