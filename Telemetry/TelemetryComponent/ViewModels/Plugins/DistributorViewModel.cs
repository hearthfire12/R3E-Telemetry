namespace TelemetryComponent.ViewModels.Plugins
{
    using System;
    using System.Threading.Tasks;
    using Base;
    using Infrastructure.Analyzer;
    using Infrastructure.Models;
    using Infrastructure.Networking;

    public class DistributorViewModel : PluginBase, IDisposable
    {
        public DistributorViewModel(EventAnalyzer analyzer, int port) : base(analyzer)
        {
            Publisher = new Publisher(port);
            Task.Factory.StartNew(Publisher.LookForClients, TaskCreationOptions.AttachedToParent);
        }


        public Publisher Publisher { get; protected set; }

        public override void OnUpdated(object sender, RequiredModel model)
        {
            Publisher?.Publish(model);
        }

        public void Dispose()
        {
            Publisher?.Dispose();
        }
    }
}