using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEO_DROID.Services.SincroService
{
    public partial class Resource
    {

        static Resource()
        {
#if ANDROID 
            global::Android.Runtime.ResourceIdManager.UpdateIdValues();
#endif
        }

        public static void UpdateIdValues()
        {
        }

        public partial class Attribute
        {

            static Attribute()
            {
#if ANDROID
                global::Android.Runtime.ResourceIdManager.UpdateIdValues();
#endif
            }

            private Attribute()
            {
            }
        }

        public partial class Color
        {

            // aapt resource value: 0x7F010000
            public const int accent = 2130771968;

            // aapt resource value: 0x7F010001
            public const int almostWhite = 2130771969;

            // aapt resource value: 0x7F010002
            public const int black = 2130771970;

            // aapt resource value: 0x7F010003
            public const int dark_blue = 2130771971;

            // aapt resource value: 0x7F010004
            public const int dark_gray = 2130771972;

            // aapt resource value: 0x7F010005
            public const int gray = 2130771973;

            // aapt resource value: 0x7F010006
            public const int green = 2130771974;

            // aapt resource value: 0x7F010007
            public const int greenTec = 2130771975;

            // aapt resource value: 0x7F010008
            public const int light_blue = 2130771976;

            // aapt resource value: 0x7F010009
            public const int material_blue_500 = 2130771977;

            // aapt resource value: 0x7F01000A
            public const int material_blue_700 = 2130771978;

            // aapt resource value: 0x7F01000B
            public const int material_green_500 = 2130771979;

            // aapt resource value: 0x7F01000C
            public const int material_green_700 = 2130771980;

            // aapt resource value: 0x7F01000D
            public const int red = 2130771981;

            // aapt resource value: 0x7F01000E // 
            public const int white = 2130771982;

            static Color()
            {
#if ANDROID
                global::Android.Runtime.ResourceIdManager.UpdateIdValues();
#endif
            }

            private Color()
            {
            }
        }

        public partial class Drawable
        {

            // aapt resource value: 0x7F020000
            public const int actualizar_software = 2130837504;

            // aapt resource value: 0x7F020001
            public const int addButton = 2130837505;

            // aapt resource value: 0x7F020002
            public const int ajustes = 2130837506;

            // aapt resource value: 0x7F020003
            public const int anadir_establecimiento = 2130837507;

            // aapt resource value: 0x7F020004
            public const int averias = 2130837508;

            // aapt resource value: 0x7F020005
            public const int averias32 = 2130837509;

            // aapt resource value: 0x7F020006
            public const int averias_amarillo = 2130837510;

            // aapt resource value: 0x7F020007
            public const int averias_rojo = 2130837511;

            // aapt resource value: 0x7F020008
            public const int averias_verde = 2130837512;

            // aapt resource value: 0x7F020009
            public const int aviso = 2130837513;

            // aapt resource value: 0x7F02000A
            public const int banderaRoja32 = 2130837514;

            // aapt resource value: 0x7F02000B
            public const int cambios1 = 2130837515;

            // aapt resource value: 0x7F02000C
            public const int cambios2 = 2130837516;

            // aapt resource value: 0x7F02000D
            public const int cambios_amarillo = 2130837517;

            // aapt resource value: 0x7F02000E
            public const int cambios_amarillo2 = 2130837518;

            // aapt resource value: 0x7F02000F
            public const int cambios_rojo = 2130837519;

            // aapt resource value: 0x7F020010
            public const int cambios_rojo2 = 2130837520;

            // aapt resource value: 0x7F020011
            public const int cambios_verde = 2130837521;

            // aapt resource value: 0x7F020012
            public const int cambios_verde2 = 2130837522;

            // aapt resource value: 0x7F020013
            public const int cancelar32 = 2130837523;

            // aapt resource value: 0x7F020014
            public const int combo_triangle = 2130837524;

            // aapt resource value: 0x7F020015
            public const int configuracion_terminal = 2130837525;

            // aapt resource value: 0x7F020016
            public const int desplegable = 2130837526;

            // aapt resource value: 0x7F020017
            public const int dummyIcon32 = 2130837527;

            // aapt resource value: 0x7F020018
            public const int editar32 = 2130837528;

            // aapt resource value: 0x7F020019
            public const int error = 2130837529;

            // aapt resource value: 0x7F02001A
            public const int establecer_contadores = 2130837530;

            // aapt resource value: 0x7F02001B
            public const int establishment_hasBreakDown = 2130837531;

            // aapt resource value: 0x7F02001C
            public const int establishment_hasCharge = 2130837532;

            // aapt resource value: 0x7F02001D
            public const int geo = 2130837533;

            // aapt resource value: 0x7F02001E
            public const int hand = 2130837534;

            // aapt resource value: 0x7F020020
            public const int Icon = 2130837536;

            // aapt resource value: 0x7F020021
            public const int iconoEmpresa = 2130837537;

            // aapt resource value: 0x7F02001F
            public const int ico_calendario = 2130837535;

            // aapt resource value: 0x7F020022
            public const int impresora = 2130837538;

            // aapt resource value: 0x7F020023
            public const int infoGeneral = 2130837539;

            // aapt resource value: 0x7F020024
            public const int infoGeneral36 = 2130837540;

            // aapt resource value: 0x7F020025
            public const int infoGeneral48 = 2130837541;

            // aapt resource value: 0x7F020026
            public const int infoGeneral72 = 2130837542;

            // aapt resource value: 0x7F020027
            public const int infoGeneral_old = 2130837543;

            // aapt resource value: 0x7F020028
            public const int mas = 2130837544;

            // aapt resource value: 0x7F020029
            public const int navigable_icon = 2130837545;

            // aapt resource value: 0x7F02002A
            public const int nullImage = 2130837546;

            // aapt resource value: 0x7F02002B
            public const int ok = 2130837547;

            // aapt resource value: 0x7F02002C
            public const int prestamos1 = 2130837548;

            // aapt resource value: 0x7F02002D
            public const int prestamos2 = 2130837549;

            // aapt resource value: 0x7F02002E
            public const int prestamos3 = 2130837550;

            // aapt resource value: 0x7F02002F
            public const int prestamos_amarillo = 2130837551;

            // aapt resource value: 0x7F020030
            public const int prestamos_rojo = 2130837552;

            // aapt resource value: 0x7F020031
            public const int prestamos_verde = 2130837553;

            // aapt resource value: 0x7F020033
            public const int recaudar1 = 2130837555;

            // aapt resource value: 0x7F020034
            public const int recaudar2 = 2130837556;

            // aapt resource value: 0x7F020035
            public const int recaudar3 = 2130837557;

            // aapt resource value: 0x7F020036
            public const int recaudar_amarillo = 2130837558;

            // aapt resource value: 0x7F020037
            public const int recaudar_rojo = 2130837559;

            // aapt resource value: 0x7F020038
            public const int recaudar_verde = 2130837560;

            // aapt resource value: 0x7F020032
            public const int recauda_rojo = 2130837554;

            // aapt resource value: 0x7F020039
            public const int ruta = 2130837561;

            // aapt resource value: 0x7F02003A
            public const int sincronizacion = 2130837562;

            // aapt resource value: 0x7F02003B
            public const int test_contadores = 2130837563;

            // aapt resource value: 0x7F02003C
            public const int transporte_rojo = 2130837564;

            // aapt resource value: 0x7F02003D
            public const int transporte_verde = 2130837565;

            // aapt resource value: 0x7F02003E
            public const int usuario32 = 2130837566;

            static Drawable()
            {
#if ANDROID
                global::Android.Runtime.ResourceIdManager.UpdateIdValues();
#endif
            }

            private Drawable()
            {
            }
        }

        public partial class Id
        {

            // aapt resource value: 0x7F030009
            public const int addButton = 2130903049;

            // aapt resource value: 0x7F03000A
            public const int addKmButton = 2130903050;

            // aapt resource value: 0x7F03000B
            public const int addRefuelingButton = 2130903051;

            // aapt resource value: 0x7F03000C
            public const int address_text = 2130903052;

            // aapt resource value: 0x7F030000
            public const int BreakDownList = 2130903040;

            // aapt resource value: 0x7F03000D
            public const int bt0 = 2130903053;

            // aapt resource value: 0x7F03000E
            public const int bt1 = 2130903054;

            // aapt resource value: 0x7F03000F
            public const int bt2 = 2130903055;

            // aapt resource value: 0x7F030010
            public const int bt3 = 2130903056;

            // aapt resource value: 0x7F030011
            public const int bt4 = 2130903057;

            // aapt resource value: 0x7F030012
            public const int bt5 = 2130903058;

            // aapt resource value: 0x7F030013
            public const int bt6 = 2130903059;

            // aapt resource value: 0x7F030014
            public const int bt7 = 2130903060;

            // aapt resource value: 0x7F030015
            public const int bt8 = 2130903061;

            // aapt resource value: 0x7F030016
            public const int bt9 = 2130903062;

            // aapt resource value: 0x7F030017
            public const int btA = 2130903063;

            // aapt resource value: 0x7F030018
            public const int btAccept = 2130903064;

            // aapt resource value: 0x7F030019
            public const int btActualizarFirmware = 2130903065;

            // aapt resource value: 0x7F03001A
            public const int btB = 2130903066;

            // aapt resource value: 0x7F03001B
            public const int btBreakDown = 2130903067;

            // aapt resource value: 0x7F03001C
            public const int btBuscar = 2130903068;

            // aapt resource value: 0x7F03001D
            public const int btC = 2130903069;

            // aapt resource value: 0x7F03001E
            public const int btCancel = 2130903070;

            // aapt resource value: 0x7F03001F
            public const int btD = 2130903071;

            // aapt resource value: 0x7F030020
            public const int btDelete = 2130903072;

            // aapt resource value: 0x7F030021
            public const int btE = 2130903073;

            // aapt resource value: 0x7F030022
            public const int btEditar = 2130903074;

            // aapt resource value: 0x7F030023
            public const int btErase = 2130903075;

            // aapt resource value: 0x7F030024
            public const int btF = 2130903076;

            // aapt resource value: 0x7F030025
            public const int btFromMachine = 2130903077;

            // aapt resource value: 0x7F030026
            public const int btFromManual = 2130903078;

            // aapt resource value: 0x7F030027
            public const int btPrint = 2130903079;

            // aapt resource value: 0x7F030028
            public const int btReadCounters = 2130903080;

            // aapt resource value: 0x7F030029
            public const int btSave = 2130903081;

            // aapt resource value: 0x7F03002A
            public const int btSyncronize = 2130903082;

            // aapt resource value: 0x7F03002B
            public const int button1 = 2130903083;

            // aapt resource value: 0x7F03002C
            public const int cbCheckBox = 2130903084;

            // aapt resource value: 0x7F03002D
            public const int comentarioButton = 2130903085;

            // aapt resource value: 0x7F03002E
            public const int dpDate = 2130903086;

            // aapt resource value: 0x7F030001
            public const int EnableSelButton = 2130903041;

            // aapt resource value: 0x7F030002
            public const int EstablishmentList = 2130903042;

            // aapt resource value: 0x7F030003
            public const int EstablishmentSummaryList = 2130903043;

            // aapt resource value: 0x7F03002F
            public const int es_caption = 2130903087;

            // aapt resource value: 0x7F030030
            public const int es_iv1 = 2130903088;

            // aapt resource value: 0x7F030031
            public const int es_iv2 = 2130903089;

            // aapt resource value: 0x7F030032
            public const int es_iv3 = 2130903090;

            // aapt resource value: 0x7F030033
            public const int es_tv1 = 2130903091;

            // aapt resource value: 0x7F030034
            public const int es_tv2 = 2130903092;

            // aapt resource value: 0x7F030035
            public const int es_tv3 = 2130903093;

            // aapt resource value: 0x7F030036
            public const int etEditText1 = 2130903094;

            // aapt resource value: 0x7F030037
            public const int etLog = 2130903095;

            // aapt resource value: 0x7F030038
            public const int etOutput = 2130903096;

            // aapt resource value: 0x7F030039
            public const int etOutputFirmware = 2130903097;

            // aapt resource value: 0x7F030004
            public const int ExpensesList = 2130903044;

            // aapt resource value: 0x7F03003A
            public const int get_address_button = 2130903098;

            // aapt resource value: 0x7F03003B
            public const int get_location = 2130903099;

            // aapt resource value: 0x7F03003C
            public const int imgBreakDown = 2130903100;

            // aapt resource value: 0x7F03003D
            public const int imgCharges = 2130903101;

            // aapt resource value: 0x7F03003E
            public const int imgCollect = 2130903102;

            // aapt resource value: 0x7F03003F
            public const int imgInstall = 2130903103;

            // aapt resource value: 0x7F030040
            public const int imgLoans = 2130903104;

            // aapt resource value: 0x7F030041
            public const int infoButton = 2130903105;

            // aapt resource value: 0x7F030042
            public const int inputSearch = 2130903106;

            // aapt resource value: 0x7F030043
            public const int ivEditar = 2130903107;

            // aapt resource value: 0x7F030044
            public const int ivImg = 2130903108;

            // aapt resource value: 0x7F030045
            public const int ivImg2 = 2130903109;

            // aapt resource value: 0x7F030046
            public const int list = 2130903110;

            // aapt resource value: 0x7F030047
            public const int llAmmount = 2130903111;

            // aapt resource value: 0x7F030048
            public const int loadOption = 2130903112;

            // aapt resource value: 0x7F030049
            public const int location_text = 2130903113;

            // aapt resource value: 0x7F030005
            public const int OptionList = 2130903045;

            // aapt resource value: 0x7F030006
            public const int OptionListFirmware = 2130903046;

            // aapt resource value: 0x7F030007
            public const int OptionsList = 2130903047;

            // aapt resource value: 0x7F03004A
            public const int printButton = 2130903114;

            // aapt resource value: 0x7F030008
            public const int ProtocolList = 2130903048;

            // aapt resource value: 0x7F03004B
            public const int ratingbar = 2130903115;

            // aapt resource value: 0x7F03004C
            public const int saveOption = 2130903116;

            // aapt resource value: 0x7F03004D
            public const int selForPrintButton = 2130903117;

            // aapt resource value: 0x7F03004E
            public const int sendOption = 2130903118;

            // aapt resource value: 0x7F03004F
            public const int spinner = 2130903119;

            // aapt resource value: 0x7F030050
            public const int tvBreakDownTypeText = 2130903120;

            // aapt resource value: 0x7F030051
            public const int tvChargeAmmount = 2130903121;

            // aapt resource value: 0x7F030052
            public const int tvCodigo = 2130903122;

            // aapt resource value: 0x7F030053
            public const int tvDate = 2130903123;

            // aapt resource value: 0x7F030054
            public const int tvDiferencia = 2130903124;

            // aapt resource value: 0x7F030055
            public const int tvEnterpriseSummary = 2130903125;

            // aapt resource value: 0x7F030056
            public const int tvEnterpriseSummary1 = 2130903126;

            // aapt resource value: 0x7F030057
            public const int tvExpandable = 2130903127;

            // aapt resource value: 0x7F030058
            public const int tvExpandable1 = 2130903128;

            // aapt resource value: 0x7F030059
            public const int tvExpandable2 = 2130903129;

            // aapt resource value: 0x7F03005A
            public const int tvHeaderL = 2130903130;

            // aapt resource value: 0x7F03005B
            public const int tvHeaderR = 2130903131;

            // aapt resource value: 0x7F03005C
            public const int tvMachineText = 2130903132;

            // aapt resource value: 0x7F03005D
            public const int tvStablishmentSummary = 2130903133;

            // aapt resource value: 0x7F03005E
            public const int tvStablishmentSummary1 = 2130903134;

            // aapt resource value: 0x7F03005F
            public const int tvState = 2130903135;

            // aapt resource value: 0x7F030060
            public const int tvSummary = 2130903136;

            // aapt resource value: 0x7F030061
            public const int tvText = 2130903137;

            // aapt resource value: 0x7F030062
            public const int tvText1 = 2130903138;

            // aapt resource value: 0x7F030063
            public const int tvText2 = 2130903139;

            // aapt resource value: 0x7F030064
            public const int tvText3 = 2130903140;

            // aapt resource value: 0x7F030065
            public const int tvText4 = 2130903141;

            // aapt resource value: 0x7F030066
            public const int tvTextInfo1 = 2130903142;

            // aapt resource value: 0x7F030067
            public const int tvTextInfo2 = 2130903143;

            // aapt resource value: 0x7F030068
            public const int tvTotal = 2130903144;

            // aapt resource value: 0x7F030069
            public const int unitOptions = 2130903145;

            static Id()
            {
#if ANDROID
                global::Android.Runtime.ResourceIdManager.UpdateIdValues();
#endif
            }

            private Id()
            {
            }
        }

        public partial class Layout
        {

            // aapt resource value: 0x7F040000
            public const int ActualizarFirmware = 2130968576;

            // aapt resource value: 0x7F040001
            public const int BigAndSmallTextListItem = 2130968577;

            // aapt resource value: 0x7F040002
            public const int BigAndSmallTextWithInfoListItem = 2130968578;

            // aapt resource value: 0x7F040003
            public const int BigAndSmallTextWithLeftImageFourTextListItem = 2130968579;

            // aapt resource value: 0x7F040004
            public const int BigAndSmallTextWithLeftImageListItem = 2130968580;

            // aapt resource value: 0x7F040005
            public const int BigAndSmallTextWithRightButtonListItem = 2130968581;

            // aapt resource value: 0x7F040006
            public const int BigAndSmallTextWithRightImageListItem = 2130968582;

            // aapt resource value: 0x7F040007
            public const int BigAndSmallTextWithSummaryListItem = 2130968583;

            // aapt resource value: 0x7F040008
            public const int BlueToothDiscovering = 2130968584;

            // aapt resource value: 0x7F040009
            public const int BreakDown = 2130968585;

            // aapt resource value: 0x7F04000A
            public const int BreakDownCU = 2130968586;

            // aapt resource value: 0x7F04000B
            public const int BreakDownListItem = 2130968587;

            // aapt resource value: 0x7F04000C
            public const int CategoryBarListItem = 2130968588;

            // aapt resource value: 0x7F04000D
            public const int Changes = 2130968589;

            // aapt resource value: 0x7F04000E
            public const int ChangesCU = 2130968590;

            // aapt resource value: 0x7F04000F
            public const int ChangesListItem = 2130968591;

            // aapt resource value: 0x7F040010
            public const int ChargesRecuperations = 2130968592;

            // aapt resource value: 0x7F040011
            public const int CheckBoxListItem = 2130968593;

            // aapt resource value: 0x7F040012
            public const int CollectActivity = 2130968594;

            // aapt resource value: 0x7F040013
            public const int Configuration = 2130968595;

            // aapt resource value: 0x7F040014
            public const int EntryDateDialog = 2130968596;

            // aapt resource value: 0x7F040015
            public const int EntryGrossTextDialog = 2130968597;

            // aapt resource value: 0x7F040016
            public const int EntryMACDialog = 2130968598;

            // aapt resource value: 0x7F040017
            public const int EntryTextDialog = 2130968599;

            // aapt resource value: 0x7F040018
            public const int EstablishmentInformation = 2130968600;

            // aapt resource value: 0x7F040019
            public const int EstablishmentListItemWithInfo = 2130968601;

            // aapt resource value: 0x7F04001A
            public const int EstablishmentListWithInfo = 2130968602;

            // aapt resource value: 0x7F04001B
            public const int EstablishmentSummary = 2130968603;

            // aapt resource value: 0x7F04001C
            public const int EstablishmentSummaryListItem = 2130968604;

            // aapt resource value: 0x7F04001D
            public const int ExpandableListItem = 2130968605;

            // aapt resource value: 0x7F04001E
            public const int ExpandableListWithSummaryItem = 2130968606;

            // aapt resource value: 0x7F04001F
            public const int Expenses = 2130968607;

            // aapt resource value: 0x7F040020
            public const int ExpensesCU = 2130968608;

            // aapt resource value: 0x7F040021
            public const int GeneralInformation = 2130968609;

            // aapt resource value: 0x7F040022
            public const int GrossBreakDownInsertion = 2130968610;

            // aapt resource value: 0x7F040023
            public const int ImageAndTextListItem = 2130968611;

            // aapt resource value: 0x7F040024
            public const int ItemList = 2130968612;

            // aapt resource value: 0x7F040025
            public const int ItemListSearch = 2130968613;

            // aapt resource value: 0x7F040026
            public const int Loans = 2130968614;

            // aapt resource value: 0x7F040027
            public const int LoansCU = 2130968615;

            // aapt resource value: 0x7F040028
            public const int LocalizacionData = 2130968616;

            // aapt resource value: 0x7F040029
            public const int Localizarme = 2130968617;

            // aapt resource value: 0x7F04002A
            public const int MachineCollect = 2130968618;

            // aapt resource value: 0x7F04002B
            public const int Main = 2130968619;

            // aapt resource value: 0x7F04002C
            public const int PhotoListItem = 2130968620;

            // aapt resource value: 0x7F04002D
            public const int PotentialEstablishmentsM = 2130968621;

            // aapt resource value: 0x7F04002E
            public const int PotentialEstablishmentsMachines = 2130968622;

            // aapt resource value: 0x7F04002F
            public const int ProtocolList = 2130968623;

            // aapt resource value: 0x7F040030
            public const int RatingBarListItem = 2130968624;

            // aapt resource value: 0x7F040031
            public const int ReadCounters = 2130968625;

            // aapt resource value: 0x7F040032
            public const int ReadCountersListItem = 2130968626;

            // aapt resource value: 0x7F040033
            public const int SetCountersByMachine = 2130968627;

            // aapt resource value: 0x7F040034
            public const int SetCountersByManual = 2130968628;

            // aapt resource value: 0x7F040035
            public const int SpinnerItemList = 2130968629;

            // aapt resource value: 0x7F040036
            public const int Sync = 2130968630;

            // aapt resource value: 0x7F040037
            public const int tecna_simple_list_item_multiple_choice = 2130968631;

            // aapt resource value: 0x7F040038
            public const int TestCounterMachineInputDialog = 2130968632;

            // aapt resource value: 0x7F040039
            public const int TestCounters = 2130968633;

            // aapt resource value: 0x7F04003A
            public const int TextLeftTextRightListItem = 2130968634;

            // aapt resource value: 0x7F04003B
            public const int TextListItem = 2130968635;

            // aapt resource value: 0x7F04003C
            public const int Tools = 2130968636;

            // aapt resource value: 0x7F04003D
            public const int VehicleManagement = 2130968637;

            static Layout()
            {
#if ANDROID
                global::Android.Runtime.ResourceIdManager.UpdateIdValues();
#endif
            }

            private Layout()
            {
            }
        }

        public partial class Menu
        {

            // aapt resource value: 0x7F050000
            public const int SetCounters = 2131034112;

            static Menu()
            {
#if ANDROID
                global::Android.Runtime.ResourceIdManager.UpdateIdValues();
#endif
            }

            private Menu()
            {
            }
        }

        public partial class String
        {

            // aapt resource value: 0x7F060000
            public const int Accept = 2131099648;

            // aapt resource value: 0x7F060001
            public const int ApplicationName = 2131099649;

            // aapt resource value: 0x7F060002
            public const int ApplicationNameVersion = 2131099650;

            // aapt resource value: 0x7F060003
            public const int ApplicationNumberVersion = 2131099651;

            // aapt resource value: 0x7F060004
            public const int ApplicationStructureVersion = 2131099652;

            // aapt resource value: 0x7F060005
            public const int ConnectionSuccess = 2131099653;

            // aapt resource value: 0x7F060006
            public const int DataLostByLicense = 2131099654;

            // aapt resource value: 0x7F060007
            public const int GeoNotFound = 2131099655;

            // aapt resource value: 0x7F060008
            public const int Hello = 2131099656;

            // aapt resource value: 0x7F060009
            public const int InitingTerminal = 2131099657;

            // aapt resource value: 0x7F06000A
            public const int SecondCopy = 2131099658;

            // aapt resource value: 0x7F06000B
            public const int UserNotFound = 2131099659;

            // aapt resource value: 0x7F06000C
            public const int ViewName = 2131099660;

            // aapt resource value: 0x7F06000D
            public const int ViewPosition = 2131099661;

            // aapt resource value: 0x7F06000E
            public const int Warning = 2131099662;

            // aapt resource value: 0x7F06000F
            public const int Welcome = 2131099663;

            static String()
            {
#if ANDROID
                global::Android.Runtime.ResourceIdManager.UpdateIdValues();
#endif
            }

            private String()
            {
            }
        }

        public partial class Style
        {

            // aapt resource value: 0x7F070000
            public const int MyTheme = 2131165184;

            // aapt resource value: 0x7F070002
            public const int MyTheme_ActionBarStyle = 2131165186;

            // aapt resource value: 0x7F070001
            public const int MyTheme_ActionBar_TitleTextStyle = 2131165185;

            // aapt resource value: 0x7F070003
            public const int MyTheme_Base = 2131165187;

            // aapt resource value: 0x7F070004
            public const int MyTheme_Dialog = 2131165188;

            static Style()
            {
#if ANDROID
                global::Android.Runtime.ResourceIdManager.UpdateIdValues();
#endif
            }

            private Style()
            {
            }
        }
    }
}
