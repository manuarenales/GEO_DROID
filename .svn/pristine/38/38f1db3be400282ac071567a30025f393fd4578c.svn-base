using Fluxor;
using GeoDroid.Data;
using GeoDroid.Data.SQL;
using IDispatcher = Fluxor.IDispatcher;

namespace GEO_DROID.Store.Rutas
{
    public class RutasEffects
    {
        private readonly GeoDroidDatabase _database;

        public RutasEffects(GeoDroidDatabase database)
        {
            _database = database;

        }

        [EffectMethod]
        public async Task GetRutasListByDate(GetRutasListByDate action, IDispatcher dispatcher)
        {
            try
            {
                List<GeoDroid.Data.Establecimiento> establecimientos = await _database._database.Table<GeoDroid.Data.Establecimiento>().ToListAsync();
                List<Ruta> rutas = await _database._database.Table<Ruta>().ToListAsync();

                rutas = rutas.Select(r =>
              {
                  r.Establecimiento = establecimientos.FirstOrDefault(e => e.id == r.idEstablecimiento);
                  return r;
              }).ToList();

                if (rutas.Count > 6)
                {
                    for (int i = rutas.Count - 1; i >= 0; i--)
                    {
                        await _database._database.DeleteAsync(rutas[i]);
                        rutas.RemoveAt(i);
                    }
                }

                dispatcher.Dispatch(new ChangeRutasListSelected(rutas));

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [EffectMethod]
        public async Task AddRuta(AddRuta action, IDispatcher dispatcher)
        {
            try
            {
                Ruta NewRute = new Ruta
                {
                    Establecimiento = action.establecimineto,
                    idEstablecimiento = action.establecimineto.id,
                    fecha = DateTime.Now
                };

                var rutasExistentes = await _database._database.Table<Ruta>()
                    .Where(r => r.idEstablecimiento == NewRute.idEstablecimiento)
                    .ToListAsync();

                bool yaExiste = rutasExistentes.Any(r =>
                    r.fecha.Date == NewRute.fecha.Date);

                if (!yaExiste)
                {
                    await _database._database.InsertAsync(NewRute);
                    Console.WriteLine("Ruta añadida");
                    dispatcher.Dispatch(new AddRutaTorutasSelected(NewRute));
                }
                else
                {
                    Console.WriteLine("Ya existe una ruta con ese establecimiento para la misma fecha.");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
    }
}
