//using GeoDroid.Data;

//namespace GEO_DROID.Services
//{
//    public class LecturaContadorDetalleService
//    {

//        public LecturaContadorDetalleService()
//        {

//        }

//        public void Reload(List<LecturaDetalle> LecturasDetalles)
//        {
//            foreach (LecturaDetalle LecturaDetalle in LecturasDetalles)
//            {
//                _dbContext.Entry(LecturaDetalle).Reload();
//            }
//        }

//        public async Task<int> SaveLecturasContadorDetalle(LecturaDetalle item)
//        {
//            try
//            {
//                var existing = await _dbContext.LecturasDetalle.FirstOrDefaultAsync(m => m.id == item.id);
//                if (existing != null)
//                {
//                    var entry = _dbContext.Entry(item);
//                    entry.State = EntityState.Modified;
//                    _dbContext.LecturasDetalle.Update(item);
//                }
//                else
//                {
//                    item.PatronContadorDetalle = null;
//                    _dbContext.LecturasDetalle.Add(item);
//                }
//                return await _dbContext.SaveChangesAsync();
//            }
//            catch (Exception ex)
//            {

//                throw;
//            }

//        }


//        public async Task<List<LecturaDetalle>> GetLecturaDetalleByLecturaContadorAsync(int idLecturaContador)
//        {

//            List<LecturaDetalle> ses = await _dbContext.LecturasDetalle.ToListAsync();
//            return await _dbContext.LecturasDetalle.Where(ld => ld.idLecturaContadores == idLecturaContador).Include(ld => ld.PatronContadorDetalle).ToListAsync();
//        }

//        public async Task<List<LecturaDetalle>> GetLecturaDetalleAsync()
//        {
//            return await _dbContext.LecturasDetalle.ToListAsync();
//        }
//    }
//}
