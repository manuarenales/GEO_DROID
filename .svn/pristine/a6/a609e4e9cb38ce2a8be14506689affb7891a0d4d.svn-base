using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.LeerInfoMaquina
{
    class InfoContadoresDia
    {
        DateTime _fecha;
        List<InfoContadores> listaContadores = new List<InfoContadores>();

        public InfoContadoresDia(DateTime fecha)
        {
            _fecha = fecha;
        }

        public void Add(int jugadas, int premios)
        {
            InfoContadores info = new InfoContadores();
            info.Entradas = jugadas;
            info.Salidas = premios;

            listaContadores.Add(info);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[" + _fecha.ToString() + "]");
            for (int i = 0; i < listaContadores.Count; i++)
            {
                sb.Append("[" + listaContadores[i].ToString() + "]");
            }
            return sb.ToString();
        }

        public string Print()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[" + _fecha.ToString() + "]");
            for (int i = 0; i < listaContadores.Count; i++)
            {
                sb.Append("[(JUGADO:" + listaContadores[i].Entradas.ToString() + ")");
                sb.Append("(PREMIOS:" + listaContadores[i].Salidas.ToString() + ")]");
            }
            return sb.ToString();
        }

    }
}
