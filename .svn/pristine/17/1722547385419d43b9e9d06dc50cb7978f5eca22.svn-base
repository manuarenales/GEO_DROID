
using GEO_DROID.Resources.Lib.Comunicacion;

namespace BLL.LeerInfoMaquina
{
    class ProtocoloZitro : ProtocoloSAS
    {
        public ProtocoloZitro(IComunicacion com, IFiltroTrama filtroTrama, string password, int timeout)
            : base(com, filtroTrama, password, timeout)
        {
            COUNTER_ENTRADAS = COUNTER_TOTAL_DROP;
            COUNTER_SALIDAS = COUNTER_TOTAL_CANCELLED_CREDITS;
        }
    }
}
