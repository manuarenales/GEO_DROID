﻿using Fluxor;
using GEO_DROID.Database.Models.DTO;
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

                //Tenemos que cargar las rutas que implican los establecimientos por recaudar  
                var startDate = action.date.Value.Date;
                var endDate = startDate.AddDays(1);
                List<Ruta> rutas = await _database._database
                    .Table<Ruta>()
                    .Where(r => r.fecha >= startDate && r.fecha < endDate)
                    .ToListAsync();

                foreach (Ruta ruta in rutas)
                {
                    ruta.Establecimiento = await _database._database.Table<GeoDroid.Data.Establecimiento>().Where(e => e.id == ruta.idEstablecimiento).FirstOrDefaultAsync();
                }
                //----------------------------------------------------------------------------------


                //Buscamos las averias que tengo asignadas  Creamos las rutas desde Averias
                GeoDroid.Data.Configuration config = await _database._database.Table<GeoDroid.Data.Configuration>().FirstOrDefaultAsync();
                if (config != null)
                {
                    List<Averia> averiasToadd = await _database._database.Table<Averia>().Where(a => a.idPersonal == config.idPersonal).ToListAsync();
                    //añadimos las averias 
                    foreach (Averia averia in averiasToadd)
                    {
                        averia.Incidencia = await _database._database.Table<Incidencia>().Where(i => i.id == averia.idIncidencias).FirstOrDefaultAsync();
                        //añadimos la maquina a la incidencia
                        if (averia.Incidencia != null)
                        {
                            averia.Incidencia.maquina = await _database._database.Table<Maquina>().Where(m => m.id == averia.Incidencia.idMaquinas).FirstOrDefaultAsync();
                        }
                        //añadimos ybuscamos el establecimiento 
                        if (averia.Incidencia.maquina != null)
                        {
                            averia.Incidencia.maquina.establecimiento = await _database._database.Table<GeoDroid.Data.Establecimiento>().Where(e => e.id == averia.Incidencia.maquina.idEstablecimiento).FirstOrDefaultAsync();
                        }
                    }

                    foreach (Averia item in averiasToadd)
                    {
                        Ruta rutaRepetida = rutas.Where(r => r.Establecimiento.id == item.Incidencia.maquina.establecimiento.id).FirstOrDefault();

                        if (rutaRepetida != null)
                        {
                            if (rutaRepetida.Averia is null)
                            {
                                rutaRepetida.Averia = new List<Averia>();
                            }
                            rutaRepetida.Averia.Add(item);
                        }
                        else
                        {

                            Ruta rutatoadd = new Ruta();
                            if (rutatoadd.Averia is null)
                            {
                                rutatoadd.Averia = new List<Averia>();
                            }
                            rutatoadd.Averia.Add(item);
                            rutatoadd.idEstablecimiento = item.Incidencia.maquina.establecimiento.id;
                            rutatoadd.Establecimiento = item.Incidencia.maquina.establecimiento;
                        }
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
                    fecha = DateTime.Now.Date
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
