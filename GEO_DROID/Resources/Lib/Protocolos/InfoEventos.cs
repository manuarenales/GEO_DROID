using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.LeerInfoMaquina
{
    class InfoEventos
    {
        public static DateTime MIN_DATETIME = new DateTime(1900, 1, 1);

        public const byte EVENTO_UNDEFINED = (byte)0xFF;
        public const byte EVENTO_MAQUINA_ENCENDIDA = (byte)0x00;
        public const byte EVENTO_MAQUINA_APAGADA = (byte)0x01;
        public const byte EVENTO_PUERTA1_ABIERTA = (byte)0x02;
        public const byte EVENTO_PUERTA1_CERRADA = (byte)0x03;
        public const byte EVENTO_PUERTA2_ABIERTA = (byte)0x04;
        public const byte EVENTO_PUERTA2_CERRADA = (byte)0x05;

        List<EventoMaquina> lista = new List<EventoMaquina>();
        
        public InfoEventos()
        {
        }

        public void Add(byte evento, DateTime fecha)
        {
            lista.Add(new EventoMaquina(evento, fecha));
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < lista.Count; i++)
            {
                sb.Append(lista[i].ToString());
            }
            return sb.ToString();
        }

        private class EventoMaquina
        {
            byte _evento;
            DateTime _fecha;

            public EventoMaquina(byte evento, DateTime fecha)
            {
                _evento = evento;
                _fecha = fecha;
            }

            public override string ToString()
            {
                string textoEvento = "";
                switch (_evento)
                {
                    case EVENTO_MAQUINA_APAGADA: 
                        textoEvento = "APAGADA";
                        break;
                    case EVENTO_MAQUINA_ENCENDIDA: 
                        textoEvento = "ENCENDIDA";
                        break;
                    case EVENTO_PUERTA1_ABIERTA: 
                        textoEvento = "P1 ABIERTA";
                        break;
                    case EVENTO_PUERTA1_CERRADA: 
                        textoEvento = "P1 CERRADA";
                        break;
                    case EVENTO_PUERTA2_ABIERTA: 
                        textoEvento = "P2 ABIERTA";
                        break;
                    case EVENTO_PUERTA2_CERRADA: 
                        textoEvento = "P2 CERRADA";
                        break;
                }

                if (_fecha.Equals(MIN_DATETIME))
                    return ("(EVENTO CON FECHA INVALIDA:"+_evento+")");
                return "(" + textoEvento + ":" + _fecha.ToString() + ")";
            }
        }
    }
}
