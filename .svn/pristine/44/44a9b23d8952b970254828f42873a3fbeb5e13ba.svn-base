using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using GEO_DROID.Services.SincroService;

namespace GEO_DROID.Services
{
    public abstract class TCPNetClientBase : TCPNetBase
    {
        private string _remoteHost;
        private string _remoteCertificateName;
        private TcpClient _client;
        private AsyncCallback _onConnectionRequest;
        private AsyncCallback _onAuthenticateAsClient;
        private StreamWriter _sw;
        private BinaryWriter _bw;

        public int RemotePort
        { get { return _port; } set { _port = value; } }
        public string RemoteHost
        { get { return _remoteHost; } set { _remoteHost = value; } }
        public string RemoteCertificateName
        { get { return _remoteCertificateName; } set { _remoteCertificateName = value; } }
        public bool Conectado
        { get { return _client != null && _client.Connected; } }
        public TCPNetClientBase(bool useSSL, bool useClientCertificates, bool useCompression)
            : base(useSSL, useClientCertificates, useCompression)
        {
            _onConnectionRequest = new AsyncCallback(OnConnectionRequest);
            _onAuthenticateAsClient = new AsyncCallback(OnAuthenticateAsClient);
        }
        public TcpClient Client
        {
            get { return _client; }
        }
        public void Connect()
        {
            if (_port > 0)
            {
                if (!string.IsNullOrEmpty(this._remoteHost))
                {

                    /*
                    // Si el host es una IP, conectamos con la IP sino como nombre de host
                    IPAddress address = new IPAddress(0);
                    if (IPAddress.TryParse(this._remoteHost, out address))
                    {
                        // Conectamos por IP
                        _client = new TcpClient(address.AddressFamily);
                        _client.BeginConnect(address, _port, _onConnectionRequest, null);
                    }
                    else
                    {
                     * */
                    // Conectamos por HOST
                    //
                    // Esto puede dar una excepcion si no es capaz de resolver el nombre
                    //
                    // ## Exception : No such host is known
                    // at System.Net.Dns.hostent_to_IPHostEntry (System.String h_name, System.String[] h_aliases, System.String[] h_addrlist) [0x00000] in <filename unknown>:0 
                    // at System.Net.Dns.GetHostByName (System.String hostName) [0x00000] in <filename unknown>:0 
                    // at System.Net.Dns.GetHostEntry (System.String hostNameOrAddress) [0x00000] in <filename unknown>:0 
                    // ...
                    // at TCPNetClientBase.Connect () [0x00000] in <filename unknown>:0 
                    try
                    {
                        _client = new TcpClient();
                        _client.BeginConnect(this._remoteHost, _port, _onConnectionRequest, null);

                    }
                    catch (Exception ex)
                    {
                        this._connectionCallback(this, new ConnectionResults(ex));
                    }
                    //}
                }
            }




            //Console.WriteLine(string.Format("Port: {0}", this._port));
            //Console.WriteLine(string.Format("SSL: {0}", this._useSSL));
            //Console.WriteLine(string.Format("ClientCertificates: {0}", this._useClientCertificates));
        }
        public virtual void Disconnect()
        {
            if (_client != null)
                _client.Close();
            _client = null;

            if (_sw != null)
                _sw.Close();
            _sw = null;
            if (_bw != null)
                _bw.Close();
            _bw = null;
        }
        private void OnConnectionRequest(IAsyncResult result)
        {

            Stream stream = null;
            SslStream sslStream = null;
            try
            {
                stream = _client.GetStream();
                if (this._useSSL)
                {
                    sslStream = new SslStream(stream, false, new RemoteCertificateValidationCallback(OnRemoteCertificateValidationCallback), new LocalCertificateSelectionCallback(CertificateSelectionCallback));
                    sslStream.BeginAuthenticateAsClient(this._remoteCertificateName,
                        (this._useClientCertificates) ? GetClientCertificates() : null,
                        SslProtocols.Default, false,
                        this._onAuthenticateAsClient,
                        sslStream);
                }
                else
                    OnClientFinallyConnect(_client, stream);
            }
            catch (Exception ex)
            {
                if (stream != null)
                {
                    stream.Dispose();
                    stream = null;
                }
                if (sslStream != null)
                {
                    sslStream.Dispose();
                    sslStream = null;
                }
                this._connectionCallback(this, new ConnectionResults(ex));
            }
        }
        private bool OnRemoteCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;

