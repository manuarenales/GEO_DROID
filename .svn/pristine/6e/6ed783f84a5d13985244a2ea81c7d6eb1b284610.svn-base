using Fluxor;
using GeoDroid.Data;


namespace GEO_DROID.Store.Forms
{
    public static class AveriaFormReducer
    {
        [ReducerMethod]
        public static AveriaFormState AveriaFormStateSelectedChangedAction(AveriaFormState state, ChangeAveriaSelectedFormAction action)
        {

            if (action.AveriaSelected is null)
            {
                return state with
                {
                    AveriaSelected = action.AveriaSelected,
                };
            }
            return state with { AveriaSelected = action.AveriaSelected };
        }

        [ReducerMethod]
        public static AveriaFormState changeLecturasDetalleSelectedForAveriasForm(AveriaFormState state, changeLecturasDetalleSelectedForAveriasForm action)
        {
            return state with { LecturaDetallesSelected = action.patronLecturas, lecturacontadorId = action.lecturacontadorId };
        }

        [ReducerMethod]
        public static AveriaFormState changeCargaSelectedForAveriasForm(AveriaFormState state, changeCargaSelectedForAveriasForm action)
        {
            return state with { CargaSelected = action.Carga };
        }

        [ReducerMethod]
        public static AveriaFormState MaquinaFormStateSelectedChangedAction(AveriaFormState state, ChangeMaquinaSelectedFormAction action)
        {
            //Averia 
            Averia averia = state.AveriaSelected;
            //Creo la Incidencia si no la tiene 
            if (averia.Incidencia == null)
            {
                averia.Incidencia = new Incidencia();

            }

            averia.Incidencia.maquina = action.MaquinaSelected;

            return state with { AveriaSelected = averia };
        }

        [ReducerMethod]
        public static AveriaFormState ConceptoAveriaFormStateSelectedChangedAction(AveriaFormState state, ChangeConceptoAveriaSelectedFormAction action)
        {
            Averia averia = state.AveriaSelected;
            averia.ConceptoAveria = action.ConceptoAveria;

            return state with { AveriaSelected = averia };
        }

        [ReducerMethod]
        public static AveriaFormState AveriaEstadoFormStateSelectedChangedAction(AveriaFormState state, ChangeAveriaEstadoSelectedFormAction action)
        {
            Averia averia = state.AveriaSelected;
            averia.AveriaEstado = action.AveriaEstadoSelected;

            return state with { AveriaSelected = averia };
        }

        [ReducerMethod]
        public static AveriaFormState ValidateAveriaFormState(AveriaFormState state, ValidateAveriaFormState action)
        {
            bool maquinaValid = false;
            bool conceptoValid = false;
            bool estadoValid = false;

            if (state.AveriaSelected.Incidencia is null)
            {
                maquinaValid = false;
            }
            else
            {
                if (state.AveriaSelected.Incidencia.maquina is not null) { maquinaValid = true; }
            }


            if (state.AveriaSelected.ConceptoAveria is not null)
            {
                conceptoValid = true;
            }
            else
            {
                conceptoValid = false;
            }

            if (state.AveriaSelected.AveriaEstado is not null)
            {
                estadoValid = true;
            }
            else
            {
                estadoValid = false;
            }

            if (maquinaValid == true && conceptoValid == true && estadoValid == true)
            {
                return state with { Valid = true, maquinaValid = maquinaValid, conceptoValid = conceptoValid, estadoValid = estadoValid };
            }
            else
            {
                return state with { Valid = false, maquinaValid = maquinaValid, conceptoValid = conceptoValid, estadoValid = estadoValid };
            }
        }

        [ReducerMethod]
        public static AveriaFormState ValidateAveriaFormStateMaquina(AveriaFormState state, ValidateAveriaFormStateMaquina action)
        {

            return state with { maquinaValid = true };
        }

        [ReducerMethod]
        public static AveriaFormState ValidateAveriaFormStateConceptoAveria(AveriaFormState state, ValidateAveriaFormStateConceptoAveria action)
        {
            //
            return state with { conceptoValid = true };
        }

        [ReducerMethod]
        public static AveriaFormState ValidateAveriaFormStateAveriaEstado(AveriaFormState state, ValidateAveriaFormStateAveriaEstado action)
        {

            return state with { estadoValid = true };
        }

        [ReducerMethod]
        public static AveriaFormState ResetAveriasForm(AveriaFormState state, ResetAveriasForm action)
        {

            return state with
            {
                AveriaSelected = null,
                CargaSelected = null,
                Valid = false,
                conceptoValid = false,
                estadoValid = false,
                maquinaValid = false,
                lecturacontadorId = 0,
                LecturaDetallesSelected = null,
            };
        }

        [ReducerMethod]
        public static AveriaFormState OnUpdateAveriaFormLecturaDetalleValueAction(AveriaFormState state, UpdateAveriaFormLecturaDetalleValueAction action)
        {
            var newLecturaDetallesSelected = state.LecturaDetallesSelected != null
                ? new Dictionary<PatContDetalle, GeoDroid.Data.LecturaDetalle>(state.LecturaDetallesSelected)
                : new Dictionary<PatContDetalle, GeoDroid.Data.LecturaDetalle>();

            GeoDroid.Data.LecturaDetalle detalleToUpdate;

            if (newLecturaDetallesSelected.TryGetValue(action.PatronDetalleKey, out var existingDetalle))
            {
                // existingDetalle is the reference from the new dictionary's copy (or original if not copied yet).
                // We will modify its properties directly.
                detalleToUpdate = existingDetalle;
            }
            else
            {
                detalleToUpdate = new GeoDroid.Data.LecturaDetalle
                {
                    // Assuming action.PatronDetalleKey.id (from SQLite PatContDetalle) is the intended FK
                    // for the EF Core PatronContadorDetalle. This is a critical assumption.
                    idPatContDetalles = action.PatronDetalleKey.id,
                    PatronContadorDetalle = null, // Cannot directly assign SQLite PatContDetalle to EF Core PatronContadorDetalle navigation property.
                    idLecturaContadores = state.lecturacontadorId,
                    valor = 0, // Initial default
                    valorAntes = 0, // Initial default
                    tieneAjuste = false
                };
            }

            if (action.IsValorAntes)
            {
                detalleToUpdate.valorAntes = action.NewValor;
            }
            else
            {
                detalleToUpdate.valor = action.NewValor;
            }

            // Determine if there's an adjustment
            detalleToUpdate.tieneAjuste = detalleToUpdate.valor != detalleToUpdate.valorAntes;

            newLecturaDetallesSelected[action.PatronDetalleKey] = detalleToUpdate;

            return state with { LecturaDetallesSelected = newLecturaDetallesSelected };
        }
    }

}

