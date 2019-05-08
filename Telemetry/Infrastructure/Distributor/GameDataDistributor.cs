namespace Infrastructure.Distributor
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;

    public class GameDataDistributor : IGameDataDistributor
    {
        public GameDataDistributor(int port)
        {
            _listener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
            Subscribers = new List<TcpClient>();
        }


        private readonly TcpListener _listener;


        public IList<TcpClient> Subscribers { get; }


        public void ListenForSubscribers()
        {
            Console.WriteLine("ListenForSubscribers");
            try
            {
                _listener.Start();
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex);
                return;
            }

            while (true)
            {
                var client = _listener.AcceptTcpClient();
                Console.WriteLine("AcceptTcpClient");
                Subscribers.Add(client);
            }
        }

        public void Publish<T>(T data)
        {
            Console.WriteLine("Publish");

            for (int i = 0; i < Subscribers.Count; i++)
            {
                try
                {
                    var s = Subscribers[i].GetStream();
                    var writer = new StreamWriter(s);
                    writer.Write(data);
                }
                catch (IOException ex)
                {
                    Console.WriteLine(ex);
                    Console.WriteLine("Subscribers Remove");
                    Subscribers.RemoveAt(i);
                    i--;
                }
            }
        }

        public void Dispose()
        {
            _listener?.Stop();
        }
    }
}