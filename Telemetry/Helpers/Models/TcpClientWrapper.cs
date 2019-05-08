namespace Helpers.Models
{
    using System.IO;
    using System.Net.NetworkInformation;
    using System.Net.Sockets;
    using Helpers;

    public class TcpClientWrapper
    {
        public TcpClientWrapper(TcpClient client)
        {
            _client = client;
        }

        private readonly TcpClient _client;
        private StreamWriter _writer;

        public TcpState State => _client.GetState();


        public void Write(string message)
        {
            _writer = _writer ?? new StreamWriter(_client.GetStream());
            _writer.Write(message, 0, message.Length);
        }

        public override string ToString()
        {
            return _client.Client.LocalEndPoint.ToString();
        }
    }
}
