using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEO_DROID.Resources.Lib.Extra
{
    public static class Time
    {
        public static DateTime MyFirstDate = new DateTime(2014, 1, 1);
        public static DateTime date1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local);

        public static long TickCount
        {
            get { return (long)((DateTime.UtcNow - MyFirstDate).TotalMilliseconds); }
        }

        public static long msFrom(long t)
        {
            return Time.TickCount - t;
        }

        public static bool haPasadoTiempoMs(long clockInicio, long ms)
        {
            return msFrom(clockInicio) > ms;
        }

        public static bool haPasadoTiempo(long clockInicio, long segundos)
        {
            return msFrom(clockInicio) > (segundos * 1000);
        }

        public static T secondsFrom<T>(long t)
        {
            if (typeof(T) == typeof(string))
            {
                string value = string.Format("{0:0.00}", (double)(Time.TickCount - t) / 1000d);
                return (T)Convert.ChangeType(value, typeof(T));
            }
            else if (typeof(T) == typeof(int))
            {
                int value = (int)((Time.TickCount - t) / 1000);
                return (T)Convert.ChangeType(value, typeof(T));
            }
            else
            {
                return default(T);
            }
        }






    }
}
