using System.Text;
using BLL;

namespace GEO_DROID.Resources.Lib.Comunicacion
{
    interface IComunicacion
    {
        bool Reconectar(IProgressCallback progreso);
        bool ConfigurarComunicacion(byte parametros);
        bool EnviarDatos(StringBuilder datos);
        StringBuilder RecibirDatos(int timeout, int timeout2);
        void LimpiarBuffer();
    }
}