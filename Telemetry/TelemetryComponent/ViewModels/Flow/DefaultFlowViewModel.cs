namespace TelemetryComponent.ViewModels.Flow
{
    using System.IO.Abstractions;
    using Base;
    using Infrastructure.Configuration;
    using Plugins;
    using Plugins.Maps;

    public class DefaultFlowViewModel : FlowBase
    {
        public DefaultFlowViewModel()
        {
            StartSearching();
        }


        public MapViewModel Maps { get; set; }
        public DistributorViewModel Distributor { get; set; }
        public DashBoardViewModel DashBoard { get; set; }


        public override void OnCollectorCreated()
        {
            base.OnCollectorCreated();

            Maps = new MapViewModel(EventAnalyzer, new FileSystem());
            DashBoard = new DashBoardViewModel(EventAnalyzer);
            Distributor = new DistributorViewModel(EventAnalyzer, ConfigMgr.DistributorPort);
        }
    }
}
