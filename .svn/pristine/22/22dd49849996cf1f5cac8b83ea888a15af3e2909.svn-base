using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEO_DROID.Resources.Lib.Extra
{
    public partial class Protocol
    {
        public static class Response
        {
            public const int Cmd = 1;
            public const int Ok = 0x0;
            public const int Error = 0xF;
        }

        // 1.x / Hardware
        public static class Hardware
        {
            public const int Cmd = 1;

            public const int Discovery = 1;   // Enviar discoverys
            public const int QueryDatos = 2;   // Solicitar datos de modulo
            public const int EnviarDatos = 3;   // Enviar datos
            public const int Reconnect = 4;   // Enviar datos de IP:Puerto a conectar
            public const int BootLoader = 5;   // Entrar en bootloader
            public const int SetFlag = 6;   // Establecer Flags (conexion, Timeout)
            public const int Ping = 7;   // Ping
        }

        //  2.x / Bootloader
        public static class Bootloader
        {
            public const int Cmd = 2;
            public const int Ok = 0x0;
            public const int Error = 0xF;

            public const int Delete = 1;
            public const int Grabar = 2;
            public const int Jumper = 3;
            public const int Ejecutar = 4;
            public const int Reset = 5;

            public const int ReadData = 0xA;
            public const int ClearData = 0x8;
            public const int WriteData = 0x9;
        }

        // 3.x / Modulo
        public static class Modulo
        {
            public const int Cmd = 3;
            public const int Ok = 0x0;
            public const int Error = 0xF;

            public const int Setup = 1;                 // Configurar parametros
            public const int SetupFlagContadores = 1;   // Flag Contadores
            public const int SetupFlagEchoRecaudacion = 2;   // Flag Echo Recaudacion

            public const int ContadoresQuery = 2;
            public const int ContadoresSend = 3;

            public const int EchoRecaudacion = 4;

        }


    }

}
