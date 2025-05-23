using GEO_DROID.Resources.Lib.Interfaces;

using System.Text;


namespace GEO_DROID.Resources.Lib.Extra
{
    public class DeviceControllerFlash : DeviceController
    {
        public static int HardwareId = 10056;

        private HardwareDevice hdevice;
        private LogFile logFile;

        public Action<string> onLogMessage;
        public Action<State> onStateChange;
        public Action<int, int, float> onProgress;
        public Action<Cmd> onSendCommand;

        private Cmd lastCommandSend;


        public DeviceControllerFlash()
        {
            state = State.Iddle;
        }

        public void Dispose()
        {
            log("# ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ");
            log("# Errores Totales registrados  : " + contadorTotalErrores);
            log("# ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ");
            log("# Tiempo total de comunicación : " + hdevice.clientNode.TimeFromConnection.ToString("mm\\:ss") + " s");
            log("# Datos Transferidos (I/O)     : " + hdevice.clientNode.GetIoStats());
            log("# ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ");

            logFile.Dispose();
        }


        public void attachTo(HardwareDevice device)
        {
            // Importante - Si este controlador ya tiene un hdevice asociado, los desvinculamos entre ellos
            if (this.hdevice != null)
            {
                this.hdevice.controller = null;
                this.hdevice = null;
            }

            this.hdevice = device;
            this.hdevice.controller = this;

            if (logFile == null)
            {
                if (Config.isLogEnabled(device.Id, Config.LogType.LogControlador))
                    logFile = new LogFile("Bootloader", device.Id + ".log", LogFile.LogByDate.Day, 10);
                else logFile = new LogFile();   // el constructor vacio no escribe nada, pero hace que el objeto no sea NULL

                log("# Attach Device : " + hdevice.ToStringId());
                log("# Start > Id(" + device.Id + ") Version (" + device.Modelo + ") Firmware(" + device.Firmware + ")");
            }
            else
            {
                log("# Attach Device : " + hdevice.ToStringId());
                log("# Start > Id(" + device.Id + ") Version (" + device.Modelo + ") Firmware(" + device.Firmware + ")");
            }
        }

        public HardwareDevice getDeviceAttached()
        {
            return hdevice;
        }

        public bool canAttach()
        {
            // Solo podemos enganchar si estamos en el estado de GRabacion
            if (FlagGrabando)
                return true;


            return false;
        }





        public void log(string message)
        {
            if (logFile != null)
                logFile.Write(message);

            if (onLogMessage != null)
                onLogMessage(message);
        }



        int contadorErrores = 0;
        int contadorErroresMax = 25;
        int contadorTotalErrores = 0;

        bool FlagGrabando = false;

        public string ReadConfigData()
        {
            LogConsole.print(ConsoleColor.Green, ConsoleColor.Black, "Bootloader > ReadConfigData");

            for (int i = 0x3D00; i <= 0x3FFF; i += 0x80)
            {
                Cmd c = new Cmd(0xF9, Protocol.Bootloader.Cmd, Protocol.Bootloader.ReadData);
                c.idPaquete = 1;

                byte upper = (byte)(i >> 8);
                byte lower = (byte)(i & 0xff);
                c.addByte(lower); // 2 segundos
                c.addByte(upper); // 2 segundos
                c.addByte(0x00); // 2 segundos
                c.addByte(0x00); // 2 segundos
                c.addByte(0x80); // 2 segundos

                string s = c.CmdToString(true);
                sendCommand(c);
            }
            return null;
        }

