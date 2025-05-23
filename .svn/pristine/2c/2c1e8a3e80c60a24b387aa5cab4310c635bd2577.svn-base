using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GEO_DROID.Services.SincroService
{
    public class SimpleData
    {
        public const int KTAM_BUFFER = 512;
        private TcpClient _client;
        private Stream _myStream;
        private byte[] _myBuffer;
        public MemoryStream TemporalStream = null;
        public int NumBytesLeidosTotal = 0;

        public TcpClient Client { get { return _client; } set { _client = value; } }
        public Stream MyStream { get { return _myStream; } set { _myStream = value; } }
        public byte[] MyBuffer { get { return _myBuffer; } set { _myBuffer = value; } }

        public SimpleData(TcpClient client, Stream stream)
        {
            this._client = client;
            this._myStream = stream;
            this._myBuffer = new byte[KTAM_BUFFER];
        }

        public void Finaliza()
        {
            if (_myStream != null)
                _myStream.Close();
            _myStream = null;

            _myBuffer = null;

            if (_client != null && _client.Client != null)
            {
                _client.Client.Close();
                _client.Close();
            }
            _client = null;

            NumBytesLeidosTotal = 0;
            if (TemporalStream != null)
                TemporalStream.Close();
            TemporalStream = null;
        }
    }
}
