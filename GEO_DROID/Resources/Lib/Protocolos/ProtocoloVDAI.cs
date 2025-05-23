using System;
using System.Collections.Generic;
using System.Text;
using GEO_DROID.Resources.Lib.Comunicacion;

namespace BLL.LeerInfoMaquina
{
    abstract class ProtocoloVDAI : Protocolo
    {
        protected const byte COD_ESC = (byte)0x1B;
        protected const byte COD_LF = (byte)0x0A;

        protected const char COD_ENQ = (char)0x05;
        protected const char COD_XON = (char)0x11;
        protected const char COD_XOFF = (char)0x13;
        protected const char COD_EOT = (char)0x04;
        protected const char COD_SYN = (char)0x16;
        protected const char COD_S = (char)'S';
        protected const char COD_C = (char)'C';

        public const byte CMD_SOLICITAR_DATOS = (byte)0x01;

        string checksumTest = "";

        protected string _password;
        protected string _flags;
        protected string _separadorDecimales = ",";

        public ProtocoloVDAI(IComunicacion com, IFiltroTrama filtroTrama, string password) : base(com, filtroTrama)
        {
            if (password != null)
                _password = password;
        }

        protected override char CrearChecksum(StringBuilder sb)
        {
            return (char)0x00;
        }

        protected override bool ComprobarChecksum(StringBuilder sb)
        {
            if (sb != null && sb.Length > 0)
            {
                checksumTest = "CHKSUM()";
                int indiceEOT = sb.ToString().IndexOf(COD_EOT);
                if (indiceEOT >= 0)
                {
                    // Calculamos el checksum de la trama recibida
                    long checksum = 0;
                    for (int i = 0; i <= indiceEOT && i < sb.Length; i++)
                    {
                        char c = sb[i];
                        checksum += c;
                    }
                    checksum = checksum % 65536;
                    checksumTest = "CHKSUM(PDA:" + checksum + ")";
                    //checksum.ToString("X")

                    // Leemos el checksum que hemos recibido
                    long checksumMaq = 0;
                    string s = sb.ToString().Substring(indiceEOT);
                    int indiceCabecera = s.IndexOf((char)COD_ESC);
                    if (indiceCabecera >= 0 && s.Length > indiceCabecera + 5)
                    {
                        if (s[indiceCabecera + 1] == COD_C)
                        {
                            StringBuilder sbb = new StringBuilder();
                            sbb.Append((char)s[indiceCabecera + 2]);
                            sbb.Append((char)s[indiceCabecera + 3]);
                            sbb.Append((char)s[indiceCabecera + 4]);
                            sbb.Append((char)s[indiceCabecera + 5]);
                            checksumMaq = long.Parse(sbb.ToString().ToUpper(), System.Globalization.NumberStyles.HexNumber);
                        }
                        checksumTest = "CHKSUM(PDA:" + checksum + "#MAQ:" + checksumMaq + ")";
                        return (checksum == checksumMaq);
                    }
                }
            }
            return false;
        }

        protected override StringBuilder MontarTrama(byte comando, byte[] datos) //throws CheckSumException
        {
            StringBuilder sbTemp = new StringBuilder();

            sbTemp.Append((char)COD_ESC);
            sbTemp.Append(COD_S);
            sbTemp.Append(_password);
            sbTemp.Append(_flags);
            sbTemp.Append((char)COD_LF);

            return sbTemp;
        }