        public string WriteConfigData(string fileName)
        {
            LogConsole.print(ConsoleColor.Green, ConsoleColor.Black, "Bootloader > WriteConfigData");

            Cmd cClear = new Cmd(0xF9, Protocol.Bootloader.Cmd, Protocol.Bootloader.ClearData);
            cClear.idPaquete = 1;
            sendCommand(cClear);

            StringBuilder sb = new StringBuilder();
#if ANDROID
            System.IO.StreamReader reader = new System.IO.StreamReader(Android.App.Application.Context.Assets.Open(fileName));

            if (reader != null && reader.BaseStream != null)
            {
                int c;
                while ((c = reader.BaseStream.ReadByte()) != -1)
                    sb.Append((char)c);
            }
            reader.Close();
#endif
            for (int i = 0x3D00; i <= 0x3FFF; i += 0x80)
            {
#if ANDROID
                Cmd c = new Cmd(0xF9, Protocol.Bootloader.Cmd, Protocol.Bootloader.WriteData);

                c.idPaquete = 1;

                byte upper = (byte)(i >> 8);
                byte lower = (byte)(i & 0xff);
                c.addByte(lower); // 2 segundos
                c.addByte(upper); // 2 segundos
                c.addByte(0x00); // 2 segundos
                c.addByte(0x00); // 2 segundos
                if (sb.Length < 0x80)
                    c.addByte(sb.Length); // 2 segundos
                else
                    c.addByte(0x80); // 2 segundos

                for (int j = 0; j < 0x80 && sb.Length > 0; j++)
                {
                    c.addByte((char)sb[0]);
                    sb.Remove(0, 1);
                }

                string s = c.CmdToString(true);
                LogConsole.print(ConsoleColor.Green, ConsoleColor.Black, "Bootloader > ReadConfigData");
                sendCommand(c);
#endif
            }

            return null;
        }

        public void Start()
        {

            /*
            if (EXPLOTAR_EN_TRAMA == 0)       EXPLOTAR_EN_TRAMA = 5;
            else if (EXPLOTAR_EN_TRAMA == 5)  EXPLOTAR_EN_TRAMA = 10;
            else if (EXPLOTAR_EN_TRAMA == 10) EXPLOTAR_EN_TRAMA = 1000;
            LogConsole.print(ConsoleColor.Yellow, ConsoleColor.Blue, "*** EXPLOTAR EN TRAMA "+ EXPLOTAR_EN_TRAMA + " ***");
            */

            contadorErrores = 0;
            currentSteep = 0;        // Empezamos en el paso 0 (siempre)

            if (!FlagGrabando)
            {
                currentTrama = 0;
            }

            NextSteep();
        }

        public void Stop()
        {

        }


        bool flagDie = false;
        public bool isDead()
        {
            // este Flag le dice al controlador que este dispositivo ya ha terminado de comunicar y lo quitará de la pila
            // y llamará al stop del controlador
            if (flagDie)
            {
                currentSteep = 100;
            }

            return flagDie;
        }


        #region Ping
        public void onPingSend()
        {
            //LogConsole.print("# PING > Send");
            log("# PING > Send");
        }
        public void onPingReceive()
        {
            //LogConsole.print("# PING > Receive");
            log("# PING > Receive");
        }
        public void onPingTimeout()
        {
            //LogConsole.print("# PING > TIMEOUT");
            log("# PING > TIMEOUT");
        }
        #endregion

        #region Send Command
        private void sendCommand(Cmd comando)
        {
            if (hdevice != null)
                hdevice.clientNode.sendCommand(comando);

            if (onSendCommand != null)
            {
                if (comando.F9CommandTimeBack > 0)
                {
                    comando.tickCreacion = Time.TickCount;

                    while (!Time.haPasadoTiempoMs(comando.tickCreacion, comando.F9CommandTimeBack)) ;
                }

                lastCommandSend = comando;
                onSendCommand(comando);
            }


        }
        #endregion

        #region Eventos de Socket / Nivel Log
        public void onSocketDataSent(byte[] data, int length)
        {
        }
        public void onSocketDataReceived(byte[] data, int length)
        {
        }
        public void onCommandSent(Cmd comando)
        {

        }
        #endregion

        #region Eventos del SOCKET / onCommandLost + onCommandReceive

        public void onCommandLost(Cmd comando)
        {
            log("# CMD Perdido - Sin respuesta de " + comando.ToString(false));
        }

