using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEO_DROID.Platforms.Android.Handlers
{
    public interface IBluetoothService
    {
        bool PairDevice();
        void ConnectToDevice(string address);
        void DisconnectDevice();
        string ReadData();
        void WriteData(string data);

    }
}
