using System.Windows.Forms;

namespace TestingProject.View
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Infrastructure.Models;
    using Infrastructure.Networking;

    public partial class Subscribers : Form
    {
        public Subscribers()
        {
            InitializeComponent();

            _publisher = new Publisher(25565);
            ActiveSubscribers.Source = _publisher.Subscribers;
            _publisher.Subscribed += subscriber =>
            {
                Invoke((MethodInvoker)(() => { SubscribersHistory.Source.Add(subscriber); }));
            };
            _cancellation = new CancellationTokenSource();

            BtnStart_Click(this, EventArgs.Empty);
        }

        private readonly CancellationTokenSource _cancellation;
        private readonly Publisher _publisher;


        private void BtnStart_Click(object sender, EventArgs e)
        {
            BtnStart.Enabled = false;
            BtnStop.Enabled = true;
            Task.Factory.StartNew(_publisher.LookForClients, _cancellation.Token);
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            BtnStart.Enabled = true;
            BtnStop.Enabled = false;
            _cancellation.Cancel();
        }

        private void BtnPublish_Click(object sender, EventArgs e)
        {
            _publisher.Publish(TBMessage.Text);
        }
    }
}
