namespace Infrastructure.Collector
{
    using System;
    using System.Net.Sockets;
    using Logging;
    using Models;
    using Networking;

    public class NetworkCollector : ICollector
    {
        public NetworkCollector(string publisherIp, int publisherPort)
        {
            PublisherIp = publisherIp;
            PublisherPort = publisherPort;

            Subscriber = new Subscriber(publisherIp, publisherPort);
        }

        protected readonly string PublisherIp;
        protected readonly int PublisherPort;


        protected readonly Subscriber Subscriber;


        public bool TryCollect(out RequiredModel collected)
        {
            collected = null;

            try
            {
                if (!Subscriber.IsSubscribed)
                {
                    Subscriber.Subscribe();
                }

                collected = Subscriber.Receive<RequiredModel>();
            }
            catch(Exception ex)
            {
                if (!(ex is SocketException))
                {
                    Logger.Fatal("Fatal error while collecting data from network.", ex);
                }
                else
                {
                    Logger.Warn($"Connecting to {PublisherIp}:{PublisherPort}...");
                }
                
                return false;
            }

            return true;
        }

        public void Dispose()
        {
            Subscriber?.Dispose();
        }
    }
}
