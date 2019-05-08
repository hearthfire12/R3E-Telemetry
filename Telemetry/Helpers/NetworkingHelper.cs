namespace Helpers
{
    using System.Linq;
    using System.Net.NetworkInformation;
    using System.Net.Sockets;

    public static class NetworkingHelper
    {
        public static TcpState GetState(this TcpClient tcpClient)
        {
            var foo = IPGlobalProperties.GetIPGlobalProperties()
                .GetActiveTcpConnections()
                .Where(x => x.LocalEndPoint.Equals(tcpClient.Client.LocalEndPoint))
                .ToList();

            switch (foo.Count)
            {
                    case 1:
                        return foo.First().State;
                    case 0:
                        return TcpState.Closed;
                    default:
                        return TcpState.Unknown;
            }
        }
    }
}