        public override InfoContadores LeerContadores(IProgressCallback callback)
        {
            bool recibidoXON = false;
            InfoContadores info = null;
            StringBuilder informacion = null;

            string _estadoError = "(S01 INIT)";
            _errorInfo = "";

            StringBuilder codigoENQ = new StringBuilder();
            codigoENQ.Append((char)COD_ENQ);
            StringBuilder codigoSYN = new StringBuilder();
            codigoSYN.Append((char)COD_SYN);

            // Enviamos ENQ hasta que la máquina responda o hasta que consideremos que no conecta
            System.Threading.Thread.Sleep(500);
            for (int i = 0; i < 30; i++)
            {
                _estadoError += "(" + i + ")";
                if (_com.EnviarDatos(codigoENQ) != null)
                {
                    //System.Threading.Thread.Sleep(500);
                    StringBuilder sb = _com.RecibirDatos(500, 0);
                    while (sb != null && sb.Length > 0)
                    {
                        _estadoError += "-" + sb[0] + ":" + ((int)sb[0]) + "-";
                        if (sb[0] != COD_XON)
                        {
                            sb.Remove(0, 1);
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (sb != null && sb.Length > 0 && sb[0] == COD_XON)
                    {
                        recibidoXON = true;
                        break;
                    }
                }
            }

            if (recibidoXON)
            {
                System.Threading.Thread.Sleep(5);
                _estadoError = "(S02 DATOS)";
                if (EnviarTrama(CMD_SOLICITAR_DATOS))
                {
                    System.Threading.Thread.Sleep(10);
                    for (int i = 0; i < 1000; i++)
                    {
                        //System.Threading.Thread.Sleep(10);
                        StringBuilder sb = _com.RecibirDatos(10, 0);
                        if (sb != null && sb.ToString().IndexOf(COD_SYN) > 0)
                        {
                            informacion = new StringBuilder();
                            // Quito lo que he enviado
                            //StringBuilder sbEnviado = MontarTrama(CMD_SOLICITAR_DATOS, null);
                            //if (sb != null && sb.Length >= sbEnviado.Length)
                            //    sb.Remove(0, sbEnviado.Length);

                            for (int j = 0; j < sb.Length; j++)
                                informacion.Append((char)sb[j]);
                            _com.EnviarDatos(codigoSYN);
                            break;
                        }
                    }
                    if (informacion != null && informacion.Length > 0)
                    {
                        // Comprobamos checksum
                        _estadoError = "(S03 CHECKSUM)";
                        if (ComprobarChecksum(informacion))
                        {
                            info = ProcesarDatos(informacion.ToString());
                            info.Buffer = informacion;
                            _error = "";
                            _estadoError = "";
                        }
                        else
                        {
                            _error = "(" + checksumTest + ")";
                            _errorInfo = informacion.ToString();
                        }
                    }
                    else
                    {
                        _error = "No recibimos datos de la máquina";
                    }
                }
            }
            else
            {
                _error = "XON no recibido";
            }
            //_error += _estadoError;
            return info;
        }

        protected abstract InfoContadores ProcesarDatos(string datos);

        private string GetDatoString(string seccion, string texto, string datos)
        {
            string valor = null;
            if (datos != null)
            {
                int indiceSeccion = datos.IndexOf(seccion);
                if (indiceSeccion >= 0)
                {
                    indiceSeccion = indiceSeccion + seccion.Length;
                    int indiceInicio = datos.IndexOf(texto, indiceSeccion);
                    if (indiceInicio >= indiceSeccion)
                    {
                        indiceInicio = indiceInicio + texto.Length;
                        int indiceFin = datos.IndexOf((char)COD_LF, indiceInicio);
                        if (indiceFin >= indiceInicio)
                            valor = datos.Substring(indiceInicio, indiceFin - indiceInicio);
                    }
                }
            }
            return valor;
        }

        protected int GetDatoEntero(string seccion, string texto, string datos)
        {
            int valor = -1;
            string s_valor = GetDatoString(seccion, texto, datos);
            if (s_valor != null)
            {
                try
                {
                    valor = int.Parse(s_valor.Trim());
                }
                catch { }
            }
            return valor;
        }

        protected decimal GetDatoDecimal(string seccion, string texto, string datos)
        {
            decimal valor = -1;
            string s_valor = GetDatoString(seccion, texto, datos);
            if (s_valor != null)
            {
                try
                {
                    System.Globalization.NumberFormatInfo nfi = new System.Globalization.NumberFormatInfo();
                    nfi.NumberDecimalSeparator = _separadorDecimales;
                    valor = decimal.Parse(s_valor.Trim(), nfi);
                }
                catch { }
            }
            return valor;
        }
    }
}
