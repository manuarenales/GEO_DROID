using GEO_DROID.Resources.Lib.Extra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEO_DROID.Resources.Lib.Interfaces
{
    public interface DeviceController : IDisposable
    {
        // Inicio y fin
        void Start();
        void Stop();
        bool isDead();

        //

        void attachTo(HardwareDevice hardwareDevice);
        HardwareDevice getDeviceAttached();
        bool canAttach();

        // Eventos A nivel de socket (DATA I/O) - Solo se usa para logear ya que la capa Socket gestiona los comandos
        void onSocketDataSent(byte[] data, int length);
        void onSocketDataReceived(byte[] data, int length);

        //  Eventos I/O a nivel de comando
        void onCommandReceive(Cmd omando);
        void onCommandLost(Cmd comando);
        void onCommandSent(Cmd comando);

        // Eventos del Ping
        void onPingSend();
        void onPingReceive();
        void onPingTimeout();

        // Eventos Sync
        void onRequestSyncData();


    }
}
