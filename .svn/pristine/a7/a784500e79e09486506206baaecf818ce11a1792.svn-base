//using GeoDroid.Data;


//namespace GEO_DROID.Services
//{
//    public class PatronContadorDetalleService
//    {
//        public PatronContadorDetalleService()
//        {
//         }
//        public IQueryable<PatronContadorDetalle> GetPatronContadorDetaleFromPatronContadorId(int patronContadorId)
//        {
//            return _dbContext.PatronesContadoresDetalle.Where(pcd => pcd.idPatronContador == patronContadorId).AsQueryable();
//        }
//        public async Task<int> SavePatronDetalleAsync(PatronContadorDetalle item)
//        {
//            var existing = await _dbContext.PatronesContadoresDetalle.FirstOrDefaultAsync(e => e.id == item.id);
//            try
//            {
//                if (existing != null)
//                {
//                    _dbContext.PatronesContadoresDetalle.Update(item);
//                }
//                else
//                {
//                    _dbContext.PatronesContadoresDetalle.Add(item);
//                }
//                return await _dbContext.SaveChangesAsync();
//            }
//            catch (Exception ex)
//            {
//                throw;
//            }
//        }
//    }
//}
