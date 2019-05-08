namespace TelemetryComponent.ViewModels.Base
{
    using Infrastructure.Analyzer;
    using Infrastructure.Logging;
    using Infrastructure.Models;

    public abstract class PluginBase
    {
        protected PluginBase(EventAnalyzer analyzer)
        {
            EventAnalyzer = analyzer;

            EventAnalyzer.Updated += OnUpdated;
            EventAnalyzer.SessionStarted += OnSessionStarted;
            EventAnalyzer.SessionFinished += OnSessionFinished;
            EventAnalyzer.LapStarted += OnLapStarted;
            EventAnalyzer.LapFinished += OnLapFinished;
            EventAnalyzer.LapInvalidated += OnLapInvalidated;
            EventAnalyzer.SectorChanged += OnSectorChanged;
            EventAnalyzer.PitlaneStarted += OnPitlaneStarted;
            EventAnalyzer.PitlaneFinished += OnPitlaneFinished;
        }


        protected EventAnalyzer EventAnalyzer;


        public virtual void OnSessionStarted(object sender, RequiredModel model)
        {
            Logger.Info("Session started.");
        }

        public virtual void OnSessionFinished(object sender, RequiredModel model)
        {

            Logger.Info("Session finished.");
        }

        public virtual void OnLapStarted(object sender, RequiredModel model)
        {
            Logger.Info("Lap started.");
        }

        public virtual void OnLapFinished(object sender, RequiredModel model)
        {
            Logger.Info("Lap finished.");
        }

        public virtual void OnLapInvalidated(object sender, RequiredModel model)
        {
            Logger.Info("Lap invalidated.");
        }

        public virtual void OnSectorChanged(object sender, RequiredModel model)
        {
            Logger.Info("Sector changed.");
        }

        public virtual void OnPitlaneStarted(object sender, RequiredModel model)
        {
            Logger.Info("Pitlane started.");
        }

        public virtual void OnPitlaneFinished(object sender, RequiredModel model)
        {
            Logger.Info("Pitlane finished.");
        }

        public virtual void OnUpdated(object sender, RequiredModel model)
        {
        }
    }
}
