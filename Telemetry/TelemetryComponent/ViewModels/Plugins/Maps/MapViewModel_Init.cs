namespace TelemetryComponent.ViewModels.Plugins.Maps
{
    using System.ComponentModel;
    using System.IO.Abstractions;
    using System.Runtime.CompilerServices;
    using Base;
    using Infrastructure.Analyzer;
    using Infrastructure.Logging;
    using Infrastructure.Management;
    using Infrastructure.Models;
    using OxyPlot;

    public partial class MapViewModel : PluginBase
    {
        public MapViewModel(EventAnalyzer analyzer,  IFileSystem fileSystem) : base(analyzer)
        {
            MapManager = new MapManager(fileSystem);
            MapPlot = new PlotModel
            {
                PlotAreaBorderThickness = new OxyThickness(0),
            };
        }


        protected readonly MapManager MapManager;


        public override void OnLapStarted(object sender, RequiredModel lap)
        {
            base.OnLapStarted(sender, lap);

            if (LoadedMap == null)
            {
                MapManager.IsRecording = true;
                Logger.Info("Start recording map.");
            }
        }

        public override void OnLapFinished(object sender, RequiredModel model)
        {
            base.OnLapFinished(sender, model);

            if (MapManager.IsRecording)
            {
                MapManager.Save(model.TrackName, model.LayoutName);
                Logger.Info("Map was created and saved.");
                LoadedMap = MapManager.LoadMap(model.TrackName, model.LayoutName);
            }
        }
        
        public override void OnSessionStarted(object sender, RequiredModel model)
        {
            base.OnSessionStarted(sender, model);
            LoadedMap = MapManager.LoadMap(model.TrackName, model.LayoutName);
        }

        public override void OnUpdated(object sender, RequiredModel model)
        {
            base.OnUpdated(sender, model);

            DrawCurrentPosition(model);
            if (MapManager.IsRecording)
            {
                MapManager.Record(model.CarPosition);
            }
        }

        public override void OnPitlaneStarted(object sender, RequiredModel model)
        {
            base.OnPitlaneStarted(sender, model);
            MapManager.Clear();
        }

        public override void OnPitlaneFinished(object sender, RequiredModel model)
        {
            base.OnPitlaneFinished(sender, model);
            MapManager.Clear();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
