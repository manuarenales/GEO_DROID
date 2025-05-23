using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEO_DROID.Services
{
    public static class DateTimeUtils
    {
        public static DateTime ChangeTime(this DateTime dateTime, int hours, int minutes, int seconds, int milliseconds)
        {
            return new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                hours,
                minutes,
                seconds,
                milliseconds,
                dateTime.Kind);
        }

        public static DateTime GetNullDate()
        {
            return new DateTime(1, 1, 1);
        }

        /// <summary>
        /// Obtiene un entero que representa las semanas en calendario entre dos fechas.
        /// Si consideramos que el primer día de la semana es el lunes, un domingo y el siguiente lunes distarán una semana.
        /// </summary>
        /// <param name="fechaDesde">Fecha desde</param>
        /// <param name="fechaHasta">Fecha hasta</param>
        /// <param name="primerDiaSemana">Día de la semana que consideramos que es el primero</param>
        /// <returns>Semanas de separación entre las fechas</returns>
        public static int GetWeeksBetween(DateTime fechaDesde, DateTime fechaHasta, DayOfWeek primerDiaSemana)
        {
            return GetWeek(fechaHasta, primerDiaSemana) - GetWeek(fechaDesde, primerDiaSemana);
        }

        /// <summary>
        /// Obtiene él número de semana en el que se localiza una fecha, desde la mínima fecha, que en .NET es 01/01/0001
        /// </summary>
        /// <param name="fecha">Fecha</param>
        /// <param name="primerDiaSemana">Día de la semana que consideramos que es el primero</param>
        /// <returns>Índice de semana en la que se localiza la fecha pasada por parámetro</returns>
        public static int GetWeek(DateTime fecha, DayOfWeek primerDiaSemana)
        {
            // Usaremos DateTime.MinValue como referencia para el cálculo del índice de semana
            DateTime fechaDesde = DateTime.MinValue;
            while (fechaDesde.DayOfWeek != primerDiaSemana)
                fechaDesde = fechaDesde.AddDays(1);

            DateTime fechaHasta = fecha;
            if (fechaHasta.DayOfWeek == primerDiaSemana)
                fechaHasta = fechaHasta.AddDays(1);
            while (fechaHasta.DayOfWeek != primerDiaSemana)
                fechaHasta = fechaHasta.AddDays(1);

            TimeSpan ts = fechaHasta - fechaDesde;
            return (ts.Days / 7) - 1; //Restamos uno, porque para ajustar el día pasado por parámetro al día de inicio de semana nos movemos hasta la siguiente semana
        }
    }
}
