namespace Infrastructure.Collector
{
    using System;
    using Models;

    public interface ICollector : IDisposable
    {
        bool TryCollect(out RequiredModel collected);
    }
}