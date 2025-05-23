using Fluxor;
using GEO_DROID.Store.Forms;
using GEO_DROID.Store.Rutas;
using GeoDroid.Data;
using GeoDroid.Data.SQL;
using IDispatcher = Fluxor.IDispatcher;

namespace GEO_DROID.Store.LecturaDetalle
{
    public class LecturaDetalleEffects
    {

        private readonly GeoDroidDatabase _database;
        private readonly IState<AveriaFormState> _averiaFormState;
        private readonly IState<RutasState> _RutasStateState;


        public LecturaDetalleEffects(GeoDroidDatabase database, IState<AveriaFormState> averiaFormState, IState<RutasState> RutasStateState)
        {
            _database = database;
            _averiaFormState = averiaFormState;
            _RutasStateState = RutasStateState;
        }


        [EffectMethod]
        public async Task LoadLecturaDetalleForAveriaForm(LoadLecturaDetalleSelected action, IDispatcher dispatcher)
        {
            try
            {
                Dictionary<PatContDetalle, GeoDroid.Data.LecturaDetalle> salida = new Dictionary<PatContDetalle, GeoDroid.Data.LecturaDetalle>();
                //Miro si la incidencia tiene LecturaContador
                LecturaContador lecturaContador = await _database._database.Table<GeoDroid.Data.LecturaContador>().Where(lc => lc.idIncidencias == action.incidencia.id).FirstOrDefaultAsync();
                //Si no tiene lectura contador tenemos que crear la lecturacontador 
                if (lecturaContador is null)
                {

                    lecturaContador = new LecturaContador();
                    lecturaContador.idIncidencias = action.incidencia.id;

                    List<GeoDroid.Data.LecturaDetalle> lecturas = new List<GeoDroid.Data.LecturaDetalle>();
                    // ahora creas las lecturas detalle dependiendo de los patron detalle que tiene la maquina 
                    foreach (PatContDetalle item in action.PatronContador)
                    {

                        GeoDroid.Data.LecturaDetalle newlectura = new GeoDroid.Data.LecturaDetalle();
                        newlectura.idPatContDetalles = item.id;
                        //esto todabia no tiene ID ojo cuidao se crea cuando se guarda;
                        newlectura.idLecturaContadores = lecturaContador.id;

                        salida.Add(item, newlectura);
                    }
                }
                else
                {
                    List<GeoDroid.Data.LecturaDetalle> lecturasnew = await _database._database.Table<GeoDroid.Data.LecturaDetalle>().Where(lcd => lcd.idLecturaContadores == lecturaContador.id).ToListAsync();

                    foreach (GeoDroid.Data.LecturaDetalle detalle in lecturasnew)
                    {
                        PatContDetalle patron = await _database._database.Table<PatContDetalle>().Where(pat => pat.id == detalle.idPatContDetalles).FirstOrDefaultAsync();
                        salida.Add(patron, detalle);
                    }
                }
                dispatcher.Dispatch(new changeLecturasDetalleSelected(salida, lecturaContador.id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
