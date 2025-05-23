
#if ANDROID 

using Android.App;
using Android.Bluetooth;
using Android.Runtime;
using Android.Util;
using BLL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.LeerInfoMaquina
{

    public enum TipoDispositivoBTEnum { sis };
    public class ComunicacionBT : Comunicacion
    {
        static public Comunicacion GetComunicacionBT(string mac, TipoDispositivoBTEnum tipoDispositivo, String pin = "")
        {
            return new ComunicacionBT(mac, tipoDispositivo, pin);
        }

        public ComunicacionBT(string mac, String pin)
        {
            Debug.Assert(mac != string.Empty /*&& pin != string.Empty*/);

            _mac = mac;
            _pin = pin;
            /*_bthAdapter = null;
            _bthDevice = null;
            _bthSocket = null;
            _threadReader=null;*/
        }

        public ComunicacionBT(string mac, TipoDispositivoBTEnum tipoDispositivo, String pin = "")
        {
            Debug.Assert(mac != string.Empty /*&& pin != string.Empty*/);

            _mac = mac;
            _pin = pin;
            /* _bthAdapter = null;
             _bthDevice = null;
             _bthSocket = null;
             _threadReader = null;*/

            _btt = (BTUtils.BluetoothConnectionType)tipoDispositivo;
        }



        public bool Conectar(BTUtils.BluetoothConnectionType btt, uint retryCount, IProgressCallback callback)
        {
            _btt = btt;
            _retryCount = retryCount;
            return Conectar(callback);
        }

        /// <summary>
        /// Tries to connect using any method and finishes when on read succesfully
        /// </summary>
        /// <returns></returns>
        public bool ConnectarTodos()
        {
            bool retVal = false;

            if (Conectar(BTUtils.BluetoothConnectionType.OfficialBTCon, 5, null) == true)
                retVal = true;
            else if (Conectar(BTUtils.BluetoothConnectionType.ReflexedBTCon, 5, null) == true)
                retVal = true;
            else if (Conectar(BTUtils.BluetoothConnectionType.InsecureBTCon, 5, null) == true)
                retVal = true;

            return retVal;
        }

        public override bool Conectar(IProgressCallback callback)
        {
            Log.Info("Info", "Starting connection...");

            for (int i = 0; i < 2/*_retryCount*/; ++i)
            {
                try
                {
                    BTUtils.Connect(_activity, _mac, true, _pin);
                    BTUtils.StartReading(_onDataRead);
                    return true;
                }
                catch (Java.IO.IOException e)
                {
                    //ResetAdapter();
                    e.PrintStackTrace();
                    Log.Info("Info", "Socked could not connect. Cause:" + e.Message + ". Retry " + i);
                }
            }
            return false;
        }

        protected void _onDataRead(byte[] readData, int numReadBytes)
        {
            for (int i = 0; i < numReadBytes; i++)
                _bufferLectura.Append((char)readData[i]);
        }


        public override bool Desconectar(IProgressCallback callback)
        {
            BTUtils.EndReading();
            return true;
        }


        public override bool EnviarDatos(StringBuilder datos)
        {
            try
            {
                _bufferLectura.Length = 0;

                int len = datos.Length;
                byte[] _buff = new byte[len];
                for (int i = 0; i < len; i++)
                    _buff[i] = (byte)datos[i];

                BTUtils.SendData(_buff);

                return true;
            }
            catch (Exception ex)
            {
                _error = ex.Message;
            }
            return false;

        }

        public override bool IsReady()
        {
            return true;
        }

        public override string Estado()
        {
            return "";
        }

        public void SetActivity(Android.App.Activity activity)
        {
            _activity = activity;
        }


        string _mac;
        string _pin;
        BTUtils.BluetoothConnectionType _btt = BTUtils.BluetoothConnectionType.InsecureBTCon;

        IProgressCallback _progressCallback;

        Android.App.Activity _activity;

        uint _retryCount = 1;
    }

}
#endif