
using IDispatcher = Fluxor.IDispatcher;
using GEO_DROID.Services.SincroService;
using Fluxor;
using Microsoft.AspNetCore.Components;
using GEO_DROID.Store.Confirmation;
using Microsoft.FluentUI.AspNetCore.Components;
using GEO_DROID.Shared.Components.Dialogs;

namespace GEO_DROID.Store.Application
{
    public class AplicacionEffects
    {
        private SyncProcess _sincroService;
        private readonly NavigationManager _navigationManager;
        private readonly IState<ConfirmationState> confirmationState;
        private readonly IDialogService _dialogService;



        public AplicacionEffects(SyncProcess sincroservice, IState<ConfirmationState> ConfirmationState, NavigationManager navigationManager, IDialogService DialogService)
        {
            _sincroService = sincroservice;
            confirmationState = ConfirmationState;
            _navigationManager = navigationManager;
            _dialogService = DialogService;

        }

        [EffectMethod]
        public async Task SincroRequestAsync(SincronizationAction action, IDispatcher dispatcher)
        {
            GenericConfirmationDialog content = new()
            {
                Title = "Iniciar Sincronizacion?",
                PrimaryAction = "Aceptar",
                DismissTitle = "Cancelar",
            };

            DialogParameters parameters = new()
            {
                Title = content.Title,
                PrimaryActionEnabled = true,
                PrimaryAction = content.PrimaryAction,
                DismissTitle = content.PrimaryAction,
                TrapFocus = true,
                Modal = true,
                PreventScroll = true
            };

            IDialogReference dialog = await _dialogService.ShowDialogAsync<GenericConformationDialog>(content, parameters);
            DialogResult? result = await dialog.Result;

            if (result.Data is not null)
            {
                if (result.Cancelled)
                {
                    // Cancelar 
                }
                else
                {
                    //Aceptar
                    await _sincroService.StartAsync(null);
                }
            }
            else
            {
                //pulsa la x  n 
            }

        }

        [EffectMethod]
        public async Task ChangeModalEstablecimientoSelecter(LaunchEstablecimientoSelecter action, IDispatcher dispatcher)
        {
            //Lanzamos un pop up con el temita este de Modal 

            EstableciminetoSelecterDialog content = new()
            {
                Title = "Añadiendo Establecimiento",
                DismissTitle = "Cancelar",
            };

            DialogParameters parameters = new()
            {
                Title = content.Title,
                PrimaryAction = null,
                DismissTitle = content.PrimaryAction,
                Height = "90%",
                PrimaryActionEnabled = false,
                Width = "95%",
                TrapFocus = true,
                DialogBodyStyle = "max-height: 60vh; overflow-y: auto;",
                Modal = true,
            };

            IDialogReference dialog = await _dialogService.ShowDialogAsync<EstablecimientoSelecter>(content, parameters);

            dispatcher.Dispatch(new ChangeModalEstablecimientoSelecter(dialog));

            DialogResult? result = await dialog.Result;
            if (result.Data is not null)
            {
                if (result.Cancelled)
                {
                    // Cancelar 
                    dispatcher.Dispatch(new ChangeModalEstablecimientoSelecter(null));
                }
                else
                {
                    //Aceptar
                    await _sincroService.StartAsync(null);
                    dispatcher.Dispatch(new ChangeModalEstablecimientoSelecter(null));
                }
            }
            else
            {
                //pulsa la x  n 
                dispatcher.Dispatch(new ChangeModalEstablecimientoSelecter(null));
            }
        }

