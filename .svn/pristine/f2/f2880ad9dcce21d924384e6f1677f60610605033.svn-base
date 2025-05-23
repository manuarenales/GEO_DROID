using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GEO_DROID.Resources.Lib.Extra
{
    public static class Config
    {

        public static void SetupWorkingPath(string wp)
        {
            pathIni = Path.Combine(wp, "Ini");

            // Estos 2 paths luego se pueden modificar en el INI
            // de momento damos 2 valores por defecto
            pathLog = Path.Combine(wp, "Log");
            pathFirmware = Path.Combine(wp, "Firmware");

        }

        // Rutas
        private static string _pathFirmware = "";
        public static string pathFirmware
        {
            get { return _pathFirmware; }
            set
            {
                if (value != null)
                    _pathFirmware = createDirectory(value);
                else _pathFirmware = createDirectory("./Firmware");
            }
        }

        private static string _pathLog = "";
        public static string pathLog
        {
            get { return _pathLog; }
            set
            {
                if (value != null)
                    _pathLog = createDirectory(value);
                else _pathLog = createDirectory("./Log");
            }
        }

        private static string _pathIni = "";
        public static string pathIni
        {
            get { return _pathIni; }
            set
            {
                if (value != null)
                    _pathIni = createDirectory(value);
                else _pathIni = createDirectory("./Ini");
            }
        }


        public static string createDirectory(string v)
        {
            // Este metodo repara el path convirtiendo los ../../ a la ruta absoluta
            string path = Path.GetFullPath(v);

            // Si no existe lo creamos
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            // Devolvemos la ruta bien construida
            return path;
        }

        #region isLogEnabled

        public enum LogType
        {
            LogDiscovery,
            LogControlador,
            LogTraffic
        }

        public static bool isLogEnabled(string id, LogType tipo)
        {
            // buscamos el archivo id.ini
            FileIni ini = loadIni("id.ini");

            // Discovery / Trafico completo
            if (tipo == LogType.LogDiscovery)
                return ini.getBool(id + ".Discovery", "Global.Discovery", true);

            // Controlador
            if (tipo == LogType.LogControlador)
                return ini.getBool(id + ".Log", "Global.Log", true);

            // Traffic
            if (tipo == LogType.LogTraffic)
                return ini.getBool(id + ".Traffic", "Global.Traffic", false);

            return false;
        }

        #endregion


        #region Loader

        private static FileIni ini { get; set; }

        public static FileIni loadIni(string filename)
        {
            string fileini = Path.Combine(pathIni, filename);

            // Esta clase gestiona una pila statica de archivos, y cada vez que se pide comprueba
            // si ha cambiado para recargar
            return FileIniCache.load(fileini);
        }

        public static void load()
        {
            ini = loadIni("config.ini");

            // Cargamos rutas de configuracion
            pathLog = ini.get("Path.Log", pathLog);
            pathFirmware = ini.get("Path.Firmware", pathFirmware);
        }

        #region Load Ini Helpers
        public static string get(string key, string defecto = null)
        {
            if (ini != null)
                return ini.get(key, defecto);
            else return defecto;
        }

        public static bool getBool(string key, bool defecto = false)
        {
            if (ini != null)
                return ini.getBool(key, defecto);
            else return defecto;

        }

        public static int getInt(string key, int defecto = 0)
        {
            if (ini != null)
                return ini.getInt(key, defecto);
            else return defecto;

        }

        public static int[] getIntArray(string key)
        {
            if (ini != null)
                return ini.getIntArray(key);
            else return new int[] { 0 };
        }

        public static IPAddress getIp(string key)
        {
            // Parse
            try
            {
                string value = get(key, null);

                // Null -> Any
                if (string.IsNullOrEmpty(value))
                    return IPAddress.Any;

                // *    -> Any
                if (value.Equals("*"))
                    return IPAddress.Any;

                // Parse xx.xx.xx.xx
                IPAddress ip = IPAddress.Parse(value);
                return ip.MapToIPv4();

            }
            catch (Exception ex)
            {
                return IPAddress.Any;
            }
        }

        #endregion

        #endregion



        #region File Ini Static Cache
        private class FileIniCache
        {
            private static ConcurrentDictionary<string, FileIniCache> listaIni = new ConcurrentDictionary<string, FileIniCache>();
            public static FileIni load(string filename)
            {
                FileIniCache cache = null;

                // Sacamos esta cache de la lista o creamos uno nuevo
                if (listaIni.ContainsKey(filename))
                {
                    listaIni.TryGetValue(filename, out cache);
                }

                if (cache == null)
                {
                    cache = new FileIniCache(filename);
                    listaIni.TryAdd(filename, cache);
                }

                // obtenemos el archivo INI y lo devolvemos (este metodo ya decide si debe cargar el archivo de nuevo)
                if (cache != null)
                {
                    return cache.getIni();
                }

                return null;
            }


            private string filename;
            private FileIni ini;
            private DateTime IniModified;
            private long IniTickRead = 0;

            protected FileIniCache(string filename)
            {
                this.filename = filename;
                load();
            }

            protected void load()
            {
                ini = new FileIni(filename);    // Cargamos el archivo ini
                IniTickRead = TickCount;           // pillamos el tiempo en que hemos cargado
                IniModified = File.GetLastWriteTimeUtc(filename);
            }

            protected FileIni getIni()
            {
                // paso 1 - Miramos cuanto tiempo hace que nos pidieron este archivo por ultima vez
                //          con esto evitamos hacer muchas consultas en intervalos cortos de tiempo (minimo 5 segunso)
                if (!haPasadoTiempoMs(IniTickRead, 5000))
                    return ini;

                // paso 2 - Si han pasado esos x segundos, entonces miramos la fecha de modificacion del archivo
                //          y si hay cambios lo cargamos de nuevo
                DateTime modified = File.GetLastWriteTimeUtc(filename);
                if (modified != IniModified)
                {
                    load();
                }

                // Devolvemos el INI esté como esté
                return ini;
            }

            #region Time
            private static DateTime MyFirstDate = new DateTime(2014, 1, 1);
            private static long TickCount
            {
                get { return (long)((DateTime.UtcNow - MyFirstDate).TotalMilliseconds); }
            }

            private static long msFrom(long t)
            {
                return TickCount - t;
            }

            private static bool haPasadoTiempoMs(long clockInicio, long ms)
            {
                return msFrom(clockInicio) > ms;
            }

            #endregion

        }

        #endregion

    }
}
