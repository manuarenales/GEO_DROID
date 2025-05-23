//using GeoDroid.Data;

//namespace GEO_DROID.Services
//{
//    public class PatronContadorService
//    {
//        public PatronContadorService()
//        {
//        }
//        public async Task<List<PatronContador>> GetPatronesAsync()
//        {
//            return await _dbContext.PatronesContador.ToListAsync();
//        }
//        public async Task<int> DeletePatronAsync(PatronContador item)
//        {
//            _dbContext.PatronesContador.Remove(item);
//            return await _dbContext.SaveChangesAsync();
//        }
//        public async Task<PatronContador> GetPatronContadorById(int? id)
//        {
//            return _dbContext.PatronesContador.Where(pc => pc.id == id).FirstOrDefault();
//        }
//        public async Task<int> SavePatronAsync(PatronContador item)
//        {
//            var existing = await _dbContext.PatronesContador.FirstOrDefaultAsync(e => e.id == item.id);
//            try
//            {
//                if (existing != null)
//                {
//                    _dbContext.PatronesContador.Update(item);
//                }
//                else
//                {
//                    _dbContext.PatronesContador.Add(item);
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
