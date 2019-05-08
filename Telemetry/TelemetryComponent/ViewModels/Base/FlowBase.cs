namespace TelemetryComponent.ViewModels.Base
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Infrastructure.Analyzer;
    using Infrastructure.Collector;
    using Infrastructure.Collector.R3E;
    using Infrastructure.Configuration;
    using Infrastructure.Logging;

    public abstract class FlowBase: IDisposable
    {
        protected FlowBase()
        {
            SearchCancellation = new CancellationTokenSource();
            UpdateCancellation = new CancellationTokenSource();
        }

        protected readonly CancellationTokenSource SearchCancellation;
        protected readonly CancellationTokenSource UpdateCancellation;
        protected ICollector Collector;
        protected EventAnalyzer EventAnalyzer;

        protected void Create(ICollector collector)
        {
            if (collector == null)
            {
                return;
            }

            Logger.Info($"Creating collector of type ${collector.GetType().Name}...");
            SearchCancellation.Cancel();

            Collector?.Dispose();
            Collector = collector;
            EventAnalyzer = new EventAnalyzer();
            OnCollectorCreated();

            StartUpdateLoop();
        }

        public virtual void OnCollectorCreated()
        {
        }

        private void SearchForGame()
        {
            Logger.Info("Waiting for supported game...");
            while (!SearchCancellation.IsCancellationRequested)
            {
                if (SupportedGames.IsR3ERunning)
                {
                    Logger.Info("RaceRoom Racing Experience game found");
                    Create(new R3ECollector());
                    break;
                }
            }
        }

        protected void StartSearching()
        {
            Task.Factory.StartNew(SearchForGame, SearchCancellation.Token);
        }

        protected void StartUpdateLoop()
        {
            Task.Factory.StartNew(UpdateLoop, UpdateCancellation.Token);
        }

        private void UpdateLoop()
        {
            Logger.Info("Update loop started...");
            while (!UpdateCancellation.IsCancellationRequested)
            {
                if (Collector.TryCollect(out var collected))
                {
                    EventAnalyzer.Analyze(collected);
                }
            }
        }

        public void Dispose()
        {
            SearchCancellation?.Cancel();
            UpdateCancellation?.Cancel();
            SearchCancellation?.Dispose();
            UpdateCancellation?.Dispose();
            Collector?.Dispose();
        }
    }
}