        public void onCommandReceive(Cmd comando)
        {
            if (flagDie)
            {
                //LogConsole.print("Bootloader < FLAGDIE < Este modulo ya se ha marcado para morir");
                // si hay trafico con el flagdie a true le mandamos un Reset
                return;
            }

            #region Cuenta de Ok / Errores

            // Standar FA OK - reseteamos el contador de errores seguidos
            if (comando.IS(0xfA, Protocol.Response.Cmd, Protocol.Response.Ok) || comando.IS(0xfA, Protocol.Bootloader.Cmd, Protocol.Bootloader.Ok))
            {
                if (contadorErrores != 0)
                    log("CMD < Golbal OK -> Reseteamos el Flag de errores (Cuenta " + contadorErrores + ")");

                contadorErrores = 0;
            }

            // Standar FA ERROR - vamos incrementando los errores
            if (comando.IS(0xfA, Protocol.Response.Cmd, Protocol.Response.Error) || comando.IS(0xfA, Protocol.Bootloader.Cmd, Protocol.Bootloader.Error))
            {
                contadorErrores++;          // contadores de errores seguidos
                contadorTotalErrores++;     // Contador Total para tener un log 

                log("CMD < Global ERROR -> Registramos Error (Cuenta " + contadorErrores + ")");
                //LogConsole.print(ConsoleColor.Red, ConsoleColor.White, "CMD < Global ERROR -> Registramos Error (Cuenta " + contadorErrores + ")");
                //LogConsole.print(ConsoleColor.DarkRed, ConsoleColor.White, "    < " + comando.ToString(false) + " < " + comando.response.ToString(false));

                // Log Error
                comando.seek(0);
                int b = comando.getByte();
                string error = "Error desconocido";
                switch (b)
                {
                    case 1: error = "Comando desconocido"; break;
                    case 2: error = "Opcion desconocida"; break;
                    case 3: error = "Respuesta no esperada"; break;
                    case 4: error = "Id no coincide"; break;
                    case 5: error = "Flag desconocido"; break;
                    case 6: error = "Programa no cargado"; break;
                    case 7: error = "Flash no borrada"; break;
                    case 8: error = "Direccion de memoria incorreta"; break;
                    case 9: error = "Numero de trama incorrecto"; break;
                    case 10: error = "CRC incorrecto"; break;
                    case 11: error = "Numero de datos excesivo"; break;
                }

                //LogConsole.print(ConsoleColor.DarkRed, ConsoleColor.White, "    < dato = " + b + ": " + error);
                //LogConsole.print(ConsoleColor.Black, ConsoleColor.DarkRed, "\t\t" + comando.ToString(false));
                //LogConsole.print(ConsoleColor.Black, ConsoleColor.DarkRed, "\t\t" + comando.response.ToString(false));
                // End log error

                if (contadorErrores == contadorErroresMax)
                {
                    log("CMD < Global ERROR -> Registramos errores maximos permitidos -> RESET");
                    ResetAndDie();
                    return;
                }
            }

            #endregion

            // En este controlador todos los comandos de entrada van a ser respuestas por lo que empezamos
            // por pillar a que comando es la respuesta, y luego iremos analizando en funcion de este comando
            Cmd CmdSrc = comando.response;

            if (CmdSrc == null)
            {
                if (onSendCommand != null)
                {
                    // Cuando estamos con un conector no tenemos pila de entrada/salida y por tanto al entrar un mensaje, no podemos saber
                    // cual era el mensaje de salida. Por eso cuando estamos en modo conector nos guardamos el ultimo mensaje enviado por la pila
                    // en este modo siempre es I > O > I > O > I

                    CmdSrc = lastCommandSend;
                    comando.response = CmdSrc;
                }
                else
                {
                    return;
                }
            }

            CmdSrc.seek(0);

            #region Respuestas a 1.6

            if (comando.IS(0xfA, Protocol.Response.Cmd, Protocol.Response.Ok, Protocol.Hardware.Cmd, Protocol.Hardware.SetFlag))
            {
                int flag = CmdSrc.getByte();

                // Flag Timeout
                if (flag == 1)
                {
                    log("CMD < OK Flag Timeout");
                    LogConsole.print(ConsoleColor.DarkGreen, ConsoleColor.White, "           < OK Flag Timeout");
                }

                // Flag Conectado
                if (flag == 2)
                {
                    log("CMD < OK Flag Conectado");
                    LogConsole.print(ConsoleColor.DarkGreen, ConsoleColor.White, "           < OK Flag Conectado");

                    if (FlagGrabando)
                    {
                        currentSteep = 2;   // pasamos al estado de grabacion (saltando el comando BORRAR FLASH)
                        NextSteep();
                    }
                    else
                    {
                        if (openFirmware())
                        {
                            currentTrama = 0;
                            currentTramaMax = (int)firm.Tramas;

                            currentSteep = 1; // comando de borrar Flash
                            NextSteep();
                        }
                        else
                        {
                            log("ERROR FATAL - No ha sido posible cargar el firmware indicado");
                            LogConsole.print(ConsoleColor.Red, ConsoleColor.White, "ERROR FATAL - No ha sido posible cargar el firmware indicado");
                        }
                    }

                }

            }

            // Error Ante un error, reintentamos el mismo paso
            if (comando.IS(0xfA, Protocol.Response.Cmd, Protocol.Response.Error, Protocol.Hardware.Cmd, Protocol.Hardware.SetFlag))
            {
                NextSteep();
            }

            #endregion

            #region Respuestas a Boot loader

            #region > 1 > Borrar Flash
            if (comando.IS(0xfA, Protocol.Bootloader.Cmd, Protocol.Bootloader.Ok, Protocol.Bootloader.Cmd, Protocol.Bootloader.Delete))
            {
                log("CMD < OK Borrar");
                LogConsole.print(ConsoleColor.DarkGreen, ConsoleColor.White, "           < OK Bootloader Borrar");
                currentSteep++;
                NextSteep();
            }

            if (comando.IS(0xfA, Protocol.Bootloader.Cmd, Protocol.Bootloader.Error, Protocol.Bootloader.Cmd, Protocol.Bootloader.Delete))
            {
                log("CMD < Error Borrar");
                LogConsole.print(ConsoleColor.DarkRed, ConsoleColor.White, "           < Error Bootloader Borrar");
                NextSteep();
            }
            #endregion

            #region > 2 > Grabar Trama
            if (comando.IS(0xfA, Protocol.Bootloader.Cmd, Protocol.Bootloader.Ok, Protocol.Bootloader.Cmd, Protocol.Bootloader.Grabar))
            {
                int idTrama = currentTrama + 1;
                // log("CMD < Ok Grabar Trama: " + idTrama);
                //LogConsole.print(ConsoleColor.DarkGreen, ConsoleColor.White, "           < OK Grabar Trama: " + idTrama);

                /*
                if (idTrama == EXPLOTAR_EN_TRAMA)
                {
                    hdevice.pingTimeout = 500;
                    hdevice.pingSend    = 500;
                    currentSteep        = 150;
                }
                */

                // Incrementamos la tama
                currentTrama++;

                // Cuando ya tenemos todas las tamas vamos al siguiente paso
                if (currentTrama >= currentTramaMax)
                    currentSteep++;

                if (onProgress != null)
                {
                    float percent = (float)currentTrama / (float)currentTramaMax;
                    if (percent < 0) percent = 0;
                    if (percent > 1) percent = 1;

                    onProgress(currentTrama, currentTramaMax, percent);
                }

                NextSteep();
            }

            if (comando.IS(0xfA, Protocol.Bootloader.Cmd, Protocol.Bootloader.Error, Protocol.Bootloader.Cmd, Protocol.Bootloader.Grabar))
            {
                int idTrama = currentTrama + 1;
                log("CMD < ERROR Grabar Trama: " + idTrama);
                LogConsole.print(ConsoleColor.DarkGreen, ConsoleColor.White, "           < ERROR Grabar Trama: " + idTrama);
                NextSteep();
            }
            #endregion

            #region > 3 > Jumper Programacion
            if (comando.IS(0xfA, Protocol.Bootloader.Cmd, Protocol.Bootloader.Ok, Protocol.Bootloader.Cmd, Protocol.Bootloader.Jumper))
            {
                bool jumperProgramacion = comando.getBool();

                log("CMD < Jumper Programacion = " + jumperProgramacion);
                LogConsole.print(ConsoleColor.DarkGreen, ConsoleColor.White, "           < OK Bootloader JumperProgramacion = " + jumperProgramacion);

                if (jumperProgramacion == true)
                {
                    // Si está el Jumper de programacion nos quedamos aqui hasta que el Jumper sea FALSE
                    LogConsole.print(ConsoleColor.DarkGreen, ConsoleColor.White, "           < OK Bootloader Pedir jumper dentro de x minutos");
                }
                else
                {
                    // Si es false ya podemos pasar al siguiente estado
                    currentSteep++;
                }
                NextSteep();
            }

            if (comando.IS(0xfA, Protocol.Bootloader.Cmd, Protocol.Bootloader.Error, Protocol.Bootloader.Cmd, Protocol.Bootloader.Jumper))
            {
                log("CMD < Error Jumper Programacion");
                LogConsole.print(ConsoleColor.DarkGreen, ConsoleColor.White, "           < ERROR Bootloader JumperProgramacion");
                NextSteep();
            }
            #endregion

            #region > 4 > Ejecutar
            if (comando.IS(0xfA, Protocol.Bootloader.Cmd, Protocol.Bootloader.Ok, Protocol.Bootloader.Cmd, Protocol.Bootloader.Ejecutar))
            {
                log("CMD < Ok Ejecutar");
                LogConsole.print(ConsoleColor.DarkGreen, ConsoleColor.White, "           < OK Bootloader Ejecutar OK");

                // FLAG DIE
                log("# Marcado Flag de comunicaciones cerradas");
                flagDie = true;
                currentSteep = 5;
                // --------

                NextSteep();
            }
            if (comando.IS(0xfA, Protocol.Bootloader.Cmd, Protocol.Bootloader.Error, Protocol.Bootloader.Cmd, Protocol.Bootloader.Ejecutar))
            {
                log("CMD < Error Ejecutar");
                LogConsole.print(ConsoleColor.DarkGreen, ConsoleColor.White, "           < OK Bootloader Ejecutar ERROR");
                NextSteep();
            }
            #endregion

            #region > 5 > RESET
            if (comando.IS(0xfA, Protocol.Bootloader.Cmd, Protocol.Bootloader.Ok, Protocol.Bootloader.Cmd, Protocol.Bootloader.Reset))
            {
                log("CMD < Ok Reset");
                LogConsole.print(ConsoleColor.DarkGreen, ConsoleColor.White, "           < OK Bootloader RESET OK");

                // FLAG DIE
                log("# Marcado Flag de comunicaciones cerradas");
                flagDie = true;
                currentSteep = 6;
                // --------
            }
            if (comando.IS(0xfA, Protocol.Bootloader.Cmd, Protocol.Bootloader.Error, Protocol.Bootloader.Cmd, Protocol.Bootloader.Reset))
            {
                log("CMD < Error Reset");
                LogConsole.print(ConsoleColor.DarkGreen, ConsoleColor.White, "           < OK Bootloader RESET ERROR");
                ResetAndDie();
            }
            #endregion

            #endregion

        }

