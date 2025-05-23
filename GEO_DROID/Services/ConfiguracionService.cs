//using GeoDroid.Data;


//namespace GEO_DROID.Services
//{
//    public class ConfiguracionService
//    {
//        public ConfiguracionService()
//        {

//        }
//        public async Task<Configuration> GetConfiguracionAsync()
//        {
//            Configuration config = await _dbContext.Configuration.FirstOrDefaultAsync();

//            if (config == null)
//            {
//                return new Configuration()
//                {
//                    password = "",
//                    unitNumber = 0,
//                    ip = "",
//                    port = 0,
//                    syncCompressionEnabled = false,
//                    printerMAC = "",
//                    printSignature = false,
//                    printTwoCopies = false,
//                    idPersonal = 0,
//                    serverDate = DateTime.Now,
//                    userName = ""
//                };
//            }
//            else
//            {
//                return config;
//            }

//        }
//        public async Task<int> SaveConfigurationAsync(Configuration item)
//        {
//            try
//            {
//                var existingMaquina = await _dbContext.Configuration.FirstOrDefaultAsync(m => m.id == item.id);
//                if (existingMaquina != null)
//                {
//                    _dbContext.Configuration.Update(item);
//                }
//                else
//                {

//                    _dbContext.Configuration.Add(item);


//                }
//                return await _dbContext.SaveChangesAsync();

//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }
//        }
//    }
//}
