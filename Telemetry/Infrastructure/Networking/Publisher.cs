namespace Infrastructure.Networking
{
    using System;
    using System.ComponentModel;
    using System.Net;
    using System.Net.NetworkInformation;
    using System.Net.Sockets;
    using System.Threading.Tasks;
    using Helpers;
    using Helpers.Models;

    public class Publisher : IDisposable
    {
        public Publisher(int port)
        {
            _listener = new TcpListener(IPAddress.Any, port);
            Subscribers = new BindingList<TcpClientWrapper>();
        }

        public event OnSubscribed Subscribed;


        public BindingList<TcpClientWrapper> Subscribers { get; protected set; }


        private readonly TcpListener _listener;
        private bool IsLooking { get; set; }


        public async void LookForClients()
        {
            _listener.Start();
            IsLooking = true;

            while (IsLooking)
            {
                var client = await _listener.AcceptTcpClientAsync();

                if (client.GetState() == TcpState.Established)
                {
                    EnsureConnected(new TcpClientWrapper(client));
                }
            }
        }

        public void Publish<T>(T data)
        {
            var serialized = data.Serialize().ToCharArray();
            var buffer = new char[10024];
            Array.Copy(serialized, buffer, serialized.Length);

            foreach (var subscriber in Subscribers)
            {
                subscriber.Write(new string(buffer));
            }
        }

        private void EnsureConnected(TcpClientWrapper client)
        {
            Subscribers.Add(client);
            Subscribed?.Invoke(client);

            Task.Factory.StartNew(() =>
            {
                while (client.State != TcpState.Closed) ;
                Subscribers.Remove(client);
            });
        }

        public void Dispose()
        {
            IsLooking = false;
            Subscribers.Clear();
            _listener.Stop();
        }
    }

    public delegate void OnSubscribed(TcpClientWrapper client);
}
