namespace Infrastructure.Distributor
{
    using System;
    using System.Collections.Generic;
    using System.Net.Sockets;

    public interface IGameDataDistributor : IDisposable
    {
        IList<TcpClient> Subscribers { get; }
        void ListenForSubscribers();
        void Publish<T>(T data);
    }
}