        #endregion



        public enum State
        {
            Iddle,          // en reposo al iniciar
            Connecting,     // estableciendo conexion 
            Preparing,      // borrando flahs
            Flashing,       // grabando
            WaitingJumper,  // esperando para ejecutar
            Finished,        // fin
            RESET

        };

        DeviceControllerFlash.State _state;
        public DeviceControllerFlash.State state
        {
            get { return _state; }
            private set
            {
                _state = value;
                if (onStateChange != null)
                    onStateChange(state);
            }
        }

        private int _currentSteep;
        public int currentSteep
        {
            get { return _currentSteep; }
            set
            {
                _currentSteep = value;

                if (_currentSteep == -1) state = State.Iddle;
                if (_currentSteep == 0) state = State.Connecting;
                if (_currentSteep == 1) state = State.Preparing;
                if (_currentSteep == 2) state = State.Flashing;
                if (_currentSteep == 3) state = State.WaitingJumper;
                if (_currentSteep == 4) state = State.WaitingJumper;
                if (_currentSteep == 5) state = State.Finished;
                if (_currentSteep == 6) state = State.RESET;
            }
        }


        int currentTrama = 0;
        int currentTramaMax = 0;

        bool primeraPeticionJumperProgramacion = true;

        public void NextSteep()
        {

            #region Step 0 - Establecer Estado Conectado
            if (currentSteep == 0)
            {
                // Configuramos TIMEOUT en el modulo // Solo si estamos comunicacion por socket
                if (hdevice != null)
                {
                    log("CMD > Establecer Flag Timeout");
                    LogConsole.print(ConsoleColor.Green, ConsoleColor.Black, "Bootloader > Establecer Flag Timeout");
                    Cmd configTimeout = Cmd.F9(Protocol.Hardware.Cmd, Protocol.Hardware.SetFlag);
                    configTimeout.addByte(1);       // Flag 1 - Timeout
                    configTimeout.addInt16(30);     // 30 segundos
                    sendCommand(configTimeout);
                }

                log("CMD > Establecer Flag conectado TRUE");
                LogConsole.print(ConsoleColor.Green, ConsoleColor.Black, "Bootloader > Establecer Flag conectado TRUE");
                Cmd conectate = Cmd.F9(Protocol.Hardware.Cmd, Protocol.Hardware.SetFlag);
                conectate.addByte(2);           // Flag 2 - Estado de conexion
                conectate.addBool(true);        // Conectado
                sendCommand(conectate);
            }

            #endregion

            #region Step 1 - Borrar Firmware
            if (currentSteep == 1)
            {
                log("CMD > Borrar Flash");
                LogConsole.print(ConsoleColor.Green, ConsoleColor.Black, "Bootloader > Borrar Flash");

                Cmd c = new Cmd(0xF9, Protocol.Bootloader.Cmd, Protocol.Bootloader.Delete);
                sendCommand(c);

            }
            #endregion

            #region Step 2 - Grabar firmware

            if (currentSteep == 2)
            {
                FlagGrabando = true;

                bool logtrama = ((currentTrama + 1) % 10 == 0);
                if (currentTrama < 10) logtrama = true;
                if (currentTrama > currentTramaMax - 10) logtrama = true;

                if (logtrama)
                {
                    log("CMD > Grabar Trama -> " + (currentTrama + 1) + "/" + currentTramaMax);
                    LogConsole.print(ConsoleColor.Green, ConsoleColor.Black, "Bootloader > Grabar Trama -> " + (currentTrama + 1) + "/" + currentTramaMax);
                }

                Cmd c = new Cmd(0xF9, Protocol.Bootloader.Cmd, Protocol.Bootloader.Grabar);
                c.addByte(firm.get(currentTrama));

                sendCommand(c);
            }
            #endregion

            #region Step 3 - Pedir Jumper de programacion
            if (currentSteep == 3)
            {
                Cmd c = new Cmd(0xF9, Protocol.Bootloader.Cmd, Protocol.Bootloader.Jumper);

                if (primeraPeticionJumperProgramacion)
                {
                    log("CMD > Pide Jumper programacion");
                    LogConsole.print(ConsoleColor.Green, ConsoleColor.Black, "Bootloader > Pide Jumper programacion");
                }
                else
                {
                    log("CMD > Pide Jumper programacion (programado para enviar en 5 segundos");
                    LogConsole.print(ConsoleColor.Green, ConsoleColor.Black, "Bootloader > Pide Jumper programacion (comando retrasado 5 segundos)");
                    // ponemos una marca de TimeBack para indicar que el comando no puede salir hasta que lleve ahi x tiempo
                    c.F9CommandTimeBack = 5 * 1000;
                }

                sendCommand(c);

                // ya no es primera peticion (asi las proximas entrarán en el if del TimeBack
                primeraPeticionJumperProgramacion = false;
            }

            #endregion

            #region Step 4 - Ejecutar
            if (currentSteep == 4)
            {
                log("CMD > Ejecutar");
                LogConsole.print(ConsoleColor.Green, ConsoleColor.Black, "Bootloader > Ejecutar");
                Cmd c = new Cmd(0xF9, Protocol.Bootloader.Cmd, Protocol.Bootloader.Ejecutar);
                c.addByte(2); // 2 segundos
                sendCommand(c);
            }

            #endregion

        }

        #region Firmware

        private string filename;
        FirmwareLoad firm = null;

        public void setFirmware(string filename)
        {
            this.filename = filename;
        }

        private bool openFirmware()
        {

            if (firm == null)
            {
                if (string.IsNullOrEmpty(filename))
                {
                    // para operaciones de debug
                    filename = @"D:\Horeca\Firmware\ESRECPLC056.X.production.frw";
                }

                log("Firm load file(" + filename + ")");

                firm = new FirmwareLoad(filename);
                if (firm.loadHeader())
                {
                    firm.loadTramas();
                    return true;
                }
            }

            return false;
        }

        #endregion

        public void ResetAndDie()
        {
            LogConsole.print(ConsoleColor.Green, ConsoleColor.Black, "Bootloader > RESET");

            Cmd c = new Cmd(0xF9, Protocol.Bootloader.Cmd, Protocol.Bootloader.Reset);
            c.addByte(2); // 2 segundos

            sendCommand(c);

        }

        public void onRequestSyncData()
        {
            throw new NotImplementedException();
        }
    }
}
