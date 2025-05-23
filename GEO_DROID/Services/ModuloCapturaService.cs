//using GeoDroid.Data;

//namespace GEO_DROID.Services
//{
//    public class ModuloCapturaService
//    {
//        public ModuloCapturaService()
//        {
//        }
//        public async Task<ModuloCaptura> SaveModuloCapturaAsync(ModuloCaptura item)
//        {
//            ModuloCaptura exist = await _dbContext.ModulosCaptura.FirstOrDefaultAsync(e => e.id == item.id);
//            try
//            {
//                if (exist != null)
//                {
//                    _dbContext.ModulosCaptura.Update(item);
//                }
//                else
//                {
//                    _dbContext.ModulosCaptura.Add(item);
//                }
//                await _dbContext.SaveChangesAsync();
//                await _dbContext.Entry(item).ReloadAsync();
//                return item;
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.ToString());
//                throw;
//            }
//        }
//    }
//}
