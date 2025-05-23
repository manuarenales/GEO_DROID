using System;
using System.Collections.Generic;
using System.Text;
using GEO_DROID.Resources.Lib.Comunicacion;

namespace BLL.LeerInfoMaquina
{
    class ProtocoloAra : Protocolo
    {
        const int MAX_INTENTOS = 1;
        const int _timeoutDefault = 300;

        public override byte ConfiguracionPuertoSerie
        {
            get { return (byte)0x05; } 
        }

        public ProtocoloAra(IComunicacion com, IFiltroTrama filtroTrama) 
            : base(com, filtroTrama)
        {
        }

        protected override char CrearChecksum(StringBuilder sb)
        {
            return (char)0x00;
        }
        protected uint CrearChecksum2(StringBuilder sb)
        {
            //#define polinomio 0x8408 

            //unsigned char datos[]={1,7,1,2,3};
            //unsigned char i,j; 
            //unsigned int crc_envio; 

            //unsigned int CalculaCrc(unsigned char datos[]);

            //void main(void) 
            //{ 
            //  while(1) 
            //  { 
            //    crc_envio=CalculaCrc(datos);
            //  } 
            //} 
            //unsigned int CalculaCrc(unsigned char datos[])
            //{ 
            //  unsigned int crc = 0xFFFF; //valor inicial

            //  for(i=0; i<5; i++) 
            //  { 
            //    crc ^= (unsigned int)datos[i];
            //    for(j=0; j<8; j++)
            //    { 
            //      if(crc & 0x0001)  crc = (crc >> 1) ^ polinomio;
            //      else crc = (crc >> 1);
            //    } 
            //  } 
            //  return((~crc) & 0xffff);
            //} 
            uint polinomio = 0x8408;
            uint crc = 0xFFFF; //valor inicial

            for (int i = 0; i < 5; i++)
            {
                crc = crc ^ (uint)sb[i];
                for (int j = 0; j < 8; j++)
                {
                    if ((crc & 0x0001) != 0)
                        crc = (crc >> 1) ^ polinomio;
                    else
                        crc = (crc >> 1);
                }
            }
            return ((~crc) & 0xFFFF);
        }

        protected override bool ComprobarChecksum(StringBuilder sb)
        {
            if (sb.Length > 2)
            {
                int longitudDatos = sb.Length - 2; //En la longitud no contamos  el checksum

                byte[] b = new byte[2];
                b[0] = (byte)sb[longitudDatos + 1];
                b[1] = (byte)sb[longitudDatos];

                uint crc = (uint)BitConverter.ToUInt16(b, 0);
                uint crcTmp = CrearChecksum2(sb);
                _error = "CRC:" + crc + "(en trama) <> " + crcTmp + "(calculado)";
                return (crc == crcTmp);
            }
            return false;
        }

        protected override StringBuilder MontarTrama(byte comando, byte[] datos) //throws CheckSumException
        {
            StringBuilder sbTemp = new StringBuilder();

            // El protocolo ARA responde al recibir 3 bytes,  p.ej. los caracteres ASCII ‘G’ ‘E’ ‘T’
            sbTemp.Append("GET");
            
            return sbTemp;
        }

        public override InfoContadores LeerContadores(IProgressCallback callback)
        {
            InfoContadores info = null;

            for (int i = 0; i < MAX_INTENTOS && info == null; i++)
            {
                // Hay que enviarle 3 bytes
                if (EnviarTrama(0))
                {
                    StringBuilder sb = RecibirDatos(_timeoutDefault, _timeoutDefault);

                    if (sb != null && sb.Length > 0)
                    {
                        if (ComprobarChecksum(sb))
                        {
                            info = ProcesarDatos(sb);
                            info.Buffer = sb;
                            _error = "";
                            return info;
                        }
                        else
                        {
                            _error = "CRC error";
                        }
                    }
                    else
                    {
                        _error = "Recibido vacio";
                    }
                }
                else
                {
                    _error = "Error al enviar comando de inicio de comunicación";
                }
            }
            return null;
        }

        private InfoContadores ProcesarDatos(StringBuilder datos)
        {
            int pasos = -1;

            if (datos != null)
            {
                string s = "";
                for (int i = 0; i < datos.Length - 2; i++)
                {
                    s += ((int)datos[i]).ToString();
                }

                try
                {
                    pasos = int.Parse(s);
                } catch {}
            }

            InfoContadores info = new InfoContadores();
            info.Auxiliar1 = pasos;
            return info;
        }

    }
}
