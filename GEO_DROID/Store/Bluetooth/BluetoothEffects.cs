using Fluxor;
using Microsoft.JSInterop;




namespace GEO_DROID.Store.Bluetooth
{
    public class BluetoothEffects
    {
        private IJSRuntime _JS;
        public BluetoothEffects(IJSRuntime JS)
        {
            _JS = JS;
        }


        [EffectMethod]
        public async Task SaveBluetoothDevice(SaveBluetoothDevice action, Fluxor.IDispatcher dispatcher)
        {
            // var json = JsonSerializer.Serialize(action.device);
            // await _JS.InvokeVoidAsync("localStorage.setItem", "lastConnectedDevice", json);



        }

        [EffectMethod]
        public async Task LoadLastBluetoothDevice(LoadLastBluetoothDevice action, Fluxor.IDispatcher dispatcher)
        {

            // var json = _JS.InvokeAsync<string>("localStorage.getItem", "lastConnectedDevice"); 



        }


    }


    public class BluetoothDevice
    {

        public string Address { get; set; }

        public string DeviceName { get; set; }

        public BluetoothDevice() { }

    }


}
