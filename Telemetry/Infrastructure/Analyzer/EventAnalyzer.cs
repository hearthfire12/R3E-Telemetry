namespace Infrastructure.Analyzer
{
    using System;
    using Models;

    public class EventAnalyzer : IAnalyzer
    {
        private RequiredModel _previousModel;

        public event EventHandler<RequiredModel> Updated;
        public event EventHandler<RequiredModel> LapInvalidated;
        public event EventHandler<RequiredModel> LapStarted;
        public event EventHandler<RequiredModel> LapFinished;
        public event EventHandler<RequiredModel> SectorChanged;
        public event EventHandler<RequiredModel> PitlaneStarted;
        public event EventHandler<RequiredModel> PitlaneFinished;
        public event EventHandler<RequiredModel> SessionStarted;
        public event EventHandler<RequiredModel> SessionFinished;

        public void Analyze(RequiredModel model)
        {
            if (model != null)
            {
                EnsureSessionFinished(model);
                EnsureSessionStarted(model);

                EnsureLapFinished(model);
                EnsureLapStarted(model);
                EnsureLapInvalidated(model);

                EnsureSectorChanged(model);
                EnsurePitlaneStarted(model);
                EnsurePitlaneFinished(model);

                _previousModel = model;
            }

            Updated?.Invoke(this, model);
        }

        private void EnsureSessionStarted(RequiredModel model)
        {
            if (model.Type != null
                && _previousModel?.Type == null)
            {
                SessionStarted?.Invoke(this, model);
            }
        }

        private void EnsureSessionFinished(RequiredModel model)
        {
            if (model.Type == null
                && _previousModel?.Type != null)
            {
                SessionFinished?.Invoke(this, model);
            }
        }

        private void EnsureLapStarted(RequiredModel model)
        {
            if (!model.InMenus 
                && model.LapNumber > _previousModel?.LapNumber)
            {
                LapStarted?.Invoke(this, model);
            }
        }

        private void EnsureLapFinished(RequiredModel model)
        {
            if (!model.InMenus 
                && model.LapNumber > _previousModel?.LapNumber)
            {
                LapFinished?.Invoke(this, model);
            }
        }

        private void EnsureLapInvalidated(RequiredModel model)
        {
            if (!model.InMenus
                && !model.LapIsValid && _previousModel?.LapIsValid == true)
            {
                LapInvalidated?.Invoke(this, model);
            }
        }

        private void EnsureSectorChanged(RequiredModel model)
        {
            if (!model.InMenus && _previousModel != null
                &&  model.SectorId != _previousModel.SectorId
                && _previousModel.SectorId != -1)
            {
                SectorChanged?.Invoke(this, model);
            }
        }

        private void EnsurePitlaneStarted(RequiredModel model)
        {
            if (!model.InMenus && model.InPitlane && _previousModel?.InPitlane == false)
            {
                PitlaneStarted?.Invoke(this, model);
            }
        }

        private void EnsurePitlaneFinished(RequiredModel model)
        {
            if (!model.InMenus && !model.InPitlane && _previousModel?.InPitlane == true)
            {
                PitlaneFinished?.Invoke(this, model);
            }
        }
    }
}
