using GEO_DROID.Resources.Lib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEO_DROID.Resources.Lib.Extra
{
    public class HardwareDevice
    {
        public GeodroidClientNode clientNode;
        public DeviceController controller;

        public string Id = string.Empty;        // Identificacor de hardware / IMEI / MAC
        public int Modelo = 0;                   // Version del modulo
        public int Firmware = 0;                   // Version de firmware

        public string ToStringId()
        {
            return null;
        }
    }
    public class GeodroidClientNode
    {
        public int TimeFromConnection;
        public int GetIoStats()
        {
            return 0;
        }
        public void sendCommand(Cmd comando)
        {

        }
    }
}
