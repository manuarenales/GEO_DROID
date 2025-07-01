using GEO_DROID.Store.Bluetooth;
using GEO_DROID.Store.Confirmation;
using GeoDroid.Data;
using InTheHand.Net.Sockets;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.FluentUI.AspNetCore.Components;
using System.Globalization;
namespace GEO_DROID.Store;

//-------------------------------------------------------------------------------------------------------------------//
public record ChangeCultureAction(CultureInfo culture);
public record ChangeAveriaSelectedAction(Averia averia);
public record changeCargaSelectedForAveriasForm(GeoDroid.Data.Carga? Carga);
public record changeLecturaSelectedForAveriasForm(LecturaContador lectura);
public record changeLecturaSelectedForRecaudacionForm(LecturaContador lectura);
public record ChangeEstablecimientoSelectedAction(GeoDroid.Data.Establecimiento establecimiento);
public record ChangeRutaSelectedAction(Ruta ruta);
public record ChangeMaquinaSelectedAction(Maquina maquina);
public record ChangeConceptoAveriaSelectedAction(ConceptoAveria ConceptoAveria);
public record ChangeEstablecimientosListSelected(List<GeoDroid.Data.Establecimiento> EstablecimientoList);
public record ChangeRutasListSelected(List<Ruta> RutaList);
public record ChangeConectedDevice(BluetoothDevice device);
public record ChangeConectedDeviceInfo(BluetoothDeviceInfo device);
public record ChangeEstablecimientosListForSelecter(List<GeoDroid.Data.Establecimiento> EstablecimientoList);
public record ChangeNavigationAction(string rute, bool replace = false);
public record ChangeAveriasListSelectedByEstablecimiento(List<Averia> averiaList);
//-------------------------------------------------------------------------------------------------------------------//
public record LaunchEstablecimientoSelecter();
public record LaunchMaquinaSelecter();
public record LaunchConceptoSelecter();
public record LaunchEstadoSelecter();
public record LaunchTestContadoresDialog();
//-------------------------------------------------------------------------------------------------------------------//
public record ChangeAveriaEstadoSelectedAction(GeoDroid.Data.AveriaEstado AveriaEstado);
public record ChangeRecaudacionSelectedAction(GeoDroid.Data.Recaudacion Recaudacion);
public record ChangeAveriaEstadoListForSelecter(List<GeoDroid.Data.AveriaEstado> AveriaEstadoList);
public record ChangeModalMaquinaSelecter(IDialogReference reference);
public record ChangeModalAveriaestadoSelecter(IDialogReference reference);
public record ChangeModalConceptoSelecter(IDialogReference reference);
public record ChangeModalEstadoSelecter(IDialogReference reference);
public record ChangeModalTestContadoresSelecter(IDialogReference reference);
public record ChangeModalEstablecimientoSelecter(IDialogReference reference);
public record ChangeSplashScreenSelecter(IDialogReference reference);
public record ChangeMaquinasListForSelecter(List<Maquina> MaquinaList);
public record ChangeMaquinasListSelected(List<Maquina> MaquinaList);
public record ChangeConceptoAveriaSelectedFormAction(ConceptoAveria ConceptoAveria);
public record ChangeAveriaEstadoSelectedFormAction(GeoDroid.Data.AveriaEstado AveriaEstadoSelected);
public record ChangeMaquinaSelectedFormAction(Maquina MaquinaSelected);
public record ChangeAveriaSelectedFormAction(Averia AveriaSelected);
public record ChangeIsNormalAction(bool Normal);
public record ChangeConfigurationSelected(GeoDroid.Data.Configuration Configuration);
public record changeLecturasDetalleSelectedForAveriasForm(Dictionary<PatContDetalle, GeoDroid.Data.LecturaDetalle> patronLecturas, int lecturacontadorId);
public record changeLecturasDetalleSelected(Dictionary<PatContDetalle, GeoDroid.Data.LecturaDetalle> patronLecturas, int lecturacontadorId);
public record ChangeAveriasCount(int averiacount);
public record changePatronContadorLecturasSelectedForAveriasForm(Dictionary<PatContDetalle, GeoDroid.Data.LecturaDetalle> patron);
public record ChangeNavigationHistory(string Rute, int action);
public record ChangeConceptoAveriasListForSelecter(List<ConceptoAveria> ConceptoAveriaList);
public record ChangeAllowNavegationAction(bool allowNavegate);
//-------------------------------------------------------------------------------------------------------------------//
public record GetEstablecimientosListByDate(DateTime date);
public record GetRutasListByDate(DateTime? date);
public record GetEstablecimientosList();
public record GetMaquinasList();
public record GetCongiguration();
public record GetMaquinasListFomEstablecimientoSelected(bool selecter);
public record GetAveriaEstadoList();
public record GetConceptoAveriaList();
public record GetAveriasCount(GeoDroid.Data.Establecimiento establecimiento);
public record GetAveriasByEstablecimiento(int establecimientoId);
//-------------------------------------------------------------------------------------------------------------------//
public record AddRutaTorutasSelected(Ruta NewRute);
public record AddRuta(GeoDroid.Data.Establecimiento establecimineto);
public record AddAveria(Averia averia);
public record AddEstablecimientoSelected(GeoDroid.Data.Establecimiento establecimiento);
//-------------------------------------------------------------------------------------------------------------------//
public record ShowConfirmationDialogAction(ConfirmationDialogRequest Dialog);
public record ConfirmationResponseAction(bool Confirmed);
public record SincronizationAction();
public record ShowSpinnerAction(string title, string message);
public record HideSpinnerAction();
public record SetCurrentRuteAction(string rute);
//-------------------------------------------------------------------------------------------------------------------//
public record NavigateBackAction(int steps);
public record NavigationAction(LocationChangingContext Context);
public record NavigationToAction(string Rute);
//-------------------------------------------------------------------------------------------------------------------//
public record ValidateAveriaFormState();
public record ValidateAveriaFormStateMaquina();
public record ValidateAveriaFormStateConceptoAveria();
public record ValidateAveriaFormStateAveriaEstado();
//-------------------------------------------------------------------------------------------------------------------//
public record CreateAveriaFromState();
public record LoadAveriaForm(Averia averia);
public record LoadLastBluetoothDevice();
public record LoadPatContForAveriaForm(Maquina maquina);
public record LoadCargaForAveriaForm(Averia averia);
public record LoadLecturaContadorForAveriaForm(Incidencia Incidencia);
public record LoadPatronLecturaContadorForAveriaForm(LecturaContador lectura, Incidencia Incidencia);
public record LoadLecturasDetalleForAveriaForm(Averia averia);
public record LoadLecturaDetalleForAveriaForm(List<PatContDetalle> PatronContador, Incidencia incidencia);
public record LoadLecturaDetalleSelected(List<PatContDetalle> PatronContador, Incidencia incidencia);
public record ResetAveriasForm();
public record UpdateAveriaFormLecturaDetalleValueAction(PatContDetalle PatronDetalleKey, long NewValor, bool IsValorAntes);
//--------------------------------------------------------------------------------------------------------------------//
public record DeleteAveria(Averia averia);
public record DeleteCarga(GeoDroid.Data.Carga carga);
public record DeleteLecturaForAveriasForm(Dictionary<PatContDetalle, GeoDroid.Data.LecturaDetalle> patronLecturas, LecturaContador lectura);
//--------------------------------------------------------------------------------------------------------------------//
public record SaveConfiguration(GeoDroid.Data.Configuration Configuration);
public record LimpiarHistorialBluetoothDevices();
public record LimpiarHistorialBluetoothDevice(BluetoothDevice device);
public record SaveBluetoothDevice(BluetoothDeviceInfo device);
public record SaveBluetoothDeviceBase(BluetoothDevice device);
public record LoadBluetoothDeviceHistorial();
public record SetBluetoothDeviceHistorial(List<BluetoothDevice> devicesHistorialList);
public record OpenSlashBluethooth(SplashScreenContent parametros);
public record CloseSlashDefault();
//--------------------------------------------------------------------------------------------------------------------//
public record GetModuloCapturaByMaquinaID(int MaquinaID);
public record ChangeModuloCapturaSelectedAction(GeoDroid.Data.ModuloCaptura ModuloCaptura);