using Fluxor;
using InTheHand.Net.Sockets;
using Microsoft.JSInterop;


namespace GEO_DROID.Store.Bluetooth
{
    [FeatureState]
    public record BluetoothState
    {
        private BluetoothState()
        {

        }

        public BluetoothDeviceInfo bluetoothDeviceInfoSaved { get; init; }
    }
}
