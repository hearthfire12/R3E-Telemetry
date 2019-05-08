namespace TelemetryComponent.ViewModels.Plugins
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using Base;
    using Infrastructure.Analyzer;
    using Infrastructure.Models;

    public class DashBoardViewModel : PluginBase, INotifyPropertyChanged
    {
        public DashBoardViewModel(EventAnalyzer analyzer) : base(analyzer)
        {
            Title = "Waiting for session";
        }


        private RequiredModel _currentDelta;
        private string _title;


        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public RequiredModel CurrentDelta
        {
            get => _currentDelta;
            set
            {
                _currentDelta = value;
                OnPropertyChanged();
            }
        }


        public override void OnUpdated(object sender, RequiredModel model)
        {
            base.OnUpdated(sender, model);

            CurrentDelta = model;
        }

        public override void OnSessionStarted(object sender, RequiredModel model)
        {
            base.OnSessionStarted(sender, model);

            Title = $"{model.TrackName} {model.LayoutName}";
        }

        public override void OnSessionFinished(object sender, RequiredModel model)
        {
            base.OnSessionFinished(sender, model);

            Title = "Waiting for session";
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}