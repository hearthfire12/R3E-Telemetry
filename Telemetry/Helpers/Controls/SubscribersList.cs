namespace Helpers.Controls
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;
    using Helpers.Models;

    [Serializable]
    public class SubscribersList : ListBox
    {
        public SubscribersList()
        {
            Source = new BindingList<TcpClientWrapper>();
        }

        private BindingList<TcpClientWrapper> _source;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] 
        public BindingList<TcpClientWrapper> Source
        {
            get => _source;
            set
            {
                if (value != null)
                {
                    _source = value;
                    Source.ListChanged += SourceOnListChanged;
                }
            }
        }

        private void SourceOnListChanged(object sender, ListChangedEventArgs e)
        {
            this.Invoke((MethodInvoker) (() =>
            {
                this.Items.Clear();
                foreach (var subscriber in Source)
                {
                    this.Items.Add(subscriber);
                }
            }));
        }
    }
}