        [EffectMethod]
        public async Task ChangeModalMaquinaSelecter(LaunchMaquinaSelecter action, IDispatcher dispatcher)
        {
            //Lanzamos un pop up con el temita este de Modal 

            MaquinaSelecterDialog content = new()
            {
                Title = "Seleccion Maquina",
                DismissTitle = "Cancelar",
            };

            DialogParameters parameters = new()
            {
                Title = content.Title,
                PrimaryAction = null,
                DismissTitle = content.PrimaryAction,
                Height = "90%",
                PrimaryActionEnabled = false,
                Width = "95%",
                TrapFocus = true,
                DialogBodyStyle = "max-height: 60vh; overflow-y: auto;",
                Modal = true,
            };

            IDialogReference dialog = await _dialogService.ShowDialogAsync<MaquinaSelecter>(content, parameters);

            dispatcher.Dispatch(new ChangeModalMaquinaSelecter(dialog));

            DialogResult? result = await dialog.Result;
            if (result.Data is not null)
            {
                if (result.Cancelled)
                {
                    // Cancelar 
                    dispatcher.Dispatch(new ChangeModalMaquinaSelecter(null));
                }
                else
                {
                    //Aceptar
                    await _sincroService.StartAsync(null);
                    dispatcher.Dispatch(new ChangeModalMaquinaSelecter(null));
                }
            }
            else
            {
                //pulsa la x  n 
                dispatcher.Dispatch(new ChangeModalMaquinaSelecter(null));
            }
        }

        [EffectMethod]
        public async Task ChangeModalConceptoSelecter(LaunchConceptoSelecter action, IDispatcher dispatcher)
        {
            //Lanzamos un pop up con el temita este de Modal 

            ConceptoSelecterDialog content = new()
            {
                Title = "Seleccion Concepto",
                DismissTitle = "Cancelar",
            };

            DialogParameters parameters = new()
            {
                Title = content.Title,
                PrimaryAction = null,
                DismissTitle = content.PrimaryAction,
                Height = "90%",
                PrimaryActionEnabled = false,
                Width = "95%",
                TrapFocus = true,
                DialogBodyStyle = "max-height: 60vh; overflow-y: auto;",
                Modal = true,
            };

            IDialogReference dialog = await _dialogService.ShowDialogAsync<ConceptoSelecter>(content, parameters);

            dispatcher.Dispatch(new ChangeModalConceptoSelecter(dialog));

            DialogResult? result = await dialog.Result;
            if (result.Data is not null)
            {
                if (result.Cancelled)
                {
                    // Cancelar 
                    dispatcher.Dispatch(new ChangeModalConceptoSelecter(null));
                }
                else
                {
                    //Aceptar
                    await _sincroService.StartAsync(null);
                    dispatcher.Dispatch(new ChangeModalConceptoSelecter(null));
                }
            }
            else
            {
                //pulsa la x  n 
                dispatcher.Dispatch(new ChangeModalConceptoSelecter(null));
            }
        }

        [EffectMethod]
        public async Task ChangeModalConceptoSelecter(LaunchEstadoSelecter action, IDispatcher dispatcher)
        {
            //Lanzamos un pop up con el temita este de Modal 

            EstadoSelecterDialog content = new()
            {
                Title = "Seleccion Estado",
                DismissTitle = "Cancelar",
            };

            DialogParameters parameters = new()
            {
                Title = content.Title,
                PrimaryAction = null,
                DismissTitle = content.PrimaryAction,
                Height = "90%",
                PrimaryActionEnabled = false,
                Width = "95%",
                TrapFocus = true,
                DialogBodyStyle = "max-height: 60vh; overflow-y: auto;",
                Modal = true,
            };

            IDialogReference dialog = await _dialogService.ShowDialogAsync<EstadoSelecter>(content, parameters);

            dispatcher.Dispatch(new ChangeModalEstadoSelecter(dialog));

            DialogResult? result = await dialog.Result;
            if (result.Data is not null)
            {
                if (result.Cancelled)
                {
                    // Cancelar 
                    dispatcher.Dispatch(new ChangeModalEstadoSelecter(null));
                }
                else
                {
                    //Aceptar
                    await _sincroService.StartAsync(null);
                    dispatcher.Dispatch(new ChangeModalEstadoSelecter(null));
                }
            }
            else
            {
                //pulsa la x  n 
                dispatcher.Dispatch(new ChangeModalEstadoSelecter(null));
            }
        }

    }

}
