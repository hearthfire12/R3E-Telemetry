namespace TestingProject
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading.Tasks;
    using Infrastructure.Networking;

    public static class RunInCmdMode
    {
        public static void Subscriber(string publisherIp, int publisherPort)
        {
            var subscriber = new Subscriber(IPAddress.Parse(publisherIp).ToString(), publisherPort);

            while (true)
            {
                try
                {
                    if (!subscriber.IsSubscribed)
                    {
                        subscriber.Subscribe();
                    }

                    string data;
                    data = subscriber.Receive<string>();
                    Console.WriteLine(data);
                }
                catch (Exception ex)
                {
                    if (ex is SocketException || ex is IOException)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        public static void Publisher(int port)
        {
            var publisher = new Publisher(port);
            Task.Factory.StartNew(publisher.LookForClients, TaskCreationOptions.AttachedToParent);

            while (true)
            {
                Console.Write(@"Write message to publish:");
                string message = Console.ReadLine();
                publisher.Publish(message);
            }
        }
    }
}