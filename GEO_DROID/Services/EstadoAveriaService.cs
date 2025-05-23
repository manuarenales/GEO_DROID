
//using GeoDroid.Data;


//namespace GEO_DROID.Services
//{
//    public class EstadoAveriaService
//    {
//        public EstadoAveriaService()
//        {

//        }

//        public async Task<List<AveriaEstado>> GetEstadoAveriaAsync()
//        {
//            return await _dbContext.AveriasEstados.ToListAsync();
//        }

//        public async Task<AveriaEstado> GetEstadoAveriaById(int? id)
//        {
//            return await _dbContext.AveriasEstados
//            .Where(a => a.id == id)
//            .FirstOrDefaultAsync();
//        }

//        public async Task<int> SaveEstadoAveriaAsync(AveriaEstado item)
//        {
//            _dbContext.AveriasEstados.Add(item);
//            return await _dbContext.SaveChangesAsync();

//        }

//        public async Task<int> DeleteEstadoAveriaAsync(AveriaEstado item)
//        {
//            _dbContext.AveriasEstados.Remove(item);
//            return await _dbContext.SaveChangesAsync();
//        }

//    }
//}
