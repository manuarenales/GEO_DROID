//using GeoDroid.Data;
//using GeoDroid.Data.SQL;

//namespace GEO_DROID.Services
//{
//    public class CargaService
//    {
//        GeoDroidDatabase _sqlService;
//        public CargaService(GeoDroidDatabase sqlService)
//        {
//            _sqlService = sqlService;
//        }

//        public async Task<List<Carga>> GetCargasAsync()
//        {
//            return await _sqlService.GetCargasToListAsync();
//        }

//        public Carga GetCargaById(int id)
//        {
//            return _sqlService.GetCargaByID(id);
//        }

//        public async Task<Carga> SaveAveriaAsync(Carga item)
//        {
//            try
//            {
//                await _sqlService.SaveEntity(item);
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.ToString());
//                throw;
//            }
//        }

//        public async Task<int> DeleteAveriaAsync(Carga item)
//        {
//            _sqlService.RemoveEntity(item);
//            return await _dbContext.SaveChangesAsync();
//        }

//        public async Task<Carga> GetCargaByAveriaId(int idAveria)
//        {
//            return await _dbContext.Cargas.Where(c => c.Averia.id == idAveria)
//                .Include(a => a.Incidencia)
//                .ThenInclude(i => i.maquina)
//                .ThenInclude(i => i.establecimiento)
//                .Include(a => a.Averia).FirstOrDefaultAsync();
//        }

//        public async Task<int> UpdateCarga(Carga carga)
//        {
//            try
//            {
//                _dbContext.Cargas.Update(carga);
//                return await _dbContext.SaveChangesAsync();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.ToString());
//                throw;
//            }

//        }

//        public async Task<int> CreateCarga(Carga carga)
//        {
//            try
//            {
//                _dbContext.Cargas.Add(carga);
//                return await _dbContext.SaveChangesAsync();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.ToString());
//                throw;

//            }

//        }

//        public void Reload(Carga carga)
//        {
//            _dbContext.Entry(carga).Reload();
//        }
//    }
//}
