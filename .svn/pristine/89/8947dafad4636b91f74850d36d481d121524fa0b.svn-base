
namespace BLL.LeerInfoMaquina
{
    public enum ProtocoloMaquina { FRANCO = 1, BARCREST = 12, BELLFRUIT = 11, UNIDESA = 6, CIRSA = 6, COSTACALIDA = 7, GIGAMES = 10, SENTE = 4, SLEIC = 9, TECNAUSA = 8, TOURVISION = 5, WORTE = 13, GEMINI = 14, AMATIC = 15, MERKUR = 16, OLYMPIC = 17, NOVOMATIC = 18, ZITRO = 19, NOVOMATIC_SAS = 20, GISTRA = 21 };

    struct TMachineProtocolData
    {
        public ProtocoloMaquina machineProtocol;
        public string protocolName;

        public TMachineProtocolData(ProtocoloMaquina mP, string pName)
        {
            machineProtocol = mP;
            protocolName = pName;
        }
    }

    static class ProtocoloMaquinaUtil
    {
        public static readonly TMachineProtocolData[] _protocols = new TMachineProtocolData[]
                {
                    new TMachineProtocolData (ProtocoloMaquina.AMATIC,"Amatic" ),
                    new TMachineProtocolData (ProtocoloMaquina.BARCREST,"Barcrest"),
                    new TMachineProtocolData (ProtocoloMaquina.BELLFRUIT,"Bellfruit"),
                    new TMachineProtocolData (ProtocoloMaquina.UNIDESA,"Cirsa/Unidesa" ),
                    new TMachineProtocolData (ProtocoloMaquina.COSTACALIDA,"Costacalida" ),
                    new TMachineProtocolData (ProtocoloMaquina.FRANCO,"Franco" ),
                    new TMachineProtocolData (ProtocoloMaquina.GEMINI,"Gemini"),
                    new TMachineProtocolData (ProtocoloMaquina.GIGAMES,"Gigames"),
                    new TMachineProtocolData (ProtocoloMaquina.GISTRA,"Gistra"),
                    new TMachineProtocolData (ProtocoloMaquina.MERKUR,"Merkur"),
                    new TMachineProtocolData (ProtocoloMaquina.NOVOMATIC,"Novomatic GSP"),
                    new TMachineProtocolData (ProtocoloMaquina.NOVOMATIC_SAS,"Novomatic SAS"),
                    new TMachineProtocolData (ProtocoloMaquina.OLYMPIC,"Olympic"),
                    new TMachineProtocolData (ProtocoloMaquina.SENTE,"Sente"),
                    new TMachineProtocolData (ProtocoloMaquina.SLEIC,"Sleic"),
                    new TMachineProtocolData (ProtocoloMaquina.TECNAUSA,"Tecnausa"),
                    new TMachineProtocolData (ProtocoloMaquina.TOURVISION,"Tourvision"),
                    new TMachineProtocolData (ProtocoloMaquina.WORTE,"Worte" ),
                    new TMachineProtocolData (ProtocoloMaquina.ZITRO,"Zitro" )
                };


        static string GetStringByEnum(ProtocoloMaquina protocolo)
        {
            string retVal = "";

            switch (protocolo)
            {
                case ProtocoloMaquina.BARCREST:
                    retVal = "Barcrest";
                    break;
                case ProtocoloMaquina.BELLFRUIT:
                    retVal = "Bellfruit";
                    break;
                case ProtocoloMaquina.UNIDESA: //Cirsa
                    retVal = "Unidesa/Cirsa";
                    break;
                case ProtocoloMaquina.COSTACALIDA:
                    retVal = "Costacalida";
                    break;
                case ProtocoloMaquina.FRANCO:
                    retVal = "Franco";
                    break;
                case ProtocoloMaquina.GIGAMES:
                    retVal = "Gigames";
                    break;
                case ProtocoloMaquina.SENTE:
                    retVal = "Sente";
                    break;
                case ProtocoloMaquina.SLEIC:
                    retVal = "Sleic";
                    break;
                case ProtocoloMaquina.TECNAUSA:
                    retVal = "Tecnausa";
                    break;
                case ProtocoloMaquina.TOURVISION:
                    retVal = "Tourvision";
                    break;
                case ProtocoloMaquina.WORTE:
                    retVal = "Worte";
                    break;
                case ProtocoloMaquina.AMATIC:
                    retVal = "Amatic";
                    break;
                case ProtocoloMaquina.GEMINI:
                    retVal = "Gemini";
                    break;
                case ProtocoloMaquina.MERKUR:
                    retVal = "Merkur";
                    break;
                case ProtocoloMaquina.OLYMPIC:
                    retVal = "Olympic";
                    break;
                case ProtocoloMaquina.NOVOMATIC:
                    retVal = "Novomatic GSP";
                    break;
                case ProtocoloMaquina.NOVOMATIC_SAS:
                    retVal = "Novomatic SAS";
                    break;
                case ProtocoloMaquina.ZITRO:
                    retVal = "Zitro";
                    break;
                case ProtocoloMaquina.GISTRA:
                    retVal = "Gistra";
                    break;
                default:
                    System.Diagnostics.Debug.Assert(false, "No se reconoce el protocolo");
                    break;
            }

            return retVal;
        }
    }
}