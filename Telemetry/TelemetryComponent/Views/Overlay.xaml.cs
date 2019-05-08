//namespace TelemetryComponent.Views
//{
//    using System.Collections.Generic;
//    using System.Threading;
//    using System.Threading.Tasks;
//    using System.Windows;
//    using System.Windows.Media;
//    using System.Windows.Media.Animation;
//    using Infrastructure.Models;
//    using Infrastructure.Models.Tires;

//    /// <summary>
//    /// Interaction logic for Layout.xaml
//    /// </summary>
//    public partial class Overlay : Window
//    {
//        public Overlay()
//        {
//            InitializeComponent();

//            var blink = FindResource("Blink") as Storyboard;
            
//            _timeDeltaBlink =  blink.Clone();

//            _infoBlink = blink.Clone();
//            _infoBlink.Completed += (sender, args) => _infoAnimationCompleted = true;

//            _warningBlink = blink.Clone();
//            _warningBlink.Completed += (sender, args) => _warningAnimationCompleted = true;

//            _canselationSource = new CancellationTokenSource();
//            Task.Factory.StartNew(InfosTracker, _canselationSource.Token);
//            Task.Factory.StartNew(WarningsTracker, _canselationSource.Token);
//        }

//        private void InfosTracker()
//        {
//            while (!_canselationSource.IsCancellationRequested)
//            {
//                while (!_infoAnimationCompleted) ;

//                if (_infos.Count > 0)
//                {
//                    string info = _infos.Dequeue();
//                    _infoAnimationCompleted = false;

//                    Dispatcher.Invoke(() =>
//                    {
//                        Info.Text = info;
//                        _infoBlink.Begin(Info);
//                    });
//                }
//            }
//        }

//        private void WarningsTracker()
//        {
//            while (!_canselationSource.IsCancellationRequested)
//            {
//                while (!_warningAnimationCompleted) ;

//                if (_warnings.Count > 0)
//                {
//                    string warning = _warnings.Dequeue();
//                    _warningAnimationCompleted = false;

//                    Dispatcher.Invoke(() =>
//                    {
//                        Warning.Text = warning;
//                        _warningBlink.Begin(Warning);
//                    });
//                }
//            }
//        }

//        private bool _infoAnimationCompleted = true;
//        private bool _warningAnimationCompleted = true;

//        private readonly Storyboard _infoBlink;
//        private readonly Storyboard _warningBlink;
//        private readonly Storyboard _timeDeltaBlink;
//        private readonly CancellationTokenSource _canselationSource;
//        private readonly Queue<string> _infos = new Queue<string>();
//        private readonly Queue<string> _warnings = new Queue<string>();


//        public void SendInfo(string warning)
//        {
//            _infos.Enqueue(warning);
//        }

//        public void SendWarning(string warning)
//        {
//            //_warnings.Enqueue(warning);
//            Dispatcher.Invoke(() =>
//            {
//                Warning.Text = warning;
//                _warningBlink.Begin(Warning);
//            });
//        }

//        public void SendTimeDelta(double value)
//        {
//            Dispatcher.Invoke(() =>
//            {
//                TimeDelta.Text = value.ToString("F");
//                TimeDelta.Foreground = value > 0 ? Brushes.Green : Brushes.Red;
//                _timeDeltaBlink.Begin(TimeDelta);
//            });
//        }

//        public void SendTires(Tires tires)
//        {
//            Dispatcher.Invoke(() =>
//            {
//                var tire = tires.FrontLeft;
//                FL.Text = $"FL\t{tire.Grip:F}";
//                FL.Foreground = tire.Grip < 0.1D ? Brushes.Red : Brushes.White;

//                tire = tires.FrontRight;
//                FR.Text = $"FR\t{tire.Grip:F}";
//                FR.Foreground = tire.Grip < 0.1D ? Brushes.Red : Brushes.White;

//                tire = tires.RearLeft;
//                RL.Text = $"RL\t{tire.Grip:F}";
//                RL.Foreground = tire.Grip < 0.1D ? Brushes.Red : Brushes.White;

//                tire = tires.RearRight;
//                RR.Text = $"RR\t{tire.Grip:F}";
//                RR.Foreground = tire.Grip < 0.1D ? Brushes.Red : Brushes.White;
//            });
//        }

//        ~Overlay()
//        {
//            _canselationSource.Cancel();
//        }
//    }
//}