            // esto es un cachondeo, en cada sistema da unos flags distintos
            /*
            // Dejamos pasar los SslPolicyErrors.RemoteCertificateChainErrors 
            // ya que no vamos a instalar en los equipos servidor/cliente la "Entidad emisora de raíz de confianza"

            // Lo hacemos así para asegurarnos la compatibilidad con mono ya que None aparece sumado en sslPolicyErrors en cualquier caso
        
            SslPolicyErrors chainErrorAux = SslPolicyErrors.None | SslPolicyErrors.RemoteCertificateChainErrors; 
            //SslPolicyErrors chainErrorAux = SslPolicyErrors.RemoteCertificateNotAvailable | SslPolicyErrors.RemoteCertificateChainErrors; 

            if (sslPolicyErrors == SslPolicyErrors.None || sslPolicyErrors == chainErrorAux)
                return true;
            else
            {
                Console.WriteLine("TCPNetClientBase.OnRemoteCertificateValidationCallback(), " + sslPolicyErrors.ToString());
                return false;
            }
            */
        }
        static X509Certificate CertificateSelectionCallback(object sender, string targetHost, X509CertificateCollection localCertificates, X509Certificate remoteCertificate, string[] acceptableIssuers)
        {
            if (localCertificates.Count == 0)
                return null;
            return
                localCertificates[0];
        }
        private void OnAuthenticateAsClient(IAsyncResult result)
        {
            SslStream sslStream = null;
            try
            {
                sslStream = result.AsyncState as SslStream;
                sslStream.EndAuthenticateAsClient(result);
                OnClientFinallyConnect(_client, sslStream);
            }
            catch (Exception ex)
            {
                if (sslStream != null)
                {
                    sslStream.Dispose();
                    sslStream = null;
                }
                this._connectionCallback(this, new ConnectionResults(ex));
            }
        }
        protected override void SetStreamWriter(TcpClient client, Stream stream)
        {
            _sw = NewStreamWriter(stream, Encoding.GetEncoding("ISO-8859-1"));
            _bw = new BinaryWriter(stream);
        }
        protected override StreamWriter GetStreamWriter(TcpClient client)
        {
            return _sw;
        }
        protected override BinaryWriter GetBinaryWriter(TcpClient client)
        {
            return _bw;
        }
        public override void EnviaDatos(string datos)
        {
            base.EnviaDatos(null, datos);
        }
        public override void EnviaDatos(byte[] buffer, int len)
        {
            if (_bw != null)
            {
                if (this._useCompression)
                    buffer = UtilsGeneric.GzipBuffer(buffer);

                try
                {
                    _bw.Write(buffer, 0, len);
                    _bw.Flush();
                }
                catch (Exception ex)
                {
                    DesconectaEx(this._client, ex);
                }

            }
        }
        public override void EnviaDatos(TcpClient client, byte[] buffer, int len)
        {
            EnviaDatos(buffer, len);
        }
        protected abstract X509CertificateCollection GetClientCertificates();
        protected override void Desconecta(TcpClient cliente)
        {
            base.Desconecta(cliente);
        }
    }

    public abstract class TCPNetBase
    {
        protected const int KDEFAULT_TIMEOUT = 20000;
        protected const int KDEFAULT_TIMEOUT_BW = 10000;
        protected bool _useSSL;
        protected bool _useClientCertificates;
        protected bool _useCompression;
        protected int _port;
        protected SecureConnectionResultsCallback _connectionCallback;
        private const int BytesPerLong = 4;
        private const int BitsPerByte = 8;

        public int TimeoutMonitorGeneral { get; protected set; }
        public int TimeoutMonitorProcessData { get; protected set; }
        public int TimeoutMonitorBinaryWriter { get; protected set; }

        public TCPNetBase(bool useSSL, bool useClientCertificates, bool useCompression) :
            this(useSSL, useClientCertificates, useCompression, KDEFAULT_TIMEOUT, KDEFAULT_TIMEOUT, KDEFAULT_TIMEOUT_BW)
        { }

        public TCPNetBase(bool useSSL, bool useClientCertificates, bool useCompression, int timeoutMonitorGeneral, int timoutMonitorProcessData, int timeoutMonitorBinaryWriter)
        {
            this._useSSL = useSSL;
            this._useClientCertificates = useClientCertificates;
            this._useCompression = useCompression;
            this.TimeoutMonitorGeneral = timeoutMonitorGeneral;
            this.TimeoutMonitorProcessData = timoutMonitorProcessData;
            this.TimeoutMonitorBinaryWriter = timeoutMonitorBinaryWriter;
            this._connectionCallback = OnConnectionResults;
        }

        protected virtual void OnClienteConectado(TcpClient client)
        {
        }
        protected virtual void OnClienteConectadoAndReady()
        {
        }
        protected virtual void OnClienteConexionError()
        {
        }
        protected virtual void DatosRecibidos(TcpClient client, byte[] buffer)
        {
        }
        protected virtual void OnConnectionResults(object sender, ConnectionResults args)
        {
            OnClienteConexionError();

            if (args.MyAsyncException != null)
            {
                Console.WriteLine("TCPNetServerInterbares.OnSecureConnectionResults(), " + args.MyAsyncException.Message);
            }
        }
        protected void OnClientFinallyConnect(TcpClient client, Stream stream)
        {
            SimpleData myData = null;
            try
            {
                myData = new SimpleData(client, stream);
                SetStreamWriter(client, myData.MyStream);

                // Existen 2 Eventos de SocketConectado.
                //
                //      Antes se llamaba al socketConectado y luego se creaba el BeginRead. eso daba como resultado que en cuando
                //      se detectaba que el socket se abria no podias enviar un dato y esperar respuesta porque no habia stream de lectura
                //
                //      Se dio la vuelta y entonces se creaba el stream y luego se notificaba que habia socket, pero era casi peor .. porque 1 de 20
                //      casos te llegaban datos antes de la llamada a tu socketConectado, y algun objeto no se habia creado
                //
                //      Solucion. Tenemos 2 eventos del socketConectado
                //
                //      OnClienteConectado(client);    -> el socket se ha conectado
                //          lo logico en este metodo será establecer unicamente estados de conectado true/false y crear objetos
                //          que se usarán durante la comunicacion
                //
                //      OnClienteConectadoAndReady();  -> conectado y streams listos
                //          este conectado ya puede enviar y hacer waits() de contestaciones
                //

                this.OnClienteConectado(client);
                myData.MyStream.BeginRead(myData.MyBuffer, 0, myData.MyBuffer.Length, new AsyncCallback(OnDataReceived), myData);
                this.OnClienteConectadoAndReady();
            }
            catch (Exception ex)
            {
                if (myData != null && myData.MyStream != null)
                {
                    myData.MyStream.Dispose();
                    myData.MyStream = null;
                }
                this._connectionCallback(this, new ConnectionResults(ex));
            }
        }
        protected StreamWriter NewStreamWriter(Stream stream, Encoding encoding)
        {
            StreamWriter sw = new StreamWriter(stream, encoding); // Si no se especifica es UTF-8 sin BOM (byte order mask) <--> StreamWriter(stream)
            sw.AutoFlush = true;
            return sw;
        }
        protected virtual void ChangeStreamWriterEncoding(TcpClient client, Encoding encoding)
        {
            throw new Exception("Not implemented exception");
        }
        protected abstract void SetStreamWriter(TcpClient client, Stream stream);
        protected abstract StreamWriter GetStreamWriter(TcpClient client);
        protected abstract BinaryWriter GetBinaryWriter(TcpClient client);
        protected virtual void OnDataReceived(IAsyncResult result)
        {
            SimpleData myData = result.AsyncState as SimpleData;
            try
            {
                bool interpretaBuffer = false;

                int numBytesLeidos = myData.MyStream.EndRead(result);
                byte[] bufferCompleto = null;
                if (numBytesLeidos > 0)
                {
                    if (this._useCompression)
                    {
                        // Concatenamos lo que hemos recibido al buffer temporal
                        if (myData.TemporalStream == null)
                            myData.TemporalStream = new MemoryStream();
                        myData.TemporalStream.Write(myData.MyBuffer, 0, numBytesLeidos);

                        if (numBytesLeidos <= SimpleData.KTAM_BUFFER)
                        {
                            // Si hemos llegado aquí se trata del último paquete de envío, podemos descomprimir
                            bufferCompleto = myData.TemporalStream.ToArray();
                            // Todo, comprobar el último caracter de bufferCompleto si hacemos _sw.WriteLine()

                            int antesDescomprimir = bufferCompleto.Length;
                            // Esto lo hacemos por si nos vienen caracteres sueltos que no eran necesarios para la descompresión
                            if (bufferCompleto.Length < 50)
                                myData.TemporalStream = null;
                            else
                            {

                                // Intentamos descomprimir lo que llevamos leido, si lo conseguimos es que 
                                bufferCompleto = UtilsGeneric.UnGzipBuffer(bufferCompleto);

                                if (bufferCompleto != null)
                                {
                                    myData.NumBytesLeidosTotal = bufferCompleto.Length;
                                    decimal compresion = antesDescomprimir / (decimal)myData.NumBytesLeidosTotal * 100m;
                                    Console.WriteLine(string.Format("Rec: {0}, Desc: {1}, Compresion: {2}%", antesDescomprimir, myData.NumBytesLeidosTotal, (int)compresion));
                                    interpretaBuffer = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        // Si no usamos compresión lo tomamos como un paquete completo 
                        bufferCompleto = UtilsGeneric.Chop(myData.MyBuffer, numBytesLeidos);
                        interpretaBuffer = true;
                    }
                    if (interpretaBuffer)
                    {
                        DatosRecibidos(myData.Client, bufferCompleto);
                        myData.TemporalStream = null;
                    }
                    myData.MyStream.BeginRead(myData.MyBuffer, 0, myData.MyBuffer.Length, new AsyncCallback(OnDataReceived), myData);
                }
                else
                    throw new Exception("Desconectado");

            }
            catch (Exception ex)
            {
                if (myData != null)
                {
                    DesconectaEx(myData.Client, ex);
                    myData.Finaliza();
                }
                else
                    this._connectionCallback(this, new ConnectionResults(ex));
            }
        }
        protected void DesconectaEx(TcpClient client, Exception ex)
        {
            this._connectionCallback(this, new ConnectionResults(ex, client));
            Desconecta(client);
        }
        protected virtual void Desconecta(TcpClient cliente)
        {
            if (cliente != null)
                cliente.Close();
        }
        public abstract void EnviaDatos(string datos);
        public abstract void EnviaDatos(byte[] buffer, int len);
        public abstract void EnviaDatos(TcpClient client, byte[] buffer, int len);
        public void EnviaDatos(TcpClient client, string datos)
        {
            try
            {
                if (_useCompression)
                {
                    BinaryWriter bw = GetBinaryWriter(client);
                    if (bw != null && bw.BaseStream.CanWrite)
                    {
                        byte[] datosB = UtilsGeneric.GzipString(datos);
                        bw.Write(datosB);
                    }
                }
                else
                {
                    StreamWriter sw = GetStreamWriter(client);
                    if (sw != null && sw.BaseStream.CanWrite)
                        sw.Write(datos);
                }
            }
            catch (Exception ex)
            {
                this._connectionCallback(this, new ConnectionResults(ex, client));
            }
        }
        public static string GetRemoteIP(EndPoint point)
        {
            string ip = null;
            if (point != null)
            {
                try
                {
                    ip = ((IPEndPoint)point).Address.ToString();
                }
                catch { }
            }
            return ip;
        }
        public static string GetRemotePort(EndPoint point)
        {
            string port = null;
            if (point != null)
            {
                try
                {
                    port = ((IPEndPoint)point).Port.ToString();
                }
                catch { }
            }
            return port;
        }
        public static string GetRemoteIP(TcpClient client)
        {
            string ip = null;
            if (client != null && client.Client != null)
            {
                try
                {
                    ip = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
                }
                catch { }
            }
            return ip;
        }
        public static string GetRemotePort(TcpClient client)
        {
            string port = null;
            if (client != null && client.Client != null)
            {
                try
                {
                    port = ((IPEndPoint)client.Client.RemoteEndPoint).Port.ToString();
                }
                catch { }
            }
            return port;
        }
        public static string getString(TcpClient tcp)
        {
            return TCPNetBase.GetRemoteIP(tcp) + ":" + TCPNetBase.GetRemotePort(tcp);
        }
        public static string getString(EndPoint point)
        {
            return TCPNetBase.GetRemoteIP(point) + ":" + TCPNetBase.GetRemotePort(point);
        }
        public static bool SetKeepAlive(Socket socket, ulong time, ulong interval)
        {
            try
            {
                // Array to hold input values.
                var input = new[]
                {
                    (time == 0 || interval == 0) ? 0UL : 1UL, // on or off
				    time,
                    interval
                };

                // Pack input into byte struct.
                byte[] inValue = new byte[3 * BytesPerLong];
                for (int i = 0; i < input.Length; i++)
                {
                    inValue[i * BytesPerLong + 3] = (byte)(input[i] >> ((BytesPerLong - 1) * BitsPerByte) & 0xff);
                    inValue[i * BytesPerLong + 2] = (byte)(input[i] >> ((BytesPerLong - 2) * BitsPerByte) & 0xff);
                    inValue[i * BytesPerLong + 1] = (byte)(input[i] >> ((BytesPerLong - 3) * BitsPerByte) & 0xff);
                    inValue[i * BytesPerLong + 0] = (byte)(input[i] >> ((BytesPerLong - 4) * BitsPerByte) & 0xff);
                }

                // Create bytestruct for result (bytes pending on server socket).
                byte[] outValue = BitConverter.GetBytes(0);

                // Write SIO_VALS to Socket IOControl.
                socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
                socket.IOControl(IOControlCode.KeepAliveValues, inValue, outValue);
            }
            catch (SocketException e)
            {
                Console.WriteLine("Failed to set keep-alive: {0} {1}", e.ErrorCode, e);
                return false;
            }

            return true;
        }
    }
}
