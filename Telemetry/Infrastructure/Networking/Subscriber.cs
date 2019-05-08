namespace Infrastructure.Networking
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using Helpers;

    public class Subscriber : IDisposable
    {
        public Subscriber(string publisherIp, int publisherPort)
        {
            _client = new TcpClient();
            _publisher = new IPEndPoint(IPAddress.Parse(publisherIp), publisherPort);
        }


        private readonly TcpClient _client;
        private readonly IPEndPoint _publisher;
        private NetworkStream _networkStream;


        public bool IsSubscribed { get; protected set; }


        public void Subscribe()
        {
            _client.Connect(_publisher);
            _networkStream = _client.GetStream();
            IsSubscribed = true;
        }

        public T Receive<T>()
        {
            var reader = new StreamReader(_networkStream);
            
            var buffer = new char[10024];
            reader.ReadBlock(buffer, 0, buffer.Length);

            // todo: fix data corruption

            string result = new string(buffer).Trim('\0');
            return result.Deserialize<T>();
        }

        public void Dispose()
        {
            _client?.Dispose();
            _networkStream?.Dispose();
        }
    }
}