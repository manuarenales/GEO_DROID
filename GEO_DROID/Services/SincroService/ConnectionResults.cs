using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GEO_DROID.Services
{
    public delegate void SecureConnectionResultsCallback(object sender, ConnectionResults args);

    public class ConnectionResults
    {
        private Stream _stream;
        private Exception _asyncException;
        private TcpClient _client;

        public Exception MyAsyncException { get { return this._asyncException; } }
        public Stream MyStream { get { return this._stream; } }
        public TcpClient MyClient { get { return this._client; } }

        internal ConnectionResults(Stream stream)
        {
            this._stream = stream;
        }
        internal ConnectionResults(Exception exception)
        {
            this._asyncException = exception;
        }
        internal ConnectionResults(Exception exception, TcpClient client)
        {
            this._asyncException = exception;
            this._client = client;
        }
    }
}
